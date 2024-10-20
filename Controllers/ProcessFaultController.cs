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
    public class ProcessFaultController : Controller
    {
        private readonly AuthenticationContext _context;

        public ProcessFaultController(AuthenticationContext context)
        {
            _context = context;
        }

        // GET: ProcessFault
        public async Task<IActionResult> Index()
        {
            var authenticationContext = _context.FaultReports.Include(f => f.Fault);
            return View(await authenticationContext.ToListAsync());
        }

        // GET: ProcessFault/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultReport = await _context.FaultReports
                .Include(f => f.Fault)
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (faultReport == null)
            {
                return NotFound();
            }

            return View(faultReport);
        }

        // GET: ProcessFault/Create
        public IActionResult Create()
        {
            ViewData["FaultID"] = new SelectList(_context.Faults, "FaultID", "FaultDescription");
            return View();
        }

        // POST: ProcessFault/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ReportID,ReportDate,ReportDetails,FaultID")] FaultReport faultReport)
        {
            if (ModelState.IsValid)
            {
                _context.Add(faultReport);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FaultID"] = new SelectList(_context.Faults, "FaultID", "FaultDescription", faultReport.FaultID);
            return View(faultReport);
        }

        // GET: ProcessFault/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport == null)
            {
                return NotFound();
            }
            ViewData["FaultID"] = new SelectList(_context.Faults, "FaultID", "FaultDescription", faultReport.FaultID);
            return View(faultReport);
        }

        // POST: ProcessFault/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ReportID,ReportDate,ReportDetails,FaultID")] FaultReport faultReport)
        {
            if (id != faultReport.ReportID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(faultReport);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FaultReportExists(faultReport.ReportID))
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
            ViewData["FaultID"] = new SelectList(_context.Faults, "FaultID", "FaultDescription", faultReport.FaultID);
            return View(faultReport);
        }

        // GET: ProcessFault/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faultReport = await _context.FaultReports
                .Include(f => f.Fault)
                .FirstOrDefaultAsync(m => m.ReportID == id);
            if (faultReport == null)
            {
                return NotFound();
            }

            return View(faultReport);
        }

        // POST: ProcessFault/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var faultReport = await _context.FaultReports.FindAsync(id);
            if (faultReport != null)
            {
                _context.FaultReports.Remove(faultReport);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FaultReportExists(int id)
        {
            return _context.FaultReports.Any(e => e.ReportID == id);
        }

    }
}
