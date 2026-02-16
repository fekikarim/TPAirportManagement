using System.ComponentModel.DataAnnotations;

namespace AM.ApplicationCore.Domain;

public class Passenger
{
    [Display(Name = "Date of Birth")]
    [DataType(DataType.Date)]
    public DateTime BirthDate { get; set; }

    [EmailAddress(ErrorMessage = "EmailAddress must be a valid email.")]
    public string EmailAddress { get; set; } = string.Empty;

    [StringLength(25, MinimumLength = 3, ErrorMessage = "FirstName must be between 3 and 25 characters.")]
    public string FirstName { get; set; } = string.Empty;
    public int Id { get; set; }
    public string LastName { get; set; } = string.Empty;

    [Key]
    [StringLength(7, MinimumLength = 7, ErrorMessage = "PassportNumber must be 7 characters.")]
    public string PassportNumber { get; set; } = string.Empty;

    [Range(10000000, 99999999, ErrorMessage = "TelNumber must be 8 digits.")]
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
