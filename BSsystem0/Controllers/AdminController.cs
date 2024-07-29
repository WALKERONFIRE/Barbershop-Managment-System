using AutoMapper;
using BSsystem0.Data;
using BSsystem0.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BSsystem0.Controllers
{
    public class AdminController : Controller
    {

        //private readonly UserManager<ApplicationUser> ApplicationDBcontext db;
        ApplicationDbContext _context1;
        ApplicationDBcontext _context;
        IMapper _mapper;


        //public AdminController(ApplicationDBcontext db)
        //{
        //    this.db = db;
        //}
        public AdminController(ApplicationDBcontext context , ApplicationDbContext context1 , IMapper mapper)
        {
            _context = context;
            _context1 = context1;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            //var a = _context1.Users.FirstOrDefault();

            return View("Login");
        }

        public IActionResult SignUp()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(string username , string password)
        {
            var lg = _context.Shops.Where(s => s.username == username && s.password == password).FirstOrDefault();
            if (lg == null)
            {
                return Unauthorized("Incorrect Email or Password");
            }
            return Redirect("/Admin/Dashboard");
        }
        [HttpGet]
        public IActionResult AppointmentInfo()
        {
            var lst = _context.Appointments.Include(a => a.Service).Include(a => a.Shop).Include(a => a.Barber).ToList();

            return View(lst);

        }
        public IActionResult AppointmentAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AppointmentAdd(AppointmentDTO o)
        {
            var appointment = _mapper.Map<Appointment>(o);
            _context.Add(appointment);
            _context.SaveChanges();
            return Redirect("AppointmentInfo");
        }
        public ActionResult AppointmentDelete()
        {
            return View();

        }
        [HttpDelete]
        public ActionResult AppointmentDelete(int id)
        {
            var res = _context.Appointments.Find(id);
            if (res == null)
            {
                return BadRequest("Not Found");
            }
            _context.Remove(res);
            _context.SaveChanges();
            return RedirectToAction("AppointmentInfo");

        }

        public ActionResult ADelete(int id)
        {
            var res = _context.Appointments.Find(id);

            _context.Remove(res);
            _context.SaveChanges();
            return Redirect("/Admin/UserInfo");
        }

        [HttpGet]
        public IActionResult UserInfo()
        {
            var lst = _context.Users.ToList();
            
            return View(lst);
        }
        public IActionResult UserAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult UserAdd(UserRegister ur)
        {
            User us = _mapper.Map<User>(ur);
            _context.Add(us);
            _context.SaveChanges();
            return Redirect("UserInfo");
        }
        //public IActionResult UserEdit(string? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //   User user = _context.Users.Where(x=>x.Id==id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(user);
        //}
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UserEdit(int id, User userRegister)
        {
            if (id != userRegister.Id)
            {
                return NotFound();
            }

           
                try
                {
                    var user = _mapper.Map<User>(userRegister);
                    _context.Update(user);
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    ViewBag.exc = ex.Message;
                }

            return Redirect("/Admin/UserInfo");
        }

        private bool UserExists(string id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public ActionResult UserDelete()
        {
            return View();

        }
        [HttpDelete]
        public ActionResult UserDelete(int id)
        {
            var res = _context.Users.Find(id);
            if (res == null)
            {
                return BadRequest("Not Found");
            }
            _context.Remove(res);
            _context.SaveChanges();
            return Redirect("/Admin/UserInfo");
        }
      
        public ActionResult UDelete(int id)
        {
            var res = _context.Users.Find(id);

            _context.Remove(res);
            _context.SaveChanges();
            return Redirect("/Admin/UserInfo");
        }
        [HttpGet]
        public IActionResult ServiceInfo()
        {
            var lst = _context.Services.ToList();

            return View(lst);
        }
        public IActionResult ServiceAdd()
        {
            return View();
        }
        [HttpPost]
        public IActionResult ServiceAdd(ServiceAdder sa)
        {
            Service s = _mapper.Map<Service>(sa);
            _context.Add(s);
            _context.SaveChanges();
            return Redirect("ServiceInfo");
        }

        public IActionResult Details(int id)
        {
            var std = _context.Services.Where(x => x.Id == id).FirstOrDefault();
            return View(std);
        }

        public IActionResult ServiceDelete()
        {
            return View();

        }
        [HttpDelete]
        public IActionResult ServiceDelete(int id)
        {
            var res = _context.Services.Find(id);
            if (res == null)
            {
                return BadRequest("Not Found");
            }
            _context.Remove(res);
            _context.SaveChanges();
            return Redirect("ServiceInfo");
        }
        public IActionResult SDelete(int id)
        {

            var res = _context.Services.Find(id);
           
            _context.Remove(res);
            _context.SaveChanges();
            return Redirect("/Admin/ServiceInfo");
        }
        //public IActionResult SEdit()
        //{
        //    return View();
        //}
        //[HttpPut]
        //public IActionResult SEdit(int id , string name ,string description , int price, int priceafteroffer)
        //{
        //    var res = _context.Services.Find(id);
        //    res.Name = name;
        //    res.Description = description;
        //    res.Price = price;
        //    res.PriceAfterOffer = priceafteroffer;
        //    _context.Update(res);
        //    _context.SaveChanges();
        //    return Redirect("ServiceInfo");
        //}
        public IActionResult Dashboard()
        {
            var viewModel = new DashboardViewModel
            {
                UserCount = _context1.Users.Count(),
                AppointmentCount = _context.Appointments.Count(),
                BarberCount = _context.Barbers.Count(),
                ServiceCount = _context.Services.Count(),
            };

            return View(viewModel);
        }

       

    }
}
