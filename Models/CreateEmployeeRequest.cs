namespace EmployeeManagement.Models;

/// <summary>
/// Request Model for creating an emåployee
/// </summary>
public record CreateEmployeeRequest
{
    public string Title { get; init; }
    public string FirstName { get; init;}

    public string LastName { get; init; }

    public string Address { get; init; }

    public int Salary { get; init; }

    public int DepartmentID { get; init; }
}
