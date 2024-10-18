using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FridgeManagement.Data;
using FridgeManagement.Models;

namespace FridgeManagement.Controllers
{
    public class FridgeAllocationsController : Controller
    {
        private readonly AuthenticationContext _context;

        public FridgeAllocationsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: FridgeAllocations
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.FridgeAllocations.Include(f => f.Customer).Include(f => f.Fridge);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: FridgeAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocations
                .Include(f => f.Customer)
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.AllocationID == id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }

            return View(fridgeAllocation);
        }

        // GET: FridgeAllocations/Create
        public IActionResult Create()
        {
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirtsName");
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: FridgeAllocations/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AllocationID,AllocationDate,FridgeID,CustomerID")] FridgeAllocation fridgeAllocation)
        {
            if (ModelState.IsValid)
            {
                // Add the new allocation to the database context
                _context.Add(fridgeAllocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index)); // Redirect to the Index action
            }

            // If model state is invalid, reload the necessary data for the dropdowns
            ViewData["CustomerID"] = new SelectList(await _context.Customers.ToListAsync(), "CustomerID", "FirtsName", fridgeAllocation.CustomerID);
            ViewData["FridgeID"] = new SelectList(await _context.Fridges.ToListAsync(), "FridgeID", "Model", fridgeAllocation.FridgeID);

            // Return the view with the current model to show validation errors
            return View(fridgeAllocation);
        }


        // GET: FridgeAllocations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocations.FindAsync(id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirtsName", fridgeAllocation.CustomerID);
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAllocation.FridgeID);
            return View(fridgeAllocation);
        }

        // POST: FridgeAllocations/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("AllocationID,AllocationDate,FridgeID,CustomerID")] FridgeAllocation fridgeAllocation)
        {
            if (id != fridgeAllocation.AllocationID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridgeAllocation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeAllocationExists(fridgeAllocation.AllocationID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["CustomerID"] = new SelectList(_context.Customers, "CustomerID", "FirtsName", fridgeAllocation.CustomerID);
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAllocation.FridgeID);
            return View(fridgeAllocation);
        }

        // GET: FridgeAllocations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocations
                .Include(f => f.Customer)
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.AllocationID == id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }

            return View(fridgeAllocation);
        }

        // POST: FridgeAllocations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeAllocation = await _context.FridgeAllocations.FindAsync(id);
            if (fridgeAllocation != null)
            {
                _context.FridgeAllocations.Remove(fridgeAllocation);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeAllocationExists(int id)
        {
            return _context.FridgeAllocations.Any(e => e.AllocationID == id);
        }
    }
}
