using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEBB
{
    public class Config
    {
        public bool InitCalendars { get; set; }
        public bool InitCalendarDates { get; set; }
        public bool InitRoutes { get; set; }
        public bool InitShapes { get; set; }
        public bool InitStopTimes { get; set; }
        public bool InitStops { get; set; }
        public bool InitTrips { get; set; }
        public bool InitNextStops { get; set; }

        public static void SetBaseFilePath(string path)
        {
            if (!string.IsNullOrEmpty(path))
                Constants.baseFilePath = path.EndsWith(@"\") ? path : path + @"\";
            else
                throw new ArgumentNullException("path", "Path cannot be null or empty.");
        }

        public Config(bool init)
        {
            InitCalendars = init;
            InitCalendarDates = init;
            InitRoutes = init;
            InitShapes = init;
            InitStopTimes = init;
            InitStops = init;
            InitTrips = init;
            InitNextStops = init;
        }

        public Config()
        { }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Init: ");

            if (InitCalendarDates)
                sb.Append("CalendarDates, ");
            if (InitCalendars)
                sb.Append("Calendars, ");
            if (InitRoutes)
                sb.Append("Routes, ");
            if (InitShapes)
                sb.Append("Shapes, ");
            if (InitStopTimes)
                sb.Append("StopTimes, ");
            if (InitStops)
                sb.Append("Stops, ");
            if (InitTrips)
                sb.Append("Trips, ");
            if (InitNextStops)
                sb.Append("NextStops, ");

            return sb.ToString().Substring(0, sb.ToString().Length - 2);
        }
    }
}
