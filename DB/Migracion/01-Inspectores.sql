--Inspectores
DECLARE 
@T1 TABLE (
Inspector VARCHAR(200))

DECLARE @T1B TABLE (
Inspector VARCHAR(200))

DECLARE @T2 TABLE ( 
Apellido VARCHAR(200),
Nombre VARCHAR(200))

DECLARE @T3 TABLE ( 
Apellido VARCHAR(200),
Nombre VARCHAR(200))

insert into @T1
select distinct(Inspector)
from [BasedeActas2012].[dbo].[01 Base de Actas]
union
select distinct(Inspector)
from [BasedeActas2012].[dbo].[02 Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2013].[dbo].[02b Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2013].[dbo].[03 Base de Actas]
union
select distinct(Inspector) 
from [BasedeActas2014].[dbo].[03 Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2015].[dbo].[05 Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2015].[dbo].[06 Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2016A].[dbo].[Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2016B].[dbo].[Base de Actas]
union 
select distinct(Inspector) 
from [BasedeActas2016B].[dbo].[Base de Actas]
union
select distinct(Inspector) 
from [BasedeActas2017A].[dbo].[Base de Actas]
union
select distinct(Inspector) 
from [BasedeActas2018A].[dbo].[Base de Actas]

insert into @T1B 
select distinct  Inspector from @T1
where Inspector is not null


insert into @T2 (Apellido,Nombre)
select substring(Inspector, 1,charindex(' ',Inspector)) as InspectorLastName  ,substring(Inspector, charindex(' ',Inspector),len(Inspector)-charindex(' ',Inspector)+1) InspectorName
from @T1B

insert into @T3  (Nombre,Apellido)
select UPPER( Nombre), UPPER(Apellido)  from @T2 
where Nombre is not null and Apellido is not null
EXCEPT
select Nombre, Apellido from GuardiaComunal.dbo.Inspectors



insert into GuardiaComunal.dbo.Inspectors (DNI,Nombre,Apellido,FechaAlta,Enable)
select null,Nombre,Apellido,GETDATE(),1  from @T3





