namespace Phoenix.Test.UnitTests
{
    using NUnit.Framework.Legacy;
    using Phoenix.Models;
    using System;
    using System.Collections.Generic;
   
    public class PhoenixReportUnitTests
    {
        [Test]
        public void PhoenixReportInitializationTest()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var name = "Test Report";
            var timestamp = DateTime.Now;
            var projectName = "Test Project";
            var suiteName = "Test Suite";
            var tests = new Queue<PhoenixTest>();

            // Act
            var phoenixReport = new PhoenixReport
            {
                GUID = guid,
                Name = name,
                Timestamp = timestamp,
                ProjectName = projectName,
                SuiteName = suiteName,
                Tests = tests
            };

            // Assert
            ClassicAssert.AreEqual(guid, phoenixReport.GUID);
            ClassicAssert.AreEqual(name, phoenixReport.Name);
            ClassicAssert.AreEqual(timestamp, phoenixReport.Timestamp);
            ClassicAssert.AreEqual(projectName, phoenixReport.ProjectName);
            ClassicAssert.AreEqual(suiteName, phoenixReport.SuiteName);
            ClassicAssert.AreEqual(tests, phoenixReport.Tests);
        }

        [Test]
        public void PhoenixReportTestsQueueTest()
        {
            // Arrange
            var phoenixReport = new PhoenixReport
            {
                Tests = new Queue<PhoenixTest>()
            };

            var test1 = new PhoenixTest { Name = "Test1" };
            var test2 = new PhoenixTest { Name = "Test2" };

            // Act
            phoenixReport.Tests.Enqueue(test1);
            phoenixReport.Tests.Enqueue(test2);

            // Assert
            ClassicAssert.AreEqual(2, phoenixReport.Tests.Count);
            ClassicAssert.AreEqual(test1, phoenixReport.Tests.Dequeue());
            ClassicAssert.AreEqual(test2, phoenixReport.Tests.Dequeue());
        }
    }
}
