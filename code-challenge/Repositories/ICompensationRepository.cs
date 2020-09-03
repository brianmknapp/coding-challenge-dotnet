using System.Collections.Generic;
using System.Threading.Tasks;
using challenge.Models;

namespace challenge.Repositories
{
    public interface ICompensationRepository
    {
        IList<Compensation> GetByEmployeeId(string employeeId);
        Compensation Add(Compensation compensation);
        Task SaveAsync();
    }
}