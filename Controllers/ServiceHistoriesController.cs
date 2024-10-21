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
    public class ServiceHistoriesController : Controller
    {
        private readonly AuthenticationContext _context;

        public ServiceHistoriesController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: ServiceHistories
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.ServiceHistories.Include(s => s.Fridge);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: ServiceHistories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceHistory = await _context.ServiceHistories
                .Include(s => s.Fridge)
                .FirstOrDefaultAsync(m => m.ServiceHistoryID == id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            return View(serviceHistory);
        }

        // GET: ServiceHistories/Create
        public IActionResult Create()
        {
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: ServiceHistories/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceHistoryID,ServiceDate,ServiceNotes,FridgeID")] ServiceHistory serviceHistory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceHistory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceHistory.FridgeID);
            return View(serviceHistory);
        }

        // GET: ServiceHistories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceHistory = await _context.ServiceHistories.FindAsync(id);
            if (serviceHistory == null)
            {
                return NotFound();
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceHistory.FridgeID);
            return View(serviceHistory);
        }

        // POST: ServiceHistories/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceHistoryID,ServiceDate,ServiceNotes,FridgeID")] ServiceHistory serviceHistory)
        {
            if (id != serviceHistory.ServiceHistoryID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceHistory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceHistoryExists(serviceHistory.ServiceHistoryID))
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
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceHistory.FridgeID);
            return View(serviceHistory);
        }

        // GET: ServiceHistories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceHistory = await _context.ServiceHistories
                .Include(s => s.Fridge)
                .FirstOrDefaultAsync(m => m.ServiceHistoryID == id);
            if (serviceHistory == null)
            {
                return NotFound();
            }

            return View(serviceHistory);
        }

        // POST: ServiceHistories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceHistory = await _context.ServiceHistories.FindAsync(id);
            if (serviceHistory != null)
            {
                _context.ServiceHistories.Remove(serviceHistory);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceHistoryExists(int id)
        {
            return _context.ServiceHistories.Any(e => e.ServiceHistoryID == id);
        }
    }
}
