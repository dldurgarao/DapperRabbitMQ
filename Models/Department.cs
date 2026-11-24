using System;
using Microsoft.EntityFrameworkCore;

namespace DapperRabbitMQ.Models;

public class Department
{
    public string DepartmentId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
    public bool IsActive { get; set; } = true;
    public override string ToString()
    {
        return $"DepartmentId: {this.DepartmentId}, Name: {this.Name}, Description: {this.Description}, CreatedOn: {this.CreatedOn.ToLocalTime()}, IsActive: {this.IsActive}";
    }
}
