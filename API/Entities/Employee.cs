using System.Collections.Generic;

namespace API.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public float Salary { get; set; }

        public virtual IList<Dependent> Dependents { get; set; }
    }
}