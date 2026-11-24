using System;

namespace DapperRabbitMQ.Services;
using DapperRabbitMQ.Models;
using DapperRabbitMQ.ServiceInterfaces;
using DapperRabbitMQ.Database.repositories;

public class EmployeeService: IEmployeeService
{
    private readonly IEmployeeRepository _employeeRepository;
    public EmployeeService(IEmployeeEFRepository employeeEFRepository, IEmployeeDPRepository employeeDPRepository)
    {
        // _employeeRepository = employeeEFRepository;
        _employeeRepository = employeeDPRepository;
    }
    public async Task<Employee?> GetEmployeeByIdAsync(string EmployeeId)
    {
        return await _employeeRepository.GetEmployeeByIdAsync(EmployeeId);
    }
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _employeeRepository.GetAllEmployeesAsync();
    }
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        return await _employeeRepository.AddEmployeeAsync(employee);
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        return await _employeeRepository.UpdateEmployeeAsync(employee);
    }
    public async Task<bool> DeleteEmployeeAsync(string EmployeeId)
    {
        return await _employeeRepository.DeleteEmployeeAsync(EmployeeId);
    }
}
