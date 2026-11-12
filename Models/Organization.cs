using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Organization
{
    public int OrganizationCard { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();
}
