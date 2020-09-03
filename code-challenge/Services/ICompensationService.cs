using System.Collections.Generic;
using challenge.Models;

namespace challenge.Services
{
    public interface ICompensationService
    {
        IList<Compensation> GetByEmployeeId(string employeeId);
        Compensation Create(Compensation compensation);
    }
}