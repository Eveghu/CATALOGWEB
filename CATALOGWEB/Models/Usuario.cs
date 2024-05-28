using System;
using System.Collections.Generic;

namespace CATALOGWEB.Models;

public partial class Usuario
{
    public int Idu { get; set; }

    public string? Nombre { get; set; }

    public string? Correo { get; set; }

    public string? Curp { get; set; }

    public int? Edad { get; set; }

    public string? Genero { get; set; }

    public int? Idr { get; set; }

    public virtual Rol? oRol { get; set; }
}
