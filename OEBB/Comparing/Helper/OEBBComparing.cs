namespace OEBB.Comparing;

public class OEBBComparing
{
    internal static ComparisonResult GetComparisonResult(Calendar[] calendar1, Calendar[] calendar2, Func<Calendar, object> propertySelector)
    {
        return calendar1.SequenceEqual(calendar2, new PropertyComparer<Calendar>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(CalendarDate[] calendarDate1, CalendarDate[] calendarDate2, Func<CalendarDate, object> propertySelector)
    {
        return calendarDate1.SequenceEqual(calendarDate2, new PropertyComparer<CalendarDate>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(Route[] route1, Route[] route2, Func<Route, object> propertySelector)
    {
        return route1.SequenceEqual(route2, new PropertyComparer<Route>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(Shape[] shape1, Shape[] shape2, Func<Shape, object> propertySelector)
    {
        return shape1.SequenceEqual(shape2, new PropertyComparer<Shape>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(Stop[] stop1, Stop[] stop2, Func<Stop, object> propertySelector)
    {
        return stop1.SequenceEqual(stop2, new PropertyComparer<Stop>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(StopTime[] stopTime1, StopTime[] stopTime2, Func<StopTime, object> propertySelector)
    {
        return stopTime1.SequenceEqual(stopTime2, new PropertyComparer<StopTime>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }

    internal static ComparisonResult GetComparisonResult(Trip[] trip1, Trip[] trip2, Func<Trip, object> propertySelector)
    {
        return trip1.SequenceEqual(trip2, new PropertyComparer<Trip>(propertySelector)) ? ComparisonResult.EQUAL : ComparisonResult.SMALLER;
    }
}
