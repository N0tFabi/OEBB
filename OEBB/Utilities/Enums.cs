namespace OEBB;

public enum TripProperty
{
    route_id, service_id, trip_id, shape_id, trip_headsign, direction_id, block_id
}

public enum StopProperty
{
    stop_id, stop_name, stop_lat, stop_lon, zone_id
}

public enum StopTimeProperty
{
    trip_id, arrival_time, departure_time, stop_id, stop_sequence, stop_headsign, pickup_type, drop_off_type, shape_dist_traveled
}

public enum ShapeProperty
{
    shape_id, shape_pt_lat, shape_pt_lon, shape_pt_sequence, shape_dist_traveled
}

public enum RouteProperty
{
    route_id, agency_id, route_short_name, route_long_name, route_type
}

public enum CalendarDateProperty
{
    service_id, date, exception_type
}

public enum CalendarProperty
{
    service_id, monday, tuesday, wednesday, thursday, friday, saturday, sunday, start_date, end_date
}