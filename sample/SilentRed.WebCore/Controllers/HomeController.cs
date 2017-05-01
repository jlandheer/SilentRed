using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SilentRed.Infrastructure;
using SilentRed.WebCore.Customers.Queries;

namespace SilentRed.WebCore.Controllers
{
    public class HomeController : Controller
    {
        private readonly ICommandBus _commandBus;
        private readonly IQueryBus _queryBus;

        public HomeController(ICommandBus commandBus, IQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }
        public async Task<IActionResult> Index()
        {
            var queryResult = await _queryBus.Get(new AllCustomers());

            return View(queryResult.Value);
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
