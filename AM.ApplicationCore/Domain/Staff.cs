using System.ComponentModel.DataAnnotations;

namespace AM.ApplicationCore.Domain;

public class Staff : Passenger
{
    public DateTime EmploymentDate { get; set; }
    public string Function { get; set; } = string.Empty;

    [DataType(DataType.Currency)]
    public double Salary { get; set; }

    public override string PassengerType()
        => $"{base.PassengerType()} I am a Staff Member";

    public override string ToString()
        => $"Staff {{ {base.ToString()}, EmploymentDate={EmploymentDate:yyyy-MM-dd}, Function={Function}, Salary={Salary} }}";
}
