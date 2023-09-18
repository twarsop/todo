using AutoFixture.NUnit3;
using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NSubstitute;
using ToDo.API.Controllers;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Shared.Dtos;

namespace ToDo.API.Tests;

public class ToDoItemsControllerTests
{
    private ToDoItemsController _controller;
    private ILogger<ToDoItemsController> _logger;
    private IMapper _mapper;
    private IToDoItemService _service;

    [SetUp]
    public void SetUp()
    {
        _logger = Substitute.For<ILogger<ToDoItemsController>>();
        _service = Substitute.For<IToDoItemService>();
        _mapper = new Mapper(TestAutoMapperConfig.Configure());
        _controller = new ToDoItemsController(_logger, _service, _mapper);
    }

    [Test]
    public async Task Get_AlwaysReturnsOkObjectResult()
    {
        var response = await _controller.Get();

        response.Result.Should().BeOfType<OkObjectResult>();

        await _service.Received().ReadAll();
    }

    [Test]
    [AutoData]
    public async Task GetToDoItem_ToDoItemDoesNotExist_ReturnsNotFoundResult(Guid id)
    {
        _service.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(null));

        var response = await _controller.GetToDoItem(id);

        response.Result.Should().BeOfType<NotFoundResult>();

        await _service.Received().Read(id);
    }

    [Test]
    [AutoData]
    public async Task GetToDoItem_ToDoItemExists_ReturnsOkObjectResultAndToDoItem(ToDoItem toDoItem)
    {
        _service.Read(Arg.Any<Guid>()).Returns(Task.FromResult<ToDoItem?>(toDoItem));

        var response = await _controller.GetToDoItem(toDoItem.Id);

        response.Result.Should().BeOfType<OkObjectResult>();

        var objectResponse = (OkObjectResult)response.Result;
        objectResponse.Value.Should().NotBeNull();
        objectResponse.Value.Should().BeOfType<ToDoItemDto>();

        var toDoItemDto = (ToDoItemDto)objectResponse.Value;
        toDoItemDto.Id.Should().Be(toDoItem.Id);
        toDoItemDto.Description.Should().Be(toDoItem.Description);

        await _service.Received().Read(toDoItem.Id);
    }

    [TestCase(null)]
    [TestCase("")]
    public async Task CreateToDoItem_NullOrEmptyDescriptionProvided_ReturnsBadRequestResult(string description)
    {
        var response = await _controller.CreateToDoItem(new ToDoItemForCreationDto(description));

        response.Result.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    [AutoData]
    public async Task CreateToDoItem_DescriptionProvided_ReturnsCreatedAtRouteResultAndCreatedToDoItem(Guid id, string description)
    {
        _service.Create(description).Returns(Task.FromResult(new ToDoItem(id, description)));

        var response = await _controller.CreateToDoItem(new ToDoItemForCreationDto(description));

        response.Result.Should().BeOfType<CreatedAtRouteResult>();

        var createdAtRouteResponse = (CreatedAtRouteResult)response.Result;
        createdAtRouteResponse.Value.Should().NotBeNull();

        var toDoItemDto = (ToDoItemDto)createdAtRouteResponse.Value;
        toDoItemDto.Id.Should().Be(id);
        toDoItemDto.Description.Should().Be(description);

        await _service.Received().Create(description);
    }

    [TestCase(null)]
    [TestCase("")]
    public async Task UpdateToDoItem_NullOrEmptyDescriptionProvided_ReturnsBadRequestResult(string description)
    {
        var response = await _controller.UpdateToDoItem(Guid.Empty, new ToDoItemForUpdateDto(description));

        response.Should().BeOfType<BadRequestResult>();
    }

    [Test]
    [AutoData]
    public async Task UpdateToDoItem_ToDoItemNotFound_ReturnsNotFoundResult(Guid id, string description)
    {
        _service.Update(id, description).Returns(Task.FromResult(false));

        var response = await _controller.UpdateToDoItem(id, new ToDoItemForUpdateDto(description));

        response.Should().BeOfType<NotFoundResult>();

        await _service.Received().Update(id, description);
    }

    [Test]
    [AutoData]
    public async Task UpdateToDoItem_ToDoItemFoundAndUpdated_ReturnsNoContentResult(Guid id, string description)
    {
        _service.Update(id, description).Returns(Task.FromResult(true));

        var response = await _controller.UpdateToDoItem(id, new ToDoItemForUpdateDto(description));

        response.Should().BeOfType<NoContentResult>();

        await _service.Received().Update(id, description);
    }

    [Test]
    [AutoData]
    public async Task DeleteToDoItem_ToDoItemNotFound_ReturnsNotFoundResult(Guid id)
    {
        _service.Delete(id).Returns(Task.FromResult(false));

        var response = await _controller.DeleteToDoItem(id);

        response.Should().BeOfType<NotFoundResult>();

        await _service.Received().Delete(id);
    }

    [Test]
    [AutoData]
    public async Task DeleteToDoItem_ToDoItemFoundAndDeleted_ReturnsNoContentResult(Guid id)
    {
        _service.Delete(id).Returns(Task.FromResult(true));

        var response = await _controller.DeleteToDoItem(id);

        response.Should().BeOfType<NoContentResult>();

        await _service.Received().Delete(id);
    }

    [Test]
    [AutoData]
    public async Task CompleteToDoItem_ToDoItemNotFound_ReturnsNotFoundResult(Guid id)
    {
        _service.Complete(id).Returns(Task.FromResult((DateTime?)null));

        var response = await _controller.CompleteToDoItem(id);

        response.Should().BeOfType<NotFoundResult>();

        await _service.Received().Complete(id);
    }

    [Test]
    [AutoData]
    public async Task CompleteToDoItem_ToDoItemFound_ReturnsOkObjectResult(Guid id)
    {
        _service.Complete(id).Returns(Task.FromResult((DateTime?)new DateTime()));

        var response = await _controller.CompleteToDoItem(id);

        response.Should().BeOfType<OkObjectResult>();

        await _service.Received().Complete(id);
    }

    [Test]
    [AutoData]
    public async Task UnCompleteToDoItem_ToDoItemNotFound_ReturnsNotFoundResult(Guid id)
    {
        _service.UnComplete(id).Returns(Task.FromResult(false));

        var response = await _controller.UnCompleteToDoItem(id);

        response.Should().BeOfType<NotFoundResult>();

        await _service.Received().UnComplete(id);
    }

    [Test]
    [AutoData]
    public async Task UnCompleteToDoItem_ToDoItemFoundAndDeleted_ReturnsNoContentResult(Guid id)
    {
        _service.UnComplete(id).Returns(Task.FromResult(true));

        var response = await _controller.UnCompleteToDoItem(id);

        response.Should().BeOfType<NoContentResult>();

        await _service.Received().UnComplete(id);
    }
}