namespace OEBB;

public class StationInformation : Utilities
{
    public StopTime[] StopTimes { get; private set; }
    public Trip[] Trips { get; private set; }
    public Calendar[] Calendars { get; private set; }
    public int Count => StopTimes.Length;

    public static StationInformation GetDepartures(Stop stop, bool allowDuplicates)
    {
        return GetDeparturesHelper(stop, DateTime.Now, DateTime.Now, true, allowDuplicates);
    }
    
    public static StationInformation GetDepartures(Stop stop, DateTime startDate, DateTime endDate, bool allowDuplicates)
    {
        return GetDeparturesHelper(stop, startDate, endDate, false, allowDuplicates);
    }

    public static StationInformation GetDepartures(string stop, DateTime startDate, DateTime endDate, bool allowDuplicates)
    {
        return GetDeparturesHelper(stop, startDate, endDate, false, allowDuplicates);
    }

    public static StationInformation GetDepartures(string stop, bool allowDuplicates)
    {
        return GetDeparturesHelper(stop, DateTime.Now, DateTime.Now, true, allowDuplicates);
    }

    private static StationInformation GetDeparturesHelper(string stop, DateTime startDate, DateTime endDate, bool ignoreDateTime, bool allowDuplicates)
    {
        Stop[] stops = Utilities.SearchStops(StopProperty.stop_name, stop, true);
        StationInformation[] stationInformations = new StationInformation[stops.Length];

        for (int i = 0; i < stops.Length; i++)
        {
            StationInformation stationInformation = GetDeparturesHelper(stops[i], startDate, endDate, ignoreDateTime, allowDuplicates);
            stationInformations[i] = stationInformation;
        }

        StationInformation result = new StationInformation();

        List<StopTime> resultStopTimes = new List<StopTime>();
        List<Trip> resultTrips = new List<Trip>();
        List<Calendar> resultCalendars = new List<Calendar>();

        for (int i = 0; i < stationInformations.Length; i++)
        {
            resultStopTimes.AddRange(stationInformations[i].StopTimes);
            resultTrips.AddRange(stationInformations[i].Trips);
            resultCalendars.AddRange(stationInformations[i].Calendars);
        }

        result.Calendars = resultCalendars.ToArray();
        result.StopTimes = resultStopTimes.ToArray();
        result.Trips = resultTrips.ToArray();

        return result;
    }

    private static StationInformation GetDeparturesHelper(Stop stop, DateTime startDate, DateTime endDate, bool ignoreDateTime, bool allowDuplicates)
    {
        TripProperty duplicateProperty = TripProperty.trip_headsign;

        string stopId = stop.stop_id;
        StopTime[] stopTimes = Utilities.SearchStopTimes(StopTimeProperty.stop_id, stopId, true);
        List<Trip> trips = new List<Trip>();

        for (int i = 0; i < stopTimes.Length; i++)
        {
            Trip[] currTrips = Utilities.SearchTrips(TripProperty.trip_id, stopTimes[i].trip_id, true);
            trips.AddRange(currTrips);
        }

        List<DateTime> arrivalTimes = new List<DateTime>();
        List<DateTime> departureTimes = new List<DateTime>();

        for (int i = 0; i < stopTimes.Length; i++)
        {
            string correctedArrivalTime = CorrectTime(stopTimes[i].arrival_time);
            string correctedDepartureTime = CorrectTime(stopTimes[i].departure_time);

            DateTime arrivalTime = DateTime.ParseExact(correctedArrivalTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
            DateTime departureTime = DateTime.ParseExact(correctedDepartureTime, "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);

            arrivalTimes.Add(arrivalTime);
            departureTimes.Add(departureTime);
        }

        StationInformation stationInformation = new StationInformation();
        stationInformation.StopTimes = stopTimes;
        stationInformation.Trips = trips.ToArray();

        List<Calendar> calendars = new List<Calendar>();
        for (int i = 0; i < trips.Count; i++)
        {
            Trip currTrip = trips[i];
            Calendar[] currCalendar = Utilities.SearchCalendars(CalendarProperty.service_id, currTrip.service_id, true);
            calendars.AddRange(currCalendar);
        }
        stationInformation.Calendars = calendars.ToArray();

        if (ignoreDateTime)
        {
            stationInformation = BubbleSort(stationInformation);
            if (!allowDuplicates)
            {
                stationInformation = RemoveDuplicates(stationInformation, duplicateProperty);
            }
            return stationInformation;
        }

        StationInformation result = new StationInformation();
        List<StopTime> resultStopTimes = new List<StopTime>();
        List<Trip> resultTrips = new List<Trip>();
        List<Calendar> resultCalendars = new List<Calendar>();
        for (int i = 0; i < stopTimes.Length; i++)
        {
            DateTime arrivalTime = arrivalTimes[i];

            if (arrivalTime >= startDate && arrivalTime <= endDate)
            {
                resultStopTimes.Add(stationInformation.StopTimes[i]);
                resultTrips.Add(stationInformation.Trips[i]);
                resultCalendars.Add(stationInformation.Calendars[i]);
            }
        }

        result.StopTimes = resultStopTimes.ToArray();
        result.Trips = resultTrips.ToArray();
        result.Calendars = resultCalendars.ToArray();

        result = BubbleSort(result);

        if (!allowDuplicates)
        {
            stationInformation = RemoveDuplicates(stationInformation, duplicateProperty);
        }

        return result;
    }

    /////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

    

    private static StationInformation BubbleSort(StationInformation stationInformation)
    {
        for (int i = 0; i < stationInformation.Count - 1; i++)
        {
            for (int j = i; j < stationInformation.Count; j++)
            {
                if (string.Compare(stationInformation.Trips[i].trip_headsign, stationInformation.Trips[j].trip_headsign) > 0)
                {
                    Trip trip = stationInformation.Trips[i];
                    stationInformation.Trips[i] = stationInformation.Trips[j];
                    stationInformation.Trips[j] = trip;

                    StopTime stopTime = stationInformation.StopTimes[i];
                    stationInformation.StopTimes[i] = stationInformation.StopTimes[j];
                    stationInformation.StopTimes[j] = stopTime;

                    Calendar calendar = stationInformation.Calendars[i];
                    stationInformation.Calendars[i] = stationInformation.Calendars[j];
                    stationInformation.Calendars[j] = calendar;
                }
            }
        }

        return stationInformation;
    }

    private static StationInformation RemoveDuplicates(StationInformation stationInformation, TripProperty tripProperty)
    {
        Trip[] trips = stationInformation.Trips;
        StopTime[] stopTimes = stationInformation.StopTimes;
        Calendar[] calendars = stationInformation.Calendars;

        List<Trip> resultTrips = new List<Trip>();
        List<StopTime> resultStopTimes = new List<StopTime>();
        List<Calendar> resultCalendars = new List<Calendar>();

        for (int i = 0; i < trips.Length; i++)
        {
            Trip currTrip = trips[i];
            bool found = false;
            for (int j = 0; j < resultTrips.Count; j++)
            {
                Trip resultTrip = resultTrips[j];
                if (tripProperty == TripProperty.route_id)
                {
                    if (currTrip.route_id == resultTrip.route_id)
                    {
                        found = true;
                        break;
                    }
                }
                else if (tripProperty == TripProperty.trip_headsign)
                {
                    if (currTrip.trip_headsign == resultTrip.trip_headsign)
                    {
                        found = true;
                        break;
                    }
                }
            }

            if (!found)
            {
                resultTrips.Add(currTrip);
                resultStopTimes.Add(stopTimes[i]);
                resultCalendars.Add(calendars[i]);
            }
        }

        StationInformation result = new();

        if (resultTrips != null && resultStopTimes != null && resultCalendars != null)
        {
            result.Trips = resultTrips.ToArray();
            result.StopTimes = resultStopTimes.ToArray();
            result.Calendars = resultCalendars.ToArray();
        }

        return result;
    }

    public override string ToString()
    {
        return "StopTimes: " + StopTimes.Length + ", Trips: " + Trips.Length + "Calendars: " + Calendars.Length + ".";
    }
}
