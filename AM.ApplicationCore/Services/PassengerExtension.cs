using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Services;

public static class PassengerExtension
{
    public static string UpperFullName(this Passenger passenger)
    {
        string firstName = CapitalizeFirstLetter(passenger.FirstName);
        string lastName = CapitalizeFirstLetter(passenger.LastName);

        return $"{firstName} {lastName}".Trim();
    }

    private static string CapitalizeFirstLetter(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            return string.Empty;
        }

        string trimmed = value.Trim();
        if (trimmed.Length == 1)
        {
            return trimmed.ToUpperInvariant();
        }

        return char.ToUpperInvariant(trimmed[0]) + trimmed[1..];
    }
}
