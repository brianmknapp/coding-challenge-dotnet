using System;
using challenge.Models;

namespace challenge.Dto
{
    public class CompensationDto
    {
        public Employee Employee { get; set; }
        public string Salary { get; set; }
        public string EffectiveDate { get; set; }

        public static CompensationDto FromModels(Employee employee, Compensation compensation)
        {
            return new CompensationDto
            {
                Employee = employee,
                Salary = $"{compensation.Salary:C}",
                EffectiveDate = $"{compensation.EffectiveDate}"
            };
        }
    }
}