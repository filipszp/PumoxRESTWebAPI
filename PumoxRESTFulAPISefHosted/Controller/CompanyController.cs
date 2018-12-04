using NHibernate;
using NHibernate.Linq;
using RESTFulAPIConsole.Model;
using RESTFulAPIConsole.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace RESTFulAPIConsole.Controller
{
    public class CompanyController : ApiController
    {

        private CompanyService companyService = new CompanyService();
        public IList<Company> GetCompany()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                var list = session.Query<Company>()
                    .Fetch(t => t.Employees)
                    .OrderByDescending(t => t.EstablishmentYear)
                    .ToList<Company>();

                //                var list2 = (from c in session.Query<Company>() select c).ToList<Company>();

                //              IQuery sqlQuery = session.CreateSQLQuery("SELECT * FROM [Company]").AddEntity(typeof(Company));

                //            var list3 = sqlQuery.List<Company>();
                return list;
            }
        }


        [HttpPost,]
        public HttpResponseMessage Create(Company company)
        {
            long id = companyService.createCompany(company);
            var response = Request.CreateResponse<long>(HttpStatusCode.Created, id);

            return response;

        }
    }
}