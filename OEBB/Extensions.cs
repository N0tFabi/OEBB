using OEBB.Comparing;

namespace OEBB;

public static class Extensions
{
    private static ComparisonResult ToComparisonResult(this int comparisonResult)
    {
        return comparisonResult switch
        {
            -1 => ComparisonResult.SMALLER,
            0 => ComparisonResult.EQUAL,
            1 => ComparisonResult.BIGGER,
            _ => throw new ArgumentException("Invalid comparison result."),
        };
    }

    public static Trip[] Arrange(this Trip[] trips, Func<Trip, bool> predicate)
    {
        List<Trip> result = new List<Trip>();
        foreach (Trip trip in trips)
        {
            if (predicate(trip))
            {
                result.Add(trip);
            }
        }
        return result.ToArray();
    }

    public static T[] Arrange<T>(this T[] values, Func<T, bool> predicate)
    {
        List<T> result = new List<T>();
        foreach (T value in values)
        {
            if (predicate(value))
            {
                result.Add(value);
            }
        }
        return result.ToArray();
    }

    public static Trip[] GetOnlyTrains(this Trip[] trips)
    {
        Trip[] result = trips.Arrange(x => !x.route_id.Contains("SV", StringComparison.OrdinalIgnoreCase));
        return result;
    }

    public static Trip[] GetOnlyBuses(this Trip[] trips)
    {
        Trip[] result = trips.Arrange(x => x.route_id.Contains("SV", StringComparison.OrdinalIgnoreCase));
        return result;
    }

    public static T[] GetSegment<T>(this T[] values, int startIndex, int stopIndex)
    {
        if (startIndex < 0 || stopIndex >= values.Count())
        {
            throw new ArgumentOutOfRangeException("Invalid start or stop index.");
        }

        List<T> result = new List<T>();
        for (int i = startIndex; i <= stopIndex; i++)
        {
            result.Add(values[i]);
        }
        return result.ToArray();
    }
}
