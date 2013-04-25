using System.Linq;
using System.Web.Mvc;
using MvcApplication2.Indexes;
using MvcApplication2.Models;
using Raven.Client;

namespace MvcApplication2.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index(int? skip = 0, bool lazy = false)
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            if (lazy)
            {
                var persons = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                                .Skip(skip.Value)
                                .Take(50)
                                .AsProjection<Persons_Search.SearchResult>()
                                .Lazily();

                var search = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                               .Search(x => x.Search, "Khalid*", escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                               .Skip(skip.Value)
                               .Take(50)
                               .AsProjection<Persons_Search.SearchResult>()
                               .Lazily();

                var user = Db.Advanced.Lazily.Load<Person>("person/1");

                var first = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                              .OrderBy(x => x.Id)
                              .As<Person>()
                              .Lazily();

                Db.Advanced.Eagerly.ExecuteAllPendingLazyOperations();
            }
            else
            {
                var persons = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                                .Skip(skip.Value)
                                .Take(50)
                                .AsProjection<Persons_Search.SearchResult>()
                                .ToList();

                var search = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                               .Search(x => x.Search, "Khalid*", escapeQueryOptions: EscapeQueryOptions.AllowPostfixWildcard)
                               .Skip(skip.Value)
                               .Take(50)
                               .AsProjection<Persons_Search.SearchResult>()
                               .ToList();

                var user = Db.Load<Person>("person/1");

                var first = Db.Query<Persons_Search.SearchResult, Persons_Search>()
                              .OrderBy(x => x.Id)
                              .As<Person>()
                              .FirstOrDefault();
            }

            return View();
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            Db = MvcApplication.DocumentStore.OpenSession();
        }

        protected IDocumentSession Db { get; set; }

        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            using (Db)
            {
                Db.SaveChanges();
            }
        }
    }
}
