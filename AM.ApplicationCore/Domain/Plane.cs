using System.ComponentModel.DataAnnotations;

namespace AM.ApplicationCore.Domain;

public class Plane
{
    public Plane()
    {
    }

    public Plane(PlaneType pt, int capacity, DateTime date)
    {
        PlaneType = pt;
        Capacity = capacity;
        ManufactureDate = date;
    }

    [Range(1, int.MaxValue, ErrorMessage = "Capacity must be a positive integer.")]
    public int Capacity { get; set; }
    public DateTime ManufactureDate { get; set; }
    public int PlaneId { get; set; }
    public PlaneType PlaneType { get; set; }

    public ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public override string ToString()
        => $"Plane {{ Id={PlaneId}, Type={PlaneType}, Capacity={Capacity}, ManufactureDate={ManufactureDate:yyyy-MM-dd}, Flights={Flights?.Count ?? 0} }}";
}
