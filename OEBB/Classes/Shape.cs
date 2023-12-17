using System.Text.RegularExpressions;

namespace OEBB;

public class Shape
{
    public string shape_id { get; set; }
    public float shape_pt_lat { get; set; }
    public float shape_pt_lon { get; set; }
    public int shape_pt_sequence { get; set; }
    public double shape_dist_traveled { get; set; }

    public override string ToString()
    {
        return $"{shape_id}, {shape_pt_sequence}, {shape_dist_traveled:f2}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.shapes;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.shapes = new Shape[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.shapes[i - 1] = new Shape()
            {
                shape_id = lineParts[0],
                shape_pt_lat = (float)Convert.ToDouble(lineParts[1].Replace(".", ",")),
                shape_pt_lon = (float)Convert.ToDouble(lineParts[2].Replace(".", ",")),
                shape_pt_sequence = Convert.ToInt32(lineParts[3]),
                shape_dist_traveled = Convert.ToDouble(lineParts[4].Replace(".", ","))
            };
        });
    }

}
