using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS7420_1.Data;
using ASS7420_1.Models;

namespace ASS7420_1.Controllers
{
    public class HatsController : Controller
    {
        private readonly ShopContext _context;

        public HatsController(ShopContext context)
        {
            _context = context;    
        }

        // GET: Hats
        public async Task<IActionResult> Index()
        {
            var shopContext = _context.Hat.Include(h => h.Category).Include(h => h.Supplier);
            return View(await shopContext.ToListAsync());
        }

        // GET: Hats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hat.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }

            return View(hat);
        }

        // GET: Hats/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryID");
            ViewData["SupplierID"] = new SelectList(_context.Set<Supplier>(), "SupplierID", "SupplierID");
            return View();
        }

        // POST: Hats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HatID,CategoryID,Description,HatName,Image,Price,SupplierID")] Hat hat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(hat);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryID", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Set<Supplier>(), "SupplierID", "SupplierID", hat.SupplierID);
            return View(hat);
        }

        // GET: Hats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hat.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryID", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Set<Supplier>(), "SupplierID", "SupplierID", hat.SupplierID);
            return View(hat);
        }

        // POST: Hats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("HatID,CategoryID,Description,HatName,Image,Price,SupplierID")] Hat hat)
        {
            if (id != hat.HatID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(hat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HatExists(hat.HatID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "CategoryID", "CategoryID", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Set<Supplier>(), "SupplierID", "SupplierID", hat.SupplierID);
            return View(hat);
        }

        // GET: Hats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hat.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }

            return View(hat);
        }

        // POST: Hats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hat = await _context.Hat.SingleOrDefaultAsync(m => m.HatID == id);
            _context.Hat.Remove(hat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HatExists(int id)
        {
            return _context.Hat.Any(e => e.HatID == id);
        }
    }
}
