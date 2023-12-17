using OEBB;

namespace OEBB.Comparing;

public class StopComparer
{
    private readonly Dictionary<StopCompareMethod, Func<Stop, Stop, int>> comparisonMethods;

    public StopComparer()
    {
        comparisonMethods = new Dictionary<StopCompareMethod, Func<Stop, Stop, int>>
        {
            { StopCompareMethod.stop_id, (x, y) => x.stop_id.CompareTo(y.stop_id) },
            { StopCompareMethod.stop_name, (x, y) => x.stop_name.CompareTo(y.stop_name) },
            { StopCompareMethod.stop_lat, (x, y) => x.stop_lat.CompareTo(y.stop_lat) },
            { StopCompareMethod.stop_lon, (x, y) => x.stop_lon.CompareTo(y.stop_lon) },
            { StopCompareMethod.zone_id, (x, y) => x.zone_id.CompareTo(y.zone_id) }
        };
    }

    public ComparisonResult Compare(Stop[] stop1, Stop[] stop2, StopCompareMethod method)
    {
        if (stop1.Length != stop2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<Stop, Stop, int> comparer = comparisonMethods[method];

        int result = comparer(stop1[0], stop2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(Stop stop1, Stop stop2, StopCompareMethod method)
    {
        return Compare(new Stop[] { stop1 }, new Stop[] { stop2 }, method);
    }
}
