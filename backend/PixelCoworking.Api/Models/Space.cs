using System;
using System.Collections.Generic;

namespace PixelCoworking.Api.Models;

public partial class Space
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public int Type { get; set; }

    public int Capacity { get; set; }

    public bool HasPrivateBathroom { get; set; }
}
