using System;
using System.Collections.Generic;

#nullable disable

namespace MvcCRUD.Models
{
    public partial class Nota
    {
        public int IdNota { get; set; }
        public int? IdMateriaAlumno { get; set; }
        public double? Parcial_I { get; set; }
        public double? Parcial_II { get; set; }
        public double? Sistematicos { get; set; }
        public double? NotaFinal { get; set; }
        public double? ConvocatoriaI { get; set; }
        public double? NfconvocatoriaI { get; set; }
        public double? ConvocatoriaIi { get; set; }
        public double? NfconvocatoriaIi { get; set; }

        public virtual AlumnoMaterium IdMateriaAlumnoNavigation { get; set; }
    }
}
