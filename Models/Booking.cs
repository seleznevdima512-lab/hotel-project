using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Booking
{
    public int BookingNo { get; set; }

    public DateTime ApplicationDate { get; set; }

    public string Person { get; set; } = null!;

    public int OrganizationCard { get; set; }

    public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual Organization OrganizationCardNavigation { get; set; } = null!;

    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();
}
