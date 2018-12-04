using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RESTFulAPIConsole.Model
{
    public class Employee
    {
        public virtual long Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual System.DateTime DateOfBirth { get; set; }
        public virtual int Company_Id { get; set; }
        //public virtual Company Company { get; set; }

        public enum JobTitle : int
        {
            Administrator,
            Developer,
            Architect,
            Manager
        }

    
        public  class EmployeeMap : ClassMap<Employee>
        {
            public EmployeeMap()
            {
                Id(x => x.Id);
                Map(x => x.FirstName);
                Map(x => x.LastName);
                Map(x => x.DateOfBirth);
                Map(x => x.Company_Id);
                //References(x => x.Company);
                //  .Column("Company_Id");
                Table("[Employee]");
            }
        }

       
    }
}
