using System;

namespace DapperRabbitMQ.ServiceInterfaces;

public interface IDepartmentService
{
    Task<IEnumerable<Models.Department>> GetAllDepartmentsAsync();
}
