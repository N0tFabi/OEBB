using OEBB;

namespace OEBB.Comparing;

public class StopTimeComparer
{
    private readonly Dictionary<StopTimeCompareMethod, Func<StopTime, StopTime, int>> comparisonMethods;

    public StopTimeComparer()
    {
        comparisonMethods = new Dictionary<StopTimeCompareMethod, Func<StopTime, StopTime, int>>
        {
            { StopTimeCompareMethod.trip_id, (x, y) => x.trip_id.CompareTo(y.trip_id) },
            { StopTimeCompareMethod.arrival_time, (x, y) => x.arrival_time.CompareTo(y.arrival_time) },
            { StopTimeCompareMethod.departure_time, (x, y) => x.departure_time.CompareTo(y.departure_time) },
            { StopTimeCompareMethod.stop_id, (x, y) => x.stop_id.CompareTo(y.stop_id) },
            { StopTimeCompareMethod.stop_sequence, (x, y) => x.stop_sequence.CompareTo(y.stop_sequence) },
            { StopTimeCompareMethod.stop_headsign, (x, y) => x.stop_headsign.CompareTo(y.stop_headsign) },
            { StopTimeCompareMethod.pickup_type, (x, y) => x.pickup_type.CompareTo(y.pickup_type) },
            { StopTimeCompareMethod.drop_off_type, (x, y) => x.drop_off_type.CompareTo(y.drop_off_type) },
            { StopTimeCompareMethod.shape_dist_traveled, (x, y) => x.shape_dist_traveled.CompareTo(y.shape_dist_traveled) }
        };
    }

    public ComparisonResult Compare(StopTime[] stopTimes1, StopTime[] stopTimes2, StopTimeCompareMethod method)
    {
        if (stopTimes1.Length != stopTimes2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<StopTime, StopTime, int> comparer = comparisonMethods[method];

        int result = comparer(stopTimes1[0], stopTimes2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(StopTime stopTime1, StopTime stopTime2, StopTimeCompareMethod method)
    {
        return Compare(new StopTime[] { stopTime1 }, new StopTime[] { stopTime2 }, method);
    }
}
