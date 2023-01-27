using System.Collections.Generic;
using System.Linq;
using System.Threading;
using API.Entities;

namespace API.Data
{
    public class DbInitializer
    {
        public static void Initialize(StoreContext context)
        {
            if (context.Employees.Any()) return;
            var employees = new List<Employee>
            {
                new Employee { EmployeeId = 1, Name="Sandeep", Salary=52000.00f, Dependents = new List<Dependent>{ new Dependent {DependentId = 1,Name= "Mounika", Relationship="Wife", EmployeeId=1 } }},
                new Employee { EmployeeId = 2, Name="Arnav", Salary=52000.00f, Dependents = new List<Dependent>{ new Dependent { DependentId = 2, Name = "Ankita", Relationship = "Wife", EmployeeId = 2 } }},
                new Employee { EmployeeId = 3, Name="Abhi", Salary=52000.00f, Dependents = new List<Dependent>{ new Dependent { DependentId = 3, Name = "Latha", Relationship = "Wife", EmployeeId = 3 }}},
                new Employee { EmployeeId = 4, Name="Tul", Salary=52000.00f, Dependents = new List<Dependent>{ new Dependent { DependentId = 4, Name = "TulaW", Relationship = "Wife", EmployeeId = 4 },new Dependent { DependentId = 5, Name = "TulaSon", Relationship = "Son", EmployeeId = 4 }}},

            };
            foreach (var employee in employees)
            {
                context.Employees.Add(employee);
            }

            if (context.ConfigurationSettings.Any()) return;
            var configurationSettings = new List<ConfigurationSetting>
            {
                new ConfigurationSetting{ConfigurationSettingId=1, Key="CostOfBenefitForAllEmployees", Value="1000"},
                new ConfigurationSetting{ConfigurationSettingId=2, Key="CostOfBenefitForAllDependents", Value="500"},
                new ConfigurationSetting{ConfigurationSettingId=3, Key="RegexCondition", Value="/^[Aa]/"},
                new ConfigurationSetting{ConfigurationSettingId=4, Key="AdditionalDiscountInPercentage", Value="10"},
            };
            foreach (var config in configurationSettings)
            {
                context.ConfigurationSettings.Add(config);
            }
            context.SaveChanges();
        }
    }
}