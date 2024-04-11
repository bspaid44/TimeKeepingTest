namespace TimeTest.Models
{
    public interface ITimeRepository
    {
        IEnumerable<Time> Times { get; }
        void SaveTime(Time time);
        void UpdateTime(Time time);
        void DeleteTime(Time time);
        double TimeWorkedOnDate(DateTime Date);
    }
}
