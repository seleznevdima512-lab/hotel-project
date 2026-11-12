using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Invoice
{
    public int InvoiceNo { get; set; }

    public DateTime IssueData { get; set; }

    public decimal AmountDue { get; set; }

    public int BookingNo { get; set; }

    public virtual Booking BookingNoNavigation { get; set; } = null!;
}
