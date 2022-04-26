using System;
using System.Collections.Generic;

namespace WebApplication4.Models
{
    public partial class Persona
    {
        public int Id { get; set; }
        public string? Nombre { get; set; }
        public int? Edad { get; set; }
        public string? ClubDeportivo { get; set; }
        public string? EstadoCivil { get; set; }
        public string? NivelEstudios { get; set; }
    }
}
