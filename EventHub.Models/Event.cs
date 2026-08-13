using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models;

public class Event
{
    public int EventId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime EventDate { get; set; }

    public string Location { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public ICollection<Category> Categories { get; set; } = new List<Category>();

    public ICollection<Registration> Registrations { get; set; } =
        new List<Registration>();
}