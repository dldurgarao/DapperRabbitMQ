using System;
using DapperRabbitMQ.Models;

namespace DapperRabbitMQ.ServiceInterfaces;

public interface IEmployeeService
{
    public Task<Employee?> GetEmployeeByIdAsync(string EmployeeId);
    public Task<IEnumerable<Employee>> GetAllEmployeesAsync();
    public Task<bool> AddEmployeeAsync(Employee employee);
    public Task<bool> UpdateEmployeeAsync(Employee employee);
    public Task<bool> DeleteEmployeeAsync(string EmployeeId);
}
