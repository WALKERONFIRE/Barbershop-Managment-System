using AutoMapper;
using BSsystem0.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BSsystem0.Controllers
{
    public class AppointmentController : Controller
    {
        private ApplicationDBcontext db;
        ApplicationDBcontext _context;
         IMapper _mapper;

        //public AppointmentController(ApplicationDBcontext db)
        //{
        //    this.db = db;
        //}
        public AppointmentController(ApplicationDBcontext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult add()
        {
            return View();
        }
        [HttpPost]
        public IActionResult add(AppointmentDTO o)
        {
            Appointment appointment = _mapper.Map<Appointment>(o);
            _context.Add(appointment);
            _context.SaveChanges();

            return Ok("Done");

        }
        public IActionResult AppointmentAdd()
        {
            AddAppointmentVM vm = new AddAppointmentVM
            {
                Shops = _context.Shops.ToList(),
            };
            return View(vm);
        }
    }
}
