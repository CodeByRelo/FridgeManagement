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
    public class RepairFridgeController : Controller
    {
        private readonly AuthenticationContext _context;

        public RepairFridgeController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: RepairFridge
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.FridgeActions.Include(f => f.Fridge);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: RepairFridge/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAction = await _context.FridgeActions
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (fridgeAction == null)
            {
                return NotFound();
            }

            return View(fridgeAction);
        }

        // GET: RepairFridge/Create
        public IActionResult Create()
        {
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: RepairFridge/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ActionID,ActionDate,ActionDescription,FridgeID")] FridgeAction fridgeAction)
        {
            if (ModelState.IsValid)
            {
                _context.Add(fridgeAction);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAction.FridgeID);
            return View(fridgeAction);
        }

        // GET: RepairFridge/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAction = await _context.FridgeActions.FindAsync(id);
            if (fridgeAction == null)
            {
                return NotFound();
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAction.FridgeID);
            return View(fridgeAction);
        }

        // POST: RepairFridge/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ActionID,ActionDate,ActionDescription,FridgeID")] FridgeAction fridgeAction)
        {
            if (id != fridgeAction.ActionID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fridgeAction);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FridgeActionExists(fridgeAction.ActionID))
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
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", fridgeAction.FridgeID);
            return View(fridgeAction);
        }

        // GET: RepairFridge/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fridgeAction = await _context.FridgeActions
                .Include(f => f.Fridge)
                .FirstOrDefaultAsync(m => m.ActionID == id);
            if (fridgeAction == null)
            {
                return NotFound();
            }

            return View(fridgeAction);
        }

        // POST: RepairFridge/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fridgeAction = await _context.FridgeActions.FindAsync(id);
            if (fridgeAction != null)
            {
                _context.FridgeActions.Remove(fridgeAction);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FridgeActionExists(int id)
        {
            return _context.FridgeActions.Any(e => e.ActionID == id);
        }
    }
}
