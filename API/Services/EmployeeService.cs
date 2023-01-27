using API.Common;
using API.Data;
using API.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileSystemGlobbing.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace API.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly StoreContext _context;
        private readonly IConfigurationSettingService _configurationSettingService;

        public EmployeeService(StoreContext context, IConfigurationSettingService configurationSettingService) 
        {
            _context = context;
            _configurationSettingService = configurationSettingService;
        }
        public async Task<List<Employee>> GetAllEmployees()
        {
            try
            {
                return await _context.Employees.Include(m => m.Dependents).ToListAsync();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<Employee> GetEmployee(int employeeId)
        {
            try
            {
                return (await _context.Employees.Include(m => m.Dependents).Where(m => m.EmployeeId == employeeId).ToListAsync()).FirstOrDefault();
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public async Task<int> AddEmployee(Employee employee)
        {
            try
            {
                var addedEmployee = _context.Employees.Add(employee);
                await _context.SaveChangesAsync();
                return 1;
            }
            catch(Exception ex)
            {
                return 0;
            }
        }

        public async Task<double> CalculateCostOfBenefits(int employeeId)
        {
            try
            {
                var employee = await GetEmployee(employeeId);
                var noOfDependents = employee.Dependents.Count();

                var costOfBenefitForEmployeePerPay = (Convert.ToDouble(_configurationSettingService.GetConfigurationSettingValue(ConfigurationSettingType.CostOfBenefitForAllEmployees.ToString()).Result.Value)) / 26;
                var costOfBenefitForAllDependentsPerPay = (Convert.ToDouble(_configurationSettingService.GetConfigurationSettingValue(ConfigurationSettingType.CostOfBenefitForAllDependents.ToString()).Result.Value)) / 26;
                var regexCondition = await _configurationSettingService.GetConfigurationSettingValue(ConfigurationSettingType.RegexCondition.ToString());
                Regex rg = new Regex(regexCondition.Value);
                MatchCollection matchedEmployee = rg.Matches(employee.Name);
                costOfBenefitForEmployeePerPay = matchedEmployee.Count() > 0 ? (costOfBenefitForEmployeePerPay - (costOfBenefitForEmployeePerPay * 0.10)) : costOfBenefitForEmployeePerPay;
                var names = new List<string>();
                var dependentNames = employee.Dependents.Select(m => m.Name).ToList();
                foreach (var dependentName in dependentNames)
                {
                    names.Add(dependentName);
                }

                MatchCollection matchedDependents = rg.Matches(String.Join(",", names));
                double paycheckpay = 0;
                if (matchedDependents.Count > 0)
                {
                    costOfBenefitForAllDependentsPerPay = (costOfBenefitForAllDependentsPerPay - (costOfBenefitForAllDependentsPerPay * 0.10)) * matchedDependents.Count;
                    paycheckpay = (employee.Salary / 26) - costOfBenefitForEmployeePerPay - (((noOfDependents - matchedDependents.Count) * costOfBenefitForAllDependentsPerPay));
                }
                else
                {
                    paycheckpay = (employee.Salary / 26) - costOfBenefitForEmployeePerPay - ((noOfDependents * costOfBenefitForAllDependentsPerPay));
                }

                return paycheckpay;
            }
            catch (Exception ex)
            {
                return 0f;
            }
        }
    }
}
