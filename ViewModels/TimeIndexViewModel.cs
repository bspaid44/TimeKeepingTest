using TimeTest.Models;

namespace TimeTest.ViewModels
{
    public class TimeIndexViewModel
    {
        private ITimeRepository _timeRepository;
        public IEnumerable<Time> Times { get; set; }

        public TimeIndexViewModel(IEnumerable<Time> times)
        {
            Times = times;
        }
    }
}
