using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using OEC.Models;

namespace OEC.Controllers
{
    public class FarmController : Controller
    {
        private readonly OECContext _context;

        public FarmController(OECContext context)
        {
            _context = context;
        }

        // GET: Farm
        public async Task<IActionResult> Index()
        {
            var oECContext = _context.Farm.Include(f => f.ProvinceCodeNavigation);
            return View(await oECContext.ToListAsync());
        }

        // GET: Farm/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // GET: Farm/Create
        public IActionResult Create()
        {
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode");
            return View();
        }

        // POST: Farm/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FarmId,Name,Address,Town,County,ProvinceCode,PostalCode,HomePhone,CellPhone,Email,Directions,DateJoined,LastContactDate")] Farm farm)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(farm);
                    await _context.SaveChangesAsync();
                    TempData["successfull"] = "Data added Successfully";
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (Exception error)
            {

                ModelState.AddModelError("", error.GetBaseException().Message);
                TempData["message"] = "Error adding the data " + error.GetBaseException().Message;
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", farm.ProvinceCode);
            return View(farm);
        }

        // GET: Farm/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm.FindAsync(id);
            if (farm == null)
            {
                return NotFound();
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", farm.ProvinceCode);
            return View(farm);
        }

        // POST: Farm/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FarmId,Name,Address,Town,County,ProvinceCode,PostalCode,HomePhone,CellPhone,Email,Directions,DateJoined,LastContactDate")] Farm farm)
        {
            if (id != farm.FarmId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(farm);
                    await _context.SaveChangesAsync();
                    TempData["successfull"] = "Data updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FarmExists(farm.FarmId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                catch (Exception error)
                {

                    ModelState.AddModelError("", error.GetBaseException().Message);
                    TempData["message"] = "Error editing the data " + error.GetBaseException().Message;
                }


              //  return RedirectToAction(nameof(Index));
            }
            ViewData["ProvinceCode"] = new SelectList(_context.Province, "ProvinceCode", "ProvinceCode", farm.ProvinceCode);
            return View(farm);
        }

        // GET: Farm/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Farm == null)
            {
                return NotFound();
            }

            var farm = await _context.Farm
                .Include(f => f.ProvinceCodeNavigation)
                .FirstOrDefaultAsync(m => m.FarmId == id);
            if (farm == null)
            {
                return NotFound();
            }

            return View(farm);
        }

        // POST: Farm/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Farm == null)
            {
                return Problem("Entity set 'OECContext.Farm'  is null.");
            }

            try
            {
                var farm = await _context.Farm.FindAsync(id);
                if (farm != null)
                {
                    _context.Farm.Remove(farm);
                }
                await _context.SaveChangesAsync();
                TempData["successfull"] = "Data deleted Successfully";
                return RedirectToAction(nameof(Index));
            }
            catch (Exception error)
            {

                ModelState.AddModelError("", error.GetBaseException().Message);
                TempData["message"] = "Error editing the data " + error.GetBaseException().Message;
            }
            return View("Delete");
        }

        private bool FarmExists(int id)
        {
          return _context.Farm.Any(e => e.FarmId == id);
        }
    }
}
