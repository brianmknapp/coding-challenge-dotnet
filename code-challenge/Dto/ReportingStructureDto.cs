using challenge.Models;
using System.Linq;

namespace challenge.Dto
{
    public class ReportingStructureDto : ReportingStructure
    {
        public static ReportingStructure FromEmployee(Employee employee)
        {
            return new ReportingStructure
            {
                Employee = employee,
                NumberOfReports = TotalDirectReportsCount(employee)
            };
        }

        private static int TotalDirectReportsCount(Employee employee)
        {
            if (employee.DirectReports is null) return 0;
            var totalDirectReports = employee.DirectReports.Count;
            foreach (var innerEmployee in employee.DirectReports)
            {
                totalDirectReports += TotalDirectReportsCount(innerEmployee);
            }

            return totalDirectReports;
        }
    }
}
