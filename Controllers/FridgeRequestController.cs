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
    public class FridgeRequestController : Controller
    {
        private readonly AuthenticationContext _context;

        public FridgeRequestController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: FridgeRequest
        public async Task<IActionResult> Index()
        {
            return View(await _context.Fridges.ToListAsync());
        }

        // GET: FridgeRequest/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridge = await _context.Fridges
                .FirstOrDefaultAsync(m => m.FridgeID == id);
            if (fridge == null)
            {
                return NotFound();
            }

            return View(fridge);
        }

        // GET: FridgeRequest/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FridgeRequest/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FridgeID,Model,SerialNumber,Status")] Fridge fridge)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fridge);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(fridge);
        }

        // GET: FridgeRequest/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridge = await _context.Fridges.FindAsync(id);
            if (fridge == null)
            {
                return NotFound();
            }
            return View(fridge);
        }

        // POST: FridgeRequest/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FridgeID,Model,SerialNumber,Status")] Fridge fridge)
        {
            if (id != fridge.FridgeID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridge);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeExists(fridge.FridgeID))
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
            return View(fridge);
        }

        // GET: FridgeRequest/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridge = await _context.Fridges
                .FirstOrDefaultAsync(m => m.FridgeID == id);
            if (fridge == null)
            {
                return NotFound();
            }

            return View(fridge);
        }

        // POST: FridgeRequest/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridge = await _context.Fridges.FindAsync(id);
            if (fridge != null)
            {
                _context.Fridges.Remove(fridge);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeExists(int id)
        {
            return _context.Fridges.Any(e => e.FridgeID == id);
        }
    }
}
