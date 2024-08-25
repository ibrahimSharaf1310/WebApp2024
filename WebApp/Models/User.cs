using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WebApp.Models;

public partial class User
{
    public int Id { get; set; }

    [Required(ErrorMessage = "User Name is required.")]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "National ID is required.")]
    public string NationalId { get; set; } = null!;

    [Required(ErrorMessage = "Address is required.")]
    public string? Address { get; set; }

    [Required(ErrorMessage = "Meter Number is required.")]
    public string? MeterNumber { get; set; }

    [Required(ErrorMessage = "Password is required.")]
    public string? Password { get; set; }

    [Required(ErrorMessage = "Created By Admin ID is required.")]
    public int? CreatedByAdminId { get; set; }

    [Required(ErrorMessage = "Role is required.")]
    public string Role { get; set; } = null!;

    public virtual Admin? CreatedByAdmin { get; set; }

    public virtual ICollection<MeterReading> MeterReadings { get; set; } = new List<MeterReading>();
}
