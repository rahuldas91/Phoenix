namespace Phoenix.Test.UnitTests
{
    using NUnit.Framework.Legacy;
    using Phoenix.Models;
    using Phoenix.ReportBuilder;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Text.Json;
    public class HTMLReportGenerationTests
    {
        [Test]
        public void HTMLReportGenerationTest()
        {
            // Arrange
            var report = new PhoenixReport
            {
                GUID = Guid.NewGuid(),
                Name = "Automated Test Report",
                Timestamp = DateTime.Now,
                ProjectName = "Customer Management System",
                SuiteName = "Regression Suite",
                Tests = new Queue<PhoenixTest>()
            };

            var test1 = new PhoenixTest
            {
                GUID = Guid.NewGuid(),
                Name = "Login Functionality Test",
                Author = "Alice Johnson",
                Description = "Verify that users can log in with valid credentials.",
                DefectLink = new Uri("http://example.com/defect1"),
                Timestamp = DateTime.Now,
                Tags = new List<string> { "login", "authentication" },
                Events = new Queue<PhoenixEvent>(),
                Status = true
            };

            var test2 = new PhoenixTest
            {
                GUID = Guid.NewGuid(),
                Name = "User Registration Test",
                Author = "Bob Smith",
                Description = "Ensure that new users can register successfully.",
                DefectLink = new Uri("http://example.com/defect2"),
                Timestamp = DateTime.Now,
                Tags = new List<string> { "registration", "user" },
                Events = new Queue<PhoenixEvent>(),
                Status = false
            };

            var test3 = new PhoenixTest
            {
                GUID = Guid.NewGuid(),
                Name = "Password Reset Test",
                Author = "Charlie Brown",
                Description = "Check that users can reset their passwords.",
                DefectLink = new Uri("http://example.com/defect3"),
                Timestamp = DateTime.Now,
                Tags = new List<string> { "password", "reset" },
                Events = new Queue<PhoenixEvent>(),
                Status = true
            };

            var test4 = new PhoenixTest
            {
                GUID = Guid.NewGuid(),
                Name = "Profile Update Test",
                Author = "Diana Prince",
                Description = "Validate that users can update their profile information.",
                DefectLink = new Uri("http://example.com/defect4"),
                Timestamp = DateTime.Now,
                Tags = new List<string> { "profile", "update" },
                Events = new Queue<PhoenixEvent>(),
                Status = false
            };

            report.Tests.Enqueue(test1);
            report.Tests.Enqueue(test2);
            report.Tests.Enqueue(test3);
            report.Tests.Enqueue(test4);

            var event1 = new PhoenixEvent { Message = "Login page loaded successfully." };
            var event2 = new PhoenixEvent { Message = "User entered valid credentials." };
            var event3 = new PhoenixEvent { Message = "User clicked the login button." };
            var event4 = new PhoenixEvent { Message = "User was redirected to the dashboard." };

            // Act
            test1.Events.Enqueue(event1);
            test1.Events.Enqueue(event2);
            test1.Events.Enqueue(event3);
            test1.Events.Enqueue(event4);

            var event5 = new PhoenixEvent { Message = "Registration page loaded successfully." };
            var event6 = new PhoenixEvent { Message = "User filled out the registration form." };
            var event7 = new PhoenixEvent { Message = "User submitted the registration form." };
            var event8 = new PhoenixEvent { Message = "User received a confirmation email." };

            test2.Events.Enqueue(event5);
            test2.Events.Enqueue(event6);
            test2.Events.Enqueue(event7);
            test2.Events.Enqueue(event8);

            var event9 = new PhoenixEvent { Message = "Password reset page loaded successfully." };
            var event10 = new PhoenixEvent { Message = "User entered their email address." };
            var event11 = new PhoenixEvent { Message = "User received a password reset email." };
            var event12 = new PhoenixEvent { Message = "User reset their password successfully." };

            test3.Events.Enqueue(event9);
            test3.Events.Enqueue(event10);
            test3.Events.Enqueue(event11);
            test3.Events.Enqueue(event12);

            var event13 = new PhoenixEvent { Message = "Profile page loaded successfully." };
            var event14 = new PhoenixEvent { Message = "User updated their profile information." };
            var event15 = new PhoenixEvent { Message = "User saved the changes." };
            var event16 = new PhoenixEvent { Message = "Profile information was updated successfully." };

            test4.Events.Enqueue(event13);
            test4.Events.Enqueue(event14);
            test4.Events.Enqueue(event15);
            test4.Events.Enqueue(event16);

            // Act
            var jsonReport = JsonSerializer.Serialize(report, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(jsonReport);

            var htmlReport = new HtmlBuilder().GenerateHTMLReport(report);
            File.WriteAllText(@"C:\Users\A2696276\Downloads\report.html", htmlReport);

            // Assert
            ClassicAssert.IsNotNull(report);
            ClassicAssert.AreEqual(4, report.Tests.Count);
            ClassicAssert.AreEqual("Automated Test Report", report.Name);
            ClassicAssert.AreEqual("Customer Management System", report.ProjectName);
            ClassicAssert.AreEqual("Regression Suite", report.SuiteName);
        }

        
    }
}