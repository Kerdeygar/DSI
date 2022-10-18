using System;
using System.Collections.Generic;

#nullable disable

namespace MvcCRUD.Models
{
    public partial class Materium
    {
        public Materium()
        {
            AlumnoMateria = new HashSet<AlumnoMaterium>();
        }

        public int IdMateria { get; set; }
        public string Nombre { get; set; }
        public string Descripcion { get; set; }

        public virtual ICollection<AlumnoMaterium> AlumnoMateria { get; set; }
    }
}
