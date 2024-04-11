using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using TimeTest.Data;


namespace TimeTest.Models
{
    public class Time
    {
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
        public string UserId { get; set; }

        public Time()
        {
        }

        public Time(string clockIn, string clockOut, DateTimeOffset date, string userId)
        {
            ClockIn = clockIn;
            ClockOut = clockOut;
            Date = date;
            UserId = userId;
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
    }
}
