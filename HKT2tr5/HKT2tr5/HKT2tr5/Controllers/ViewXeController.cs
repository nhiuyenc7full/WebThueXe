using HKT2tr5.Data;
using HKT2tr5.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HKT2tr5.Controllers
{
    public class ViewXeController : Controller
    {
        private readonly AppDbContext _context;

        public ViewXeController(AppDbContext context)
        {
            this._context = context;
        }
        // GET: ViewXe
        public IActionResult Index()
        {
            var indexModelView = new IndexModelView();

            indexModelView.Xes = _context.Xe.Include(x => x.DongXe).ThenInclude(l => l.NhaSanXuat)
                .Include(x => x.LoaiXe).Include(x => x.Tinh).ToList();

            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
            ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh");
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
            ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe");
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");

            return View(indexModelView);

        }

        [ActionName("Index")]
        [HttpPost]
        public IActionResult SearchXes(IndexModelView model)
        {
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
            ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh");
            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
            ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe");
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");

            var indexModelView = new IndexModelView();
            if (model.SearchXeViewModel.SearchAll == true)
            {
                var mau = _context.MauXe.Find(model.SearchXeViewModel.MauXeId).TenMauXe;
                indexModelView.Xes = _context.Xe.Where(x => x.DaThue == false)
                                                .Include(x => x.DongXe).ThenInclude(l => l.NhaSanXuat)
                                                .Include(x => x.LoaiXe).Include(x => x.Tinh).Where(a => a.LoaiXeId == model.SearchXeViewModel.LoaiXeId)
                                                .Include(x => x.DongXe).ThenInclude(l => l.MauDongXe).Where(a => a.DongXeId == model.SearchXeViewModel.DongXeId && a.Mau == mau)
                                                .Where(c => c.TinhId == model.SearchXeViewModel.TinhId).ToList();
            }
            else
            {
                indexModelView.Xes = _context.Xe.Include(x => x.DongXe).ThenInclude(l => l.NhaSanXuat)
                                .Include(x => x.LoaiXe).Include(x => x.Tinh)
                                .Include(x => x.DongXe).ThenInclude(l => l.MauDongXe)
                                .Where(c => c.TinhId == model.SearchXeViewModel.TinhId).ToList();

            }

            return View(indexModelView);
        }
        // GET: ViewXe/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ViewXe/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ViewXe/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewXe/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ViewXe/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ViewXe/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ViewXe/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}