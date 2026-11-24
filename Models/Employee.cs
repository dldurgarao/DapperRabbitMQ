using System;
using Microsoft.EntityFrameworkCore;

namespace DapperRabbitMQ.Models;

public class Employee
{
    public required string EmployeeId { get; set; }
    public string Name { get; set; } = default!;
    public string Email { get; set; } = default!;
    public string Department { get; set; } = default!;
    [Precision(18, 2)]
    public decimal Salary { get; set; }
    public DateTime JoinedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
}

public record EmployeeRequest(string Name, string Email, string Department, decimal Salary)
{
    public Employee ToEmployee(string id) => new()
    {
        EmployeeId = id, Name = Name, Email = Email, Department = Department, Salary = Salary
    };
}
