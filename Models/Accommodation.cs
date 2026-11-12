using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Accommodation
{
    public string Record { get; set; } = null!;

    public DateTime Arrival { get; set; }

    public DateTime Exit { get; set; }

    public int BookingNo { get; set; }

    public string RoomDesigination { get; set; } = null!;

    public virtual Booking BookingNoNavigation { get; set; } = null!;

    public virtual ICollection<Guest> Guests { get; set; } = new List<Guest>();

    public virtual Room RoomDesiginationNavigation { get; set; } = null!;
}
