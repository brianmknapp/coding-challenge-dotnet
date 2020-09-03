﻿using System;
using Microsoft.Extensions.Logging;
using Moq;

namespace code_challenge.Tests.Unit.Extensions
{
    public static class MockExtensions
    {
        public static Mock<ILogger<T>> VerifyLogging<T>(this Mock<ILogger<T>> logger, string expectedMessage,
            LogLevel expectedLogLevel = LogLevel.Debug, Times? times = null)
        {
            times = times ?? Times.Once();

            Func<object, Type, bool> state = (v, t) =>
                string.Compare(v.ToString(), expectedMessage, StringComparison.Ordinal) == 0;

            logger.Verify(
                x => x.Log(
                    It.Is<LogLevel>(l => l == expectedLogLevel),
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) => state(v, t)),
                    It.IsAny<Exception>(),
                    It.Is<Func<It.IsAnyType, Exception, string>>((v, t) => true)), (Times) times);

            return logger;
        }
    }
}