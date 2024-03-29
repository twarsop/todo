using AutoFixture.NUnit3;
using FluentAssertions;
using NSubstitute;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Services;
using ToDo.Domain.Entities;

namespace ToDo.Application.Tests;

public class ToDoItemServiceTests
{
    private ToDoItemService _toDoItemService;
    private IToDoItemRepository _toDoItemRepository;

    [SetUp]
    public void SetUp()
    {
        _toDoItemRepository = Substitute.For<IToDoItemRepository>();
        _toDoItemService = new ToDoItemService(_toDoItemRepository);
    }

    [Test]
    [AutoData]
    public async Task Update_ToDoItemDoesNotExist_ReturnsFalse(Guid id, string descrption)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(null));

        var updateSuccessful = await _toDoItemService.Update(id, descrption);

        updateSuccessful.Should().BeFalse();

        await _toDoItemRepository.Received().Read(id);
    }

    [Test]
    [AutoData]
    public async Task Update_ToDoItemDoesExist_ReturnsTrue(ToDoItem toDoItem, string descrption)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        var updateSuccessful = await _toDoItemService.Update(toDoItem.Id, descrption);

        updateSuccessful.Should().BeTrue();

        await _toDoItemRepository.Received().Read(toDoItem.Id);
        await _toDoItemRepository.Received().Update(toDoItem);
    }

    [Test]
    [AutoData]
    public async Task Delete_ToDoItemDoesNotExist_ReturnsFalse(Guid id)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(null));

        var deleteSuccessful = await _toDoItemService.Delete(id);

        deleteSuccessful.Should().BeFalse();

        await _toDoItemRepository.Received().Read(id);
    }

    [Test]
    [AutoData]
    public async Task Delete_ToDoItemDoesExist_ReturnsTrue(ToDoItem toDoItem)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        var deleteSuccessful = await _toDoItemService.Delete(toDoItem.Id);

        deleteSuccessful.Should().BeTrue();

        await _toDoItemRepository.Received().Read(toDoItem.Id);
        _toDoItemRepository.Received().Delete(toDoItem.Id);
    }

    [Test]
    [AutoData]
    public async Task Complete_ToDoItemDoesNotExist_ReturnsNull(Guid id)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(null));

        var completeResponse = await _toDoItemService.Complete(id);

        completeResponse.Should().BeNull();

        await _toDoItemRepository.Received().Read(id);
    }

    [Test]
    [AutoData]
    public async Task Complete_ToDoItemDoesExist_ReturnsADateTime(ToDoItem toDoItem)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        var completeResponse = await _toDoItemService.Complete(toDoItem.Id);

        completeResponse.Should().NotBeNull();
        completeResponse.Should().BeBefore(DateTime.Now);

        await _toDoItemRepository.Received().Read(toDoItem.Id);
        await _toDoItemRepository.Received().Update(toDoItem);
    }

    [Test]
    [AutoData]
    public async Task UnComplete_ToDoItemDoesNotExist_ReturnsFalse(Guid id)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(null));

        var completeSuccessful = await _toDoItemService.UnComplete(id);

        completeSuccessful.Should().BeFalse();

        await _toDoItemRepository.Received().Read(id);
    }

    [Test]
    [AutoData]
    public async Task UnComplete_ToDoItemDoesExist_ReturnsTrue(ToDoItem toDoItem)
    {
        _toDoItemRepository.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        var completeSuccessful = await _toDoItemService.UnComplete(toDoItem.Id);

        completeSuccessful.Should().BeTrue();

        await _toDoItemRepository.Received().Read(toDoItem.Id);
        await _toDoItemRepository.Received().Update(toDoItem);
    }
}