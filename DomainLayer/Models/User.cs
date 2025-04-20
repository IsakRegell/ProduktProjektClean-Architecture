using System;
using System.Collections.Generic;

namespace DomainLayer.Entities;

public partial class User
{
    public int Id { get; set; }

    public string FullName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
