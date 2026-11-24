using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DapperRabbitMQ.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly ServiceInterfaces.IEmployeeService _employeeService;

        public EmployeeController(ServiceInterfaces.IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        [HttpGet("{EmployeeId}")]
        public async Task<IActionResult> GetEmployeeById(string EmployeeId)
        {
            var employee = await _employeeService.GetEmployeeByIdAsync(EmployeeId);
            if (employee == null)
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllEmployees()
        {
            var employees = await _employeeService.GetAllEmployeesAsync();
            return Ok(employees);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee([FromBody] Models.Employee employee)
        {
            var result = await _employeeService.AddEmployeeAsync(employee);
            if (result)
            {
                return CreatedAtAction(nameof(GetEmployeeById), new { EmployeeId = employee.EmployeeId }, employee);
            }
            return BadRequest();
        }

        [HttpPut("{EmployeeId}")]
        public async Task<IActionResult> UpdateEmployee(string EmployeeId, [FromBody] Models.Employee employee)
        {
            if (EmployeeId != employee.EmployeeId)
            {
                return BadRequest();
            }

            var result = await _employeeService.UpdateEmployeeAsync(employee);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }

        [HttpDelete("{EmployeeId}")]
        public async Task<IActionResult> DeleteEmployee(string EmployeeId)
        {
            var result = await _employeeService.DeleteEmployeeAsync(EmployeeId);
            if (result)
            {
                return NoContent();
            }
            return NotFound();
        }
    }
}
