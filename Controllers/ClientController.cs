using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TimeTest.Models.Clients;
using TimeTest.ViewModels;

namespace TimeTest.Controllers
{
    [Authorize]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }
        public IActionResult Index()
        {
            ClientIndexViewModel clientIndexViewModel = new ClientIndexViewModel(_clientRepository.Clients);
            return View(clientIndexViewModel);
        }
    }
}
