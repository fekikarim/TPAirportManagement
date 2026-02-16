using System.Globalization;
using System.Reflection;
using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Interfaces;
namespace AM.ApplicationCore.Services;

public class FlightMethods : IFlightMethods
{
    public List<Flight> Flights { get; set; } = new();

    public List<DateTime> GetFlightDates(string destination)
    {
        return Flights
            .Where(f => string.Equals(f.Destination, destination, StringComparison.OrdinalIgnoreCase))
            .Select(f => f.FlightDate)
            .ToList();
    }

    public List<DateTime> GetFlightDatesForeach(string destination)
    {
        List<DateTime> dates = new();

        foreach (Flight flight in Flights)
        {
            if (string.Equals(flight.Destination, destination, StringComparison.OrdinalIgnoreCase))
            {
                dates.Add(flight.FlightDate);
            }
        }

        return dates;
    }

    public List<Flight> GetFlights(string filterType, string filterValue)
    {
        if (string.IsNullOrWhiteSpace(filterType))
        {
            throw new ArgumentException("filterType is required.", nameof(filterType));
        }

        PropertyInfo? property = typeof(Flight).GetProperty(
            filterType,
            BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);

        if (property is null)
        {
            throw new ArgumentException($"Unknown Flight property '{filterType}'.", nameof(filterType));
        }

        Type propertyType = Nullable.GetUnderlyingType(property.PropertyType) ?? property.PropertyType;
        object? typedFilterValue = ConvertStringTo(propertyType, filterValue);

        List<Flight> result = new();

        foreach (Flight flight in Flights)
        {
            object? rawValue = property.GetValue(flight);
            if (IsMatch(propertyType, rawValue, typedFilterValue, filterValue))
            {
                result.Add(flight);
            }
        }

        foreach (Flight flight in result)
        {
            Console.WriteLine(flight);
        }

        return result;
    }

    public void ShowFlightDetails(Plane plane)
    {
        IEnumerable<Flight> flights = Flights.Where(f => f.Plane == plane);

        foreach (Flight flight in flights)
        {
            Console.WriteLine($"{flight.FlightDate:dd/MM/yyyy HH:mm:ss} - {flight.Destination}");
        }
    }

    //* 3
    public int ProgrammedFlightNumber(DateTime startDate)
    {
        // var query = from f in Flights
        //             where f.FlightDate >= startDate && f.FlightDate < startDate.AddDays(7)
        //             select f;
        // return query.Count();
        //**************************

        return Flights.Count(f => f.FlightDate >= startDate 
        && f.FlightDate < startDate.AddDays(7));
    }

    //* 4
    public double DurationAverage(string destination)
    {
        // var query = from f in Flights
        //             where string.Equals(f.Destination, destination, StringComparison.OrdinalIgnoreCase)
        //             select f.EstimatedDuration;
        // return query.DefaultIfEmpty(0).Average();
        //**************************

        return Flights
            .Where(f => string.Equals(f.Destination, destination, StringComparison.OrdinalIgnoreCase))
            .Select(f => f.EstimatedDuration)
            .DefaultIfEmpty(0)
            .Average();
    }

    //* 5
    public IEnumerable<Flight> OrderedDurationFlights()
    {
        // var query = from f in Flights
        //             orderby f.EstimatedDuration descending
        //             select f;
        // return query;
        //**************************

        return Flights.OrderByDescending(f => f.EstimatedDuration);
    }

    //* 6
    public List<Traveller> SeniorTravellers(Flight flight)
    {
        // var query = from t in flight.Passengers.OfType<Traveller>()
        //             orderby t.BirthDate
        //             select t;
        // return query.Take(3).ToList();
        //**************************

        return flight.Passengers
            .OfType<Traveller>()
            .OrderBy(t => t.BirthDate)
            .Take(3)
            .ToList();
    }

    //* 7
    public void DestinationGroupedFlights()
    {
        // var query = from flight in Flights
        //             group flight by flight.Destination into destinationGroup
        //             orderby destinationGroup.Key
        //             select destinationGroup;
        // foreach (var dGroup in query)
        // {
        //     Console.WriteLine($"Destination {dGroup.Key}");
        //     var orderedFlights = from f in dGroup
        //                          orderby f.FlightDate
        //                          select f;
        //     foreach (var flight in orderedFlights)
        //     {
        //         Console.WriteLine($"Décollage : {flight.FlightDate:dd/MM/yyyy HH:mm:ss}");
        //     }
        // }
        //**************************

        var groups = Flights
            .GroupBy(f => f.Destination)
            .OrderBy(g => g.Key);

        foreach (var dGroup in groups)
        {
            Console.WriteLine($"Destination {dGroup.Key}");

            foreach (var flight in dGroup.OrderBy(f => f.FlightDate))
            {
                Console.WriteLine($"Décollage : {flight.FlightDate:dd/MM/yyyy HH:mm:ss}");
            }
        }
    }

    private static object? ConvertStringTo(Type targetType, string value)
    {
        if (targetType == typeof(string))
        {
            return value;
        }

        if (targetType == typeof(int))
        {
            return int.TryParse(value, NumberStyles.Integer, CultureInfo.InvariantCulture, out int i) ? i : null;
        }

        if (targetType == typeof(double))
        {
            return double.TryParse(value, NumberStyles.Float, CultureInfo.InvariantCulture, out double d) ? d : null;
        }

        if (targetType == typeof(DateTime))
        {
            return DateTime.TryParse(value, CultureInfo.InvariantCulture, DateTimeStyles.AssumeLocal, out DateTime dt) ? dt : null;
        }

        if (targetType.IsEnum)
        {
            return Enum.TryParse(targetType, value, ignoreCase: true, out object? e) ? e : null;
        }

        try
        {
            return Convert.ChangeType(value, targetType, CultureInfo.InvariantCulture);
        }
        catch
        {
            return null;
        }
    }

    private static bool IsMatch(Type propertyType, object? rawValue, object? typedFilterValue, string originalFilterValue)
    {
        if (propertyType == typeof(string))
        {
            return string.Equals(rawValue as string, originalFilterValue, StringComparison.OrdinalIgnoreCase);
        }

        if (typedFilterValue is null)
        {
            return false;
        }

        if (propertyType == typeof(DateTime))
        {
            if (rawValue is not DateTime dtRaw || typedFilterValue is not DateTime dtFilter)
            {
                return false;
            }

            return dtRaw == dtFilter;
        }

        return Equals(rawValue, typedFilterValue);
    }
}
