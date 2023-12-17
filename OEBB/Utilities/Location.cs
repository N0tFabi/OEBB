using System.Text.Json;

namespace OEBB;

public class Location
{
    private float _lon;
    private float _lat;
    private string _name;
    private string _display_name;

    public float lon
    {
        get => _lon;
        set => _lon = value;
    }

    public float lat
    {
        get => _lat;
        set => _lat = value;
    }

    public string name
    {
        get => _name;
        set => _name = value;
    }

    public string display_name
    {
        get => _display_name;
        set => _display_name = value;
    }

    public override string ToString()
    {
        return $"-> {name}, {display_name}";
    }

    public Location(float lon, float lat, string name, string displayInfo)
    {
        this.lon = lon;
        this.lat = lat;
        this.name = name;
        this.display_name = displayInfo;
    }
}

