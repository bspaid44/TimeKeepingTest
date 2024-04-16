using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTest.Data;
using TimeTest.Models;
using TimeTest.Models.Clients;
using TimeTest.ViewModels;

namespace TimeTest.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;
        private readonly ITimeRepository _timeRepository;

        public ClientController(IClientRepository clientRepository, ITimeRepository timeRepository)
        {
            _clientRepository = clientRepository;
            _timeRepository = timeRepository;
        }
        public IActionResult Index()
        {
            ClientIndexViewModel clientIndexViewModel = new ClientIndexViewModel(_clientRepository.Clients);
            return View(clientIndexViewModel);
        }

        public IActionResult Details(int id)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == id);
            return View(client);
        }

        public IActionResult TimeDetails(int id)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == id);
            IEnumerable<Time> times = _timeRepository.Times.Where(t => t.ClientId == id);
            ClientTimeViewModel clientTimeViewModel = new ClientTimeViewModel(client, times);
            return View(clientTimeViewModel);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult AddTime([FromForm] double TimeBlock, int id)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == id);
            client.TimeBlock = client.AddTimeBlock(TimeBlock);
            _clientRepository.UpdateClient(client);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Client client)
        {
            if (TryValidateModel(client))
            {
                _clientRepository.SaveClient(client);
                return RedirectToAction("Index");
            }
            else
            {
                return View(client);
            }
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(int id)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == id);
            return View(client);
        }

        public IActionResult Update(Client client)
        {
            if (TryValidateModel(client))
            {
                _clientRepository.UpdateClient(client);
                return RedirectToAction("Index");
            }
            else
            {
                return View(client);
            }
        }

        [Authorize(Roles = "Admin")]
        public RedirectToActionResult Delete(int id)
        {
            Client client = _clientRepository.Clients.FirstOrDefault(c => c.Id == id);
            _clientRepository.DeleteClient(client);
            return RedirectToAction("Index");
        }
    }
}
