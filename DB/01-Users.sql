insert into Rols (Nombre,Descripcion) values ('Administrador','Administrador');

insert into Usuarios (nombreusuario,enable,role_Id,Contraseña) values
('admin',1,(select id from Rols where Nombre='Administrador' ),'1234');




