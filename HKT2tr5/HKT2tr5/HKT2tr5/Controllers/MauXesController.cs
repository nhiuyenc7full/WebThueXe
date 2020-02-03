using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class MauXesController : Controller
    {
        private readonly AppDbContext _context;

        public MauXesController(AppDbContext context)
        {
            _context = context;
        }

        // GET: MauXes
        public async Task<IActionResult> Index()
        {
            return View(await _context.MauXe.ToListAsync());
        }

        // GET: MauXes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauXe = await _context.MauXe
                .FirstOrDefaultAsync(m => m.MauXeId == id);
            if (mauXe == null)
            {
                return NotFound();
            }

            return View(mauXe);
        }

        // GET: MauXes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: MauXes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("MauXeId,TenMauXe")] MauXe mauXe)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mauXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(mauXe);
        }

        // GET: MauXes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauXe = await _context.MauXe.FindAsync(id);
            if (mauXe == null)
            {
                return NotFound();
            }
            return View(mauXe);
        }

        // POST: MauXes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MauXeId,TenMauXe")] MauXe mauXe)
        {
            if (id != mauXe.MauXeId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(mauXe);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MauXeExists(mauXe.MauXeId))
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
            return View(mauXe);
        }

        // GET: MauXes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var mauXe = await _context.MauXe
                .FirstOrDefaultAsync(m => m.MauXeId == id);
            if (mauXe == null)
            {
                return NotFound();
            }

            return View(mauXe);
        }

        // POST: MauXes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var mauXe = await _context.MauXe.FindAsync(id);
            _context.MauXe.Remove(mauXe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MauXeExists(int id)
        {
            return _context.MauXe.Any(e => e.MauXeId == id);
        }
    }
}
