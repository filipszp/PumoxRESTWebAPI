using FluentNHibernate.Mapping;
using System;

namespace RESTFulAPIConsole.Model
{
    ///<summary>
    ///Encja  tabeli Employee
    ///</summary>
    public class Employee
    {
        public virtual Int64 Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual System.DateTime DateOfBirth { get; set; }
        public virtual Int64 Company_Id { get; set; }
        public virtual string JobTitle { get; set; }
        public enum JobTitleEnum : int
        {
            Administrator,
            Developer,
            Architect,
            Manager
        }


        public class EmployeeMap : ClassMap<Employee>
        {
            public EmployeeMap()
            {
                Id(x => x.Id);
                Map(x => x.FirstName).Not.Nullable();
                Map(x => x.LastName).Not.Nullable();
                Map(x => x.DateOfBirth).Not.Nullable();
                Map(x => x.Company_Id).Not.Nullable();
                Map(x => x.JobTitle).Not.Nullable();
                Table("[Employee]");
            }
        }
    }
}
