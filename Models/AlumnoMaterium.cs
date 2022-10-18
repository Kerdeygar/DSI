using System;
using System.Collections.Generic;

#nullable disable

namespace MvcCRUD.Models
{
    public partial class AlumnoMaterium
    {
        public AlumnoMaterium()
        {
            Nota = new HashSet<Nota>();
        }

        public int IdAlumnoMateria { get; set; }
        public int? IdAlumno { get; set; }
        public int? IdMateria { get; set; }

        public virtual Estudiante IdAlumnoNavigation { get; set; }
        public virtual Materium IdMateriaNavigation { get; set; }
        public virtual ICollection<Nota> Nota { get; set; }
    }
}
