/* 
 * NOTE: the allowDuplicates argument removes duplicates based on the PROPERTY given as an argument!
 */

namespace OEBB;

public class Utilities
{
    internal static string FormatDate(string date) 
        => $"{date[0]}{date[1]}{date[2]}{date[3]}-{date[4]}{date[5]}-{date[6]}{date[7]}";

    public static Trip[] SearchTrips(TripProperty tripProperty, string keyword, bool allowDuplicates)
    {
        List<Trip> result = GlobalVariables.trips.ToList();

        Func<Trip, object>? orderingSelector = tripProperty switch
        {
            TripProperty.route_id => trip => trip.trip_headsign,
            TripProperty.service_id => trip => trip.service_id,
            TripProperty.trip_id => trip => trip.trip_id,
            TripProperty.shape_id => trip => trip.shape_id,
            TripProperty.trip_headsign => trip => trip.trip_headsign,
            TripProperty.direction_id => trip => trip.direction_id,
            TripProperty.block_id => trip => trip.block_id,
            _ => null
        };

        Func<Trip, bool>? whereSelector = tripProperty switch
        {
            TripProperty.route_id => trip => trip.route_id.ToLower().Contains(keyword.ToLower()),
            TripProperty.service_id => trip => trip.service_id.ToLower().Contains(keyword.ToLower()),
            TripProperty.trip_id => trip => trip.trip_id.ToLower().Contains(keyword.ToLower()),
            TripProperty.shape_id => trip => trip.shape_id.ToLower().Contains(keyword.ToLower()),
            TripProperty.trip_headsign => trip => trip.trip_headsign.ToLower().Contains(keyword.ToLower()),
            TripProperty.direction_id => trip => trip.direction_id.ToString().ToLower().Contains(keyword.ToLower()),
            TripProperty.block_id => trip => trip.block_id.ToString().ToLower().Contains(keyword.ToLower()),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if (!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = tripProperty switch
                {
                    TripProperty.route_id => result[i].trip_headsign,
                    TripProperty.service_id => result[i].service_id,
                    TripProperty.trip_id => result[i].trip_id,
                    TripProperty.shape_id => result[i].shape_id,
                    TripProperty.trip_headsign => result[i].trip_headsign,
                    TripProperty.direction_id => result[i].direction_id.ToString(),
                    TripProperty.block_id => result[i].block_id.ToString(),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue))
                {
                    strings.Add(comparisonValue);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static Stop[] SearchStops(StopProperty stopProperty, string keyword, bool allowDuplicates)
    {
        List<Stop> result = GlobalVariables.stops.ToList();

        Func<Stop, object>? orderingSelector = stopProperty switch
        {
            StopProperty.stop_id => stop => stop.stop_id,
            StopProperty.stop_name => stop => stop.stop_name,
            StopProperty.stop_lat => stop => stop.stop_lat,
            StopProperty.stop_lon => stop => stop.stop_lon,
            StopProperty.zone_id => stop => stop.zone_id,
            _ => null
        };

        Func<Stop, bool>? whereSelector = stopProperty switch
        {
            StopProperty.stop_id => stop => stop.stop_id.ToLower().Contains(keyword.ToLower()),
            StopProperty.stop_name => stop => stop.stop_name.ToLower().Contains(keyword.ToLower()),
            StopProperty.stop_lat => stop => stop.stop_lat.ToString().Replace(",", ".").Contains(keyword.Replace(",", ".")),
            StopProperty.stop_lon => stop => stop.stop_lon.ToString().Replace(",", ".").Contains(keyword.Replace(",", ".")),
            StopProperty.zone_id => stop => stop.zone_id.ToString().Replace(",", ".").Contains(keyword.Replace(",", ".")),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if (!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = stopProperty switch
                {
                    StopProperty.stop_id => result[i].stop_id,
                    StopProperty.stop_name => result[i].stop_name,
                    StopProperty.stop_lat => result[i].stop_lat.ToString(),
                    StopProperty.stop_lon => result[i].stop_lon.ToString(),
                    StopProperty.zone_id => result[i].zone_id.ToString(),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static StopTime[] SearchStopTimes(StopTimeProperty stopTimeProperty, string keyword, bool allowDuplicates)
    {
        List<StopTime> result = GlobalVariables.stopTimes.ToList();

        Func<StopTime, object>? orderingSelector = stopTimeProperty switch
        {
            StopTimeProperty.trip_id => stopTime => stopTime.trip_id,
            StopTimeProperty.arrival_time => stopTime => stopTime.arrival_time,
            StopTimeProperty.departure_time => stopTime => stopTime.departure_time,
            StopTimeProperty.stop_id => stopTime => stopTime.stop_id,
            StopTimeProperty.stop_sequence => stopTime => stopTime.stop_sequence,
            StopTimeProperty.stop_headsign => stopTime => stopTime.stop_headsign,
            StopTimeProperty.pickup_type => stopTime => stopTime.pickup_type,
            StopTimeProperty.drop_off_type => stopTime => stopTime.drop_off_type,
            StopTimeProperty.shape_dist_traveled => stopTime => stopTime.shape_dist_traveled,
            _ => null
        };

        Func<StopTime, bool>? whereSelector = stopTimeProperty switch
        {
            StopTimeProperty.trip_id => stopTime => stopTime.trip_id.ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.arrival_time => stopTime => stopTime.arrival_time.ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.departure_time => stopTime => stopTime.departure_time.ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.stop_id => stopTime => stopTime.stop_id.ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.stop_sequence => stopTime => stopTime.stop_sequence.ToString().ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.stop_headsign => stopTime => stopTime.stop_headsign.ToString().ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.pickup_type => stopTime => stopTime.pickup_type.ToString().ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.drop_off_type => stopTime => stopTime.drop_off_type.ToString().ToLower().Contains(keyword.ToLower()),
            StopTimeProperty.shape_dist_traveled => stopTime => stopTime.shape_dist_traveled.ToString().Replace(",", ".").ToLower().Contains(keyword.ToLower()),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if (!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = stopTimeProperty switch
                {
                    StopTimeProperty.trip_id => result[i].trip_id,
                    StopTimeProperty.arrival_time => result[i].arrival_time,
                    StopTimeProperty.departure_time => result[i].departure_time,
                    StopTimeProperty.stop_id => result[i].stop_id,
                    StopTimeProperty.stop_sequence => result[i].stop_sequence.ToString(),
                    StopTimeProperty.stop_headsign => result[i].stop_headsign.ToString(),
                    StopTimeProperty.pickup_type => result[i].pickup_type.ToString(),
                    StopTimeProperty.drop_off_type => result[i].drop_off_type.ToString(),
                    StopTimeProperty.shape_dist_traveled => result[i].shape_dist_traveled.ToString().Replace(",", "."),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static Shape[] SearchShapes(ShapeProperty shapeProperty, string keyword, bool allowDuplicates)
    {
        List<Shape> result = GlobalVariables.shapes.ToList();

        Func<Shape, object>? orderingSelector = shapeProperty switch
        {
            ShapeProperty.shape_id => shape => shape.shape_id,
            ShapeProperty.shape_pt_lat => shape => shape.shape_pt_lat,
            ShapeProperty.shape_pt_lon => shape => shape.shape_pt_lon,
            ShapeProperty.shape_pt_sequence => shape => shape.shape_pt_sequence,
            ShapeProperty.shape_dist_traveled => shape => shape.shape_dist_traveled,
            _ => null
        };

        Func<Shape, bool>? whereSelector = shapeProperty switch
        {
            ShapeProperty.shape_id => shape => shape.shape_id.ToLower().Contains(keyword.ToLower()),
            ShapeProperty.shape_pt_lat => shape => shape.shape_pt_lat.ToString().Replace(",", ".").Contains(keyword.ToLower()),
            ShapeProperty.shape_pt_lon => shape => shape.shape_pt_lon.ToString().Replace(",", ".").Contains(keyword.ToLower()),
            ShapeProperty.shape_pt_sequence => shape => shape.shape_pt_sequence.ToString().Contains(keyword.ToLower()),
            ShapeProperty.shape_dist_traveled => shape => shape.shape_dist_traveled.ToString().Replace(",", ".").Contains(keyword.ToLower()),
            _ => null   
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if (!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = shapeProperty switch
                {
                    ShapeProperty.shape_id => result[i].shape_id,
                    ShapeProperty.shape_pt_lat => result[i].shape_pt_lat.ToString().Replace(",", "."),
                    ShapeProperty.shape_pt_lon => result[i].shape_pt_lon.ToString().Replace(",", "."),
                    ShapeProperty.shape_pt_sequence => result[i].shape_pt_sequence.ToString(),
                    ShapeProperty.shape_dist_traveled => result[i].shape_dist_traveled.ToString().Replace(",", "."),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static Route[] SearchRoutes(RouteProperty routeProperty, string keyword, bool allowDuplicates)
    {
        List<Route> result = GlobalVariables.routes.ToList();

        Func<Route, object>? orderingSelector = routeProperty switch
        {
            RouteProperty.route_id => route => route.route_id,
            RouteProperty.agency_id => route => route.agency_id,
            RouteProperty.route_short_name => route => route.route_short_name,
            RouteProperty.route_long_name => route => route.route_long_name,
            RouteProperty.route_type => route => route.route_type,
            _ => null
        };

        Func<Route, bool>? whereSelector = routeProperty switch
        {
            RouteProperty.route_id => route => route.route_id.ToLower().Contains(keyword.ToLower()),
            RouteProperty.agency_id => route => route.agency_id.ToString().Contains(keyword.ToLower()),
            RouteProperty.route_short_name => route => route.route_short_name.ToLower().Contains(keyword.ToLower()),
            RouteProperty.route_long_name => route => route.route_long_name.ToLower().Contains(keyword.ToLower()),
            RouteProperty.route_type => route => route.route_type.ToString().Contains(keyword.ToLower()),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if (!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = routeProperty switch
                {
                    RouteProperty.route_id => result[i].route_id.ToString(),
                    RouteProperty.agency_id => result[i].agency_id.ToString(),
                    RouteProperty.route_short_name => result[i].route_short_name.ToString(),
                    RouteProperty.route_long_name => result[i].route_long_name.ToString(),
                    RouteProperty.route_type => result[i].route_type.ToString(),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static CalendarDate[] SearchCalendarDates(CalendarDateProperty calendarDateProperty, string keyword, bool allowDuplicates)
    {
        List<CalendarDate> result = GlobalVariables.calendarDates.ToList();

        Func<CalendarDate, object>? orderingSelector = calendarDateProperty switch
        {
            CalendarDateProperty.service_id => calendarDate => calendarDate.service_id,
            CalendarDateProperty.date => calendarDate => calendarDate.date,
            CalendarDateProperty.exception_type => calendarDate => calendarDate.exception_type,
            _ => null
        };

        Func<CalendarDate, bool>? whereSelector = calendarDateProperty switch
        {
            CalendarDateProperty.service_id => calendarDate => calendarDate.service_id.ToLower().Contains(keyword.ToLower()),
            CalendarDateProperty.date => calendarDate => calendarDate.date.ToLower().Contains(keyword.ToLower()),
            CalendarDateProperty.exception_type => calendarDate => calendarDate.exception_type.ToString().Contains(keyword),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if(!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = calendarDateProperty switch
                {
                    CalendarDateProperty.service_id => result[i].service_id,
                    CalendarDateProperty.date => result[i].date,
                    CalendarDateProperty.exception_type => result[i].exception_type.ToString(),
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static Calendar[] SearchCalendars(CalendarProperty calendarProperty, string keyword, bool allowDuplicates)
    {
        List<Calendar> result = GlobalVariables.calendar.ToList();

        Func<Calendar, object>? orderingSelector = calendarProperty switch
        {
            CalendarProperty.service_id => calendar => calendar.service_id,
            CalendarProperty.monday => calendar => calendar.monday,
            CalendarProperty.tuesday => calendar => calendar.tuesday,
            CalendarProperty.wednesday => calendar => calendar.wednesday,
            CalendarProperty.thursday => calendar => calendar.thursday,
            CalendarProperty.friday => calendar => calendar.friday,
            CalendarProperty.saturday => calendar => calendar.saturday,
            CalendarProperty.sunday => calendar => calendar.sunday,
            CalendarProperty.start_date => calendar => calendar.start_date,
            CalendarProperty.end_date => calendar => calendar.end_date,
            _ => null
        };

        Func<Calendar, bool>? whereSelector = calendarProperty switch
        {
            CalendarProperty.service_id => calendar => calendar.service_id.ToLower().Contains(keyword.ToLower()),
            CalendarProperty.monday => calendar => calendar.monday.ToString().Contains(keyword),
            CalendarProperty.tuesday => calendar => calendar.tuesday.ToString().Contains(keyword),
            CalendarProperty.wednesday => calendar => calendar.wednesday.ToString().Contains(keyword),
            CalendarProperty.thursday => calendar => calendar.thursday.ToString().Contains(keyword),
            CalendarProperty.friday => calendar => calendar.friday.ToString().Contains(keyword),
            CalendarProperty.saturday => calendar => calendar.saturday.ToString().Contains(keyword),
            CalendarProperty.sunday => calendar => calendar.sunday.ToString().Contains(keyword),
            CalendarProperty.start_date => calendar => calendar.start_date.ToLower().Contains(keyword.ToLower()),
            CalendarProperty.end_date => calendar => calendar.end_date.ToLower().Contains(keyword.ToLower()),
            _ => null
        };

        if (orderingSelector != null && whereSelector != null)
        {
            result = result.Where(whereSelector).OrderBy(orderingSelector).ToList();
        }

        if(!allowDuplicates)
        {
            List<string> strings = new List<string>();

            for (int i = result.Count - 1; i >= 0; i--)
            {
                string comparisonValue = calendarProperty switch
                {
                    CalendarProperty.service_id => result[i].service_id,
                    CalendarProperty.monday => result[i].monday.ToString(),
                    CalendarProperty.tuesday => result[i].tuesday.ToString(),
                    CalendarProperty.wednesday => result[i].wednesday.ToString(),
                    CalendarProperty.thursday => result[i].thursday.ToString(),
                    CalendarProperty.friday => result[i].friday.ToString(),
                    CalendarProperty.saturday => result[i].saturday.ToString(),
                    CalendarProperty.sunday => result[i].sunday.ToString(),
                    CalendarProperty.start_date => result[i].start_date,
                    CalendarProperty.end_date => result[i].end_date,
                    _ => null
                !};

                if (!strings.Contains(comparisonValue!))
                {
                    strings.Add(comparisonValue!);
                }
                else
                {
                    result.RemoveAt(i);
                }
            }
        }

        return result.ToArray();
    }

    public static DateTime DateStringToDate(string date)
    {
        int year = int.Parse(date[0].ToString() + date[1].ToString() + date[2].ToString() + date[3].ToString());
        int month = int.Parse(date[4].ToString() + date[5].ToString());
        int day = int.Parse(date[6].ToString() + date[7].ToString());
        DateTime dateTime = new DateTime(year, month, day);
        return dateTime;
    }

    internal static string CorrectTime(string time)
    {
        string[] timeParts = time.Split(':');
        if (timeParts.Length == 3)
        {
            int hour = int.Parse(timeParts[0]);
            int minute = int.Parse(timeParts[1]);
            int second = int.Parse(timeParts[2]);

            if (hour >= 24)
            {
                hour %= 24;
                return hour.ToString("00") + ":" + minute.ToString("00") + ":" + second.ToString("00");
            }
            else
            {
                return time;
            }
        }
        else
        {
            return "Invalid Time Format";
        }
    }

}
