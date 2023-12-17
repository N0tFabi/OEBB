using System.Text.RegularExpressions;

namespace OEBB;

public class StopTime
{
    public string trip_id { get; set; }
    public string arrival_time { get; set; }
    public string departure_time { get; set; }
    public string stop_id { get; set; }
    public int stop_sequence { get; set; }
    public int stop_headsign { get; set; }
    public int pickup_type { get; set; }
    public int drop_off_type { get; set; }
    public double shape_dist_traveled { get; set; }

    public override string ToString()
    {
        return $"arr: {arrival_time} - dep: {departure_time}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.stop_times;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.stopTimes = new StopTime[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.stopTimes[i - 1] = new StopTime()
            {
                trip_id = lineParts[0],
                arrival_time = lineParts[1],
                departure_time = lineParts[2],
                stop_id = lineParts[3],
                stop_sequence = Convert.ToInt32(lineParts[4]),
                stop_headsign = string.IsNullOrEmpty(lineParts[5]) ? -1 : Convert.ToInt32(lineParts[5]),
                pickup_type = Convert.ToInt32(lineParts[6]),
                drop_off_type = Convert.ToInt32(lineParts[7]),
                shape_dist_traveled = Convert.ToDouble(lineParts[8].Replace(".", ","))
            };
        });
    }

    public static StopTime[] GetStopTimes(Stop[] stops, Stop origin, Stop destination)
    {
        List<StopTime> stopTimes = new List<StopTime>();

        for (int i = 0; i < GlobalVariables.stopTimes.Length; i++)
        {
            StopTime stopTime = GlobalVariables.stopTimes[i];

            if (stopTime.stop_id == origin.stop_id || stopTime.stop_id == destination.stop_id)
            {
                stopTimes.Add(stopTime);
            }
        }

        return stopTimes.ToArray();
    }
}
