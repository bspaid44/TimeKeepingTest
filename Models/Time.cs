using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing.Text;
using TimeTest.Data;
using TimeTest.Models.Clients;


namespace TimeTest.Models
{
    public class Time
    {
        private readonly ApplicationDbContext context;
        public int Id { get; set; }

        [Required]
        [RegularExpression("([0-9]?[0-9]):([0-9][0-9])" , ErrorMessage = "Invalid Format: Please use HH:MM in 24 hour time")]
        [StringLength(5)]
        public string ClockIn { get; set; }

        [Required]
        [RegularExpression("([0-9]?[0-9]):([0-9][0-9])", ErrorMessage = "Invalid Format: Please use HH:MM in 24 hour time")]
        [StringLength(5)]
        public string ClockOut { get; set; }

        public DateTimeOffset Date { get; set; }

        [Required]
        public string UserEmail { get; set; }

        public string? TaskNotes { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Time()
        {
        }

        public Time(string clockIn, string clockOut, DateTimeOffset date, string userId, string? taskNotes, int clientId)
        {
            ClockIn = clockIn;
            ClockOut = clockOut;
            Date = date;
            UserEmail = userId;
            TaskNotes = taskNotes;
            ClientId = clientId;
        }

        public double HoursWorked()
        {
            var clockIn = ClockIn.Split(':');
            var clockOut = ClockOut.Split(':');
            var inHour = int.Parse(clockIn[0]);
            var inMinute = int.Parse(clockIn[1]);
            var outHour = int.Parse(clockOut[0]);
            var outMinute = int.Parse(clockOut[1]);
            var inTime = inHour + (inMinute / 60.0);
            var outTime = outHour + (outMinute / 60.0);

            return Math.Round(outTime - inTime, 2);
        }

        public string ClientName()
        {
            var client = context.Clients.FirstOrDefault(c => c.Id == ClientId);
            string clientName = client.Name;
            return clientName;
        }
    }
}
