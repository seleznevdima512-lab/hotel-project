using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class ProvidedService
{
    public int DocumentNo { get; set; }

    public int Qauntity { get; set; }

    public DateTime Date { get; set; }

    public int ServiceCode { get; set; }

    public int BookingNo { get; set; }

    public virtual Booking BookingNoNavigation { get; set; } = null!;

    public virtual Service ServiceCodeNavigation { get; set; } = null!;
}
