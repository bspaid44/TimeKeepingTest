using TimeTest.Data;
using TimeTest.Models.Clients;

namespace TimeTest.Models
{
    public class TimeRepository : ITimeRepository
    {
        private ApplicationDbContext context;

        public TimeRepository(ApplicationDbContext ctx)
        {
            context = ctx;
        }

        public IEnumerable<Time> Times => context.Times;

        public void SaveTime(Time time)
        {
            context.Times.Add(time);
            context.SaveChanges();
        }

        public void UpdateTime(Time time)
        {
            time = context.Times.Update(time).Entity;
            context.SaveChanges();
        }

        public void DeleteTime(Time time)
        {
            context.Times.Remove(time);
            context.SaveChanges();
        }

        public double TimeWorkedOnDate(DateTime Date)
        {
            throw new NotImplementedException();

        }

    }
}
