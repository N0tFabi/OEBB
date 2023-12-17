using OEBB;

namespace OEBB.Comparing;

public class CalendarDateComparer
{
    private readonly Dictionary<CalendarDateCompareMethod, Func<CalendarDate, CalendarDate, int>> comparisonMethods;

    public CalendarDateComparer()
    {
        comparisonMethods = new Dictionary<CalendarDateCompareMethod, Func<CalendarDate, CalendarDate, int>>
        {
            { CalendarDateCompareMethod.service_id, (x, y) => x.service_id.CompareTo(y.service_id) },
            { CalendarDateCompareMethod.date, (x, y) => x.date.CompareTo(y.date) },
            { CalendarDateCompareMethod.exception_type, (x, y) => x.exception_type.CompareTo(y.exception_type) }
        };
    }

    public ComparisonResult Compare(CalendarDate[] calendardDate1, CalendarDate[] calendarDate2, CalendarDateCompareMethod method)
    {
        if (calendardDate1.Length != calendarDate2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<CalendarDate, CalendarDate, int> comparer = comparisonMethods[method];

        int result = comparer(calendardDate1[0], calendarDate2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(CalendarDate calendar1, CalendarDate calendar2, CalendarDateCompareMethod method)
    {
        return Compare(new CalendarDate[] { calendar1 }, new CalendarDate[] { calendar2 }, method);
    }
}
