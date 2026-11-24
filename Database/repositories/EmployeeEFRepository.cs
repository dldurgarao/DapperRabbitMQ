using System;
using DapperRabbitMQ.Models;
using Microsoft.EntityFrameworkCore;
using DapperRabbitMQ.Database.repositories;
using System.Linq;

namespace DapperRabbitMQ.Database.repositories;

public class EmployeeEFRepository:IEmployeeEFRepository
{
    private readonly AppDBContext _context;
    public EmployeeEFRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }
    public async Task<Employee?> GetEmployeeByIdAsync(string EmployeeId)
    {
        return await _context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == EmployeeId);
    }
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        employee.EmployeeId = Guid.NewGuid().ToString();
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
        return true;
    }
    public async Task<bool> DeleteEmployeeAsync(string EmployeeId)
    {
        var employee = await _context.Employees.FirstOrDefaultAsync(emp => emp.EmployeeId == EmployeeId);
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
