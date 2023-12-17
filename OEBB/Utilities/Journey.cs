namespace OEBB;

public class Journey : Utilities
{
    private Exit[] exits;
    public int Count => exits.Length;
    public int Switches => Count - 1;
    public int Duration => (int)exits[^1].ExitTime.Subtract(exits[0].HopOnTime).TotalMinutes;

    public Exit[] Exits
    {
        get => exits;
        set => exits = value;
    }

    public Journey GetJourney(Stop origin, Stop destination, DateTime startTime)
    {
        return GetJourneyHelper(origin, destination, startTime, DateTime.Now, true);
    }

    private Journey GetJourneyHelper(string origin, string destination, DateTime startTime, DateTime endTime, bool ignoreDateTime, bool allowDuplicates)
    {
        throw new NotImplementedException();
    }

    private Journey GetJourneyHelper(Stop origin, Stop destination, DateTime startTime, DateTime endTime, bool allowDuplicates)
    {
        Journey result = new Journey();

        StationInformation originStartInformation = StationInformation.GetDepartures(origin, startTime, endTime, allowDuplicates);
        
        for (int i = 0; i < originStartInformation.Count; i++)
        {
            Trip trip = originStartInformation.Trips[i];
            Calendar calendar = originStartInformation.Calendars[i];
            StopTime stopTime = originStartInformation.StopTimes[i];
            Stop[] tripStops = trip.GetStops();

        }

        return result;
    }
}

public class Exit
{
    public int HopOnIndex { get; set; }
    public int ExitIndex { get; set; }
    public Trip Trips { get; set; }
    public DateTime HopOnTime { get; set; }
    public DateTime ExitTime { get; set; }
    public Stop HopOnStop { get; set; }
    public Stop ExitStop { get; set; }
}