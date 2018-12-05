using FluentNHibernate.Mapping;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RESTFulAPIConsole.Model
{
    ///<summary>
    ///Encja  tabeli Company
    ///</summary>
    public class Company
    {
        public Company()
        {
           Employees = new List<Employee>();
        }
        public virtual Int64 Id { get; set; }

        public virtual string CompanyName { get; set; }

        public virtual Int32 EstablishmentYear { get; set; }

        public virtual IList<Employee> Employees { get; set; }

        public class CompanyMap : ClassMap<Company>
        {
            public CompanyMap()
            {
                Id(x => x.Id);
                Map(x => x.CompanyName);
                Map(x => x.EstablishmentYear);
                HasMany(x => x.Employees)
                    .Inverse()
                    .KeyColumn("Company_Id");
                Table("[Company]");

            }
        }
        ///<summary>
        ///Walidacja obligatorności pól z Request'a 
        ///Zabezpieczenie przed pustym Stringiem z JSONa oraz podsawieniem 0 dla Int32
        ///</summary>
        public virtual bool validateObligatoryField()
        {
            bool res = true;
            if (String.IsNullOrEmpty(this.CompanyName))
                return false;
            if (this.EstablishmentYear == 0)
                return false;
            this.Employees.ToList().ForEach(e =>
            {
                if (String.IsNullOrEmpty(e.FirstName)) res = false;
                if (String.IsNullOrEmpty(e.LastName)) res = false;
                if (String.IsNullOrEmpty(e.JobTitle.ToString())) res = false;
                if (e.DateOfBirth.Day == 1 && e.DateOfBirth.Month == 1 && e.DateOfBirth.Year == 1) res = false;
            });

            return res;
        }
    }
}
