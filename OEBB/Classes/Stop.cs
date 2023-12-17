using System.Text.Json;
using System.Text.RegularExpressions;

namespace OEBB;

public class Stop
{
    public string stop_id { get; set; }
    public string stop_name { get; set; }
    public float stop_lat { get; set; }
    public float stop_lon { get; set; }
    public int zone_id { get; set; }

    public override string ToString()
    {
        return $"{stop_id} - {stop_name}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.stops;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.stops = new Stop[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.stops[i - 1] = new Stop()
            {
                stop_id = lineParts[0],
                stop_name = lineParts[1],
                stop_lat = (float)Convert.ToDouble(lineParts[2].Replace(".", ",")),
                stop_lon = (float)Convert.ToDouble(lineParts[3].Replace(".", ",")),
                zone_id = string.IsNullOrEmpty(lineParts[4]) ? -1 : Convert.ToInt32(lineParts[4])
            };
        });
    }

    public static void InitNextStops()
    {
        List<Stop> stops = new List<Stop>();

        for (int i = 0; i < GlobalVariables.stops.Length; i++)
        {
            StationInformation stationInformation = StationInformation.GetDepartures(GlobalVariables.stops[i], false);

            for (int j = 0; j < stationInformation.Count; j++)
            {
                
            }
        }
    }

    public async Task<string> GetAddress()
    {
        return await GetAddressFromCoordinates(stop_lat, stop_lon);
    }

    public static async Task<string> GetAddressFromCoordinates(float lat, float lon)
    {
        string url = $"https://nominatim.openstreetmap.org/reverse?format=json&lat={lat}&lon={lon}&zoom=18&addressdetails=1";

        using HttpClient client = new HttpClient();
        using HttpResponseMessage response = await client.GetAsync(url);
        using HttpContent content = response.Content;

        string result = await content.ReadAsStringAsync();

        if (result == null)
        {
            url = $"https://geocode.maps.co/reverse?lat={lat}&lon={lon}";

            using HttpClient client2 = new HttpClient();
            using HttpResponseMessage response2 = await client.GetAsync(url);
            using HttpContent content2 = response.Content;

            if (result == null)
            {
                return "No address found.";
            }
        }

        return GetFirstAddressProperty(result);
    }

    private static string GetFirstAddressProperty(string jsonString)
    {
        try
        {
            JsonDocument doc = JsonDocument.Parse(jsonString);

            JsonElement root = doc.RootElement;

            if (root.TryGetProperty("address", out JsonElement addressElement))
            {
                foreach (JsonProperty property in addressElement.EnumerateObject())
                {
                    return property.Value.ToString();
                }
            }
            else
            {
                return "No 'address' property found in the JSON.";
            }
        }
        catch (JsonException ex)
        {
            return "Error parsing JSON: " + ex.Message;
        }

        return "No property found under 'address'.";
    }

    public static Stop[] GetStops(Stop[] stops, Stop startStop, Stop endStop, out int startIndex, out int endIndex)
    {
        List<Stop> result = new List<Stop>();
        
        bool startStopFound = false;
        bool endStopFound = false;
        int i = 0;

        startIndex = -1;
        endIndex = -1;

        foreach (Stop stop in stops)
        {
            if (stop.Equals(startStop))
            {
                startStopFound = true;
                startIndex = i;
            }

            if (startStopFound)
            {
                result.Add(stop);
            }

            if (stop.Equals(endStop))
            {
                endStopFound = true;
                endIndex = i;
            }

            if (endStopFound)
            {
                break;
            }

            i++;
        }

        return result.ToArray();
    }


}
