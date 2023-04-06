using Domain.Models;

namespace Domain.UnitTests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void UpdatingDescriptionCorrectlyUpdates()
        {
            var newToDoItem = new ToDoItem("A description");

            var updatedDescription = "An updated description";

            newToDoItem.UpdateDescription("An updated description");

            Assert.AreEqual(updatedDescription, newToDoItem.Description);
        }

        [Test]
        public void ProvidingEmptyDescriptionInConstructorThrowsException()
        {
            Assert.Throws<ArgumentException>(() => new ToDoItem(""));
        }

        [Test]
        public void ProvidingEmptyDescriptionInUpdateThrowsException()
        {
            var newToDoItem = new ToDoItem("A description");

            Assert.Throws<ArgumentException>(() => newToDoItem.UpdateDescription(""));
        }
    }
}