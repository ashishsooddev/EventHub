using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHub.DAL.Data;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.BLL.Services;

public class EventService
{
    private readonly ApplicationDbContext _context;
    public EventService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Event>> GetAllAsync()
    {
        return await _context.Events
            .Include(e => e.Categories)
            .Include(e => e.Registrations)
            .ToListAsync();
    }

    public async Task<Event?> GetByIdAsync(int id)
    {
        return await _context.Events
            .Include(e => e.Categories)
            .Include(e => e.Registrations)
            .FirstOrDefaultAsync(e => e.EventId == id);
    }

    public async Task CreateAsync(Event eventItem, int[] categoryIds)
    {
        foreach (int categoryId in categoryIds)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category != null)
            {
                eventItem.Categories.Add(category);
            }
        }
        _context.Events.Add(eventItem);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Event eventItem, int[] categoryIds)
    {
        var existingEvent = await _context.Events
            .Include(e => e.Categories)
            .FirstOrDefaultAsync(e => e.EventId == eventItem.EventId);

        if (existingEvent == null)
        {
            return;
        }

        existingEvent.Title = eventItem.Title;
        existingEvent.Description = eventItem.Description;
        existingEvent.EventDate = eventItem.EventDate;
        existingEvent.Location = eventItem.Location;
        existingEvent.Capacity = eventItem.Capacity;

        existingEvent.Categories.Clear();

        foreach (int categoryId in categoryIds)
        {
            var category = await _context.Categories.FindAsync(categoryId);

            if (category != null)
            {
                existingEvent.Categories.Add(category);
            }
        }

        await _context.SaveChangesAsync();
    }
    public async Task DeleteAsync(int id)
    {
        var eventItem = await _context.Events
            .FindAsync(id);

        if (eventItem != null)
        {
            _context.Events.Remove(eventItem);

            await _context.SaveChangesAsync();
        }
    }
    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _context.Categories
            .OrderBy(c => c.Name)
            .ToListAsync();
    }
}