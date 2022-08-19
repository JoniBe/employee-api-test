using Aptude.Entities.Constants;
using System;
using System.ComponentModel.DataAnnotations;

namespace Aptude.Entities
{
    public class Employee
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public decimal Salary { get; set; }

        [Required]
        public string Designation { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public string EndDate { get; set; }

        public void Validates()
        {
            // Salary
            if (decimal.Round(Salary, 5) != Salary)
                throw new Exception("Salary must have a precision of 5!");

            // Designation
            if (DesignationTypes.ValidateType(Designation) is false)
                throw new Exception("Designation is not valid!");

            // End Date
            if(DateTime.TryParse(EndDate, out var enddate) is false)
                throw new Exception("End Date is not valid!");

            if(enddate < StartDate)
                throw new Exception("Start Date must be before than End Date!");

        }
    }
}
