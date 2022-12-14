DROP TABLE IF EXISTS tmp;
create table tmp
(
	fila int not null,
	COD_HOSPITAL VARCHAR(150) null,
	NOMBRE_HOSPITAL VARCHAR(300) null,
	DIRECCION_HOSPITAL VARCHAR(350) null,
	REGION VARCHAR(200) null,
	DEPARTAMENTO VARCHAR(150) null,
	COD_DOCTOR VARCHAR(150) null,
	NOMBRE_DOCTOR VARCHAR(200) null,
	FECHACASO VARCHAR(50) null,
	DPI VARCHAR(150) null,
	NOMBREPACIENTE VARCHAR(200) null,
	FECHANACPERSONA VARCHAR(50) null,
	GENERO VARCHAR(50) null,
	TIPOCASO VARCHAR(300) null
);