using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using DapperRabbitMQ.Auth;

namespace DapperRabbitMQ.Database;

public class AppDBContext : IdentityDbContext<ApplicationUsers, ApplicationRoles,Guid>
{
    public AppDBContext(DbContextOptions options) : base(options)
    {
    }
    public AppDBContext()
    {
        
    }
    public DbSet<Models.Employee> Employees { get; set; }
    public DbSet<Models.Department> Departments { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<Models.Department>().HasData(
            new Models.Department { DepartmentId = "1", Name = "HR", Description = "Human Resources", CreatedOn = DateTime.UtcNow, IsActive = true },
            new Models.Department { DepartmentId = "2", Name = "IT", Description = "Information Technology", CreatedOn = DateTime.UtcNow, IsActive = true },
            new Models.Department { DepartmentId = "3", Name = "Finance", Description = "Finance and Accounting", CreatedOn = DateTime.UtcNow, IsActive = true }
        );
        
    }

}
