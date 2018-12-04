using NHibernate.Criterion;
using RESTFulAPIConsole.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;


namespace RESTFulAPIConsole.Services
{
    public abstract class AbstractService<T> : IAbstractService<T> where T : class
    {
        public ICollection<T> getAll(string sortField = "", SortOrder order = SortOrder.Ascending)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                ICollection<T> list = new List<T>();
                if (sortField.Equals(""))
                {
                    list = session.Query<T>().ToList<T>();
                }
                else
                {
                    //TODO sort
                    list = session.Query<T>().ToList<T>();
                }
                return list;
            }
        }
        public T saveNewEntity(T entity)
        {
            using (var session = NHibernateHelper.OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    T persistEntity;
                    int entityId = (int)session.Save(entity);
                    transaction.Commit();
                    persistEntity = session.Get<T>(entityId);
                    return persistEntity;
                }
            }
        }

        public T saveEntity(T entity)
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
                        .Add(Restrictions.Eq("Id", (int)propertyId.GetValue(entity))).UniqueResult();
                }
            }
            return persistEntity;
        }

        public int deleteEntity(T entity)
        {
            throw new NotImplementedException();
        }

        public ICollection<T> findByNameField(string field, int userId = -1, string stringValue = "", int intValue = -1)
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