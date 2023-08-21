using AutoFixture.NUnit3;
using FluentAssertions;
using NSubstitute;
using ToDo.Application.Common.Interfaces;
using ToDo.Application.Services;
using ToDo.Domain.Entities;

namespace ToDo.Application.Tests
{
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
        public async Task Update_ToDoItemDoesNotExist_ReturnsFalse(int id, string descrption)
        {
            _toDoItemRepository.Read(Arg.Any<int>()).Returns(Task.FromResult<ToDoItem?>(null));

            var updateSuccessful = await _toDoItemService.Update(id, descrption);

            updateSuccessful.Should().BeFalse();

            await _toDoItemRepository.Received().Read(id);
        }

        [Test]
        [AutoData]
        public async Task Update_ToDoItemDoesExist_ReturnsTrue(ToDoItem toDoItem, string descrption)
        {
            _toDoItemRepository.Read(Arg.Any<int>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

            var updateSuccessful = await _toDoItemService.Update(toDoItem.Id, descrption);

            updateSuccessful.Should().BeTrue();

            await _toDoItemRepository.Received().Read(toDoItem.Id);
        }

        [Test]
        [AutoData]
        public async Task Delete_ToDoItemDoesNotExist_ReturnsFalse(int id)
        {
            _toDoItemRepository.Read(Arg.Any<int>()).Returns(Task.FromResult<ToDoItem?>(null));

            var deleteSuccessful = await _toDoItemService.Delete(id);

            deleteSuccessful.Should().BeFalse();

            await _toDoItemRepository.Received().Read(id);
        }

        [Test]
        [AutoData]
        public async Task Delete_ToDoItemDoesExist_ReturnsTrue(ToDoItem toDoItem)
        {
            _toDoItemRepository.Read(Arg.Any<int>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

            var deleteSuccessful = await _toDoItemService.Delete(toDoItem.Id);

            deleteSuccessful.Should().BeTrue();

            await _toDoItemRepository.Received().Read(toDoItem.Id);
        }
    }
}