using System;
using System.Collections.Generic;

namespace WebApp.Models;

public partial class Admin
{
    public int Id { get; set; }

    public string AdminName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string? Password { get; set; }

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
