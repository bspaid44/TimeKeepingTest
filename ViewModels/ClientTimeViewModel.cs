using TimeTest.Models;
using TimeTest.Models.Clients;

namespace TimeTest.ViewModels
{
    public class ClientTimeViewModel
    {
        private ITimeRepository _timeRepository;
        private IClientRepository _clientRepository;

        public Client Client { get; set; }
        public IEnumerable<Time> Times { get; set; }

        public ClientTimeViewModel(Client client, IEnumerable<Time> times)
        {
            Client = client;
            Times = times;
        }
    }
}
