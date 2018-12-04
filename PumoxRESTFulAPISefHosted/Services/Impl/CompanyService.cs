using PumoxRESTFulAPI.Model;
using RESTFulAPIConsole.Model;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;


namespace RESTFulAPIConsole.Services
{
    public class CompanyService : AbstractService<Company>, ICompanyService
    {
        public int createCompany(Company entityToAdd)
        {
            var newCompany = base.saveNewEntity(entityToAdd);
            return newCompany.Id;
        }

        public void deleteCompany(Company entityToDelete)
        {
            throw new NotImplementedException();
        }

        public IList seachCompanies(CompanySearchCriteria searchCriteria)
        {
            throw new NotImplementedException();
        }

        public void updateCompany(Company entityToPersist)
        {
            throw new NotImplementedException();
        }

        //public User createUser(User user)
        //{
        //    return base.saveNewEntity(user);
        //}

        //public int deleteUser(User user)
        //{
        //    throw new System.NotImplementedException();
        //}

        //public User login(User user)
        //{
        //    var userFromDb = new User();
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        userFromDb = session.Query<User>()
        //            .Where(u => u.Login == user.Login)
        //            .FirstOrDefault<User>();

        //        if (userFromDb == null)
        //            return userFromDb;
        //        else
        //        {
        //            if (PasswordCrypto.Decrypt(userFromDb.Password).Equals(user.Password))
        //                userFromDb.isLogged = true;
        //        }
        //    }
        //    return userFromDb;
        //}

        //public User lastLoginTimeUpdate(User user)
        //{
        //    var userFromDb = new User();
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        userFromDb = session.Get<User>(user.Id);
        //        userFromDb.LastLoginDate = DateTime.Now;
        //        return base.saveEntity(userFromDb);
        //    }



        //}

    }
}
