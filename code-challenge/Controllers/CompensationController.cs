using System.Collections.Generic;
using System.Linq;
using challenge.Dto;
using challenge.Models;
using challenge.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace challenge.Controllers
{
    [Route("api/compensation")]
    public class CompensationController : Controller
    {
        private readonly ILogger<CompensationController> _logger;
        private readonly IEmployeeService _employeeService;
        private readonly ICompensationService _compensationService;
        
        public CompensationController(ILogger<CompensationController> logger, IEmployeeService employeeService, ICompensationService compensationService)
        {
            _logger = logger;
            _employeeService = employeeService;
            _compensationService = compensationService;
        }
        
        [HttpPost]
        public IActionResult CreateEmployee([FromBody] Compensation compensation)
        {
            _logger.LogDebug(
                $"Received compensation create request compensation for employee id: '{compensation.EmployeeId}'");
            var existingEmployee = _employeeService.GetById(compensation.EmployeeId);
            if (existingEmployee is null) return NotFound();
            var createdCompensation = _compensationService.Create(compensation);
            if (createdCompensation is null) return BadRequest();
            return Created("", CompensationDto.FromModels(existingEmployee, compensation));
        }

        [HttpGet("{employeeId}")]
        public IActionResult GetEmployeeCompensations(string employeeId)
        {
            _logger.LogDebug($"Received compensation get request for employee id: '{employeeId}'");
            var existingEmployee = _employeeService.GetById(employeeId);
            if (existingEmployee is null) return NotFound();
            var compensations = _compensationService.GetByEmployeeId(employeeId);
            return Ok(CompensationsDto.FromModels(existingEmployee, compensations));
        }
    }
}