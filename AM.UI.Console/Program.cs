﻿﻿using AM.ApplicationCore.Domain;
using AM.ApplicationCore.Services;

Plane plane1 = new Plane();
plane1.PlaneId = 1;
plane1.PlaneType = PlaneType.Boing;
plane1.Capacity = 180;
plane1.ManufactureDate = new DateTime(2018, 6, 1);

Plane plane2 = new Plane(PlaneType.Airbus, 220, new DateTime(2020, 3, 15))
{
	PlaneId = 2
};

Plane plane3 = new Plane
{
	PlaneId = 3,
	PlaneType = PlaneType.Boing,
	Capacity = 160,
	ManufactureDate = new DateTime(2016, 11, 20)
};

Console.WriteLine(plane1);
Console.WriteLine(plane2);
Console.WriteLine(plane3);

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- PassengerType() ---");

Passenger p = new Passenger
{
	Id = 10,
	FirstName = "Karim",
	LastName = "Feki",
	EmailAddress = "karim@example.com",
	BirthDate = new DateTime(2000, 1, 1),
	PassportNumber = "TN123456",
	TelNumber = 12345678
};

Staff s = new Staff
{
	Id = 11,
	FirstName = "Sami",
	LastName = "Ben Ali",
	EmailAddress = "sami@example.com",
	BirthDate = new DateTime(1995, 5, 5),
	PassportNumber = "TN654321",
	TelNumber = 87654321,
	EmploymentDate = new DateTime(2022, 9, 1),
	Function = "Pilot",
	Salary = 5000
};

Traveller t = new Traveller
{
	Id = 12,
	FirstName = "Meriem",
	LastName = "Trabelsi",
	EmailAddress = "meriem@example.com",
	BirthDate = new DateTime(2001, 3, 3),
	PassportNumber = "TN777777",
	TelNumber = 22223333,
	Nationality = "Tunisian",
	HealthInformation = "None"
};

Console.WriteLine(p.PassengerType());
Console.WriteLine(s.PassengerType());
Console.WriteLine(t.PassengerType());

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- CheckProfile() ---");
Console.WriteLine(p.CheckProfile("Feki", "Karim"));
Console.WriteLine(p.CheckProfile("Feki", "Karim", "karim@example.com"));
Console.WriteLine(p.CheckProfile("Feki", "Karim", "wrong@example.com"));

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- TestData + FlightMethods ---");
FlightMethods flightService = new FlightMethods();
flightService.Flights = TestData.listFlights;
Console.WriteLine($"Flights loaded: {flightService.Flights.Count}");

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- GetFlightDates() ---");
foreach (DateTime d in flightService.GetFlightDates("Paris"))
{
	Console.WriteLine(d);
}

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- GetFlightDates() ---");
foreach (DateTime d in flightService.GetFlightDatesForeach("Paris"))
{
	Console.WriteLine(d);
}

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- GetFlights() ---");
flightService.GetFlights("Destination", "Paris");

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- GetFlights() ---");
flightService.GetFlights("EstimatedDuration", "110");

Console.WriteLine("\n######################################");
Console.WriteLine("\n############ Partie 3 ###############");
Console.WriteLine("\n######################################");
Console.WriteLine("\n--- ShowFlightDetails(Plane) ---");
Plane planeForDetails = TestData.planes.First(p => p.PlaneType == PlaneType.Airbus);
flightService.ShowFlightDetails(planeForDetails);

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- ProgrammedFlightNumber(startDate) ---");
int countWeek = flightService.ProgrammedFlightNumber(new DateTime(2022, 1, 1));
Console.WriteLine(countWeek);

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- DurationAverage(Paris) ---");
Console.WriteLine(flightService.DurationAverage("Paris"));

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- OrderedDurationFlights() ---");
foreach (Flight f in flightService.OrderedDurationFlights())
{
	Console.WriteLine(f);
}

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- SeniorTravellers(flight) ---");
Flight flightWithTravellers = flightService.Flights.First();
foreach (Traveller traveller in flightService.SeniorTravellers(flightWithTravellers))
{
	Console.WriteLine(traveller);
}

Console.WriteLine("\n######################################");
Console.WriteLine("\n--- DestinationGroupedFlights() ---");
flightService.DestinationGroupedFlights();

Console.WriteLine("\n######################################");
Console.WriteLine("\n-----------------------");
Passenger passengerUpper = new Passenger
{
	FirstName = "karim",
	LastName = "feki"
};
Console.WriteLine($"{passengerUpper.FirstName} {passengerUpper.LastName}");
Console.WriteLine("-----------------------");
Console.WriteLine("\n--- UpperFullName ---");
passengerUpper.UpperFullName();
Console.WriteLine($"{passengerUpper.FirstName} {passengerUpper.LastName}");
Console.WriteLine("-----------------------");