using FullStack.API.Data;
using FullStack.API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FullStack.API.Controllers
{
    [ApiController]
    [Route("api/employees")]

    
    public class EmployeeController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public EmployeeController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        
        public async Task<IActionResult> GetAllEmployee()
        {
             var employee = await dbContext.Employees.ToListAsync();

            return Ok(employee);
        }

        [HttpPost]
        public async Task<IActionResult> AddEmployee( [FromBody] Employee employeeRequest)
        {
            employeeRequest.Id = Guid.NewGuid();

            await dbContext.Employees.AddAsync(employeeRequest);
            await dbContext.SaveChangesAsync();

            return Ok(employeeRequest);

        }

        [HttpGet]
        [Route("{id:Guid}")]

        public async Task<IActionResult> GetEmployee([FromRoute]Guid id)
        {
            var employee= await  dbContext.Employees.FirstOrDefaultAsync( x => x.Id == id);

            if (employee == null) 
            {
                return NotFound();
            }
            return Ok(employee);
        }

        [HttpPut]
        [Route("{id:guid}")]
        public async Task<IActionResult> UpdateEmployee([FromRoute] Guid id, Employee UpdateEmployee)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

            employee.Name = UpdateEmployee.Name;
            employee.Email = UpdateEmployee.Email;
            employee.Phone = UpdateEmployee.Phone;
            employee.Salary = UpdateEmployee.Salary;
            employee.Department = UpdateEmployee.Department;

            await dbContext.SaveChangesAsync();

            return Ok(employee);
        }

        [HttpDelete]
        [Route("{id:guid}")]
        public async Task<IActionResult> DeletEmployee([FromRoute] Guid id)
        {
            var employee = await dbContext.Employees.FindAsync(id);

            if (employee == null)
            {
                return NotFound();
            }

             dbContext.Employees.Remove(employee);
            await dbContext.SaveChangesAsync();

            return Ok(employee);


        }


    }
}
