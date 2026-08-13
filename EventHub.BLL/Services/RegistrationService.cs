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

}