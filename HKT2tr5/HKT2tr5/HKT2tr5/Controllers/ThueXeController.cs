using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HKT2tr5.Data;
using HKT2tr5.Models.Entities;
using MailKit.Net.Smtp;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MimeKit;

namespace HKT2tr5.Controllers
{
    public class ThueXeController : Controller
    {
        private readonly AppDbContext _appDbContext;

        private readonly UserManager<ApplicationUser> userManger;

        public ThueXeController(AppDbContext appDbContext, UserManager<ApplicationUser> userManger)
        {
            _appDbContext = appDbContext;

            this.userManger = userManger;
        }
        //public IActionResult Index(int id)
        //{
        //    var thuexe = _appDbContext.Xe.FirstOrDefault(x => x.XeId == id);
        //    thuexe.DaThue = true;
        //    thuexe.Rate = thuexe.Rate + 1;
        //    _appDbContext.SaveChanges();
        //    return RedirectToAction("Index", "ViewXe");
        //}
        public IActionResult Index(int id, string userEmail)
        {
            if (userEmail != null)
            {

                var thuexe = _appDbContext.Xe
                    .Include(x => x.LoaiXe)
                    .FirstOrDefault(x => x.XeId == id);
                var user = userManger.FindByEmailAsync(userEmail);

                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Web Thuê xe", "nhiuyenphan111@gmail.com"));
                message.To.Add(new MailboxAddress("Tên khách hàng", user.Result.Email));
                message.Subject = "Xác nhận đặt hàng";
                message.Body = new TextPart("plain")
                {
                    Text = "Đã nhận được yêu cầu thuê xe " + thuexe.LoaiXe.TenLoaiXe + ". Quá trình cho thuê xe đang được xử lý, sẽ có nhân viên gọi tư vấn dịch vụ."
                };
                using (var client = new SmtpClient())
                {
                    client.Connect("smtp.gmail.com", 587, false);
                    client.Authenticate("nhiuyenphan111@gmail.com", "gianhuembiet1005lahuuhan");
                    client.Send(message);
                    client.Disconnect(true);
                }
                thuexe.DaThue = true;
                thuexe.Rate = thuexe.Rate + 1;
                _appDbContext.SaveChanges();
                return RedirectToAction("Index", "ViewXe");
            }
            else
            {

                return RedirectToAction("Login", "Account");
            }
        }
    }
}