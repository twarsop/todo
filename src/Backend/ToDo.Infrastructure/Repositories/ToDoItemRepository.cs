﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ToDo.Application.Common.Interfaces;
using ToDo.Domain.Entities;
using ToDo.Infrastructure.Common;
using ToDo.Infrastructure.DbItems;

namespace ToDo.Infrastructure.Repositories;

public class ToDoItemRepository : IToDoItemRepository
{
    private readonly SqlLiteContext _context;
    private readonly IMapper _mapper;

    public ToDoItemRepository(SqlLiteContext context, IMapper mapper)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    public async Task<ToDoItem> Create(ToDoItem toDoItem)
    {
        var toDoItemDbItem = new ToDoItemDbItem(toDoItem.Description);
        await _context.AddAsync(toDoItemDbItem);
        await _context.SaveChangesAsync();
        return _mapper.Map<ToDoItem>(toDoItemDbItem);

    }
    public async Task<ToDoItem?> Read(Guid id)
    {
        var toDoItemDbItem = await ReadDbItem(id);
        return toDoItemDbItem == null ? null : _mapper.Map<ToDoItem>(toDoItemDbItem);
    }

    public async Task<IEnumerable<ToDoItem>> ReadAll()
    {
        var toDoItemDbItems = await _context.ToDoItems.ToListAsync();

        return _mapper.Map<IEnumerable<ToDoItem>>(toDoItemDbItems);
    }

    public async Task Update(ToDoItem toDoItem)
    {
        var toDoItemDbItem = await ReadDbItem(toDoItem.Id);
        if (toDoItemDbItem != null)
        {
            _mapper.Map(toDoItem, toDoItemDbItem);
            await _context.SaveChangesAsync();
        }
    }

    public async void Delete(Guid id)
    {
        var toDoItemDbItem = await ReadDbItem(id);
        if (toDoItemDbItem != null)
        {
            _context.Remove(toDoItemDbItem);
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> Count()
    {
        return await _context.ToDoItems.CountAsync();
    }

    private async Task<ToDoItemDbItem?> ReadDbItem(Guid id)
    {
        return await _context.ToDoItems.Where(t => t.Id == id).FirstOrDefaultAsync();
    }
}
