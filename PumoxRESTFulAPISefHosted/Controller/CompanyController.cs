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
    /// 
    [BasicAuthenticationFilter]
    public class CompanyController : ApiController
    {
        private CompanyService companyService = new CompanyService();

        [BasicAuthenticationFilter(true)]
        [HttpGet]
        [Route("GetCompanies")]
        public IList<Company> GetCompanies()
        {
            Console.WriteLine("[HttpGet] -> /company/GetCompanies");
            return companyService.GetAllCompanies().CompanyList;
        }

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

        [BasicAuthenticationFilter(false)]
        [HttpPost]
        [Route("company/search")]
        public HttpResponseMessage Search([FromBody]CompanySearchCriteria criteria)
        {
            Console.WriteLine("[HttpDelete] -> /company/search");
            //Console.WriteLine
            var response = new HttpResponseMessage();
            var serviceOperationResult = companyService.SearchCompanies(criteria);
            if (serviceOperationResult.Result)
                response = Request.CreateResponse(HttpStatusCode.OK, serviceOperationResult.CompanyList);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest, serviceOperationResult.Message);
            return response;

        }
    }
}