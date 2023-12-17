using OEBB.Comparing;
using OEBB;

public class CalendarComparer
{
    private readonly Dictionary<CalendarCompareMethod, Func<Calendar, Calendar, int>> comparisonMethods;

    public CalendarComparer()
    {
        comparisonMethods = new Dictionary<CalendarCompareMethod, Func<Calendar, Calendar, int>>
        {
            { CalendarCompareMethod.service_id, (x, y) => x.service_id.CompareTo(y.service_id) },
            { CalendarCompareMethod.monday, (x, y) => x.monday.CompareTo(y.monday) },
            { CalendarCompareMethod.tuesday, (x, y) => x.tuesday.CompareTo(y.tuesday) },
            { CalendarCompareMethod.wednesday, (x, y) => x.wednesday.CompareTo(y.wednesday) },
            { CalendarCompareMethod.thursday, (x, y) => x.thursday.CompareTo(y.thursday) },
            { CalendarCompareMethod.friday, (x, y) => x.friday.CompareTo(y.friday) },
            { CalendarCompareMethod.saturday, (x, y) => x.saturday.CompareTo(y.saturday) },
            { CalendarCompareMethod.sunday, (x, y) => x.sunday.CompareTo(y.sunday) },
            { CalendarCompareMethod.start_date, (x, y) => x.start_date.CompareTo(y.start_date) },
            { CalendarCompareMethod.end_date, (x, y) => x.end_date.CompareTo(y.end_date) }
        };
    }

    public ComparisonResult Compare(Calendar[] calendar1, Calendar[] calendar2, CalendarCompareMethod method)
    {
        if (calendar1.Length != calendar2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<Calendar, Calendar, int> comparer = comparisonMethods[method];

        int result = comparer(calendar1[0], calendar2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(Calendar calendar1, Calendar calendar2, CalendarCompareMethod method)
    {
        return Compare(new Calendar[] { calendar1 }, new Calendar[] { calendar2 }, method);
    }
}