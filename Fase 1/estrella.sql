use dbss2g21;

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
	genero CHAR NOT NULL
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