using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SilentRed.Infrastructure.AspNet;
using Studiekring.Business.Customers.Queries;

namespace SilentRed.WebCore.Api
{
    public class CustomersController : Controller
    {
        private readonly MvcCommandBus _commandBus;
        private readonly MvcQueryBus _queryBus;

        public CustomersController(MvcCommandBus commandBus, MvcQueryBus queryBus)
        {
            _commandBus = commandBus;
            _queryBus = queryBus;
        }

        public async Task<AllCustomers.Result> Index()
        {
            return await _queryBus.Get(new AllCustomers());
        }

        //// GET: NewCustomerCommands/Details/5
        //public async Task<IActionResult> Details(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var customer = await _queryBus.Get(new GetCustomerDetails(id.Value));

        //    if (customer == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(customer);
        //}

        // GET: NewCustomerCommands/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: NewCustomerCommands/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,Email,Phone,Name,State")] NewCustomerCommand newCustomerCommand)
        //{
        //    //if (ModelState.IsValid)
        //    //{
        //    //    newCustomerCommand.Id = Guid.NewGuid();
        //    //    _context.Add(newCustomerCommand);
        //    //    await _context.SaveChangesAsync();
        //    //    return RedirectToAction("Index");
        //    //}
        //    return View(newCustomerCommand);
        //}

        //// GET: NewCustomerCommands/Edit/5
        //public async Task<IActionResult> Edit(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var newCustomerCommand = await _context.NewCustomerCommand.SingleOrDefaultAsync(m => m.Id == id);
        //    if (newCustomerCommand == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(newCustomerCommand);
        //}

        //// POST: NewCustomerCommands/Edit/5
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(Guid id, [Bind("Id,Email,Phone,Name,State")] NewCustomerCommand newCustomerCommand)
        //{
        //    if (id != newCustomerCommand.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(newCustomerCommand);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!NewCustomerCommandExists(newCustomerCommand.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction("Index");
        //    }
        //    return View(newCustomerCommand);
        //}

        //// GET: NewCustomerCommands/Delete/5
        //public async Task<IActionResult> Delete(Guid? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var newCustomerCommand = await _context.NewCustomerCommand
        //        .SingleOrDefaultAsync(m => m.Id == id);
        //    if (newCustomerCommand == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(newCustomerCommand);
        //}

        //// POST: NewCustomerCommands/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(Guid id)
        //{
        //    var newCustomerCommand = await _context.NewCustomerCommand.SingleOrDefaultAsync(m => m.Id == id);
        //    _context.NewCustomerCommand.Remove(newCustomerCommand);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction("Index");
        //}

        //private bool NewCustomerCommandExists(Guid id)
        //{
        //    return _context.NewCustomerCommand.Any(e => e.Id == id);
        //}
    }
}
