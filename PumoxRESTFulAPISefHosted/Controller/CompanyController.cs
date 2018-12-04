using RESTFulAPIConsole.Model;
using RESTFulAPIConsole.Services;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFulAPIConsole.Controller
{
    public class CompanyController : ApiController
    {
        private CompanyService companyService = new CompanyService();

        [HttpGet]
        [Route("GetCompanies")]
        public IList<Company> GetCompanies()
        {
            Console.WriteLine("[HttpGet] -> /company/GetCompanies");
            return companyService.getAllCompany();
        }


        [HttpPost]
        public HttpResponseMessage Create(Company company)
        {
            Console.WriteLine("[HttpPost] -> /company/create");
            var response = new HttpResponseMessage();
            Int64 id = companyService.createCompany(company);
            if (id != 0)
                response = Request.CreateResponse<long>(HttpStatusCode.Created, id);
            else
                response = Request.CreateErrorResponse(HttpStatusCode.BadRequest,"Bad data request");

            return response;

        }

        [HttpPut]
        public HttpResponseMessage Update()
        {
            Console.WriteLine("[HttpPut] -> /company/update");
            // Int64 id = companyService.createCompany(company);
            var response = Request.CreateResponse<long>(HttpStatusCode.OK, 1);
            return response;

        }
    }
}