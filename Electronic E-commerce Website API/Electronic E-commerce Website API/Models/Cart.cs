﻿using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Electronic_E_commerce_Website_API.Models;

public partial class Cart
{
    public int Id { get; set; }

    public int? UserId { get; set; }

    public int? ProductId { get; set; }

    public int? Quantity { get; set; }

    [JsonIgnore]
    public virtual Product? Product { get; set; }

    public virtual User? User { get; set; }
}
