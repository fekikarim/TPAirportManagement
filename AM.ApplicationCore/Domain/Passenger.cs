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

    public override string ToString()
        => $"Passenger {{ Id={Id}, Name={FirstName} {LastName}, BirthDate={BirthDate:yyyy-MM-dd}, Passport={PassportNumber}, Email={EmailAddress}, Tel={TelNumber}, Flights={Flights?.Count ?? 0} }}";
}
