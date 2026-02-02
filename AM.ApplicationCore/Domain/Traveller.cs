namespace AM.ApplicationCore.Domain;

public class Traveller : Passenger
{
    public string HealthInformation { get; set; } = string.Empty;
    public string Nationality { get; set; } = string.Empty;

    public override string PassengerType()
        => $"{base.PassengerType()} I am a traveller";

    public override string ToString()
        => $"Traveller {{ {base.ToString()}, Nationality={Nationality}, HealthInformation={HealthInformation} }}";
}
