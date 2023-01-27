using System.Threading.Tasks;
using System;
using API.Entities;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

namespace API.Services
{
    public interface IEmployeeService
    {
        Task<List<Employee>> GetAllEmployees();
        Task<int> AddEmployee(Employee employee);
        Task<Employee> GetEmployee(int employeeId);
        Task<double> CalculateCostOfBenefits(int employeeId);
    }
}
