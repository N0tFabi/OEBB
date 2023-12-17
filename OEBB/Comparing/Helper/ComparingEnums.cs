using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OEBB.Comparing;

public enum CalendarCompareMethod
{
    service_id, monday, tuesday, wednesday, thursday, friday, saturday, sunday, start_date, end_date
}

public enum CalendarDateCompareMethod
{
    service_id, date, exception_type
}

public enum RouteCompareMethod
{
    route_id, agency_id, route_short_name, route_long_name, route_desc, route_type, route_url, route_color, route_text_color
}

public enum ShapeCompareMethod
{
    shape_id, shape_pt_lat, shape_pt_lon, shape_pt_sequence, shape_dist_traveled
}

public enum StopCompareMethod
{
    stop_id, stop_code, stop_name, stop_desc, stop_lat, stop_lon, zone_id, stop_url, location_type, parent_station, stop_timezone, wheelchair_boarding
}

public enum StopTimeCompareMethod
{
    trip_id, arrival_time, departure_time, stop_id, stop_sequence, stop_headsign, pickup_type, drop_off_type, shape_dist_traveled, timepoint
}

public enum TripCompareMethod
{
    route_id, service_id, trip_id, shape_id, trip_headsign, direction_id, block_id
}