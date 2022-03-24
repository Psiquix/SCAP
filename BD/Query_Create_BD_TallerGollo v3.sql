use master;
drop database TallerGorroBD; 

create database TallerGorroBD;

use TallerGorroBD;
----
create table Cita(
id int identity(1,1) primary key,
idUsuario int,
fechaCita date,
horaCita datetime,
descripcion nvarchar(40),
idTipoCita int,
estado bit
);
create table TipoCita(
id int identity(1,1) primary key,
descripcion nvarchar(40),
estado bit
)

create table Usuario(
id int primary key,
nombre nvarchar(40),
apellidos nvarchar(80),
email nvarchar(50),
contrasena nvarchar(50),
idTelf int,
idRol int,
estado bit
);

create table Rol(
id int identity(1,1) primary key,
descripcion nvarchar(20),
estado bit
);

create table TelefonoUsuario(
id int identity(1,1) primary key,
numeroTelf nvarchar(20),
estado bit
);


create table Orden(
id int identity(1,1) primary key,
idUsuario int,
impuesto float,
total float,
fecha date,
estado bit
);

create table DetalleOrden(
idOrden int,
idProd int,
subtotal float,
cantidad int,
primary key(idOrden,idProd)
);


create table Producto(
id int identity(1,1) primary key,
nombre nvarchar(30),
descripcion nvarchar(50),
idTipoUnidad int,
idTipoProducto int,
idMarca int,
precioUnitario float,
cantidadNum int null,
cantidadPeso float null,
imagen Varbinary(max),
estado bit
);

create table TipoProducto(
id int identity(1,1) primary key,
descripcionTipo nvarchar(20),
estado bit
);

create table TipoUnidad(
id int identity(1,1) primary key,
descripcionTipo nvarchar(50),
estado bit
);

create table Marca(
id int identity(1,1) primary key,
idTipoProducto int,
descripcion nvarchar(50),
estado bit
);


alter table Cita add foreign key (idUsuario) references Usuario(id);
alter table Usuario add foreign key (idTelf) references TelefonoUsuario(id);
alter table Usuario add foreign key (idRol) references Rol(id);

alter table Orden add foreign key (idUsuario) references Usuario(id);
alter table DetalleOrden add foreign key (idOrden) references Orden(id);
alter table DetalleOrden add foreign key (idProd) references Producto(id);
alter table Producto add foreign key (idTipoProducto) references TipoProducto(id);
alter table Producto add foreign key (idMarca) references Marca(id);
alter table Producto add foreign key (idTipoUnidad) references TipoUnidad(id);
alter table Cita add foreign key (idTipoCita) references TipoCita(id);


insert into TipoUnidad values('Unidad',1);
insert into TipoUnidad values('Peso',1);

Insert into TipoProducto values('Aceite', 1);
Insert into TipoProducto values('Grasa', 1);
Insert into TipoProducto values('Anticongelante', 1);

Insert into Marca values(1, 'Motul', 1);
Insert into Marca values(2, 'Bardahl', 1);
Insert into Marca values(3, 'Freezetone', 1);

Insert into TipoCita values('Reparación', 1);
Insert into TipoCita values('Mecánica rápida', 1);
Insert into TipoCita values('Pintura', 1);

insert into TelefonoUsuario values ('88888888',1);

insert into Rol values ('Admin',1);
insert into Rol values ('Emp',1);

insert into Usuario values (1,'admin',' ','root@root','123',1,1,1);
insert into Usuario values (2,'emp',' ','emp@root','123',1,2,1);

select * from Rol
select * from producto
select * from Usuario
select * from TipoProducto
select * from Marca

