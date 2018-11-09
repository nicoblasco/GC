--Doy de Alta todas las personas

--Vehiculos tipos
DECLARE 
@T1 TABLE (
DNI nvarchar(MAX),
Apellido nvarchar(MAX),
Nombre nvarchar(MAX),
Calle nvarchar(MAX),
Altura nvarchar(MAX),
EntreCalle nvarchar(MAX),
Partido nvarchar(MAX))

DECLARE 
@T2 TABLE (
DNI nvarchar(MAX),
Apellido nvarchar(MAX),
Nombre nvarchar(MAX),
Calle nvarchar(MAX),
Altura nvarchar(MAX),
EntreCalle nvarchar(MAX),
Partido nvarchar(MAX))


insert into @T1
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2012].[dbo].[02 Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2013].[dbo].[03 Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2014].[dbo].[04 Liberados]l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2015].[dbo].[05 Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2015].[dbo].[06 Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2016A].[dbo].[Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2016B].[dbo].[Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2017A].[dbo].[Liberados] l
UNION
select distinct CONVERT(NVARCHAR(MAX), CONVERT(INT,l.DNI)), l.Apellido, l.Nombre, l.Domicilio as Calle, l.Numero as Altura, l.Entre as EntreCalle, l.Partido
from [BasedeActas2018A].[dbo].[Liberados] l

insert into @T2 (DNI,Apellido, Nombre, Calle, Altura, EntreCalle, Partido)
select distinct DNI,isnull(Apellido,'NN'), isnull(Nombre,'NN'),  Calle, Altura, EntreCalle, Partido from @T1
where DNI is not null



insert into GuardiaComunal.dbo.People (DNI,Apellido, Nombre, Calle, Altura, EntreCalle, Partido)
select DNI,Apellido,Nombre, Calle,Altura, EntreCalle, Partido from @T2 where DNI not in (select DNI from GuardiaComunal.dbo.People)





