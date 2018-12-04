using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PumoxRESTFulAPI.Model
{
    public class CompanySearchCriteria
    {
        public virtual string Keyword { get; set; }
        public virtual DateTime? EmployeeDateOfBirthFrom { get; set; }
        public virtual DateTime? EmployeeDateOfBirthTo { get; set; }
        public virtual string EmployeeJobTitles { get; set; }
    }
}
