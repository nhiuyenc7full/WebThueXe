using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class NhaSanXuatsController : Controller
    {
        private readonly AppDbContext _context;

        public NhaSanXuatsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: NhaSanXuats
        public async Task<IActionResult> Index()
        {
            return View(await _context.NhaSanXuat.ToListAsync());
        }

        // GET: NhaSanXuats/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaSanXuat = await _context.NhaSanXuat
                .FirstOrDefaultAsync(m => m.NhaSanXuatId == id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }

            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: NhaSanXuats/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NhaSanXuatId,TenNSX")] NhaSanXuat nhaSanXuat)
        {
            if (ModelState.IsValid)
            {
                _context.Add(nhaSanXuat);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaSanXuat = await _context.NhaSanXuat.FindAsync(id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }
            return View(nhaSanXuat);
        }

        // POST: NhaSanXuats/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("NhaSanXuatId,TenNSX")] NhaSanXuat nhaSanXuat)
        {
            if (id != nhaSanXuat.NhaSanXuatId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(nhaSanXuat);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!NhaSanXuatExists(nhaSanXuat.NhaSanXuatId))
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
            return View(nhaSanXuat);
        }

        // GET: NhaSanXuats/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var nhaSanXuat = await _context.NhaSanXuat
                .FirstOrDefaultAsync(m => m.NhaSanXuatId == id);
            if (nhaSanXuat == null)
            {
                return NotFound();
            }

            return View(nhaSanXuat);
        }

        // POST: NhaSanXuats/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var nhaSanXuat = await _context.NhaSanXuat.FindAsync(id);
            _context.NhaSanXuat.Remove(nhaSanXuat);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NhaSanXuatExists(int id)
        {
            return _context.NhaSanXuat.Any(e => e.NhaSanXuatId == id);
        }
    }
}
