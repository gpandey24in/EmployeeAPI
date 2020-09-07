using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EmployeeAPI.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class EmpController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public EmpController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<List<Employee>> Get()
        {
            return Ok(_context.Employee.ToList());
        }

        [Route("{Id}")]
        [HttpGet]
        public ActionResult<Employee> Get(int Id)
        {
           var employee = _context.Employee.FirstOrDefault(a => a.Id == Id);
            return Ok(employee);
        }

        [HttpPost]
        public ActionResult<Employee> Post(Employee employee)
        {
             _context.Employee.Add(employee);
            _context.SaveChanges();
            return Ok(employee);
        }

        [HttpPut]
        public ActionResult<Employee> Put(Employee employee)
        {
           var employeeInDb = _context.Employee.FirstOrDefault(a => a.Id == employee.Id);
            employeeInDb.Name = employee.Name;
            employeeInDb.Email = employee.Email;
            employeeInDb.Password = employee.Password;
            _context.SaveChanges();
            return Ok(employee);

        }

       
        [Route("{Id}")]
        [HttpDelete]
        public ActionResult<Employee> Delete(int Id)
        {
            var employeeInDb = _context.Employee.FirstOrDefault(a => a.Id == Id);
            _context.Remove(employeeInDb);
            _context.SaveChanges();
            return Ok(employeeInDb);
            

        }
    }
}