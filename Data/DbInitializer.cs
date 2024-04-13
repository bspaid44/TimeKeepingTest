using TimeTest.Models.Clients;

namespace TimeTest.Data
{
    public static class DbInitializer
    {
        public static void Initialize(IApplicationBuilder applicationBuilder)
        {
            ApplicationDbContext context = applicationBuilder.ApplicationServices.CreateScope().ServiceProvider.GetRequiredService<ApplicationDbContext>();
            context.Database.EnsureCreated();

            if (context.Clients.Any())
            {
                return;
            }

            var clients = new Client[]
            {
                new Client { Name = "Real Industries", BillingAddressStreet = "1234 Main St", BillingAddressCity = "Anytown", BillingAddressState = "CA", BillingAddressZip = "12345", Email = "real@industry.com", Phone = "518-222-2222" },
                new Client { Name = "Faux Corp", BillingAddressStreet = "5678 Elm St", BillingAddressCity = "Othertown", BillingAddressState = "CA", BillingAddressZip = "54321", Email = "faux@corp.com", Phone = "111-222-3333" }
            };

            foreach (Client c in clients)
            {
                context.Clients.Add(c);
            }

            context.SaveChanges();
        }
    }
}
