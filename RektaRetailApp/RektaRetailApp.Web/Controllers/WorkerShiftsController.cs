using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RektaRetailApp.Domain.Data;
using RektaRetailApp.Domain.DomainModels;

namespace RektaRetailApp.Web.Controllers
{
    public class WorkerShiftsController : Controller
    {
        private readonly RektaContext _context;

        public WorkerShiftsController(RektaContext context)
        {
            _context = context;
        }

        // GET: WorkerShifts
        public async Task<IActionResult> Index()
        {
            return View(await _context.WorkerShifts.ToListAsync());
        }

        // GET: WorkerShifts/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.WorkerShifts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // GET: WorkerShifts/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: WorkerShifts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,ShiftStartsAt,SiftEndsAt,HourlyRate,CreatedAt,UpdatedAt,Id,CreatedBy,UpdatedBy,IsDeleted")] Shift shift)
        {
            if (ModelState.IsValid)
            {
                _context.Add(shift);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shift);
        }

        // GET: WorkerShifts/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.WorkerShifts.FindAsync(id);
            if (shift == null)
            {
                return NotFound();
            }
            return View(shift);
        }

        // POST: WorkerShifts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,ShiftStartsAt,SiftEndsAt,HourlyRate,CreatedAt,UpdatedAt,Id,CreatedBy,UpdatedBy,IsDeleted")] Shift shift)
        {
            if (id != shift.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(shift);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShiftExists(shift.Id))
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
            return View(shift);
        }

        // GET: WorkerShifts/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var shift = await _context.WorkerShifts
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shift == null)
            {
                return NotFound();
            }

            return View(shift);
        }

        // POST: WorkerShifts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var shift = await _context.WorkerShifts.FindAsync(id);
            _context.WorkerShifts.Remove(shift);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShiftExists(int id)
        {
            return _context.WorkerShifts.Any(e => e.Id == id);
        }
    }
}
