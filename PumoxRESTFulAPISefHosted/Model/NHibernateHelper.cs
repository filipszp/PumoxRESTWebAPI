using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;

namespace RESTFulAPIConsole.Model
{
    public sealed class NHibernateHelper
    {

        private static ISessionFactory _sessionFactory;

        private static ISessionFactory SessionFactory
        {
            get
            {
                if (_sessionFactory == null)
                    CreateSessionFactory();
                return _sessionFactory;
            }
        }


        private static ISessionFactory CreateSessionFactory()
        {


            _sessionFactory = Fluently.Configure()
                 .Database(FluentNHibernate.Cfg.Db.MsSqlCeConfiguration.Standard.ShowSql()
                 .ConnectionString(c => c.FromConnectionStringWithKey("DbConnectionString")))
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Company.CompanyMap>().Conventions.Add(DefaultLazy.Always()))
                 .Mappings(m => m.FluentMappings.AddFromAssemblyOf<Employee.EmployeeMap>())
                 .BuildSessionFactory();


            return _sessionFactory;
        }

        public static ISession OpenSession()
        {
            return SessionFactory.OpenSession();
        }
    }
}
