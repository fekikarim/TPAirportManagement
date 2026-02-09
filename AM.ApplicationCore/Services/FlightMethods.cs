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

    public int ProgrammedFlightNumber(DateTime startDate)
    {
        var query = from f in Flights
                    where f.FlightDate >= startDate && f.FlightDate < startDate.AddDays(7)
                    select f;

        return query.Count();
    }

    public double DurationAverage(string destination)
    {
        return Flights
            .Where(f => string.Equals(f.Destination, destination, StringComparison.OrdinalIgnoreCase))
            .Select(f => f.EstimatedDuration)
            .DefaultIfEmpty(0)
            .Average();
    }

    public IEnumerable<Flight> OrderedDurationFlights()
    {
        return Flights.OrderByDescending(f => f.EstimatedDuration);
    }

    public List<Traveller> SeniorTravellers(Flight flight)
    {
        return flight.Passengers
            .OfType<Traveller>()
            .OrderBy(t => t.BirthDate)
            .Take(3)
            .ToList();
    }

    public void DestinationGroupedFlights()
    {
        IEnumerable<IGrouping<string, Flight>> groups = Flights
            .GroupBy(f => f.Destination)
            .OrderBy(g => g.Key);

        foreach (IGrouping<string, Flight> group in groups)
        {
            Console.WriteLine($"Destination {group.Key}");

            foreach (Flight flight in group.OrderBy(f => f.FlightDate))
            {
                Console.WriteLine($"Décollage : {flight.FlightDate:dd/MM/yyyy HH : mm : ss}");
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
