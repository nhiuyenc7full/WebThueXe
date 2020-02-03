using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class DongXesController : Controller
    {
        private readonly AppDbContext _context;

        public DongXesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: DongXes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.DongXe.Include(d => d.NhaSanXuat);
            return View(await appDbContext.ToListAsync());
        }

        // GET: DongXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dongXe = await _context.DongXe
                .Include(d => d.NhaSanXuat)
                .FirstOrDefaultAsync(m => m.DongXeId == id);
            if (dongXe == null)
            {
                return NotFound();
            }

            return View(dongXe);
        }

        // GET: DongXes/Create
        public IActionResult Create()
        {
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");
            return View();
        }

        // POST: DongXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create( DongXe dongXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(dongXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX", dongXe.NhaSanXuatId);
            return View(dongXe);
        }

        // GET: DongXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dongXe = await _context.DongXe.FindAsync(id);
            if (dongXe == null)
            {
                return NotFound();
            }
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX", dongXe.NhaSanXuatId);
            return View(dongXe);
        }

        // POST: DongXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, DongXe dongXe)
        {
            if (id != dongXe.DongXeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(dongXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DongXeExists(dongXe.DongXeId))
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
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX", dongXe.NhaSanXuatId);
            return View(dongXe);
        }

        // GET: DongXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var dongXe = await _context.DongXe
                .Include(d => d.NhaSanXuat)
                .FirstOrDefaultAsync(m => m.DongXeId == id);
            if (dongXe == null)
            {
                return NotFound();
            }

            return View(dongXe);
        }

        // POST: DongXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var dongXe = await _context.DongXe.FindAsync(id);
            _context.DongXe.Remove(dongXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DongXeExists(int id)
        {
            return _context.DongXe.Any(e => e.DongXeId == id);
        }
    }
}
