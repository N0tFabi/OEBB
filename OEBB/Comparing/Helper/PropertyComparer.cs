namespace OEBB.Comparing;

public class PropertyComparer<T> : IEqualityComparer<T>
{
    private readonly Func<T, object> _propertySelector;

    public PropertyComparer(Func<T, object> propertySelector)
    {
        _propertySelector = propertySelector ?? throw new ArgumentNullException(nameof(propertySelector));
    }

    public bool Equals(T x, T y)
    {
        object xPropertyValue = _propertySelector(x);
        object yPropertyValue = _propertySelector(y);

        return xPropertyValue.Equals(yPropertyValue);
    }

    public int GetHashCode(T obj)
    {
        object propertyValue = _propertySelector(obj);

        return propertyValue.GetHashCode();
    }
}