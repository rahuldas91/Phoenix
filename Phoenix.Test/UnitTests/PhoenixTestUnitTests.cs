namespace Phoenix.Test.UnitTests
{
    using NUnit.Framework.Legacy;
    using Phoenix.Attributes;
    using Phoenix.Models;
    [PhoenixTestClass]
    public class PhoenixTestUnitTests
    {
        [Test]
        public void PhoenixTestInitializationTest()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var name = "Test Name";
            var author = "Test Author";
            var description = "Test Description";
            var defectLink = new Uri("http://example.com");
            var timestamp = DateTime.Now;
            var tags = new List<string> { "tag1", "tag2" };
            var events = new Queue<PhoenixEvent>();

            // Act
            var phoenixTest = new PhoenixTest
            {
                GUID = guid,
                Name = name,
                Author = author,
                Description = description,
                DefectLink = defectLink,
                Timestamp = timestamp,
                Tags = tags,
                Events = events
            };

            // Assert
            ClassicAssert.AreEqual(guid, phoenixTest.GUID);
            ClassicAssert.AreEqual(name, phoenixTest.Name);
            ClassicAssert.AreEqual(author, phoenixTest.Author);
            ClassicAssert.AreEqual(description, phoenixTest.Description);
            ClassicAssert.AreEqual(defectLink, phoenixTest.DefectLink);
            ClassicAssert.AreEqual(timestamp, phoenixTest.Timestamp);
            ClassicAssert.AreEqual(tags, phoenixTest.Tags);
            ClassicAssert.AreEqual(events, phoenixTest.Events);
        }

        [Test]
        public void PhoenixTestEventsQueueTest()
        {
            // Arrange
            var phoenixTest = new PhoenixTest
            {
                Events = new Queue<PhoenixEvent>()
            };

            var event1 = new PhoenixEvent { Message = "Event1" };
            var event2 = new PhoenixEvent { Message = "Event2" };

            // Act
            phoenixTest.Events.Enqueue(event1);
            phoenixTest.Events.Enqueue(event2);

            // Assert
            ClassicAssert.AreEqual(2, phoenixTest.Events.Count);
            ClassicAssert.AreEqual(event1, phoenixTest.Events.Dequeue());
            ClassicAssert.AreEqual(event2, phoenixTest.Events.Dequeue());
        }
    }
}
