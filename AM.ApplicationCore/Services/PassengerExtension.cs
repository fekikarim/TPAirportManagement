using System.Runtime.CompilerServices;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Services;

public static class PassengerExtension
{

    public static void UpperFullName(this Passenger passenger)
    {
        passenger.FirstName = passenger.FirstName[0].ToString().ToUpper() 
        + passenger.FirstName.Substring(1);

        passenger.LastName = passenger.LastName[0].ToString().ToUpper()
        + passenger.LastName.Substring(1);
    }

}
