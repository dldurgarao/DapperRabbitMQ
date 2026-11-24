using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using DapperRabbitMQ.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using RabbitMQ.Client;
using DapperRabbitMQ.MessageBus;

namespace DapperRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        private readonly Publisher _publisher;
        public DepartmentController(IDepartmentService departmentService, Publisher publisher)
        {
            _departmentService = departmentService;
            _publisher = publisher;
        }
        [HttpGet("GetDepartments")]
        public IActionResult GetDepartments()
        {
            var departments = _departmentService.GetAllDepartmentsAsync().Result;
            return Ok(departments);
        }
        [HttpPost("PublishDepartment")]
        public async Task<IActionResult> PublishDepartment([FromBody] Models.Department department)
        {
            // Logic to add the department
            await _publisher.PublishMessageAsync<Models.Department>("department-created",department);
            return Ok();
        }
    }
}
