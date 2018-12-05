using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumoxRESTFulAPI.Model
{

    /// <summary>Wrapper klasy Employee (ukrycie properties w JSON, zwaracanie StringEnum)</summary>
    public class EmployeeWrapper
    {
        //public Int64 Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        //public Int64 Company_Id { get; set; }
        public String JobTitle { get; set; }

        public void WrappEmployee(Employee employee)
        {
            this.FirstName = employee.FirstName;
            this.LastName = employee.LastName;
            this.DateOfBirth = employee.DateOfBirth;
            this.JobTitle = employee.JobTitle.ToString();
        }
    }
}
