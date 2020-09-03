using System;
using System.Net;
using System.Net.Http;
using System.Text;
using challenge.Dto;
using challenge.Models;
using code_challenge.Tests.Integration.Extensions;
using code_challenge.Tests.Integration.Helpers;
using FluentAssertions;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.TestHost;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace code_challenge.Tests.Integration
{
    [TestClass]
    public class CompensationControllerTests
    {
        private HttpClient _httpClient;
        private TestServer _testServer;
        
        [TestInitialize]
        public void InitializeClass()
        {
            _testServer = new TestServer(WebHost.CreateDefaultBuilder()
                .UseStartup<TestServerStartup>()
                .UseEnvironment("Development"));

            _httpClient = _testServer.CreateClient();
        }
        
        [TestCleanup]
        public void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreatedCompensation_Returns_Created()
        {
            // Arrange
            var compensation = new Compensation
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = 120000,
                EffectiveDate = new DateTime(2020, 01, 01)
            };
            var requestContent = new JsonSerialization().ToJson(compensation);
            // Act
            var postRequestTask = _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            var response = postRequestTask.Result;
            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            var newCompensation = response.DeserializeContent<CompensationDto>();
            newCompensation.Should().BeOfType<CompensationDto>();
        }

        [TestMethod]
        public void GetCompensationByEmployeeId_Returns_Ok()
        {
            // Arrange
            var compensation = new Compensation
            {
                EmployeeId = "03aa1462-ffa9-4978-901b-7c001562cf6f",
                Salary = 120000,
                EffectiveDate = new DateTime(2020, 01, 01)
            };
            var requestContent = new JsonSerialization().ToJson(compensation);
            _httpClient.PostAsync("api/compensation",
                new StringContent(requestContent, Encoding.UTF8, "application/json"));
            // Act
            var getRequestTask = _httpClient.GetAsync($"api/compensation/{compensation.EmployeeId}");
            var response = getRequestTask.Result;
            // Assert
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var compensations = response.DeserializeContent<CompensationsDto>();
            compensations.Should().BeOfType<CompensationsDto>();
        }
    }
}