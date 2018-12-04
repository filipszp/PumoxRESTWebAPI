using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumoxRESTFulAPI.Model
{
    public class ServiceOperationResult
    {
        const String defaultMessage = "Error during proccesing Company (Bad request data)!";
        public ServiceOperationResult()
        {
            Id = 0; Result = true; Message = defaultMessage; CompanyList = new List<Company>(); 
        }

        public Int64 Id { get; set; }
        public bool Result { get; set; }
        public String Message { get; set; }
        public List<Company> CompanyList { get; set; }
    }
}
