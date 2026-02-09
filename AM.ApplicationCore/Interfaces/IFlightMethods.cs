using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Interfaces;

public interface IFlightMethods
{
    List<Flight> Flights { get; set; }

    List<DateTime> GetFlightDates(string destination);
    List<DateTime> GetFlightDatesForeach(string destination);
    List<Flight> GetFlights(string filterType, string filterValue);

    void ShowFlightDetails(Plane plane);
    int ProgrammedFlightNumber(DateTime startDate);
    double DurationAverage(string destination);
    IEnumerable<Flight> OrderedDurationFlights();
    List<Traveller> SeniorTravellers(Flight flight);
    void DestinationGroupedFlights();
}
