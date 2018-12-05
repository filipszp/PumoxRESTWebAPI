using PumoxRESTFulAPI.Filters.Filters;
using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using RESTFulAPIConsole.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFulAPIConsole.Controller
{
    /// <summary>
    /// RESTFul WebAPI Controller
    /// </summary>
    [BasicAuthenticationFilter]
    public class CompanyController : ApiController
    {
        private CompanyService companyService = new CompanyService();

        /// <summary>Tworzenie encji Company i listy Employees</summary>
        /// <param name="company">Obiekt klasy Company</param>
        [BasicAuthenticationFilter(true)]
        [HttpPost]
        public HttpResponseMessage Create([FromBody]Company company)
        {
            Console.WriteLine("[HttpPost] -> /company/create");
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.CreateCompany(company);
            if (serviceOperationResult.Id != 0)
                response = Request.CreateResponse<long>(HttpStatusCode.Created, serviceOperationResult.Id);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest,serviceOperationResult.Message);
            return response;
        }

        /// <summary>Aktualizacja encji Company i Employee</summary>
        /// <param name="Id">Id Company do edycji</param>
        /// <param name="company">Obiekt klasy Company</param>
        [BasicAuthenticationFilter(true)]
        [HttpPut]
        [Route("company/update/{id}", Name ="Id")]
        public HttpResponseMessage Update(Int64 Id, [FromBody]Company company)
        {
            Console.WriteLine("[HttpPut] -> /company/update/"+Id);
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.UpdateCompany(company, Id);
            if (serviceOperationResult.Result)
                response = Request.CreateResponse(HttpStatusCode.OK);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceOperationResult.Message);
            return response;

        }

        /// <summary>Usuwanie encji Company</summary>
        /// <param name="Id">Id Company do usunięcia wraz z podpiętymi Employees</param>
        [BasicAuthenticationFilter(true)]
        [HttpDelete]
        [Route("company/delete/{id}", Name = "Id")]
        public HttpResponseMessage Delete(Int64 Id)
        {
            Console.WriteLine("[HttpDelete] -> /company/delete/" + Id);
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.DeleteCompanyWithEmployees(Id);
            if (serviceOperationResult.Result)
                response = Request.CreateResponse(HttpStatusCode.OK);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceOperationResult.Message);
            return response;

        }

        /// <summary>Wyszukiwanie encji Company</summary>
        /// <param name="criteria">Search kryteria wyszukiwnia</param>
        [BasicAuthenticationFilter(false)]
        [HttpPost]
        [Route("company/search")]
        public HttpResponseMessage Search([FromBody]CompanySearchCriteria criteria)
        {
            Console.WriteLine("[HttpDelete] -> /company/search");
            Console.WriteLine(criteria.ToString());
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.SearchCompanies(criteria);
            if (serviceOperationResult.Result)
                response = Request.CreateResponse<List<CompanyWrapper>>(HttpStatusCode.OK, serviceOperationResult.CompanyWrappers);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceOperationResult.Message);
            return response;

        }

        /// <summary>Pobieranie wszystkich Company</summary>
        /// <returns>HttpResponseMessage</returns>
        [BasicAuthenticationFilter(true)]
        [HttpGet]
        [Route("company/getcompanies")]
        public HttpResponseMessage GetCompanies()
        {
            Console.WriteLine("[HttpGet] -> /company/GetCompanies");
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.GetAllCompanies();
            if (serviceOperationResult.Result)
                response = Request.CreateResponse<List<CompanyWrapper>>(HttpStatusCode.OK, serviceOperationResult.CompanyWrappers);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceOperationResult.Message);
            return response;
        }

    }
}