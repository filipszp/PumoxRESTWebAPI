using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RESTFulAPIConsole.Services
{
    public interface ICompanyService<Company>
    {
        /// <summary>
        /// Pobiera wszystkie Company
        /// </summary>
        /// <returns>IList<Company></returns>
        IList<Company> getAllCompany();
        Int64 createCompany(Company entityToAdd);

        IList<Company> seachCompanies(CompanySearchCriteria searchCriteria);

        void updateCompany(Company entityToPersist);

        void deleteCompany(Company entityToDelete);

    }
}
