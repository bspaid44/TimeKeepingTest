using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TimeTest.Models;
using TimeTest.ViewModels;

namespace TimeTest.Controllers
{
    [Authorize]
    public class TimeController : Controller
    {
        private readonly ITimeRepository _timeRepository;

        public TimeController(ITimeRepository timeRepository)
        {
            _timeRepository = timeRepository;
        }

        public IActionResult Index()
        {
            TimeIndexViewModel timeIndexViewModel = new TimeIndexViewModel(_timeRepository.Times.Where(t => t.UserId == User.Identity.Name));
            return View(timeIndexViewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public IActionResult List()
        {
            TimeIndexViewModel timeIndexViewModel = new TimeIndexViewModel(_timeRepository.Times);
            return View(timeIndexViewModel);
        }

        [HttpPost]
        public IActionResult Create(Time time)
        {
            if (time.UserId != User.Identity.Name)
            {
                return Forbid();
            }
            else if (TryValidateModel(time))
            {
                _timeRepository.SaveTime(time);
                return RedirectToAction("Index");
            }
            else
            {
                return View(time);
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
            var times = _timeRepository.Times.Where(t => t.UserId == user && t.Date >= startDate && t.Date <= endDate);
            var csv = new System.Text.StringBuilder();
            csv.AppendLine("Clock In, Clock Out, Date, Hours Worked");
            foreach (var time in times)
            {
                csv.AppendLine($"{time.ClockIn}, {time.ClockOut}, {time.Date}, {time.HoursWorked()}");
            }
            return File(System.Text.Encoding.UTF8.GetBytes(csv.ToString()), "text/csv", "times.csv");
        }
    }
}
