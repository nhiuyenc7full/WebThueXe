using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class MauDongXesController : Controller
    {
        private readonly AppDbContext _context;

        public MauDongXesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MauDongXes
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.MauDongXe.Include(m => m.DongXe).Include(m => m.MauXe);
            return View(await appDbContext.ToListAsync());
        }

        // GET: MauDongXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauDongXe = await _context.MauDongXe
                .Include(m => m.DongXe)
                .Include(m => m.MauXe)
                .FirstOrDefaultAsync(m => m.MauDongXeId == id);
            if (mauDongXe == null)
            {
                return NotFound();
            }

            return View(mauDongXe);
        }

        // GET: MauDongXes/Create
        public IActionResult Create()
        {
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
            return View();
        }

        // POST: MauDongXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MauDongXeId,MauXeId,DongXeId")] MauDongXe mauDongXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mauDongXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe", mauDongXe.DongXeId);
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe", mauDongXe.MauXeId);
            return View(mauDongXe);
        }

        // GET: MauDongXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauDongXe = await _context.MauDongXe.FindAsync(id);
            if (mauDongXe == null)
            {
                return NotFound();
            }
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe", mauDongXe.DongXeId);
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe", mauDongXe.MauXeId);
            return View(mauDongXe);
        }

        // POST: MauDongXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MauDongXeId,MauXeId,DongXeId")] MauDongXe mauDongXe)
        {
            if (id != mauDongXe.MauDongXeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mauDongXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MauDongXeExists(mauDongXe.MauDongXeId))
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
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe", mauDongXe.DongXeId);
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe", mauDongXe.MauXeId);
            return View(mauDongXe);
        }

        // GET: MauDongXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauDongXe = await _context.MauDongXe
                .Include(m => m.DongXe)
                .Include(m => m.MauXe)
                .FirstOrDefaultAsync(m => m.MauDongXeId == id);
            if (mauDongXe == null)
            {
                return NotFound();
            }

            return View(mauDongXe);
        }

        // POST: MauDongXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mauDongXe = await _context.MauDongXe.FindAsync(id);
            _context.MauDongXe.Remove(mauDongXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MauDongXeExists(int id)
        {
            return _context.MauDongXe.Any(e => e.MauDongXeId == id);
        }
    }
}
