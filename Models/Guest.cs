using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Guest
{
    public int GuestCard { get; set; }

    public string FullName { get; set; } = null!;

    public DateTime BirthDay { get; set; }

    public string Record { get; set; } = null!;

    public virtual Accommodation RecordNavigation { get; set; } = null!;
}
