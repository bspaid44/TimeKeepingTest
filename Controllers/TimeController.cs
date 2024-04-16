using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using NuGet.Protocol;
using System.Threading.Tasks.Dataflow;
using TimeTest.Data;
using TimeTest.Models;
using TimeTest.Models.Clients;
using TimeTest.ViewModels;

namespace TimeTest.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        private readonly ITimeRepository _timeRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ApplicationDbContext _context;

        public TimeController(ITimeRepository timeRepository, IClientRepository clientRepository)
        {
            _timeRepository = timeRepository;
            _clientRepository = clientRepository;
        }

        public IActionResult Index()
        {
            TimeIndexViewModel timeIndexViewModel = new TimeIndexViewModel(_timeRepository.Times.Where(t => t.UserEmail == User.Identity.Name));
            return View(timeIndexViewModel);
        }

        public IActionResult Create()
        {
            CreateTimeViewModel createTimeViewModel = new CreateTimeViewModel(_clientRepository.Clients);
            return View(createTimeViewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            TimeIndexViewModel timeIndexViewModel = new TimeIndexViewModel(_timeRepository.Times);
            return View(timeIndexViewModel);
        }

        public IActionResult Details(int id)
        {
            Time time = _timeRepository.Times.FirstOrDefault(t => t.Id == id);
            return View(time);
        }

        [HttpPost]
        public IActionResult Create(Time time)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == time.ClientId);
            CreateTimeViewModel createTimeViewModel = new CreateTimeViewModel(_clientRepository.Clients);
            if (time.UserEmail != User.Identity.Name)
            {
                return Forbid();
            }
            else if (TryValidateModel(time))
            {         
                client.TimeBlock = client.SubtractTimeWorked(time.HoursWorked());
                _timeRepository.SaveTime(time);
                _clientRepository.UpdateClient(client);
                return RedirectToAction("Index");
            }
            else
            {
                return View(createTimeViewModel);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Time time = _timeRepository.Times.FirstOrDefault(t => t.Id == id);
            return View(time);
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Update(Time time)
        {
            if (TryValidateModel(time))
            {
                _timeRepository.UpdateTime(time);
                return RedirectToAction("List");
            }
            else
            {
                return View(time);
            }
        }

        [Authorize(Roles = "Admin")]
        public RedirectToActionResult Delete(int id)
        {
            Time time = _timeRepository.Times.FirstOrDefault(t => t.Id == id);
            _timeRepository.DeleteTime(time);
            return RedirectToAction("List");
        }

        public IActionResult Export()
        {
            return View();
        }

        [HttpPost]
        public FileResult ExportToCSV([FromForm] string user, [FromForm] DateTime startDate, [FromForm] DateTime endDate)
        {
            var times = _timeRepository.Times.Where(t => t.UserEmail == user && t.Date >= startDate && t.Date <= endDate);
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Clock In, Clock Out, Date, Hours Worked, Notes");
            foreach (var time in times)
            {
                csv.AppendLine($"{time.ClockIn}, {time.ClockOut}, {time.Date}, {time.HoursWorked()}, {time.TaskNotes}");
            }
            return File(System.Text.Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "times.csv");
        }
    }
}
