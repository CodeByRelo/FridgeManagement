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
    public class ServiceCheckupsController : Controller
    {
        private readonly AuthenticationContext _context;

        public ServiceCheckupsController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: ServiceCheckups
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.ServiceCheckups.Include(s => s.Fridge);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: ServiceCheckups/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCheckup = await _context.ServiceCheckups
                .Include(s => s.Fridge)
                .FirstOrDefaultAsync(m => m.ServiceCheckupID == id);
            if (serviceCheckup == null)
            {
                return NotFound();
            }

            return View(serviceCheckup);
        }

        // GET: ServiceCheckups/Create
        public IActionResult Create()
        {
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model");
            return View();
        }

        // POST: ServiceCheckups/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ServiceCheckupID,CheckupDate,Notes,FridgeID")] ServiceCheckup serviceCheckup)
        {
            if (ModelState.IsValid)
            {
                _context.Add(serviceCheckup);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceCheckup.FridgeID);
            return View(serviceCheckup);
        }

        // GET: ServiceCheckups/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCheckup = await _context.ServiceCheckups.FindAsync(id);
            if (serviceCheckup == null)
            {
                return NotFound();
            }
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceCheckup.FridgeID);
            return View(serviceCheckup);
        }

        // POST: ServiceCheckups/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ServiceCheckupID,CheckupDate,Notes,FridgeID")] ServiceCheckup serviceCheckup)
        {
            if (id != serviceCheckup.ServiceCheckupID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(serviceCheckup);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCheckupExists(serviceCheckup.ServiceCheckupID))
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
            ViewData["FridgeID"] = new SelectList(_context.Fridges, "FridgeID", "Model", serviceCheckup.FridgeID);
            return View(serviceCheckup);
        }

        // GET: ServiceCheckups/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCheckup = await _context.ServiceCheckups
                .Include(s => s.Fridge)
                .FirstOrDefaultAsync(m => m.ServiceCheckupID == id);
            if (serviceCheckup == null)
            {
                return NotFound();
            }

            return View(serviceCheckup);
        }

        // POST: ServiceCheckups/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCheckup = await _context.ServiceCheckups.FindAsync(id);
            if (serviceCheckup != null)
            {
                _context.ServiceCheckups.Remove(serviceCheckup);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCheckupExists(int id)
        {
            return _context.ServiceCheckups.Any(e => e.ServiceCheckupID == id);
        }
    }
}
