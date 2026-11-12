using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Room
{
    public string Desigination { get; set; } = null!;

    public int RoomNo { get; set; }

    public string Address { get; set; } = null!;

    public int Capacity { get; set; }

    public string Building { get; set; } = null!;

    public string Category { get; set; } = null!;

    public virtual ICollection<Accommodation> Accommodations { get; set; } = new List<Accommodation>();

    public virtual RoomCategory CategoryNavigation { get; set; } = null!;
}
