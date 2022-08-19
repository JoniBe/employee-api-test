using System;

namespace Aptude.Entities.DTOs
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public string Designation { get; set; }
        public DateTime StartDate { get; set; }
        public string EndDate { get; set; }
    }
}
