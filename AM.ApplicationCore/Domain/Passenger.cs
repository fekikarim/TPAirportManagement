namespace AM.ApplicationCore.Domain;

public class Passenger
{
    public DateTime BirthDate { get; set; }
    public string EmailAddress { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;
    public string PassportNumber { get; set; } = string.Empty;
    public int TelNumber { get; set; }

    public ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public bool CheckProfile(string lastName, string firstName)
        => CheckProfile(lastName, firstName, null, checkEmail: false);

    public bool CheckProfile(string lastName, string firstName, string email)
        => CheckProfile(lastName, firstName, email, checkEmail: true);

    public bool CheckProfile(string lastName, string firstName, string? email, bool checkEmail)
    {
        bool nameMatch =
            string.Equals(LastName, lastName, StringComparison.OrdinalIgnoreCase)
            && string.Equals(FirstName, firstName, StringComparison.OrdinalIgnoreCase);

        if (!nameMatch)
        {
            return false;
        }

        if (!checkEmail)
        {
            return true;
        }

        return string.Equals(EmailAddress, email ?? string.Empty, StringComparison.OrdinalIgnoreCase);
    }

    public virtual string PassengerType()
        => "I am a passenger";

    public override string ToString()
        => $"Passenger {{ Id={Id}, Name={FirstName} {LastName}, BirthDate={BirthDate:yyyy-MM-dd}, Passport={PassportNumber}, Email={EmailAddress}, Tel={TelNumber}, Flights={Flights?.Count ?? 0} }}";
}
