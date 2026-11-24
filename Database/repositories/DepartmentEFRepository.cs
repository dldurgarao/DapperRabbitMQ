using System;
using DapperRabbitMQ.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using DapperRabbitMQ.Database.repositories;

namespace DapperRabbitMQ.Database.repositories;

public class DepartmentEFRepository:IDepartmentEFRepository
{
    private readonly AppDBContext _context;
    public DepartmentEFRepository(AppDBContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
    {
        return await _context.Departments.ToListAsync();
    }
}
