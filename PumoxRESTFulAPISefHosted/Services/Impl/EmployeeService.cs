using NHibernate.Linq;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace RESTFulAPIConsole.Services
{
    public class TaskService //: AbstractService<Task>, ITaskService
    {

        //public List<Task> getAll()
        //{
        //    var list = base.getAll();
        //    return (List<Task>)list;
        //}

        //public List<Task> getTasksByUserId(int userId)
        //{
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        var list = session.Query<Task>()
        //            .Fetch(t => t.User)
        //            .Fetch(t => t.TaskCategory)
        //            .Where(t => t.User.Id == userId)
        //            .OrderByDescending(t => t.TaskDate)
        //            .ToList<Task>();
        //        return list;
        //    }
        //}

        //public Task getEagerTaskById(int taskId)
        //{
        //    using (var session = NHibernateHelper.OpenSession())
        //    {
        //        var task = session.Query<Task>()
        //        .Where(t => t.Id == taskId)
        //        .Fetch(t => t.User)
        //        .Fetch(t => t.TaskCategory)
        //        .FirstOrDefault<Task>();

        //        return task;
        //    }
        //}

        //public Task updateTask(Task task)
        //{
        //    return base.saveEntity(task);
        //}

    }
}