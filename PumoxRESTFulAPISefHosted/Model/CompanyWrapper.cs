using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumoxRESTFulAPI.Model
{
    /// <summary>Wrapper klasy Company (ukrycie properties w wynikowym JSON)</summary>
    public class CompanyWrapper
    {
        public CompanyWrapper()
        {
            Employees = new List<EmployeeWrapper>();
        }
        public Int64 Id { get; set; }

        public string CompanyName { get; set; }

        public Int32 EstablishmentYear { get; set; }

        public virtual IList<EmployeeWrapper> Employees { get; set; }
        public void WrappCompany(Company company)
        {
            this.Id = company.Id;
            this.CompanyName = company.CompanyName;
            this.EstablishmentYear = company.EstablishmentYear;
            foreach(var emp in company.Employees)
            {
                EmployeeWrapper employeeWrapper = new EmployeeWrapper();
                employeeWrapper.WrappEmployee(emp);
                this.Employees.Add(employeeWrapper);
            }
        }
    }
}
