using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public partial class MeterReading
{
    public int Id { get; set; }

    public int? UserId { get; set; }
    [Required]
    public DateOnly ReadingDate { get; set; }

    [Required]
    [Range(0, double.MaxValue, ErrorMessage = "Please enter a valid meter reading")]
    public decimal MeterReading1 { get; set; }

    public virtual ICollection<Bill> Bills { get; set; } = new List<Bill>();

    public virtual User? User { get; set; }
    
}
