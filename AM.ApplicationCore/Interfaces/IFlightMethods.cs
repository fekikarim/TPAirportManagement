using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Interfaces;

public interface IFlightMethods
{
    List<Flight> Flights { get; set; }

    List<DateTime> GetFlightDates(string destination);
    List<DateTime> GetFlightDatesForeach(string destination);
    List<Flight> GetFlights(string filterType, string filterValue);
}
