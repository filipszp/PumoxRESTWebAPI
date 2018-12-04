using NHibernate.Criterion;
using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;


namespace RESTFulAPIConsole.Services
{
    public abstract class AbstractService<T> : IAbstractService<T> where T : class
    {
        public virtual IList<T> getAll()
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                IList<T> list = new List<T>();
                list = session.Query<T>().ToList<T>();

                return list;
            }
        }
        public virtual T saveNewEntity(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    T persistEntity;
                    Int64 entityId = (Int64)session.Save(entity);
                    
                    transaction.Commit();
                    persistEntity = session.Get<T>(entityId);
                    return persistEntity;
                }
            }
        }

        public virtual T saveEntity(T entity)
        {
            T persistEntity;
            Type entityType = entity.GetType();
            PropertyInfo propertyId = entityType.GetProperty("Id");

            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.SaveOrUpdate(entity);
                    transaction.Commit();

                    persistEntity = (T)session.CreateCriteria<T>()
                        .Add(Restrictions.Eq("Id", (Int64)propertyId.GetValue(entity))).UniqueResult();
                }
            }
            return persistEntity;
        }

        public virtual int deleteEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public virtual IList<T> findByNameField(string field, int userId = -1, string stringValue = "", int intValue = -1)
        {
            List<T> persistEntity = new List<T>();
            using (var session = NHibernateHelper.OpenSession())
            {
                if (!stringValue.Equals("") && userId == -1)
                {
                    persistEntity = (List<T>)session.CreateCriteria<T>()
                            .Add(Restrictions.Eq(field, stringValue)).List<T>();
                }
                else if (intValue != -1 && userId == -1)
                {
                    persistEntity = (List<T>)session.CreateCriteria<T>()
                            .Add(Restrictions.Eq(field, intValue)).List<T>();
                }
                else if (intValue != -1 && userId != -1)
                {
                    persistEntity = (List<T>)session.CreateCriteria<T>()
                           .Add(Restrictions.Eq("User.Id", userId))
                           .Add(Restrictions.Eq(field, intValue)).List<T>();
                }
                else if (!stringValue.Equals("") && userId != -1)
                {
                    persistEntity = (List<T>)session.CreateCriteria<T>()
                            .Add(Restrictions.Eq("User_Id", userId))
                            .Add(Restrictions.Eq(field, stringValue)).List<T>();
                }
                return persistEntity;
            }
        }
    }
}