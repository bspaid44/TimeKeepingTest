using Humanizer;

namespace TimeTest.Models
{
    public class TimeRecord
    {
        public int Id { get; set; }
        public int TimeId { get; set; }
        public Time Time { get; set; }
        public ApplicationUser User { get; set; }

        public TimeRecord()
        {
        }

        public TimeRecord(Time time, string userEmail)
        {
            Time = time;
            User = new ApplicationUser();
        }
    }
}
