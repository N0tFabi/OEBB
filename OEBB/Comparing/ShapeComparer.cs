using OEBB;

namespace OEBB.Comparing;

public class ShapeComparer
{
    private readonly Dictionary<ShapeCompareMethod, Func<Shape, Shape, int>> comparisonMethods;

    public ShapeComparer()
    {
        comparisonMethods = new Dictionary<ShapeCompareMethod, Func<Shape, Shape, int>>
        {
            { ShapeCompareMethod.shape_id, (x, y) => x.shape_id.CompareTo(y.shape_id) },
            { ShapeCompareMethod.shape_pt_lat, (x, y) => x.shape_pt_lat.CompareTo(y.shape_pt_lat) },
            { ShapeCompareMethod.shape_pt_lon, (x, y) => x.shape_pt_lon.CompareTo(y.shape_pt_lon) },
            { ShapeCompareMethod.shape_pt_sequence, (x, y) => x.shape_pt_sequence.CompareTo(y.shape_pt_sequence) },
            { ShapeCompareMethod.shape_dist_traveled, (x, y) => x.shape_dist_traveled.CompareTo(y.shape_dist_traveled) }
        };
    }

    public ComparisonResult Compare(Shape[] shape1, Shape[] shape2, ShapeCompareMethod method)
    {
        if (shape1.Length != shape2.Length)
            return ComparisonResult.SMALLER;

        if (!comparisonMethods.ContainsKey(method))
            throw new ArgumentException("Invalid method.", nameof(method));

        Func<Shape, Shape, int> comparer = comparisonMethods[method];

        int result = comparer(shape1[0], shape2[0]);
        return (ComparisonResult)result;
    }

    public ComparisonResult Compare(Shape shape1, Shape shape2, ShapeCompareMethod method)
    {
        return Compare(new Shape[] { shape1 }, new Shape[] { shape2 }, method);
    }
}
