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
        ServiceOperationResult GetAllCompanies();
        ServiceOperationResult CreateCompany(Company entityToAdd);

        ServiceOperationResult SearchCompanies(CompanySearchCriteria searchCriteria);

        ServiceOperationResult UpdateCompany(Company entityToPersist, Int64 id);

        ServiceOperationResult DeleteCompanyWithEmployees(Int64 id);

    }
}
