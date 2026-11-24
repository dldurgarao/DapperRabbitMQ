using System;
using DapperRabbitMQ.Models;
using DapperRabbitMQ.Database.repositories;
using DapperRabbitMQ.Database;
using Microsoft.Data.SqlClient;
using Dapper;


namespace DapperRabbitMQ.Database.repositories;

public class EmployeeDPRepository:IEmployeeDPRepository
{
   private readonly string _connectionString;

    public EmployeeDPRepository(IConfiguration configuration)
    {
        _connectionString = configuration.GetConnectionString("DefaultConnection")
            ?? throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.");
    }
    public async Task<IEnumerable<Employee>> GetAllEmployeesAsync()
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QueryAsync<Employee>("SELECT * FROM Employees");
    }
    public async Task<Employee?> GetEmployeeByIdAsync(string EmployeeId)
    {
        using var connection = new SqlConnection(_connectionString);
        return await connection.QuerySingleOrDefaultAsync<Employee>("SELECT * FROM Employees WHERE EmployeeId = @EmployeeId", new { EmployeeId = EmployeeId });
    }
    public async Task<bool> AddEmployeeAsync(Employee employee)
    {
        using var connection = new SqlConnection(_connectionString);
        employee.EmployeeId = Guid.NewGuid().ToString();
        var query = "INSERT INTO Employees (Name, Email, Department, Salary,EmployeeId) VALUES (@Name, @Email, @Department, @Salary,@EmployeeId)";
        await connection.ExecuteAsync(query, employee);
        return true;
    }
    public async Task<bool> UpdateEmployeeAsync(Employee employee)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "UPDATE Employees SET Name = @Name, Email = @Email, Department = @Department, Salary = @Salary WHERE EmployeeId = @EmployeeId";
        await connection.ExecuteAsync(query, employee);
        return true;
    }
    public async Task<bool> DeleteEmployeeAsync(string EmployeeId)
    {
        using var connection = new SqlConnection(_connectionString);
        var query = "DELETE FROM Employees WHERE EmployeeId = @EmployeeId";
        await connection.ExecuteAsync(query, new { EmployeeId = EmployeeId });
        return true;
    }
}
