using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumoxRESTFulAPI.Model
{
    /// <summary>Klasa rezultatu operacji serwisowych</summary>
    public class ServiceOperationResult
    {
        const String defaultMessage = "Error during proccesing Company (Bad request data)!";
        public ServiceOperationResult()
        {
            Id = 0; Result = true; Message = defaultMessage;
            CompanyWrappers = new List<CompanyWrapper>();
        }
        public Int64 Id { get; set; }
        public bool Result { get; set; }
        public String Message { get; set; }
        public List<CompanyWrapper> CompanyWrappers { get; set; }
    }
}
