using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models;

public class DashboardViewModel
{
    public int TotalEvents { get; set; }

    public int TotalCategories { get; set; }

    public int TotalRegistrations { get; set; }

    public List<Event> UpcomingEvents { get; set; } = new List<Event>();
}
