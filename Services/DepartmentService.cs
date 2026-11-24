using System;
using DapperRabbitMQ.Database.repositories;
using DapperRabbitMQ.ServiceInterfaces;
using Microsoft.Extensions.Caching.Distributed;

namespace DapperRabbitMQ.Services;

public class DepartmentService:IDepartmentService
{
    private readonly IDepartmentEFRepository _departmentRepository;
    private readonly IDistributedCache _cache;
    public DepartmentService(IDepartmentEFRepository departmentRepository, IDistributedCache cache)
    {
        _departmentRepository = departmentRepository;
        _cache = cache;
    }

    public async Task<IEnumerable<Models.Department>> GetAllDepartmentsAsync()
    {
        var departments = await _cache.GetStringAsync("Departments");
        if (departments != null)
        {
            Console.WriteLine("Departments retrieved from cache.");
            return System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Models.Department>>(departments)
                ?? Array.Empty<Models.Department>();
        }
        Console.WriteLine("Departments not found in cache. Retrieving from database.");
        var departmentsFromDb = await _departmentRepository.GetAllDepartmentsAsync();
        await _cache.SetStringAsync("Departments", System.Text.Json.JsonSerializer.Serialize(departmentsFromDb));
        return departmentsFromDb;
    }
}
