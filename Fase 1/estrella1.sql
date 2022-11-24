use dbss2g21;

DROP TABLE IF EXISTS fecha;
DROP TABLE IF EXISTS region;
DROP TABLE IF EXISTS departemento;
DROP TABLE IF EXISTS doctor;
DROP TABLE IF EXISTS hospital;
DROP TABLE IF EXISTS paciente;
DROP TABLE IF EXISTS caso;


create table fecha(
	cod INT PRIMARY KEY IDENTITY(1,1),
	dia VARCHAR(50) NOT NULL,
	mes VARCHAR(50) NOT NULL,
	semestre VARCHAR(50) NOT NULL,
	anio VARCHAR(50) NOT NULL
);

create table region(
	cod INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(150) NOT NULL
);

create table departamento(
	cod INT PRIMARY KEY IDENTITY(1,1),
	nombre VARCHAR(150) NOT NULL
);

create table doctor(
	cod INT PRIMARY KEY NOT NULL,
	nombre VARCHAR(150) NOT NULL
);

create table hospital(
	cod INT PRIMARY KEY NOT NULL,
	nombre VARCHAR(150) NOT NULL,
	direccion VARCHAR(150) NOT NULL
);

create table paciente(
	dpi INT PRIMARY KEY NOT NULL,
	nombre VARCHAR(150) NOT NULL,
	fecha_nacimiento DATE NOT NULL,
	edad INT NOT NULL,
	genero VARCHAR(15) NOT NULL
);

create table caso(
	cod_fecha INT NOT NULL,
	cod_region INT NOT NULL,
	cod_departamento INT NOT NULL,
	cod_doctor INT NOT NULL,
	cod_hospital INT NOT NULL,
	dpi_paciente INT NOT NULL,
    cantidad INT NOT NULL,
	tipo VARCHAR(100) NOT NULL,
	FOREIGN KEY (cod_hospital) REFERENCES hospital(cod),
	FOREIGN KEY (cod_region) REFERENCES region(cod),
	FOREIGN KEY (cod_departamento) REFERENCES departamento(cod),
	FOREIGN KEY (cod_doctor) REFERENCES doctor(cod),
	FOREIGN KEY (cod_hospital) REFERENCES hospital(cod),
	FOREIGN KEY (dpi_paciente) REFERENCES paciente(dpi)
);

CREATE TABLE error(
    linea int NOT NULL,
    descripcion nvarchar(100) NOT NULL,
    completitud char(1) NOT NULL,
    credibilidad char(1) NOT NULL,
    precision char(1) NOT NULL,
    consitencia char(1) NOT NULL,
    interpretabilidad char(1) NOT NULL,
 CONSTRAINT PK_Error PRIMARY KEY CLUSTERED 
(
    linea,
    descripcion
)
);