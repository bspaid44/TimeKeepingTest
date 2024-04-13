using TimeTest.Models.Clients;

namespace TimeTest.ViewModels
{
    public class ClientIndexViewModel
    {
        private IClientRepository _clientRepository;
        public IEnumerable<Client> Clients { get; set; }

        public ClientIndexViewModel(IEnumerable<Client> clients)
        {
            Clients = clients;
        }
    }
}
