using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;

namespace RESTFulAPIConsole.Model
{

    public class Company
    {
        public Company()
        {
           Employees = new List<Employee>();
        }
        public virtual int Id { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual int EstablishmentYear { get; set; }

        public virtual IList<Employee> Employees { get; set; }

        public class CompanyMap : ClassMap<Company>
        {
            public CompanyMap()
            {
                Id(x => x.Id);
                Map(x => x.CompanyName);
                Map(x => x.EstablishmentYear);
                HasMany(x => x.Employees)
                    .KeyColumn("Company_Id")
                    .Inverse();
                Table("[Company]");

            }
        }
    }
}
