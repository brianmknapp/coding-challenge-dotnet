using System.Collections.Generic;
using System.Linq;
using challenge.Models;

namespace challenge.Dto
{
    public class CompensationsDto
    {
        public IList<CompensationDto> Compensations { get; set; }

        public static CompensationsDto FromModels(Employee employee, IList<Compensation> compensations)
        {
            return new CompensationsDto
            {
                Compensations = compensations
                    .Select(compensation => CompensationDto.FromModels(employee, compensation)).ToList()
            };
        }
    }
}