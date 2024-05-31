using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CATALOGWEB.Models;

public partial class Usuario
{
    
        public int Idu { get; set; }

        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO.")]
    [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑüÜ\s]+$", ErrorMessage = "EL CAMPO NOMBRE NO PUEDE CONTENER NÚMEROS.")]

    public string? Nombre { get; set; }

        [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
        [EmailAddress(ErrorMessage = "FORMATO DE CORREO ELECTRÓNICO INVÁLIDO")]
        public string? Correo { get; set; }

    [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
    [StringLength(18, ErrorMessage = "EL CAMPO CURP NO PUEDE TENER")]
    [RegularExpression("^[A-Z0-9]+$", ErrorMessage = "EL CAMPO CURP SÓLO PUEDE CONTENER  MÁS DE 18 CARACTERES ALFANUMÉRICOS Y DEBE ESTAR EN MAYÚSCULAS")]
    public string? Curp { get; set; }

    [Required(ErrorMessage = "ESTE CAMPO OBLIGATORIO")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "EL CAMPO EDAD SÓLO PUEDE CONTENER NÚMEROS")]
        public int? Edad { get; set; }

    [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
    public string? Genero { get; set; }

    [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
    public int? Idr { get; set; }

    public virtual Rol? oRol { get; set; }

    [Required(ErrorMessage = "ESTE CAMPO ES OBLIGATORIO")]
    public string Contraseña { get; set; }
    public bool Activo { get; set; }
}

