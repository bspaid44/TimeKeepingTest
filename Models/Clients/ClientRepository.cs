using TimeTest.Data;

namespace TimeTest.Models.Clients
{
    public class ClientRepository : IClientRepository
    {

        private ApplicationDbContext context;

        public ClientRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }
        public IEnumerable<Client> Clients => context.Clients;

        public void DeleteClient(Client client)
        {
            context.Clients.Remove(client);
            context.SaveChanges();
        }

        public void SaveClient(Client client)
        {
            context.Clients.Add(client);
            context.SaveChanges();
        }

        public void UpdateClient(Client client)
        {
            context.Clients.Update(client);
            context.SaveChanges();
        }
    }
}
