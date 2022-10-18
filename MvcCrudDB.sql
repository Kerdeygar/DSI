use master
IF NOT EXISTS (SELECT * FROM sysdatabases WHERE (name = 'MvcCrud')) 
	BEGIN
		create database MvcCrud
	END
else 
	begin 
	drop database MvcCrud
	Create database MvcCrud
	end 
go
use MvcCrud
go

-----------------------------------------------------------------------CREACION DE TABLAS--------------------------------------------------------------------------------
create Table Usuarios(
Id int identity(1,1) primary key,
Nombre varchar(50),
Fecha date,
clave varchar(50) 
)
go

create table Estudiante(
IdEstudiante int identity(1,1) primary key,
Carnet nvarchar(10),
Nombres nvarchar(50),
Apellidos nvarchar(50),
)
go

create table Materia(
IdMateria int identity(1,1) primary key,
Nombre nvarchar(100),
Descripcion nvarchar(100)
)
go

create table Alumno_Materia(
IdAlumno_Materia int identity(1,1) primary key,
IdAlumno int foreign key references Estudiante(IdEstudiante),
IdMateria int foreign key references Materia(IdMateria)
)
go

create table Notas(
IdNota int identity(1,1) primary key,
IdMateria_Alumno int foreign key references Alumno_Materia(IdAlumno_Materia),
Parcial_I float,
Parcial_II float,
Sistematicos float,
Nota_Final float,
Convocatoria_I float,
NFConvocatoria_I float,
Convocatoria_II float,
NFConvocatoria_II float
)
go

-----------------------------------------------------------------------CREACION DE  TRIGGERS--------------------------------------------------------------------------------
Create trigger RegistroAlumnoNota
on dbo.Alumno_Materia
for insert
as
declare @MateriaAlumno int
select @MateriaAlumno = i.IdAlumno_Materia from inserted as i 
insert into dbo.Notas(IdMateria_Alumno,Parcial_I,Parcial_II,Nota_Final) values (@MateriaAlumno,0,0,0)
go
---------------------------------------------------------------------------------------------------------------------------------------------------------------------------	
create trigger Update_NotaFinal
on dbo.Notas
after update
as 
declare @NotaFinal float
declare @Id int
select @NotaFinal = i.Parcial_I+i.Parcial_II, @Id = i.IdNota from inserted as i
update dbo.Notas
set Nota_Final = @NotaFinal where IdNota = @Id
go

-----------------------------------------------------------------------INSERTANDO DATOS--------------------------------------------------------------------------------

insert into Estudiante(Carnet,Nombres,Apellidos) values('2017-0249U','Crisleydy De los Angeles','madrigal Lara'),
																						  ('2017-0434U','Waleska Alejandra','mendienta Medrano'),
																						  ('2016-0873U','Keren Yumari','Acuña Duarte'),
																						  ('2016-1345U','Alberto Javier','Espinoza Ortega')
insert into Materia(Nombre,Descripcion) values ('Diseños de Sistemas de Internet','Diseños de Sistemas de Internet')

