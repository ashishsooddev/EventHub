using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EventHub.DAL.Data;
using EventHub.Models;
using Microsoft.EntityFrameworkCore;

namespace EventHub.BLL.Services;

public class RegistrationService
{
    private readonly ApplicationDbContext _context;

    public RegistrationService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<List<Registration>> GetAllAsync()
    {
        return await _context.Registrations
            .Include(r => r.Event)
            .ToListAsync();
    }

    public async Task<Registration?> GetByIdAsync(int id)
    {
        return await _context.Registrations
            .Include(r => r.Event)
            .FirstOrDefaultAsync(r => r.RegistrationId == id);
    }
    public async Task CreateAsync(Registration registration)
    {
        _context.Registrations.Add(registration);
        await _context.SaveChangesAsync();
    }
    public async Task UpdateAsync(Registration registration)
    {
        _context.Registrations.Update(registration);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var registration = await _context.Registrations
            .FindAsync(id);

        if (registration != null)
        {
            _context.Registrations.Remove(registration);
            await _context.SaveChangesAsync();
        }
    }
}