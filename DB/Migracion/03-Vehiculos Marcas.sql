--Vehiculos Marcas
DECLARE 
@T1 TABLE (
Vehiculo INT,
Marca VARCHAR(200))

DECLARE 
@T1B TABLE (
Vehiculo INT,
Marca VARCHAR(200))

DECLARE @T2 TABLE ( 
Vehiculo INT,
Marca VARCHAR(200))

insert into @T1 (Vehiculo, Marca)
select DISTINCT  b.Id, Marca
from [BasedeActas2012].[dbo].[01 Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union
select  DISTINCT  b.Id, Marca 
from [BasedeActas2013].[dbo].[02b Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2013].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union
select  DISTINCT  b.Id, Marca
from [BasedeActas2014].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2015].[dbo].[05 Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2015].[dbo].[06 Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2016A].[dbo].[Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2016B].[dbo].[Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union 
select  DISTINCT  b.Id, Marca
from [BasedeActas2016B].[dbo].[Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union
select  DISTINCT  b.Id, Marca 
from [BasedeActas2017A].[dbo].[Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null
union
select  DISTINCT  b.Id, Marca 
from [BasedeActas2018A].[dbo].[Base de Actas] a, GuardiaComunal.dbo.VehicleTypes b
where a.Vehiculo is not null 
and a.Vehiculo = b.Descripcion
and a.Marca is not null

insert into @T1B 
select distinct Vehiculo, Marca  from @T1
where Marca is not null

insert into @T2 (Vehiculo, Marca)
select Vehiculo,UPPER(Marca) from @T1
EXCEPT
select [VehicleTypeId], UPPER(Descripcion) from GuardiaComunal.dbo.VehicleBrands

insert into GuardiaComunal.dbo.VehicleBrands (Descripcion,FechaAlta,Enable,VehicleTypeId )
select Marca,GETDATE(),1 ,Vehiculo from @T2

