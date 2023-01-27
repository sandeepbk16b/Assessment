using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using API.Properties;
using API.Services;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Newtonsoft.Json;

namespace API.Controllers
{
    [ApiController]
    [Route("api/employees")]
    public class EmployeesController : ControllerBase
    {
        //private readonly StoreContext _context;
        readonly IEmployeeService _employeeService;

        public EmployeesController(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet("GetAllEmployees")]
        public async Task<ActionResult<List<Employee>>> GetAllEmployees()
        {
            var result = await _employeeService.GetAllEmployees();
            if (result.Count == 0) return Content(Resources.NoEmployeeFound);
            return result;
        }

        [HttpGet("GetEmployee")]
        public async Task<ActionResult<Employee>> GetEmployee(int employeeId)
        {
            var employee = await _employeeService.GetEmployee(employeeId);
            return Ok(employee);
        }
        [HttpPost]
        [Route("AddEmployee")]
        public async Task<IActionResult> AddEmployee(Employee employee)
        {
            var result = await _employeeService.AddEmployee(employee);
            if (result < 1) return Content(Resources.EmployeeNotAdded);
            return Content(Resources.EmployeeAddedSuccessfully); 
        }

        [HttpGet("calculateBonus")]
        public async Task<ActionResult<string>> CalculateCostOfBenefits(int employeeId)
        {
            double cost = 0;
            try
            {
                cost = await _employeeService.CalculateCostOfBenefits(employeeId);
                if(cost == 0) Content(Resources.ErrorCalculatingCostOfBenefit);
                return cost.ToString("C");
            }
            catch(Exception ex)
            {
                return Content(Resources.ErrorCalculatingCostOfBenefit);
            }
            
        }
    }
}