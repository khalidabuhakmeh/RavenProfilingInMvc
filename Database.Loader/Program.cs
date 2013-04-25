using System;
using System.Diagnostics;
using MvcApplication2.Models;
using Raven.Abstractions.Data;
using Raven.Client;
using Raven.Client.Document;

namespace Database.Loader
{
    class Program
    {
        public static readonly IDocumentStore Db = new DocumentStore() { ConnectionStringName = "RavenDb" }.Initialize();

        static void Main(string[] args)
        {
            var count = 150000;
            var stopwatch = new Stopwatch();
            stopwatch.Start();

            using (var bulkInsert = Db.BulkInsert("Test", new BulkInsertOptions { CheckForUpdates = true }))
            {
                for (var i = 0; i < count ; i++)
                {
                    var person = Person.New(i);
                    bulkInsert.Store(person);

                    Console.WriteLine("inserted person #{0}: {1}", i, person.Name);
                }
            }
            stopwatch.Stop();
            
            Console.WriteLine("inserted {0} in {1} minutes ", count, stopwatch.Elapsed.TotalMinutes);
            Console.ReadLine();
        }
    }
}
