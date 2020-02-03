using HKT2tr5.Data;
using HKT2tr5.Models;
using HKT2tr5.Models.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HKT2tr5.Controllers
{
    public class BannerJQController : Controller
    {
        private readonly AppDbContext _appDbContext;

        public BannerJQController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IActionResult Index()
        {
            return View();
        }

        public JsonResult Get()
        {
            var bannerJQ = new List<BannerModelView>();
            try
            {
                bannerJQ = (from s in _appDbContext.Banner
                            select new BannerModelView()
                            {
                                BannerId = s.BannerId,
                                Link = s.Link,
                                TenBanner = s.TenBanner
                            }).ToList();
                return new JsonResult(new { response = bannerJQ, status = 1 });
            }
            catch (Exception ex)
            {

                return new JsonResult(new { status = -1, messenger = "Something went wrong, please contact administrator." });
            }
        }
        [HttpPost]
        public IActionResult Save([FromBody] BannerModelView model)
        {
            try
            {
                if (model != null)
                {
                    //create new student
                    if (model.BannerId == 0)
                    {
                        var student = new Banner()
                        {
                            BannerId = model.BannerId,
                            TenBanner = model.TenBanner,
                            Link = model.Link
                        };
                        _appDbContext.Banner.Add(student);
                        var saveResult = _appDbContext.SaveChanges();
                        if (saveResult > 0)
                        {
                            return new JsonResult(new
                            {
                                status = 1,
                                messenge = "Student has been created successfully."
                            });
                        }

                    }
                    else // updated student by StudentId
                    {
                        //var studentDetail = appDbContext.Student.Where(s => s.StudentId == model.StudentId).FirstOrDefault();
                        //studentDetail.FullName = model.FullName;
                        //studentDetail.Sex = model.Sex == 1 ? true : false;
                        //studentDetail.ClassRoomId = model.ClassRoomId;
                        //studentDetail.DOB = model.DOB;
                        //studentDetail.StudentId = model.StudentId;
                        //appDbContext.Student.Update(studentDetail);
                        //var updateResult = appDbContext.SaveChanges();
                        //if (updateResult > 0)
                        //{
                        //    return new JsonResult(new
                        //    {
                        //        status = 1,
                        //        messenge = "Student has been updated successfully."
                        //    });
                        //}
                    }


                }
                return new JsonResult(new
                {
                    status = -1,
                    messenge = "Something went wrong, please contact administrator."
                });
            }
            catch (Exception ex)
            {
                return new JsonResult(new
                {
                    status = -1,
                    messenge = "Something went wrong, please contact administrator."
                });
            }
        }

    }
}