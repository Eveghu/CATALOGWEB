using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CATALOGWEB.Models;

public partial class Usuario
{
    
        public int Idu { get; set; }

        [Required(ErrorMessage = "EL CAMPO NOMBRE ES OBLIGATORIO.")]
        [RegularExpression("^[a-zA-Z]+$", ErrorMessage = "EL CAMPO NOMBRE NO PUEDE CONTENER NÚMEROS.")]
        public string? Nombre { get; set; }

        [Required(ErrorMessage = "EL CAMPO CORREO ELECTRÓNICO ES OBLIGATORIO")]
        [EmailAddress(ErrorMessage = "FORMATO DE CORREO ELECTRÓNICO INVÁLIDO")]
        public string? Correo { get; set; }

        [Required(ErrorMessage = "EL CAMPO CURP ES OBLIGATORIO")]
        [StringLength(18, ErrorMessage = "EL CAMPO CURP NO PUEDE TENER MÁS DE 18 CARACTERES")]
        [RegularExpression("^[a-zA-Z0-9]+$", ErrorMessage = "EL CAMPO CURP SÓLO PUEDE CONTENER CARACTERES ALFANUMÉRICOS")]
        public string? Curp { get; set; }

        [Required(ErrorMessage = "EL CAMPO EDAD ES OBLIGATORIO")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "EL CAMPO EDAD SÓLO PUEDE CONTENER NÚMEROS")]
        public int? Edad { get; set; }

        public string? Genero { get; set; }

        public int? Idr { get; set; }

        public virtual Rol? oRol { get; set; }

        public string Contraseña { get; set; }
    }
}

