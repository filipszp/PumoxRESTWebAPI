using RESTFulAPIConsole.Model;
using System;
using System.Text;

namespace PumoxRESTFulAPI.Model
{
    public class CompanySearchCriteria
    {
        public string Keyword { get; set; }
        public DateTime? EmployeeDateOfBirthFrom { get; set; }
        public DateTime? EmployeeDateOfBirthTo { get; set; }
        public JobTitleEnum? EmployeeJobTitles { get; set; }

        public override string ToString()
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("   #CompanySearchCriteria: ");
            if (!String.IsNullOrEmpty(Keyword))
                stringBuilder.AppendLine("Keyword: " + Keyword);
            if (EmployeeDateOfBirthFrom.HasValue)
                stringBuilder.AppendLine("EmployeeDateOfBirthFrom: " + EmployeeDateOfBirthFrom.Value);
            if (EmployeeDateOfBirthTo.HasValue)
                stringBuilder.AppendLine("EmployeeDateOfBirthTo: " + EmployeeDateOfBirthTo.Value);
            if (EmployeeJobTitles != null)
                stringBuilder.AppendLine("EmployeeJobTitles: " + EmployeeJobTitles.Value);

            return stringBuilder.ToString();
        }
    }
}
