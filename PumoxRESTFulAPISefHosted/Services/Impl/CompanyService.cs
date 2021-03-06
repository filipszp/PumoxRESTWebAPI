﻿using NHibernate;
using NHibernate.Criterion;
using NHibernate.Linq;
using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using System;
using System.Linq;

namespace RESTFulAPIConsole.Services
{
    /// <summary>Klasa servisowa obsługująca logikę biznesową operacji z kontrolera</summary>
    public class CompanyService : ICompanyService<Company>
    {
        /// <summary>Tworzenie encji Company</summary>
        /// <param name="entityToAdd">Company</param>
        public ServiceOperationResult CreateCompany(Company entityToAdd)
        {
            var serviceOperationResult = new ServiceOperationResult();
            if (entityToAdd.validateObligatoryField())
            {
                try
                {
                    using (var session = NHibernateHelper.OpenSession())
                    {

                        using (var transaction = session.BeginTransaction())
                        {
                            serviceOperationResult.Id = (Int64)session.Save(entityToAdd);
                            entityToAdd.Employees.ToList().ForEach(emp =>
                            {
                                emp.Company_Id = serviceOperationResult.Id;
                                session.Save(emp);
                            });
                            transaction.Commit();
                        }
                    }
                }
                catch
                {
                    serviceOperationResult.Result = false;
                }
            }
            else
                serviceOperationResult.Message = "All fields in Company object are requied";
            return serviceOperationResult;
        }
        /// <summary>Aktualizacja encji Company</summary>
        /// <param name="entityToPersist">Encja Company do aktualizacji</param>
        /// <param name="id">Id encji Company do edycji</param>
        public ServiceOperationResult UpdateCompany(Company entityToPersist, Int64 id)
        {
            var serviceOperationResult = new ServiceOperationResult();
            if (entityToPersist.validateObligatoryField())
            {
                try
                {
                    using (var session = NHibernateHelper.OpenSession())
                    {

                        using (var transaction = session.BeginTransaction())
                        {
                            var findCompany = session.Query<Company>()
                              .Fetch(t => t.Employees)
                              .Where(t => t.Id == id)
                              .ToList<Company>();

                            if (findCompany.ToList().Count == 1)
                            {
                                findCompany[0].CompanyName = entityToPersist.CompanyName;
                                findCompany[0].EstablishmentYear = entityToPersist.EstablishmentYear;
                                //usuwanie pracownikow z encji Company i zalozenie nowych z Requesta
                                if (entityToPersist.Employees.Count > 0)
                                {
                                    deleteEmployeesFromCompany(session, findCompany[0]);
                                    entityToPersist.Employees.ToList().ForEach(emp =>
                                    {
                                        emp.Company_Id = findCompany[0].Id;
                                        session.Save(emp);
                                    });
                                }
                                session.Update(findCompany[0]);
                                transaction.Commit();
                                return serviceOperationResult;
                            }
                            else
                            {
                                serviceOperationResult.Message = "Company entity id[=" + id + "] not found";
                                serviceOperationResult.Result = false;
                            }
                        }
                    }
                }
                catch
                {
                    serviceOperationResult.Result = false;
                }
            }
            else
            {
                serviceOperationResult.Message = "All fields in Company object are requied";
                serviceOperationResult.Result = false;
            }
            return serviceOperationResult;
        }
        /// <summary>Usuwanie encji Company</summary>
        /// <param name="id">Id Company do usunięcia</param>
        public ServiceOperationResult DeleteCompanyWithEmployees(Int64 id)
        {
            var serviceOperationResult = new ServiceOperationResult();
            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {

                    using (var transaction = session.BeginTransaction())
                    {
                        var findCompany = session.Query<Company>()
                          .Fetch(t => t.Employees)
                          .Where(t => t.Id == id)
                          .ToList<Company>();

                        if (findCompany.ToList().Count == 1)
                        {
                            deleteEmployeesFromCompany(session, findCompany[0]);
                            session.Delete(findCompany[0]);
                            transaction.Commit();
                            return serviceOperationResult;
                        }
                        else
                        {
                            serviceOperationResult.Message = "Company entity id=[" + id + "] not found";
                            serviceOperationResult.Result = false;
                        }
                    }
                }
            }
            catch
            {
                serviceOperationResult.Result = false;
            }
            return serviceOperationResult;
        }
        /// <summary>Wyszukiwanie encji Company</summary>
        /// <param name="searchCriteria">Kryteria wyszukiwania encji Company</param>
        public ServiceOperationResult SearchCompanies(CompanySearchCriteria searchCriteria)
        {
            var serviceOperationResult = new ServiceOperationResult();
            try
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    ICriterion crCompanyName, crFirstName, crLastName;
                    ICriterion crKeywordOr = null;
                    ICriterion crDateBetween = null;
                    ICriterion crJobTitle = null;
                    ICriterion crAllOr = null;

                    if (!String.IsNullOrEmpty(searchCriteria.Keyword))
                    {
                        crCompanyName = Restrictions.Like("c.CompanyName", "%" + searchCriteria.Keyword +"%");
                        crFirstName = Restrictions.Like("e.FirstName", "%" + searchCriteria.Keyword + "%");
                        crLastName = Restrictions.Like("e.LastName", "%" + searchCriteria.Keyword + "%");
                        crKeywordOr = Restrictions.Disjunction()
                            .Add(crCompanyName)
                            .Add(crFirstName)
                            .Add(crLastName);
                    }
                    if (!String.IsNullOrEmpty(searchCriteria.EmployeeJobTitles.ToString()))
                        crJobTitle = Restrictions.Eq("e.JobTitle", searchCriteria.EmployeeJobTitles);

                    if (searchCriteria.EmployeeDateOfBirthFrom.HasValue && searchCriteria.EmployeeDateOfBirthTo.HasValue)
                        crDateBetween = Restrictions.Between("e.DateOfBirth", searchCriteria.EmployeeDateOfBirthFrom.Value, searchCriteria.EmployeeDateOfBirthTo.Value);

                    if (crKeywordOr != null && crJobTitle != null && crDateBetween != null)
                    {
                        crAllOr = Restrictions.Disjunction()
                            .Add(crKeywordOr)
                            .Add(crJobTitle)
                            .Add(crDateBetween);
                    }
                    if (crKeywordOr != null && crJobTitle == null && crDateBetween == null)
                    {
                        crAllOr = crKeywordOr;
                    }
                    if (crKeywordOr != null & crJobTitle != null && crDateBetween == null)
                    {
                        crAllOr = Restrictions.Disjunction()
                            .Add(crKeywordOr)
                            .Add(crJobTitle);
                    }
                    if (crKeywordOr != null && crJobTitle == null && crDateBetween != null)
                    {
                        crAllOr = Restrictions.Disjunction()
                           .Add(crKeywordOr)
                           .Add(crDateBetween);
                    }
                    if (crKeywordOr == null && crJobTitle != null && crDateBetween != null)
                    {
                        crAllOr = Restrictions.Disjunction()
                           .Add(crJobTitle)
                           .Add(crDateBetween);
                    }
                    if (crKeywordOr == null && crJobTitle == null && crDateBetween != null)
                    {
                        crAllOr = Restrictions.Disjunction()
                           .Add(crDateBetween);
                    }
                    if (crKeywordOr == null && crJobTitle != null && crDateBetween == null)
                    {
                        crAllOr = Restrictions.Disjunction()
                           .Add(crJobTitle);
                    }
                    var query = session.CreateCriteria<Company>("c")
                        .Fetch(SelectMode.Fetch, "c.Employees");
                    query.CreateCriteria("c.Employees", "e");
                    query.Add(crAllOr);

                    var companySearchList = query.List<Company>().ToList<Company>();
                    companyWrapper(serviceOperationResult, companySearchList);
                }
            }
            catch
            {
                serviceOperationResult.Result = false;
            }
            return serviceOperationResult;
        }
        /// <summary>Pobiera wszystkie Company</summary>
        /// <returns>ServiceOperationResult</returns>
        public ServiceOperationResult GetAllCompanies()
        {
            var serviceOperationResult = new ServiceOperationResult();
            using (var session = NHibernateHelper.OpenSession())
            {
                var companyList = session.Query<Company>()
                    .Fetch(t => t.Employees)
                    .OrderBy(t => t.CompanyName)
                    .ToList<Company>();
                companyWrapper(serviceOperationResult, companyList);
            }
            return serviceOperationResult;
        }

        #region Metody prywatne Utils
        private static void companyWrapper(ServiceOperationResult serviceOperationResult, System.Collections.Generic.List<Company> companyList)
        {
            foreach (var company in companyList)
            {
                var wr = new CompanyWrapper();
                wr.WrappCompany(company);
                serviceOperationResult.CompanyWrappers.Add(wr);
            }
        }

        private void deleteEmployeesFromCompany(ISession session, Company company)
        {
            if (company.Employees.Count > 0)
            {
                session.CreateQuery("delete Employee e where e.Company_Id = " + company.Id)
                    .ExecuteUpdate();
            }
        }
        #endregion

    }
}
