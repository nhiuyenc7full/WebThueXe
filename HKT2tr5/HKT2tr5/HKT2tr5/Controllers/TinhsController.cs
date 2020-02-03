using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace HKT2tr5.Controllers
{
    public class TinhsController : Controller
    {
        private readonly AppDbContext _context;

        public TinhsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Tinhs
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tinh.ToListAsync());
        }

        // GET: Tinhs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh
                .FirstOrDefaultAsync(m => m.TinhId == id);
            if (tinh == null)
            {
                return NotFound();
            }

            return View(tinh);
        }

        // GET: Tinhs/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tinhs/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("TinhId,TenTinh")] Tinh tinh)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tinh);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tinh);
        }

        // GET: Tinhs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh.FindAsync(id);
            if (tinh == null)
            {
                return NotFound();
            }
            return View(tinh);
        }

        // POST: Tinhs/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("TinhId,TenTinh")] Tinh tinh)
        {
            if (id != tinh.TinhId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tinh);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TinhExists(tinh.TinhId))
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
            return View(tinh);
        }

        // GET: Tinhs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tinh = await _context.Tinh
                .FirstOrDefaultAsync(m => m.TinhId == id);
            if (tinh == null)
            {
                return NotFound();
            }

            return View(tinh);
        }

        // POST: Tinhs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tinh = await _context.Tinh.FindAsync(id);
            _context.Tinh.Remove(tinh);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TinhExists(int id)
        {
            return _context.Tinh.Any(e => e.TinhId == id);
        }
    }
}
