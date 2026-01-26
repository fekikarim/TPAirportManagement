namespace AM.ApplicationCore.Domain;

public class Flight
{
    public string Departure { get; set; } = string.Empty;
    public string Destination { get; set; } = string.Empty;
    public DateTime EffectiveArrival { get; set; }
    public int EstimatedDuration { get; set; }
    public DateTime FlightDate { get; set; }
    public int FlightId { get; set; }

    public Plane? Plane { get; set; }
    public ICollection<Passenger> Passengers { get; set; } = new List<Passenger>();

    public override string ToString()
        => $"Flight {{ Id={FlightId}, {Departure}->{Destination}, Date={FlightDate:yyyy-MM-dd HH:mm}, EstimatedDuration={EstimatedDuration}, EffectiveArrival={EffectiveArrival:yyyy-MM-dd HH:mm}, PlaneId={Plane?.PlaneId}, Passengers={Passengers?.Count ?? 0} }}";
}
