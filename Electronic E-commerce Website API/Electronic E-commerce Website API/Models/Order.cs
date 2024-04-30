using System;
using System.Collections.Generic;

namespace Electronic_E_commerce_Website_API.Models;

public partial class Order
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public string? Status { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime? OrderDate { get; set; }

    public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();

    public virtual User? User { get; set; }
}
