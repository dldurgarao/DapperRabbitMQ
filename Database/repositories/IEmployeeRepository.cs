using System;
using DapperRabbitMQ.Models;

namespace DapperRabbitMQ.Database.repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetEmployeeByIdAsync(string EmployeeId);
    Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    Task<bool> AddEmployeeAsync(Employee employee);
    Task<bool> UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(string EmployeeId);
}
