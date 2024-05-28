using System;
using System.Collections.Generic;

namespace CATALOGWEB.Models;

public partial class Rol
{
    public int Idr { get; set; }

    public string? Rol1 { get; set; }

    public virtual ICollection<Usuario> Usuarios { get; set; } = new List<Usuario>();
}
