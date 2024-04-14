using TimeTest.Models;
using TimeTest.Models.Clients;

namespace TimeTest.ViewModels
{
    public class CreateTimeViewModel
    {
        private ITimeRepository _timeRepository;
        private IClientRepository _clientRepository;

        public IEnumerable<Client> Clients { get; set; }
        public Time Time { get; set; }

        public CreateTimeViewModel(IEnumerable<Client> clients)
        {
            Clients = clients;
        }

    }
}
