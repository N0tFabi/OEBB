using System.Diagnostics.SymbolStore;
using System.Globalization;
using System.Text;
using System.Text.RegularExpressions;

namespace OEBB;

public class Calendar
{
    private static CultureInfo language = new("en-US");

    public string service_id { get; set; }
    public int monday { get; set; }
    public int tuesday { get; set; }
    public int wednesday { get; set; }
    public int thursday { get; set; }
    public int friday { get; set; }
    public int saturday { get; set; }
    public int sunday { get; set; }
    public string start_date { get; set; }
    public string end_date { get; set; }

    public override string ToString()
    {
        StringBuilder sb = new();
        if (monday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Monday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (tuesday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Tuesday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (wednesday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Wednesday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (thursday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Thursday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (friday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Friday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (saturday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Saturday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }
        if (sunday == 1)
        {
            DayOfWeek dayOfWeek = DayOfWeek.Sunday;
            string dayOfWeekString = language.DateTimeFormat.GetDayName(dayOfWeek);
            sb.Append(dayOfWeekString);
            sb.Append(", ");
        }

        sb.Replace(", ", string.Empty, sb.Length - 2, 2);

        return sb.ToString();
    }

    public static void SetLanguage(string lang)
    {
        if (lang == null)
        {
            language = new CultureInfo("en-US");
        }
        else
        {
            language = new CultureInfo(lang);
        }
    }

    public static void GetFromFile()
    {
        string filePath = Constants.baseFilePath + Constants.calendar;
        string[] lines = File.ReadAllLines(filePath);

        GlobalVariables.calendar = new Calendar[lines.Length - 1];

        Parallel.For(1, lines.Length, i =>
        {
            string[] lineParts = Regex.Split(lines[i], @",(?=(?:[^""]*""[^""]*"")*[^""]*$)").Select(x => x.Replace("\"", string.Empty)).ToArray();

            GlobalVariables.calendar[i - 1] = new Calendar
            {
                service_id = lineParts[0],
                monday = Convert.ToInt32(lineParts[1]),
                tuesday = Convert.ToInt32(lineParts[2]),
                wednesday = Convert.ToInt32(lineParts[3]),
                thursday = Convert.ToInt32(lineParts[4]),
                friday = Convert.ToInt32(lineParts[5]),
                saturday = Convert.ToInt32(lineParts[6]),
                sunday = Convert.ToInt32(lineParts[7]),
                start_date = lineParts[8],
                end_date = lineParts[9]
            };
        });
    }
}
