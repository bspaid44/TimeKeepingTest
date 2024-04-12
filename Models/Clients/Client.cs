namespace TimeTest.Models.Clients
{
    public class Client
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? BillingAddressStreet { get; set; }
        public string? BillingAddressCity { get; set; }
        public string? BillingAddressState { get; set; }
        public string? BillingAddressZip { get; set; }

        public Client()
        {
        }

        public Client(string name, string? billingAddressStreet, string? billingAddressCity, string? billingAddressState, string? billingAddressZip)
        {
            Name = name;
            BillingAddressStreet = billingAddressStreet;
            BillingAddressCity = billingAddressCity;
            BillingAddressState = billingAddressState;
            BillingAddressZip = billingAddressZip;
        }
    }
}
