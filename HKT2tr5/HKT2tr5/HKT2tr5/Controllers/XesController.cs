using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using HKT2tr5.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace HKT2tr5.Controllers
{
    public class XesController : Controller
    {
        private readonly AppDbContext _context;
        private readonly IHostingEnvironment _hostingEnvironment;

        public XesController(AppDbContext context, IHostingEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        // GET: Xes
        public async Task<IActionResult> Index()
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
                indexModelView.Xes = _context.Xe.Include(x => x.DongXe).ThenInclude(l => l.NhaSanXuat)
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

        // GET: Xes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.DongXe).ThenInclude(l => l.NhaSanXuat)
                .Include(x => x.LoaiXe)
                .Include(x => x.Tinh)
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        public IActionResult GetDongXes(int id)
        {
            try
            {
                var dongXe = (from dx in _context.DongXe
                              where dx.NhaSanXuatId == id
                              select new DongXeItem()
                              {
                                  Id = dx.DongXeId,
                                  Name = dx.TenDongXe
                              }).ToList();

                return new JsonResult(new { status = 1, response = dongXe });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = -1, message = "Something went wrong." });
            }
        }

        public IActionResult GetMauXes(int id)
        {
            try
            {
                var mauXe = (from mx in _context.MauXe
                             join mdx in _context.MauDongXe on mx.MauXeId equals mdx.MauXeId
                             where mdx.DongXeId == id
                             select new MauXeItem()
                             {
                                 Id = mx.MauXeId,
                                 TenMau = mx.TenMauXe
                             }).ToList().OrderBy(c => c.TenMau);

                return new JsonResult(new { status = 1, response = mauXe });
            }
            catch (Exception ex)
            {
                return new JsonResult(new { status = -1, message = "Something went wrong." });
            }
        }

        // GET: Xes/Create
        public IActionResult Create(string userEmail)
        {
            if (userEmail != null)
            {
                ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
                ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");
                ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe");
                ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
                ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh");
                return View();
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        // POST: Xes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(XeViewModel model, string userEmail)
        {
            if (userEmail != null)
            {
                string uniqueFileName = ProcessUpLoadedFile(model);
                string uniqueFileName1 = ProcessUpLoadedFile1(model);
                string uniqueFileName2 = ProcessUpLoadedFile2(model);
                string uniqueFileName3 = ProcessUpLoadedFile3(model);
                var check = IsValid(uniqueFileName);
                var check1 = IsValid(uniqueFileName1);
                var check2 = IsValid(uniqueFileName2);
                var check3 = IsValid(uniqueFileName3);

                if (!check && !check1 && !check2 && !check3)
                {
                    ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe", model.DongXeId);
                    ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX", model.DongXeId);
                    ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe", model.LoaiXeId);
                    ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe", model.MauXeId);
                    ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh", model.TinhId);
                    return View(model);
                }

                //var xe = new Xe();
                if (ModelState.IsValid)
                {
                    Xe xe = new Xe
                    {
                        TinhId = model.TinhId,
                        Mau = _context.MauXe.FirstOrDefault(c => c.MauXeId == model.MauXeId).TenMauXe,
                        LoaiXeId = model.LoaiXeId,
                        GiaTheoGio = model.GiaTheoGio,
                        GiaTheoNgay = model.GiaTheoNgay,
                        DangKinhDoanh = model.DangKinhDoanh,
                        DongXeId = model.DongXeId,
                        Tittle = model.Tittle,
                        ImageDauXe = uniqueFileName,
                        ImageDuoiXe = uniqueFileName,
                        ImageNoiThatXe = uniqueFileName,
                        ImageThanXe = uniqueFileName,
                        NamSx = model.NamSx
                    };

                    _context.Add(xe);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index", "ViewXe");
                }
                ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe", model.DongXeId);
                ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX", model.DongXeId);
                ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe", model.LoaiXeId);
                ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe", model.MauXeId);
                ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh", model.TinhId);
                return View(model);
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        public bool IsValid(string value)
        {
            string[] strings = value.ToString().Split('.');
            return strings[1].ToUpper() == "JPG" || strings[1].ToUpper() == "PNG";
        }

        private string ProcessUpLoadedFile(XeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Tittle != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageDauXe.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ImageDauXe.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private string ProcessUpLoadedFile1(XeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Tittle != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageDuoiXe.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ImageDuoiXe.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private string ProcessUpLoadedFile2(XeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Tittle != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageNoiThatXe.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ImageNoiThatXe.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }
        private string ProcessUpLoadedFile3(XeViewModel model)
        {
            string uniqueFileName = null;
            if (model.Tittle != null)
            {
                string uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "images");
                uniqueFileName = Guid.NewGuid().ToString() + "_" + model.ImageThanXe.FileName;
                string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                model.ImageThanXe.CopyTo(new FileStream(filePath, FileMode.Create));
            }
            return uniqueFileName;
        }

        // GET: Xes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(a => a.DongXe)
                .Include(b => b.Tinh)
                .Include(c => c.LoaiXe)
                .FirstOrDefaultAsync(d => d.XeId == id);
            if (xe == null)
            {
                return NotFound();
            }

            var editXe = new EditXeViewModel
            {
                XeId = xe.XeId,
                NamSx = xe.NamSx,
                ImageDauXe1 = xe.ImageDauXe,
                ImageNoiThatXe1 = xe.ImageNoiThatXe,
                ImageThanXe1 = xe.ImageThanXe,
                ImageDuoiXe1 = xe.ImageDuoiXe,
                DaThue = xe.DaThue,
                DangKinhDoanh = xe.DangKinhDoanh,
                MauXeId = _context.MauXe.FirstOrDefault(m => m.TenMauXe == xe.Mau).MauXeId,
                LoaiXeId = xe.LoaiXeId,
                TinhId = xe.TinhId,
                Tittle = xe.Tittle,
                DongXeId = xe.DongXeId,
                GiaTheoGio = xe.GiaTheoGio,
                GiaTheoNgay = xe.GiaTheoNgay
            };

            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");
            ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe");
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
            ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh");
            
            return View(editXe);
        }

        // POST: Xes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditXeViewModel model)
        {
            if (ModelState.IsValid)
            {
                Xe editXe = _context.Xe.Find(model.XeId);
                editXe.Tittle = model.Tittle;
                editXe.TinhId = model.TinhId;
                editXe.Rate = model.Rate;
                editXe.NamSx = model.NamSx;
                editXe.LoaiXeId = model.LoaiXeId;
                editXe.GiaTheoNgay = model.GiaTheoNgay;
                editXe.GiaTheoGio = model.GiaTheoGio;
                editXe.DaThue = model.DaThue;
                editXe.DangKinhDoanh = model.DangKinhDoanh;

                if (model.DongXeId != 0)
                {
                    editXe.DongXeId = model.DongXeId;
                }
                if (model.MauXeId != 0)
                {
                    editXe.Mau = _context.MauXe.Find(model.MauXeId).TenMauXe;
                }

                if (model.ImageDauXe != null)
                {
                    if (model.ImageDauXe1 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ImageDauXe1);
                        System.IO.File.Delete(filePath);
                    }
                    editXe.ImageDauXe = ProcessUpLoadedFile(model);
                }

                if (model.ImageDuoiXe != null)
                {
                    if (model.ImageDuoiXe1 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ImageDuoiXe1);
                        System.IO.File.Delete(filePath);
                    }
                    editXe.ImageDuoiXe = ProcessUpLoadedFile1(model);
                }
                if (model.ImageNoiThatXe != null)
                {
                    if (model.ImageNoiThatXe1 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ImageNoiThatXe1);
                        System.IO.File.Delete(filePath);
                    }
                    editXe.ImageNoiThatXe = ProcessUpLoadedFile2(model);
                }
                if (model.ImageThanXe != null)
                {
                    if (model.ImageThanXe1 != null)
                    {
                        string filePath = Path.Combine(_hostingEnvironment.WebRootPath, "images", model.ImageThanXe1);
                        System.IO.File.Delete(filePath);
                    }
                    editXe.ImageThanXe = ProcessUpLoadedFile3(model);
                }



                _context.Update(editXe);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["DongXeId"] = new SelectList(_context.DongXe, "DongXeId", "TenDongXe");
            ViewData["NhaSanXuatId"] = new SelectList(_context.NhaSanXuat, "NhaSanXuatId", "TenNSX");
            ViewData["LoaiXeId"] = new SelectList(_context.LoaiXe, "LoaiXeId", "TenLoaiXe");
            ViewData["MauXeId"] = new SelectList(_context.MauXe, "MauXeId", "TenMauXe");
            ViewData["TinhId"] = new SelectList(_context.Tinh, "TinhId", "TenTinh");
            return View(model);

        }

        // GET: Xes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var xe = await _context.Xe
                .Include(x => x.DongXe)
                .Include(x => x.LoaiXe)
                .Include(x => x.Tinh)
                .FirstOrDefaultAsync(m => m.XeId == id);
            if (xe == null)
            {
                return NotFound();
            }

            return View(xe);
        }

        // POST: Xes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var xe = await _context.Xe.FindAsync(id);
            _context.Xe.Remove(xe);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool XeExists(int id)
        {
            return _context.Xe.Any(e => e.XeId == id);
        }
    }
}
