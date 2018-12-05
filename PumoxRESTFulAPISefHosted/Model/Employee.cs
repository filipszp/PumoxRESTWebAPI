using FluentNHibernate.Mapping;
using System;

namespace RESTFulAPIConsole.Model
{
    ///<summary>
    ///Encja  tabeli <see cref="Employee"/>
    ///</summary>
    public class Employee
    {
        public virtual Int64 Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual System.DateTime DateOfBirth { get; set; }
        public virtual Int64 Company_Id { get; set; }
        public virtual JobTitleEnum JobTitle{ get; set; }

        public class EmployeeMap : ClassMap<Employee>
        {
            public EmployeeMap()
            {
                Id(x => x.Id);
                Map(x => x.FirstName);
                Map(x => x.LastName);
                Map(x => x.DateOfBirth);
                Map(x => x.Company_Id);
                Map(x => x.JobTitle).CustomType<GenericEnumMapper<JobTitleEnum>>();
                ImportType<JobTitleEnum>();
                Table("[Employee]");
            }
        }
    }

    public enum JobTitleEnum {
        Administrator,
        Developer,
        Architect,
        Manager
    }
}
