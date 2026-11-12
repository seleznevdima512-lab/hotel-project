using System;
using System.Collections.Generic;

namespace hotel.Models;

public partial class Service
{
    public int ServiceCode { get; set; }

    public string Name { get; set; } = null!;

    public int Quantity { get; set; }

    public decimal Price { get; set; }

    public virtual ICollection<ProvidedService> ProvidedServices { get; set; } = new List<ProvidedService>();
}
