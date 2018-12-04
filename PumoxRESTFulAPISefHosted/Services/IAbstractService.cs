using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace RESTFulAPIConsole.Services
{
    public interface IAbstractService<T>
    {
        T saveNewEntity(T entity);
        T saveEntity(T entity);
        int deleteEntity(T entity);
        IList<T> findByNameField(string field, int userId = -1, string stringValue = "", int intValue = -1);
        IList<T> getAll();
    }
}
