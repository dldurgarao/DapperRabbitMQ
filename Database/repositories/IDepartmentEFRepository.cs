using System;
using DapperRabbitMQ.Models;
namespace DapperRabbitMQ.Database.repositories;

public interface IDepartmentEFRepository
{
    Task<IEnumerable<Department>> GetAllDepartmentsAsync();
}
