using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class RoomCategory
{
    public string Category { get; set; } = null!;

    public decimal PriceNight { get; set; }

    public virtual ICollection<Room> Rooms { get; set; } = new List<Room>();
}
