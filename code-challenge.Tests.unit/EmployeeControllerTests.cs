using System;
using System.Collections.Generic;
using Bogus;
using challenge.Controllers;
using challenge.Dto;
using challenge.Models;
using challenge.Services;
using code_challenge.Tests.Unit.Extensions;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace code_challenge.Tests.Unit
{
    public class EmployeeControllerTests
    {
        private Mock<ILogger<EmployeeController>> LoggerMock { get; }
        private Mock<IEmployeeService> EmployeeServiceMock { get; }
        private static readonly Faker Faker  = new Faker();

        public EmployeeControllerTests()
        {
            LoggerMock = new Mock<ILogger<EmployeeController>>();
            EmployeeServiceMock = new Mock<IEmployeeService>();
        }
        
        [Theory]
        [InlineData("123")]
        [InlineData("")]
        [InlineData(null)]
        public void GetEmployeeDirectReportsShouldLogRequestToDebug(string id)
        {
            // Arrange
            var expectedMessage = $"Received employee get reporting structure request for '{id}'";
            var sut = new EmployeeController(LoggerMock.Object, EmployeeServiceMock.Object);
            // Act
            sut.GetEmployeeReportingStructure(id);
            // Assert
            LoggerMock.VerifyLogging(expectedMessage);
        }

        [Fact]
        public void GetEmployeeDirectReportsShouldReturnReportingStructure()
        {
            // Arrange
            var employee = new Employee
            {
                EmployeeId = Guid.NewGuid().ToString(),
                FirstName = Faker.Name.FirstName(),
                LastName = Faker.Name.LastName(),
                Position = Faker.Commerce.ProductName(),
                Department = Faker.Commerce.Department(),
                DirectReports = new List<Employee>()
            };
            EmployeeServiceMock.Setup(x => x.GetById(It.IsAny<string>())).Returns(employee);
            var sut = new EmployeeController(LoggerMock.Object, EmployeeServiceMock.Object);
            // Act
            var result = sut.GetEmployeeReportingStructure(employee.EmployeeId) as OkObjectResult;
            // Assert
            result.Value.Should().BeOfType<ReportingStructure>();
        }
    }
}