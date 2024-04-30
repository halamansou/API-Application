using System;
using System.Collections.Generic;

namespace Electronic_E_commerce_Website_API.Models;

public partial class Favorite
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
