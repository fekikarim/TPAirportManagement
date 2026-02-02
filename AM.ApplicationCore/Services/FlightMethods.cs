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
        List<DateTime> dates = new();

        for (int i = 0; i < Flights.Count; i++)
        {
            Flight flight = Flights[i];
            if (string.Equals(flight.Destination, destination, StringComparison.OrdinalIgnoreCase))
            {
                dates.Add(flight.FlightDate);
            }
        }

        return dates;
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
