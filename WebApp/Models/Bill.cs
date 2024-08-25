using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Bill
{
    public int Id { get; set; }

    public int? MeterReadingId { get; set; }

    public decimal BillAmount { get; set; }

    public DateOnly BillDate { get; set; }

    public bool IsPaid { get; set; }

    public virtual MeterReading? MeterReading { get; set; }
}
