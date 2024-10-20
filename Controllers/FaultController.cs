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
    public class FaultController : Controller
    {
        private readonly AuthenticationContext _context;

        public FaultController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: Fault
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.Faults.Include(f => f.Fridge);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: Fault/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.FaultID == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // GET: Fault/Create
        public IActionResult Create()
        {
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: Fault/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FaultID,FaultDescription,Status,FridgeID")] Fault fault)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fault);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fault.FridgeID);
            return View(fault);
        }

        // GET: Fault/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults.FindAsync(id);
            if (fault == null)
            {
                return NotFound();
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fault.FridgeID);
            return View(fault);
        }

        // POST: Fault/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FaultID,FaultDescription,Status,FridgeID")] Fault fault)
        {
            if (id != fault.FaultID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fault);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultExists(fault.FaultID))
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
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fault.FridgeID);
            return View(fault);
        }

        // GET: Fault/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fault = await _context.Faults
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.FaultID == id);
            if (fault == null)
            {
                return NotFound();
            }

            return View(fault);
        }

        // POST: Fault/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fault = await _context.Faults.FindAsync(id);
            if (fault != null)
            {
                _context.Faults.Remove(fault);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultExists(int id)
        {
            return _context.Faults.Any(e => e.FaultID == id);
        }
    }
}
