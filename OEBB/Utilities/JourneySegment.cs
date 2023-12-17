using System.Diagnostics;
using System.Globalization;
using System.Runtime.CompilerServices;

namespace OEBB;

public class JourneySegment
{
    public Trip Trip { get; set; }
    public Stop HopOnStop { get; set; }
    public Stop HopOffStop { get; set; }
    public DateTime HopOnTime { get; set; }
    public DateTime HopOffTime { get; set; }
    public Stop[] Stops { get; set; }
    public StopTime[] StopTimes { get; set; }
    public TimeSpan Duration => HopOffTime.Subtract(HopOnTime);
    public int StopCount => Stops.Length;

    public JourneySegment(Trip trip, Stop origin, Stop destination)
    {
        Trip = trip;
        HopOnStop = origin;
        HopOffStop = destination;

        int startIndex, endIndex;

        Stops = trip.GetStops();
        Stops = Stop.GetStops(Stops, HopOnStop, HopOffStop, out startIndex, out endIndex);
        StopTime[] stopTimes = trip.GetStopTimes();
        StopTimes = stopTimes.GetSegment(startIndex, endIndex);

        DateTime hopOnTime, hopOffTime;

        DateTime.TryParseExact(stopTimes[0].departure_time, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out hopOnTime);
        DateTime.TryParseExact(stopTimes[^(Stops.Count() - endIndex + 1)].arrival_time, "HH:mm:ss", CultureInfo.InvariantCulture, DateTimeStyles.None, out hopOffTime);

        HopOnTime = hopOnTime;
        HopOffTime = hopOffTime;
    }

    public override string ToString()
    {
        string result = $"-> {HopOnStop.stop_name} ({HopOnTime.ToString("HH:mm")}) -> {HopOffStop.stop_name} ({HopOffTime.ToString("HH:mm")})";
        return result;
    }
}
