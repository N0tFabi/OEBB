using System.Text.RegularExpressions;

namespace OEBB;

public class Route
{
    public string route_id { get; set; }
    public int agency_id { get; set; }
    public string route_short_name { get; set; }
    public string route_long_name { get; set; }
    public int route_type { get; set; }

    public override string ToString()
    {
        return $"{route_id}, {agency_id}, {route_short_name}, {route_long_name}, {route_type}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.routes;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.routes = new Route[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.routes[i - 1] = new Route()
            {
                route_id = lineParts[0],
                agency_id = Convert.ToInt32(lineParts[1]),
                route_short_name = lineParts[2],
                route_long_name = lineParts[3],
                route_type = Convert.ToInt32(lineParts[4])
            };
        });
    }
}
