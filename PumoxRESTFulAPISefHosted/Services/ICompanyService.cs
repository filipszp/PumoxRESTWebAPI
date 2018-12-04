using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using System;
using System.Collections;
using System.Collections.Generic;


namespace RESTFulAPIConsole.Services
{
    public interface ICompanyService
    {

        int createCompany(Company entityToAdd);

        IList seachCompanies(CompanySearchCriteria searchCriteria);

        void updateCompany(Company entityToPersist);

        void deleteCompany(Company entityToDelete);

    }
}
