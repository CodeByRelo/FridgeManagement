using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FridgeManagement.Data;
using FridgeManagement.Models;
using FridgeManagement.Areas.Identity.Data; // Add this namespace for your Identity user

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
            var fridgeAllocations = await _context.FridgeAllocations
                .Include(f => f.Customer) // You may need to adjust this based on your FridgeAllocation model
                .Include(f => f.Fridge)
                .ToListAsync();
            return View(fridgeAllocations);
        }

        // GET: FridgeAllocations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAllocation = await _context.FridgeAllocations
                .Include(f => f.Customer) // Adjust based on your FridgeAllocation model
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.AllocationID == id);
            if (fridgeAllocation == null)
            {
                return NotFound();
            }

            return View(fridgeAllocation);
        }

        // GET: FridgeAllocations/Create
        public async Task<IActionResult> Create()
        {
            // Fetch customers based on UserRole
            var customers = await _context.Users
                .Where(u => u.UserRole == "Customer") // Filter for customers
                .ToListAsync();

            ViewData["CustomerID"] = new SelectList(customers, "Id", "FirstName"); // Assuming FirstName is a property
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: FridgeAllocations/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AllocationID,AllocationDate,FridgeID,CustomerID")] FridgeAllocation fridgeAllocation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fridgeAllocation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            // Reload necessary data for the dropdowns if model state is invalid
            var customers = await _context.Users
                .Where(u => u.UserRole == "Customer") // Filter for customers
                .ToListAsync();
            ViewData["CustomerID"] = new SelectList(customers, "Id", "FirstName", fridgeAllocation.CustomerID);
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAllocation.FridgeID);
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

            var customers = await _context.Users
                .Where(u => u.UserRole == "Customer") // Filter for customers
                .ToListAsync();
            ViewData["CustomerID"] = new SelectList(customers, "Id", "FirstName", fridgeAllocation.CustomerID);
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAllocation.FridgeID);
            return View(fridgeAllocation);
        }

        // POST: FridgeAllocations/Edit/5
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

            // Reload necessary data for the dropdowns if model state is invalid
            var customers = await _context.Users
                .Where(u => u.UserRole == "Customer") // Filter for customers
                .ToListAsync();
            ViewData["CustomerID"] = new SelectList(customers, "Id", "FirstName", fridgeAllocation.CustomerID);
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
                .Include(f => f.Customer) // Adjust based on your FridgeAllocation model
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
