using System.Collections.Generic;
using challenge.Models;
using challenge.Repositories;
using Microsoft.Extensions.Logging;

namespace challenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;
        private readonly ILogger<EmployeeService> _logger;

        public CompensationService(ILogger<EmployeeService> logger, ICompensationRepository compensationRepository)
        {
            _logger = logger;
            _compensationRepository = compensationRepository;
        }
        
        public IList<Compensation> GetByEmployeeId(string employeeId)
        {
            return string.IsNullOrWhiteSpace(employeeId) ? null : _compensationRepository.GetByEmployeeId(employeeId);
        }

        public Compensation Create(Compensation compensation)
        {
            if (compensation is null) return null;
            _compensationRepository.Add(compensation);
            _compensationRepository.SaveAsync().Wait();
            return compensation;
        }
    }
}