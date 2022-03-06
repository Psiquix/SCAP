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
idTipoCita int
);
create table TipoCita(
id int identity(1,1) primary key,
descripcion nvarchar(40)
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
descripcionTipo nvarchar(20)
);

create table TipoUnidad(
id int identity(1,1) primary key,
descripcionTipo nvarchar(20)
);

create table Marca(
id int identity(1,1) primary key,
idTipoProducto int,
descripcion nvarchar(15)
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


insert into TelefonoUsuario values ('123',1);
insert into Rol values ('a',1);

insert into Usuario values (2,'admin','test','123','123',1,1,1);
insert into TipoProducto values ('babayaga');
insert into Marca values (1,'test marca');
insert into TipoUnidad values ('test unidad');
insert into Producto values ('testProd','test',1,1,1,100,30,null,111,1);

select * from producto
select * from Usuario