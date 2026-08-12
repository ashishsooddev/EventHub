using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventHub.Models;

public class Registration
{
    public int RegistrationId { get; set; }

    public string ApplicationUserId { get; set; } = string.Empty;

    public int EventId { get; set; }

    public DateTime RegistrationDate { get; set; }

    public ApplicationUser? ApplicationUser { get; set; }

    public Event? Event { get; set; }
}
