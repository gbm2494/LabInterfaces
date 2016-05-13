CREATE TABLE Estudiante(
Cedula varchar(9) PRIMARY KEY,
carne varchar(6) UNIQUE,
nombre varchar(20) not null,
apellido1 varchar(20) not null,
apellido2 varchar(20) not null,
email varchar(40),
sexo char(1) not null,
fechaNac date not null,
direccion varchar(100),
telefono varchar(12),
estado bit not null
);