using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace MvcCRUD.Models
{
    public partial class MvcCrudContext : DbContext
    {
        public MvcCrudContext()
        {
        }

        public MvcCrudContext(DbContextOptions<MvcCrudContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AlumnoMaterium> AlumnoMateria { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Materium> Materia { get; set; }
        public virtual DbSet<Nota> Notas { get; set; }
        public virtual DbSet<Usuario> Usuarios { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
//                optionsBuilder.UseSqlServer("server=KERYMS-G\\KERYMS_G_REYES; Database=MvcCrud; integrated security =true;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Modern_Spanish_CI_AS");

            modelBuilder.Entity<AlumnoMaterium>(entity =>
            {
                entity.HasKey(e => e.IdAlumnoMateria)
                    .HasName("PK__Alumno_M__37E7E3707E778859");

                entity.ToTable("Alumno_Materia");

                entity.Property(e => e.IdAlumnoMateria).HasColumnName("IdAlumno_Materia");

                entity.HasOne(d => d.IdAlumnoNavigation)
                    .WithMany(p => p.AlumnoMateria)
                    .HasForeignKey(d => d.IdAlumno)
                    .HasConstraintName("FK__Alumno_Ma__IdAlu__2A4B4B5E");

                entity.HasOne(d => d.IdMateriaNavigation)
                    .WithMany(p => p.AlumnoMateria)
                    .HasForeignKey(d => d.IdMateria)
                    .HasConstraintName("FK__Alumno_Ma__IdMat__2B3F6F97");
            });

            modelBuilder.Entity<Estudiante>(entity =>
            {
                entity.HasKey(e => e.IdEstudiante)
                    .HasName("PK__Estudian__B5007C24748E8F72");

                entity.ToTable("Estudiante");

                entity.Property(e => e.Apellidos).HasMaxLength(50);

                entity.Property(e => e.Carnet).HasMaxLength(10);

                entity.Property(e => e.Nombres).HasMaxLength(50);
            });

            modelBuilder.Entity<Materium>(entity =>
            {
                entity.HasKey(e => e.IdMateria)
                    .HasName("PK__Materia__EC174670CE89E632");

                entity.Property(e => e.Descripcion).HasMaxLength(100);

                entity.Property(e => e.Nombre).HasMaxLength(100);
            });

            modelBuilder.Entity<Nota>(entity =>
            {
                entity.HasKey(e => e.IdNota)
                    .HasName("PK__Notas__4B2ACFF208AC3A4F");

                entity.Property(e => e.ConvocatoriaI).HasColumnName("Convocatoria_I");

                entity.Property(e => e.ConvocatoriaIi).HasColumnName("Convocatoria_II");

                entity.Property(e => e.IdMateriaAlumno).HasColumnName("IdMateria_Alumno");

                entity.Property(e => e.NfconvocatoriaI).HasColumnName("NFConvocatoria_I");

                entity.Property(e => e.NfconvocatoriaIi).HasColumnName("NFConvocatoria_II");

                entity.Property(e => e.NotaFinal).HasColumnName("Nota_Final");

                entity.Property(e => e.Parcial_I).HasColumnName("Parcial_I");

                entity.Property(e => e.Parcial_II).HasColumnName("Parcial_II");

                entity.HasOne(d => d.IdMateriaAlumnoNavigation)
                    .WithMany(p => p.Nota)
                    .HasForeignKey(d => d.IdMateriaAlumno)
                    .HasConstraintName("FK__Notas__IdMateria__2E1BDC42");
            });

            modelBuilder.Entity<Usuario>(entity =>
            {
                entity.Property(e => e.Clave)
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("clave");

                entity.Property(e => e.Fecha).HasColumnType("date");

                entity.Property(e => e.Nombre)
                    .HasMaxLength(50)
                    .IsUnicode(false);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
