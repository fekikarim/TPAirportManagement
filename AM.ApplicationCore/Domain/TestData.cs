namespace AM.ApplicationCore.Domain;

public static class TestData
{
    public static List<Plane> planes = new()
    {
        new Plane
        {
            PlaneId = 1,
            PlaneType = PlaneType.Boing,
            Capacity = 150,
            ManufactureDate = new DateTime(2015, 2, 3)
        },
        new Plane
        {
            PlaneId = 2,
            PlaneType = PlaneType.Airbus,
            Capacity = 250,
            ManufactureDate = new DateTime(2020, 11, 11)
        }
    };

    public static List<Staff> staff = new()
    {
        new Staff
        {
            Id = 1,
            FirstName = "captain",
            LastName = "captain",
            EmailAddress = "Captain.captain@gmail.com",
            BirthDate = new DateTime(1965, 1, 1),
            EmploymentDate = new DateTime(1999, 1, 1),
            Salary = 99999,
            Function = "Captain",
            PassportNumber = "CPT0001",
            TelNumber = 11111111
        },
        new Staff
        {
            Id = 2,
            FirstName = "hostess1",
            LastName = "hostess1",
            EmailAddress = "hostess1.hostess1@gmail.com",
            BirthDate = new DateTime(1995, 1, 1),
            EmploymentDate = new DateTime(2020, 1, 1),
            Salary = 999,
            Function = "Hostess",
            PassportNumber = "HST0001",
            TelNumber = 22222222
        },
        new Staff
        {
            Id = 3,
            FirstName = "hostess2",
            LastName = "hostess2",
            EmailAddress = "hostess2.hostess2@gmail.com",
            BirthDate = new DateTime(1996, 1, 1),
            EmploymentDate = new DateTime(2020, 1, 1),
            Salary = 999,
            Function = "Hostess",
            PassportNumber = "HST0002",
            TelNumber = 33333333
        }
    };

    public static List<Traveller> travellers = new()
    {
        new Traveller
        {
            Id = 10,
            FirstName = "Traveller1",
            LastName = "Traveller1",
            EmailAddress = "Traveller1.Traveller1@gmail.com",
            BirthDate = new DateTime(1980, 1, 1),
            HealthInformation = "No troubles",
            Nationality = "American",
            PassportNumber = "TRV0001",
            TelNumber = 44444444
        },
        new Traveller
        {
            Id = 11,
            FirstName = "Traveller2",
            LastName = "Traveller2",
            EmailAddress = "Traveller2.Traveller2@gmail.com",
            BirthDate = new DateTime(1981, 1, 1),
            HealthInformation = "Some troubles",
            Nationality = "French",
            PassportNumber = "TRV0002",
            TelNumber = 55555555
        },
        new Traveller
        {
            Id = 12,
            FirstName = "Traveller3",
            LastName = "Traveller3",
            EmailAddress = "Traveller3.Traveller3@gmail.com",
            BirthDate = new DateTime(1982, 1, 1),
            HealthInformation = "No troubles",
            Nationality = "Tunisian",
            PassportNumber = "TRV0003",
            TelNumber = 66666666
        },
        new Traveller
        {
            Id = 13,
            FirstName = "Traveller4",
            LastName = "Traveller4",
            EmailAddress = "Traveller4.Traveller4@gmail.com",
            BirthDate = new DateTime(1983, 1, 1),
            HealthInformation = "Some troubles",
            Nationality = "American",
            PassportNumber = "TRV0004",
            TelNumber = 77777777
        },
        new Traveller
        {
            Id = 14,
            FirstName = "Traveller5",
            LastName = "Traveller5",
            EmailAddress = "Traveller5.Traveller5@gmail.com",
            BirthDate = new DateTime(1984, 1, 1),
            HealthInformation = "Some troubles",
            Nationality = "Spanish",
            PassportNumber = "TRV0005",
            TelNumber = 88888888
        }
    };

    public static List<Flight> listFlights = new()
    {
        new Flight
        {
            FlightId = 1,
            Departure = "Tunis",
            Destination = "Paris",
            FlightDate = new DateTime(2022, 1, 1, 15, 10, 10),
            EffectiveArrival = new DateTime(2022, 1, 1, 17, 10, 10),
            EstimatedDuration = 110,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Airbus),
            Passengers = travellers.Cast<Passenger>().ToList()
        },
        new Flight
        {
            FlightId = 2,
            Departure = "Tunis",
            Destination = "Paris",
            FlightDate = new DateTime(2022, 2, 1, 21, 10, 10),
            EffectiveArrival = new DateTime(2022, 2, 1, 23, 10, 10),
            EstimatedDuration = 105,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Boing)
        },
        new Flight
        {
            FlightId = 3,
            Departure = "Tunis",
            Destination = "Paris",
            FlightDate = new DateTime(2022, 3, 1, 5, 10, 10),
            EffectiveArrival = new DateTime(2022, 3, 1, 6, 40, 10),
            EstimatedDuration = 100,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Boing)
        },
        new Flight
        {
            FlightId = 4,
            Departure = "Tunis",
            Destination = "Madrid",
            FlightDate = new DateTime(2022, 4, 1, 6, 10, 10),
            EffectiveArrival = new DateTime(2022, 4, 1, 8, 10, 10),
            EstimatedDuration = 130,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Boing)
        },
        new Flight
        {
            FlightId = 5,
            Departure = "Tunis",
            Destination = "Madrid",
            FlightDate = new DateTime(2022, 5, 1, 17, 10, 10),
            EffectiveArrival = new DateTime(2022, 5, 1, 18, 50, 10),
            EstimatedDuration = 105,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Boing)
        },
        new Flight
        {
            FlightId = 6,
            Departure = "Tunis",
            Destination = "Lisbonne",
            FlightDate = new DateTime(2022, 6, 1, 20, 10, 10),
            EffectiveArrival = new DateTime(2022, 6, 1, 22, 30, 10),
            EstimatedDuration = 200,
            Plane = planes.Single(p => p.PlaneType == PlaneType.Airbus)
        }
    };

    static TestData()
    {
        foreach (Flight flight in listFlights)
        {
            if (flight.Plane is not null && !flight.Plane.Flights.Contains(flight))
            {
                flight.Plane.Flights.Add(flight);
            }

            foreach (Passenger passenger in flight.Passengers)
            {
                if (!passenger.Flights.Contains(flight))
                {
                    passenger.Flights.Add(flight);
                }
            }
        }
    }
}
