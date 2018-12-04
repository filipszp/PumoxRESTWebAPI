using NHibernate.Linq;
using System;
using System.Data;
using System.Linq;
using Microsoft.Owin.Hosting;
using RESTFulAPIConsole.Model;

namespace RESTFulAPIConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            const string url = "http://localhost:8080";
            
            using (WebApp.Start<Startup>(url))
            {
                Console.WriteLine("Server started at:" + url);
               Console.ReadLine();
            }
        }
    }
}
