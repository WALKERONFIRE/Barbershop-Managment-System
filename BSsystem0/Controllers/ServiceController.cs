using AutoMapper;
using BSsystem0.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSsystem0.Controllers
{
    public class ServiceController : Controller
    {
        private ApplicationDBcontext db;
        ApplicationDBcontext _context;
        IMapper _mapper;
        private readonly IWebHostEnvironment _environment;
        //public ServiceController(ApplicationDBcontext db)
        //{
        //    this.db = db;
        //}
        public ServiceController(ApplicationDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }

        public IActionResult AddService()

        { return View(); }

        // POST: books/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddService(Service service, IFormFile img_file)
        {
            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image"));

            //string path = Path.Combine(_environment.WebRootPath, "Img"); // wwwroot/Img/
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img_file != null && img_file.Length > 0)
            {
                path = Path.Combine(path, img_file.FileName); // for example : /Img/Photoname.png
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img_file.CopyTo(stream);
                    ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                }
                service.Image = img_file.FileName;
            }
            else
            {
                service.Image = "default.jpeg"; // to save the default image path in database.
            }

            try
            {
                _context.Add(service);
                _context.SaveChanges();
                return Redirect("/Admin/ServiceInfo");
            }
            catch (Exception ex)
            {
                ViewBag.exc = ex.Message;
            }

            return View();
        }
        public IActionResult EditService(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Service service = _context.Services.Find(id);

            if (service == null)
            {
                return NotFound();
            }

            return View(service);
        }

        // POST: Admin/EditService/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditService(int id, Service service, IFormFile img_file)
        {
            if (id != service.Id)
            {
                return NotFound();
            }

            string path = Path.GetFullPath(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\image"));

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            if (img_file != null && img_file.Length > 0)
            {
                path = Path.Combine(path, img_file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    img_file.CopyTo(stream);
                    ViewBag.Message = string.Format("<b>{0}</b> uploaded.</br>", img_file.FileName.ToString());
                }
                service.Image = img_file.FileName;
            }

            try
            {
                _context.Update(service);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                ViewBag.exc = ex.Message;
            }

            return Redirect("/Admin/ServiceInfo");
        }
        [HttpGet]
        public IActionResult ServiceList()
        {
            var lst = _context.Services.ToList();

            return View(lst);
        }
        //public IActionResult OneService()
        //{
        //    return View();
        //}
        //[HttpPost]

        //public IActionResult OneService(int id)
        //{

        //    var service = _context.Services.Include(s => s.Reviews).FirstOrDefaultAsync(s => s.Id == id);
        //    if (service == null)
        //    {
        //        return BadRequest("Not Found");
        //    }
        //    //var model = new ServiceDetailViewModel)
        //    //{
        //    //    Service = service
        //    //};
        //    return View(service);

        //}
        public async Task<IActionResult> Details(int id)
        {
            //     var service = await _context.Services
            //.Include(s => s.Reviews)
            ///*.Include(s => s.barbers)*/ // Include associated Barbers
            //.FirstOrDefaultAsync(s => s.Id == id);

            //if (service == null)
            //{

            //    return NotFound();
            //}

            //var model = new AddAppointmentVM();
            ////{
            ////    //Shops = _context.Shops.ToList(),
            ////    //services = _context.Services.ToList(),

            ////};
            var model = new ServiceReviewVM()
            {
                ss = await _context.Services.Include(s => s.Reviews).FirstOrDefaultAsync(s => s.Id == id),
                Reviews = _context.Reviews.Where(x => x.ServiceId == id).ToList()

            };
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> CreateAppointment(int serviceId, DateTime appointmentDate, int barberId)
        {

            // Get the logged-in user


            // Get the selected Service
            var service = await _context.Services.FindAsync(serviceId);

            if (service == null)
            {
                return NotFound();
            }

            // Get the selected Barber
            var barber = await _context.Barbers.FindAsync(barberId);

            if (barber == null)
            {
                return NotFound();
            }

            // Create a new Appointment for the selected Service, User, and Barber
            var appointment = new Appointment
            {
                DateCreated = DateTime.Now,
                ServiceId = service.Id,
                BarberId = barber.Id
            };

            _context.Appointments.Add(appointment);
            await _context.SaveChangesAsync();

            return RedirectToAction("Confirmation", "Service");
        }
        public IActionResult Confirmation(int id)
        {
            var appoint = _context.Appointments.Include(a=>a.Shop).Include(a=>a.Barber).Include(a => a.Service).FirstOrDefault(x => x.Id == id);
            if (appoint == null)
            {
                // Handle the case where the appointment with the given ID is not found
                return NotFound();
            }
            return View(appoint);
        }
        //public ActionResult Details(int id) {

        //    var res = _context.Services.Find(id);
        //    if (res == null)
        //    {
        //        return BadRequest("Not Found");
        //    }

        //    return View(id);
        //}
        public IActionResult AddReview(ServiceReviewVM sr)
        {
            Review review = new Review();

            review.DateCreated = DateTime.Now;
            review.ServiceId = sr.ServiceId;
            review.description = sr.description;

            _context.Reviews.Add(review);
            _context.SaveChanges();
            return RedirectToAction("ServiceList");


        }

        [Authorize]
        public IActionResult Checkout(int shopId ,int id )
        {
            var vm = new AddAppointmentVM()
            {
                ss = _context.Services.Include(s => s.Reviews).FirstOrDefault(s => s.Id == id),
                Shops = _context.Shops.ToList(),
                Barbers = _context.Barbers.Where(x => x.ShopId == shopId).ToList(),


            };
            return View(vm);
        }
        [HttpPost]
        public IActionResult FinalAdd(AddAppointmentVM vm)
        {
            Appointment appointment = new Appointment();
            
            appointment.ServiceId = vm.ServiceId;
            appointment.ShopId = vm.ShopId;
            appointment.BarberId = vm.BarberId;
            appointment.DateCreated = vm.DateCreated;
            _context.Appointments.Add(appointment);
            _context.SaveChanges();
            int appointmentId = appointment.Id;
            return RedirectToAction("Confirmation", new { id = appointmentId });
        }
        public IActionResult GetBarbers(int shopId)
        {
            var barbers = _context.Barbers.Where(x => x.ShopId == shopId).ToList();
            return Ok (barbers);
        }
        //[HttpPost]
        //public IActionResult AddAppointment0()
    }
}
