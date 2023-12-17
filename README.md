# OEBB
OEBB train transportation library

Note that this Program was not developed in corporation with the Austrian Federal Railways.

```csharp
Config.SetBaseFilePath(@"<filepath>");

await OEBB_class.Init(
    new Config()
    {
        InitCalendarDates = true,
        InitStopTimes = true,
        InitTrips = true,
        InitRoutes = true,
        InitShapes = false,
        InitCalendars = true,
        InitStops = true,
        InitNextStops = false
    }, false
);

Stop stop = Utilities.SearchStops(StopProperty.stop_name, "Villach", true)[0];
StationInformation stationInformation = StationInformation.GetDepartures(stop, false);
Assert.IsTrue(stationInformation.Count > 0);

stop = Utilities.SearchStops(StopProperty.stop_name, "Pölten H", true)[0];
        

//Journey journey = new Journey();
//journey.GetJourney(stop, stop, DateTime.Now, DateTime.Now, false);

Trip[] trips = Utilities.SearchTrips(TripProperty.trip_headsign, "Selzthal", true);
trips = trips.GetOnlyTrains();
Stop[] stops = trips[0].GetStops();
StopTime[] stopTimes = trips[0].GetStopTimes();
Assert.IsTrue(stops.Length == stopTimes.Length);

StationInformation s = StationInformation.GetDepartures(stop, true);
        
JourneySegment journeySegment = new JourneySegment(trips[0], stops[0], stops[^2]);
```

It is imperative to clarify that you are expressly forbidden from asserting ownership of this program as your exclusive creation. While you may have introduced adjustments or enhancements to it, the fundamental program and its initial design continue to be the intellectual property of the lawful owner or developer. When employing this program, you are obligated to attribute the core program to me as the original creator. Thanks for your understanding.
