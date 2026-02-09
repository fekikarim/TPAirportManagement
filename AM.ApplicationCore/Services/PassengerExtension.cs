using System.Runtime.CompilerServices;
using AM.ApplicationCore.Domain;

namespace AM.ApplicationCore.Services;

public static class PassengerExtension
{
    public static string UpperFullName(this Passenger passenger)
    {
        string firstName = passenger.FirstName[0].ToString().ToUpper() 
        + passenger.FirstName.Substring(1);

        string lastName = passenger.LastName[0].ToString().ToUpper()
        + passenger.LastName.Substring(1);

        return $"{firstName} {lastName}".Trim();
    }
    
}
