using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System;
using System.Text.RegularExpressions;
using System.Runtime.CompilerServices;

namespace OEBB;

public class Trip
{
    public string route_id { get; set; }
    public string service_id { get; set; }
    public string trip_id {  get; set; }
    public string shape_id { get; set; }
    public string trip_headsign { get; set; }
    public int direction_id { get; set; }
    public int block_id { get; set; }

    public override string ToString()
    {
        return $"-> {trip_headsign}, {route_id}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.trips;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.trips = new Trip[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.trips[i - 1] = new Trip
            {
                route_id = lineParts[0],
                service_id = lineParts[1],
                trip_id = lineParts[2],
                shape_id = lineParts[3],
                trip_headsign = lineParts[4],
                direction_id = Convert.ToInt32(lineParts[5]),
                block_id = string.IsNullOrEmpty(lineParts[6]) ? -1 : Convert.ToInt32(lineParts[6])
            };
        });
    }

    /// <remarks>
    /// If a train goes from A to B and between A and B its Train number changes (e.g. R10 -> CJX10)
    /// Only the stops will be returned, at which the Train number is R10
    /// </remarks>
    public Stop[] GetStops()
    {
        string filePath = Constants.baseFilePath + Constants.stop_times;
        string[] lines = File.ReadAllLines(filePath);

        List<Stop> stops = new List<Stop>();

        for (int i = 1; i < lines.Length; i++)
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            if (lineParts[0].Equals(trip_id))
            {
                stops.Add(GlobalVariables.stops[Array.IndexOf(GlobalVariables.stops, GlobalVariables.stops.First(x => x.stop_id == lineParts[3]))]);
            }
        }

        return stops.ToArray(); 
    }

    public StopTime[] GetStopTimes()
    {
        return Utilities.SearchStopTimes(StopTimeProperty.trip_id, trip_id, true);
    }

    public Stop GetStart()
    {
        Stop[] stops = GetStops();
        return stops[0];
    }

    public Stop GetEnd()
    {
        Stop[] stops = GetStops();
        return stops[^1];
    }
}