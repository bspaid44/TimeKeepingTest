namespace TimeTest.Models.Clients
{
    public interface IClientRepository
    {
        IEnumerable<Client> Clients { get; }
        void SaveClient(Client client);
        void UpdateClient(Client client);
        void DeleteClient(Client client);
    }
}
