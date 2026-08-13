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

}