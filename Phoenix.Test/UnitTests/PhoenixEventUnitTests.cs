namespace Phoenix.Test.UnitTests
{
    using NUnit.Framework.Legacy;
    using Phoenix.Models;
    using System;
    public class PhoenixEventUnitTests
    {
        [Test]
        public void PhoenixEventInitializationTest()
        {
            // Arrange
            var type = 1;
            var message = "Test Message";
            var timestamp = DateTime.Now;
            var actualData = new { Data = "Actual" };
            var expectedData = new { Data = "Expected" };
            var status = true;
            var screenshot = "screenshot.png";

            // Act
            var phoenixEvent = new PhoenixEvent
            {
                Type = type,
                Message = message,
                Timestamp = timestamp,
                ActualData = actualData,
                ExpectedData = expectedData,
                Status = status,
                Screenshot = screenshot
            };

            // Assert
            ClassicAssert.AreEqual(type, phoenixEvent.Type);
            ClassicAssert.AreEqual(message, phoenixEvent.Message);
            ClassicAssert.AreEqual(timestamp, phoenixEvent.Timestamp);
            ClassicAssert.AreEqual(actualData, phoenixEvent.ActualData);
            ClassicAssert.AreEqual(expectedData, phoenixEvent.ExpectedData);
            ClassicAssert.AreEqual(status, phoenixEvent.Status);
            ClassicAssert.AreEqual(screenshot, phoenixEvent.Screenshot);
        }
    }
}
