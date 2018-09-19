DECLARE 
@T1 TABLE (
Id Int,
Descripcion VARCHAR(MAX))

DECLARE 
@T2 TABLE (
Id Int,
Descripcion VARCHAR(MAX))

insert into @T1
select b.Id , CONVERT(nvarchar(MAX), ContravensionOtras )
from [BasedeActas2012].[dbo].[01 Base de Actas] a,  GuardiaComunal.dbo.Acts b
where ContravensionOtras is not null
and a.IdActa = b.NroActa
and a.FechaEmicion=b.FechaInfraccion
Union
select b.Id ,  CONVERT(nvarchar(MAX), ContravensionOtras ) 
from [BasedeActas2013].[dbo].[02b Base de Actas] a,  GuardiaComunal.dbo.Acts b
where ContravensionOtras is not null
and a.IdActa = b.NroActa
and a.FechaEmicion=b.FechaInfraccion
Union
select b.Id ,  CONVERT(nvarchar(MAX), ContravensionOtras ) 
from [BasedeActas2013].[dbo].[03 Base de Actas] a,  GuardiaComunal.dbo.Acts b
where ContravensionOtras is not null
and a.IdActa = b.NroActa
and a.FechaEmision=b.FechaInfraccion
Union
select b.Id ,  CONVERT(nvarchar(MAX), ContravensionOtras ) 
from [BasedeActas2014].[dbo].[03 Base de Actas] a,  GuardiaComunal.dbo.Acts b
where ContravensionOtras is not null
and a.IdActa = b.NroActa
and a.FechaEmision=b.FechaInfraccion

insert into @T2 (Id, Descripcion)
select distinct Id, Descripcion from @T1


;WITH tmp(Id,DataItem,Descripcion) AS
(
    SELECT
		Id,
        LEFT(Descripcion, CHARINDEX(',', Descripcion + ',') - 1),
        STUFF(Descripcion, 1, CHARINDEX(',', Descripcion + ','), '')
    FROM @T2
    UNION all

    SELECT
		Id,
        LEFT(Descripcion, CHARINDEX(',', Descripcion + ',') - 1),
        STUFF(Descripcion, 1, CHARINDEX(',', Descripcion + ','), '')
    FROM tmp
    WHERE
        Descripcion > ''
)

--Aca tengo las contravenciones separadas por id de acta y hago merge con la descripcion para filtrar solo las que existen
insert into GuardiaComunal.dbo.ContraventionActs ( Act_Id,Contravention_Id)
SELECT t.Id as ActId, c.Id as ContraventionId
FROM tmp t,GuardiaComunal.dbo.Contraventions c
where Upper(c.Descripcion)= UPPER( t.DataItem)
and c.Id not in (select b.Contravention_Id from GuardiaComunal.dbo.ContraventionActs b where b.Act_Id = t.Id)
