using Diagnostics = System.Diagnostics;

namespace OEBB
{
    public static class OEBB_class
    {
        public static async Task Init(OEBB.Config config, bool debug)
        {
            Diagnostics::Stopwatch sw = new Diagnostics::Stopwatch();
            sw.Start();

            if (config == null)
                throw new ArgumentNullException("config", "Config cannot be null.");

            if (config.InitCalendarDates)
            {
                if (debug)
                    Console.WriteLine("Initializing CalendarDates...");
                CalendarDate.GetFromFile();
            }

            if (config.InitCalendars)
            {
                if (debug)
                    Console.WriteLine("Initializing Calendars...");
                Calendar.GetFromFile();
            }

            if (config.InitRoutes)
            {
                if (debug)
                    Console.WriteLine("Initializing Routes...");
                Route.GetFromFile();
            }

            if (config.InitShapes)
            {
                if (debug)
                    Console.WriteLine("Initializing Shapes...");
                Shape.GetFromFile();
            }

            if (config.InitStopTimes)
            {
                if (debug)
                    Console.WriteLine("Initializing StopTimes...");
                StopTime.GetFromFile();
            }

            if (config.InitStops)
            {
                if (debug)
                    Console.WriteLine("Initializing Stops...");
                Stop.GetFromFile();
            }

            if (config.InitTrips)
            {
                if (debug)
                    Console.WriteLine("Initializing Trips...");
                Trip.GetFromFile();
            }

            if (config.InitNextStops)
            {
                throw new NotImplementedException();
                /*if (debug)
                    Console.WriteLine("Initializing NextStops...");
                Stop.InitNextStops();*/
            }

            sw.Stop();
            if (debug)
                Console.WriteLine($"Initialization took {sw.ElapsedMilliseconds}ms.");
        }

        public static async Task Init(bool debug)
        {
            await Init(new OEBB.Config()
            {
                InitCalendarDates = true,
                InitCalendars = true,
                InitRoutes = true,
                InitShapes = true,
                InitStopTimes = true,
                InitStops = true,
                InitTrips = true
            }, debug);
        }

        public static bool IsCorrectGtfsPath()
            => Directory.Exists(Constants.baseFilePath)
                && File.Exists(Constants.baseFilePath + Constants.agency)
                && File.Exists(Constants.baseFilePath + Constants.calendar)
                && File.Exists(Constants.baseFilePath + Constants.calendar_dates)
                && File.Exists(Constants.baseFilePath + Constants.routes)
                && File.Exists(Constants.baseFilePath + Constants.shapes)
                && File.Exists(Constants.baseFilePath + Constants.stop_times)
                && File.Exists(Constants.baseFilePath + Constants.stops)
                && File.Exists(Constants.baseFilePath + Constants.trips);
    }
}
