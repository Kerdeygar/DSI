using System;
using System.Collections.Generic;

#nullable disable

namespace MvcCRUD.Models
{
    public partial class Estudiante
    {
        public Estudiante()
        {
            AlumnoMateria = new HashSet<AlumnoMaterium>();
        }

        public int IdEstudiante { get; set; }
        public string Carnet { get; set; }
        public string Nombres { get; set; }
        public string Apellidos { get; set; }

        public virtual ICollection<AlumnoMaterium> AlumnoMateria { get; set; }
    }
}
