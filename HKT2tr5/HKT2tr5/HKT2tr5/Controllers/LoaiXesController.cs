using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class LoaiXesController : Controller
    {
        private readonly AppDbContext _context;

        public LoaiXesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: LoaiXes
        public async Task<IActionResult> Index()
        {
            return View(await _context.LoaiXe.ToListAsync());
        }

        // GET: LoaiXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXe
                .FirstOrDefaultAsync(m => m.LoaiXeId == id);
            if (loaiXe == null)
            {
                return NotFound();
            }

            return View(loaiXe);
        }

        // GET: LoaiXes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: LoaiXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("LoaiXeId,TenLoaiXe")] LoaiXe loaiXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(loaiXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(loaiXe);
        }

        // GET: LoaiXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXe.FindAsync(id);
            if (loaiXe == null)
            {
                return NotFound();
            }
            return View(loaiXe);
        }

        // POST: LoaiXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("LoaiXeId,TenLoaiXe")] LoaiXe loaiXe)
        {
            if (id != loaiXe.LoaiXeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(loaiXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LoaiXeExists(loaiXe.LoaiXeId))
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
            return View(loaiXe);
        }

        // GET: LoaiXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var loaiXe = await _context.LoaiXe
                .FirstOrDefaultAsync(m => m.LoaiXeId == id);
            if (loaiXe == null)
            {
                return NotFound();
            }

            return View(loaiXe);
        }

        // POST: LoaiXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var loaiXe = await _context.LoaiXe.FindAsync(id);
            _context.LoaiXe.Remove(loaiXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LoaiXeExists(int id)
        {
            return _context.LoaiXe.Any(e => e.LoaiXeId == id);
        }
    }
}
