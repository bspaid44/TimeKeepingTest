using System.ComponentModel.DataAnnotations;

namespace TimeTest.Models.Clients
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string? BillingAddressStreet { get; set; }

        [Required]
        public string? BillingAddressCity { get; set; }

        [Required]
        public string? BillingAddressState { get; set; }

        [Required]
        [StringLength(5, MinimumLength = 5)]
        public string? BillingAddressZip { get; set; }

        [DataType(DataType.EmailAddress)]
        public string? Email { get; set; }

        [DataType(DataType.PhoneNumber)]
        [StringLength(12, MinimumLength = 12, ErrorMessage = "Not a valid phone number, please format as 111-111-1111")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number, please format as 111-111-1111")]
        public string? Phone { get; set; }
        public double TimeBlock { get; set; } = 0;

        public Client()
        {
        }

        public Client(string name, string? billingAddressStreet, string? billingAddressCity, string? billingAddressState, string? billingAddressZip, string? email, string? phone)
        {
            Name = name;
            BillingAddressStreet = billingAddressStreet;
            BillingAddressCity = billingAddressCity;
            BillingAddressState = billingAddressState;
            BillingAddressZip = billingAddressZip;
            Email = email;
            Phone = phone;
        }

        public string GetBillingAddress()
        {
            return $"{BillingAddressStreet}, {BillingAddressCity}, {BillingAddressState} {BillingAddressZip}";
        }

        public double AddTimeBlock(double timeBlock)
        {
            TimeBlock += timeBlock;
            return (double)TimeBlock;
        }
    }
}
