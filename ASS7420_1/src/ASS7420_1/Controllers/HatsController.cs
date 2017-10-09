using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ASS7420_1.Data;
using ASS7420_1.Models;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting; 
using Microsoft.AspNetCore.Http; 
using System.IO;


namespace ASS7420_1.Controllers
{
    public class HatsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IHostingEnvironment _hostingEnv;

        public HatsController(ApplicationDbContext context, IHostingEnvironment hEnv)
        {
            _context = context;
            _hostingEnv = hEnv;
        }

        // GET: Hats
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            // var shopContext = _context.Hats.Include(h => h.Category).Include(h => h.Supplier);
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["PriceSortParm"] = sortOrder == "Price" ? "price_desc" : "Price";
            ViewData["CurrentFilter"] = searchString;
            var hats = from h in _context.Hats
                            select h;
            if (!String.IsNullOrEmpty(searchString))
            {
                hats = hats.Where(h => h.HatName.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    hats = hats.OrderByDescending(h => h.HatName);
                    break;
                case "Price":
                    hats = hats.OrderBy(h => h.Price);
                    break;
                case "price_desc":
                    hats = hats.OrderByDescending(h => h.Price);
                    break;
                default:
                    hats = hats.OrderBy(h => h.HatName);
                    break;
            }
            return View(await hats.AsNoTracking().ToListAsync());
          //  return View(await shopContext.AsNoTracking().ToListAsync());
        }

        // GET: Hats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hats.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }

            return View(hat);
        }

        // GET: Hats/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Categorys, "CategoryID", "CategoryID");
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID");
            return View();
        }

        // POST: Hats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("HatID,CategoryID,Description,HatName,Image,Price,SupplierID")] Hat hat, IList<IFormFile> _files)


        {
            var relativeName = "";
            var fileName = "";

            if (_files.Count < 1)
            {
                relativeName = "/Images/Default.jpg";
            }
            else
            {
                foreach (var file in _files)
                {
                    fileName = ContentDispositionHeaderValue
                                      .Parse(file.ContentDisposition)
                                      .FileName
                                      .Trim('"');
                    //Path for localhost
                    relativeName = "/Images/HatImages/" + DateTime.Now.ToString("ddMMyyyy-HHmmssffffff") + fileName;

                    using (FileStream fs = System.IO.File.Create(_hostingEnv.WebRootPath + relativeName))
                    {
                        await file.CopyToAsync(fs);
                        fs.Flush();
                    }
                }
            }
            hat.Image = relativeName;
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(hat);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " + "Try again, and if the problem persists " + "see your system administrator.");
            }
            return View(hat);
        }

    // GET: Hats/Edit/5
    public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hats.SingleOrDefaultAsync(m => m.HatID == id);
            if (hat == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Categorys, "CategoryID", "CategoryID", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", hat.SupplierID);
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
            ViewData["CategoryID"] = new SelectList(_context.Categorys, "CategoryID", "CategoryID", hat.CategoryID);
            ViewData["SupplierID"] = new SelectList(_context.Suppliers, "SupplierID", "SupplierID", hat.SupplierID);
            return View(hat);
        }

        // GET: Hats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var hat = await _context.Hats.SingleOrDefaultAsync(m => m.HatID == id);
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
            var hat = await _context.Hats.SingleOrDefaultAsync(m => m.HatID == id);
            _context.Hats.Remove(hat);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool HatExists(int id)
        {
            return _context.Hats.Any(e => e.HatID == id);
        }
    }
}
