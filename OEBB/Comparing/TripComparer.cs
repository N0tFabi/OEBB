using OEBB;

namespace OEBB.Comparing;

public class TripComparer
{
    private readonly Dictionary<TripCompareMethod, Func<Trip, Trip, int>> comparisonMethods;

    public TripComparer()
    {
        comparisonMethods = new Dictionary<TripCompareMethod, Func<Trip, Trip, int>>
        {
            { TripCompareMethod.route_id, (x, y) => x.route_id.CompareTo(y.route_id) },
            { TripCompareMethod.service_id, (x, y) => x.service_id.CompareTo(y.service_id) },
            { TripCompareMethod.trip_id, (x, y) => x.trip_id.CompareTo(y.trip_id) },
            { TripCompareMethod.shape_id, (x, y) => x.shape_id.CompareTo(y.shape_id) },
            { TripCompareMethod.trip_headsign, (x, y) => x.trip_headsign.CompareTo(y.trip_headsign) },
            { TripCompareMethod.direction_id, (x, y) => x.direction_id.CompareTo(y.direction_id) },
            { TripCompareMethod.block_id, (x, y) => x.block_id.CompareTo(y.block_id) }
        };
    }

    public ComparisonResult Compare(Trip[] trips1, Trip[] trips2, TripCompareMethod method)
    {
        if (trips1.Length != trips2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<Trip, Trip, int> comparer = comparisonMethods[method];

        int result = comparer(trips1[0], trips2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(Trip trip1, Trip trip2, TripCompareMethod method)
    {
        return Compare(new Trip[] { trip1 }, new Trip[] { trip2 }, method);
    }
}
