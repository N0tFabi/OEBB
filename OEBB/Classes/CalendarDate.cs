using System.Text.RegularExpressions;

namespace OEBB;

public class CalendarDate
{
    public string service_id { get; set; }
    public string date { get; set; }
    public int exception_type { get; set; }

    public override string ToString()
    {
        return $"{service_id}, {Utilities.FormatDate(date)}, {exception_type}";
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.calendar_dates;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.calendarDates = new CalendarDate[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.calendarDates[i - 1] = new CalendarDate()
            {
                service_id = lineParts[0],
                date = lineParts[1],
                exception_type = Convert.ToInt32(lineParts[2])
            };
        });
    }
}
