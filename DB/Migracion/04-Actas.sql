DECLARE 
@T1 TABLE (
TipoDeActa nvarchar(MAX),
NroActa VARCHAR(200),
FechaInfraccion datetime,
Tanda int,
Calle nvarchar(MAX),
Altura nvarchar(MAX),
EntreCalle nvarchar(MAX),
Barrio  nvarchar(MAX),
FechaEnvioAlJuzgado datetime,
ActaAdjunta nvarchar(MAX),
FechaCarga datetime,
Color nvarchar(MAX),
NroMotor nvarchar(MAX),
NroChasis nvarchar(MAX),
EstadoVehiculo nvarchar(MAX),
FechaEstado datetime,
TipoAgente nvarchar(MAX), 
VehiculoRetenido bit,
LicenciaRetenida bit,
TicketAlcoholemia bit,
ResultadoAlcoholemia nvarchar(MAX),
TicketAlcoholemiaAdjunto nvarchar(MAX),
Informe nvarchar(MAX),
InformeAdjunto nvarchar(MAX),
Detalle nvarchar(MAX),
Enable bit,
InspectorId int,
PoliceId int,
DomainId int,
UsuarioId int, 
VehicleBrandId int,
VehicleModelId int, 
VehicleTypeId int,
StreetId int,
NighborhoodId int,
DNI nvarchar(MAX),
Nombre nvarchar(MAX),
Apellido nvarchar(MAX),
NroLicencia nvarchar(MAX),
Dominio nvarchar(MAX),
Policia nvarchar(MAX)  )

insert into @T1
--2012
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmicion as FechaInfraccion, 1 as Tanda,  
case when a.Domicilio is not null then a.Domicilio else 'SIN ESPECIFICAR' End as Calle,
case when a.Numero is not null then CONVERT(nvarchar(255),a.Numero) else 'S/N' End as Altura,
ISNULL(a.Zona,'SIN ESPECIFICAR') as EntreCalle, ISNULL(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmicion as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmicion) FechaCarga, null as Color, null as NroMotor, null as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from [BasedeActas2012].[dbo].[01 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmicion is not null
union
--2013
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmicion as FechaInfraccion, 1 as Tanda,  
case when a.Domicilio is not null then a.Domicilio else 'SIN ESPECIFICAR' End as Calle,
case when a.Numero is not null then CONVERT(nvarchar(255),a.Numero) else 'S/N' End as Altura,
ISNULL(a.Zona,'SIN ESPECIFICAR') as EntreCalle, ISNULL(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmicion as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmicion) FechaCarga, null as Color, null as NroMotor, null as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from [BasedeActas2013].[dbo].[02b Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmicion is not null
union
--2013 B
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion, 1 as Tanda,  
case when a.Domicilio is not null then a.Domicilio else 'SIN ESPECIFICAR' End as Calle,
case when a.Numero is not null then CONVERT(nvarchar(255),a.Numero) else 'S/N' End as Altura,
ISNULL(a.Zona,'SIN ESPECIFICAR') as EntreCalle, ISNULL(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, null as Color, null as NroMotor, null as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from [BasedeActas2013].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2014
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion, 1 as Tanda,  
case when a.Domicilio is not null then a.Domicilio else 'SIN ESPECIFICAR' End as Calle,
case when a.Numero is not null then CONVERT(nvarchar(255),a.Numero) else 'S/N' End as Altura,
ISNULL(a.Zona,'SIN ESPECIFICAR') as EntreCalle, ISNULL(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, null as Color, null as NroMotor, null as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from  [BasedeActas2014].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2014 B
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
'SIN ESPECIFICAR'  Calle, 'S/N' Altura,
ISNULL(a.Zona,'SIN ESPECIFICAR') as EntreCalle, 'SIN ESPECIFICAR' as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, null as Color, null as NroMotor, null as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from  [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2015
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle, 'SIN ESPECIFICAR' as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from  [BasedeActas2015].[dbo].[05 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2015 B
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle, 'SIN ESPECIFICAR' as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from  [BasedeActas2015].[dbo].[06 Base de Actas] a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2016
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle, 'SIN ESPECIFICAR' as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else [Dominio M] end as Dominio, null
from  [BasedeActas2016A].[dbo].[Base de Actas]  a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2016 B
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle,  isnull(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else case when a.[Dominio M] is null then ISNULL(a.[DM A],a.[DM M]) else a.[Dominio M] End end as Dominio, null
from  [BasedeActas2016B].[dbo].[Base de Actas]  a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
--2017
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle,  isnull(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else case when a.[Dominio M] is null then ISNULL(a.[Dominio N A] ,a.[Dominio N M] ) else a.[Dominio M] End end as Dominio, null
from  [BasedeActas2017A].[dbo].[Base de Actas]  a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null
----2018
UNION
select case when a.Apellido = 'Oficio' or a.Apellido is null then 'Oficio' else 'Documentada' End as TipoDeActa, CONVERT(nvarchar(255),a.IdActa) as NroActa,
FechaEmision as FechaInfraccion,  Isnull(Tanda,1) as Tanda,  
ISNULL([En Calle], 'SIN ESPECIFICAR' ) Calle, 'S/N' Altura,
ISNULL(a.[Y Calle],'SIN ESPECIFICAR') as EntreCalle, isnull(a.Barrio,'SIN ESPECIFICAR') as Barrio, a.FechaEmision as FechaEnvioAlJuzgado,
null ActaAdjunto,ISNULL(a.FechadeCarga,a.FechaEmision) FechaCarga, Color as Color, NumMotor as NroMotor, NumChasis as NroChasis , 
case when Liberado = 1 then 'Liberado' else case when Remitido=1 then 'Retenido' else 'No Retenido' end end   as EstadoVehiculo,
getdate() as FechaEstado, 'Inspector' as TipoAgente, Remitido as VehiculoRetenido, Isnull(a.LicenciaRetenida,0) LicenciaRetenida, 0 as TicketAlcoholemia,
null as ResultadoAlcoholemia, null TicketAlcoholemiaAdjunto,null as Informe, null as InformeAdjunto, null as Detalle, 1 as Enable, i.Id as InspectorId, null as PoliceId,
(select Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo') as DomainID,
(select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId, 
m.Id as VehicleBrandId, null as VehicleModelId, t.Id as VehicleTypeId,null as StreetId, null as NighborhoodId, a.DNI, a.Nombre, 
case when a.Apellido ='Oficio' then null else a.Apellido end as Apellido,a.NumLicencia,
case when [Dominio A] is not null then [Dominio A] else case when a.[Dominio M] is null then ISNULL(a.[Dominio N A] ,a.[Dominio N M] ) else a.[Dominio M] End end as Dominio, null
from  [BasedeActas2018A].[dbo].[Base de Actas]  a, GuardiaComunal.dbo.Inspectors i, GuardiaComunal.dbo.VehicleTypes t,GuardiaComunal.dbo.VehicleBrands m
where Upper(i.Apellido)= substring(a.Inspector, 1,charindex(' ',a.Inspector))
and Upper(i.Nombre)=substring(a.Inspector, charindex(' ',a.Inspector),len(a.Inspector)-charindex(' ',a.Inspector)+1) 
and Upper(t.Descripcion) = Upper(a.Vehiculo)
and Upper(m.Descripcion) = Upper(a.Marca) and t.Id=m.VehicleTypeId
and a.FechaEmision is not null


insert into  GuardiaComunal.dbo.Acts
select * from @T1
