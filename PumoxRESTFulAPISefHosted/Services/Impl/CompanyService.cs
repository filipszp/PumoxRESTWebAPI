using NHibernate.Linq;
using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;


namespace RESTFulAPIConsole.Services
{
    public class CompanyService : AbstractService<Company>, ICompanyService<Company>
    {
        public Int64 createCompany(Company entityToAdd)
        {
            Int64 companyId = 0;
            if (entityToAdd.validateObligatoryField())
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    try
                    {
                        using (var transaction = session.BeginTransaction())
                        {
                            companyId = (Int64)session.Save(entityToAdd);
                            entityToAdd.Employees.ToList().ForEach(emp =>
                            {
                                emp.Company_Id = companyId;
                                session.Save(emp);
                            });
                            transaction.Commit();
                        }
                    }
                    catch
                    {
                        return 0;
                    }
                }
            }
            return companyId;
        }

        public bool updateCompany(Company entityToPersist, Int64 id)
        {
            if (entityToPersist.validateObligatoryField())
            {
                using (var session = NHibernateHelper.OpenSession())
                {
                    // try
                    //  {
                    using (var transaction = session.BeginTransaction())
                    {
                        var findCompany = session.Query<Company>()
                        .Fetch(t => t.Employees)
                        .Where(t => t.Id == id)
                        .FirstOrDefault<Company>();

                        if (findCompany != null)
                        {
                            findCompany.CompanyName = entityToPersist.CompanyName;
                            findCompany.EstablishmentYear = entityToPersist.EstablishmentYear;
                            
                            //usuwanie pracownikow z encji Company i zalozenie nowych z Requesta
                            if (entityToPersist.Employees.Count > 0)
                            {
                                findCompany.Employees.ToList().ForEach(empToDel =>
                                {
                                    session.Delete(empToDel);
                                });
                                entityToPersist.Employees.ToList().ForEach(emp =>
                                {
                                    emp.Company_Id = findCompany.Id;
                                    session.Save(emp);
                                });
                                //findCompany.Employees.Clear();
                                //findCompany.Employees.ToList().AddRange(entityToPersist.Employees);
                            }
                            session.SaveOrUpdate(findCompany);
                            transaction.Commit();
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    // }
                    //  catch
                    //  {
                    //      return false;
                    // }
                }
            }
            else
                return false;
        }

        public void deleteCompany(Company entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IList<Company> getAllCompany()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var list = session.Query<Company>()
                    .Fetch(t => t.Employees)
                    .OrderByDescending(t => t.Id)
                    .ToList<Company>();
                return list;
            }
        }

        public IList<Company> seachCompanies(CompanySearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }



        //public User createUser(User user)
        //{
        //    return base.saveNewEntity(user);
        //}

        //public int deleteUser(User user)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public User login(User user)
        //{
        //    var userFromDb = new User();
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        userFromDb = session.Query<User>()
        //            .Where(u => u.Login == user.Login)
        //            .FirstOrDefault<User>();

        //        if (userFromDb == null)
        //            return userFromDb;
        //        else
        //        {
        //            if (PasswordCrypto.Decrypt(userFromDb.Password).Equals(user.Password))
        //                userFromDb.isLogged = true;
        //        }
        //    }
        //    return userFromDb;
        //}

        //public User lastLoginTimeUpdate(User user)
        //{
        //    var userFromDb = new User();
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        userFromDb = session.Get<User>(user.Id);
        //        userFromDb.LastLoginDate = DateTime.Now;
        //        return base.saveEntity(userFromDb);
        //    }



        //}

    }
}
