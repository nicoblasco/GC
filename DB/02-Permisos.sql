select Id from [dbo].[Modules] where Descripcion='Actas'

insert into [dbo].[Modules] (Descripcion,Enable) values ('Actas',1);
insert into [dbo].[Modules] (Descripcion,Enable) values ('AMB Maestros',1);
insert into [dbo].[Modules] (Descripcion,Enable) values ('Vehiculos',1);
insert into [dbo].[Modules] (Descripcion,Enable) values ('Reportes',1);
insert into [dbo].[Modules] (Descripcion,Enable) values ('Configuración',1);

insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Altas',1,'Acts',1,(select Id from [dbo].[Modules] where Descripcion='Actas'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Altas',1,'Seach',2,(select Id from [dbo].[Modules] where Descripcion='Actas'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Contravenciones',1,'Contraventions',1,(select Id from [dbo].[Modules] where Descripcion='AMB Maestros'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Observaciones',1,'Observations',2,(select Id from [dbo].[Modules] where Descripcion='AMB Maestros'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Inspectores',1,'Inspectors',3,(select Id from [dbo].[Modules] where Descripcion='AMB Maestros'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Policias',1,'Police',4,(select Id from [dbo].[Modules] where Descripcion='AMB Maestros'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Tipos',1,'VehicleTypes',1,(select Id from [dbo].[Modules] where Descripcion='Vehiculos'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Marcas',1,'VehicleBrands',2,(select Id from [dbo].[Modules] where Descripcion='Vehiculos'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Modelos',1,'VehicleModels',3,(select Id from [dbo].[Modules] where Descripcion='Vehiculos'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Reporte 1',1,'Reporte1',1,(select Id from [dbo].[Modules] where Descripcion='Reportes'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Reporte 2',1,'Reporte1',2,(select Id from [dbo].[Modules] where Descripcion='Reportes'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Reporte 3',1,'Reporte1',3,(select Id from [dbo].[Modules] where Descripcion='Reportes'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Reporte 4',1,'Reporte1',4,(select Id from [dbo].[Modules] where Descripcion='Reportes'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Roles',1,'Rols',1,(select Id from [dbo].[Modules] where Descripcion='Configuración'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Usuarios',1,'Usuarios',2,(select Id from [dbo].[Modules] where Descripcion='Configuración'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Permisos',1,'Permissions',3,(select Id from [dbo].[Modules] where Descripcion='Configuración'))
insert into [dbo].[Windows] (Descripcion,Enable,Url,Orden,Module_Id) values ('Auditoria',1,'Audits',4,(select Id from [dbo].[Modules] where Descripcion='Configuración'))



