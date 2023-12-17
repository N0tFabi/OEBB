namespace OEBB.Comparing;

public class RouteComparer
{
    private readonly Dictionary<RouteCompareMethod, Func<Route, Route, int>> comparisonMethods;

    public RouteComparer()
    {
        comparisonMethods = new Dictionary<RouteCompareMethod, Func<Route, Route, int>>
        {
            { RouteCompareMethod.route_id, (x, y) => x.route_id.CompareTo(y.route_id) },
            { RouteCompareMethod.agency_id, (x, y) => x.agency_id.CompareTo(y.agency_id) },
            { RouteCompareMethod.route_short_name, (x, y) => x.route_short_name.CompareTo(y.route_short_name) },
            { RouteCompareMethod.route_long_name, (x, y) => x.route_long_name.CompareTo(y.route_long_name) },
            { RouteCompareMethod.route_type, (x, y) => x.route_type.CompareTo(y.route_type) }
        };
    }

    public ComparisonResult Compare(Route[] route1, Route[] route2, RouteCompareMethod method)
    {
        if (route1.Length != route2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<Route, Route, int> comparer = comparisonMethods[method];

        int result = comparer(route1[0], route2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(Route route1, Route route2, RouteCompareMethod method)
    {
        return Compare(new Route[] { route1 }, new Route[] { route2 }, method);
    }
}