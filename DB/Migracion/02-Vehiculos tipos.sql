--Vehiculos tipos
DECLARE 
@T1 TABLE (
Vehiculo VARCHAR(200))

DECLARE 
@T1B TABLE (
Vehiculo VARCHAR(200))

DECLARE @T2 TABLE ( 
Vehiculo VARCHAR(200))


insert into @T1
select distinct(Vehiculo)
from [BasedeActas2012].[dbo].[01 Base de Actas]
where Vehiculo is not null
union
select distinct(Vehiculo)
from [BasedeActas2012].[dbo].[02 Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2013].[dbo].[02b Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2013].[dbo].[03 Base de Actas]
where Vehiculo is not null
union
select distinct(Vehiculo) 
from [BasedeActas2014].[dbo].[03 Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2015].[dbo].[05 Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2015].[dbo].[06 Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2016A].[dbo].[Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2016B].[dbo].[Base de Actas]
where Vehiculo is not null
union 
select distinct(Vehiculo) 
from [BasedeActas2016B].[dbo].[Base de Actas]
where Vehiculo is not null
union
select distinct(Vehiculo) 
from [BasedeActas2017A].[dbo].[Base de Actas]
where Vehiculo is not null
union
select distinct(Vehiculo) 
from [BasedeActas2018A].[dbo].[Base de Actas]
where Vehiculo is not null


insert into @T1B 
select distinct  Vehiculo from @T1
where Vehiculo is not null

insert into @T2
select UPPER(Vehiculo)  from @T1
EXCEPT
select UPPER(Descripcion) from GuardiaComunal.dbo.VehicleTypes

insert into GuardiaComunal.dbo.VehicleTypes (Descripcion,FechaAlta,Enable)
select Vehiculo,GETDATE(),1  from @T2



