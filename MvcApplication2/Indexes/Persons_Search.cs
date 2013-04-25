using System.Linq;
using MvcApplication2.Models;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace MvcApplication2.Indexes
{
    public class Persons_Search : AbstractIndexCreationTask<Person, Persons_Search.SearchResult>
    {
        public class SearchResult
        {
            public string Name { get; set; }
            public string City { get; set; }
            public int Age { get; set; }
            public object[] Search { get; set; }
            public string Phone { get; set; }
            public string Id { get; set; }
            public string Profile { get; set; }
        }

        public Persons_Search()
        {
            Map = persons => from p in persons
                             select new SearchResult
                             {
                                 Id = p.Id,
                                 Age = p.Age,
                                 City = p.City,
                                 Name = p.Name,
                                 Phone = p.Phone,
                                 // dumb I know, but trying to get bad behavior
                                 Profile = p.Profile,
                                 Search = new object[] { p.Name, p.City, p.Phone, p.Age }
                             };

            StoreAllFields(FieldStorage.Yes);
        }
    }


}