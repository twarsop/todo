using AutoFixture.NUnit3;
using FluentAssertions;
using ToDo.Domain.Entities;

namespace ToDo.Domain.Tests;

public class Tests
{
    [Test]
    [AutoData]
    public void UpdatingDescription_CorrectlyUpdates(ToDoItem toDoItem, string updatedDescription)
    {
        toDoItem.UpdateDescription(updatedDescription);

        toDoItem.Description.Should().Be(updatedDescription);
    }

    [Test]
    public void ProvidingNullDescriptionInConstructor_ThrowsArgumentNullException()
    {
        var toDoItemCtor = () => new ToDoItem(null);
        toDoItemCtor.Should().Throw<ArgumentNullException>();
    }

    [Test]
    public void ProvidingEmptyDescriptionInConstructor_ThrowsArgumentException()
    {
        var toDoItemCtor = () => new ToDoItem("");
        toDoItemCtor.Should().Throw<ArgumentException>();
    }

    [Test]
    [AutoData]
    public void ProvidingNullDescriptionInUpdate_ThrowsArgumentNullException(ToDoItem toDoItem)
    {
        var toDoItemUpdateDescription = () => toDoItem.UpdateDescription(null);
        toDoItemUpdateDescription.Should().Throw<ArgumentNullException>();
    }

    [Test]
    [AutoData]
    public void ProvidingEmptyDescriptionInUpdate_ThrowsArgumentException(ToDoItem toDoItem)
    {
        var toDoItemUpdateDescription = () => toDoItem.UpdateDescription("");
        toDoItemUpdateDescription.Should().Throw<ArgumentException>();
    }
}