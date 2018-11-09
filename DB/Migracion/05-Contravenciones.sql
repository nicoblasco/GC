--Contravencion

DECLARE 
@T1 TABLE (
IdAct Int,
IdContravencion Int)

DECLARE 
@T2 TABLE (
IdAct Int,
IdContravencion Int)

--Solo grabo las que coinciden
insert into @T1 (IdAct,IdContravencion)
select distinct b.Id,c.Id
from [BasedeActas2012].[dbo].[01 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension is not null
and a.IdActa=b.NroActa
and a.FechaEmicion=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2013].[dbo].[02b Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension is not null
and a.IdActa=b.NroActa
and a.FechaEmicion=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2013].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[03 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension1 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension1)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension2 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension2)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension3 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension3)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension4 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension4)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension5 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension5)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension6 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension6)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2014].[dbo].[04 Base de Actas] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravension7 is not null
and a.IdActa=b.NroActa
and a.FechaEmision=b.FechaInfraccion
and upper(CONVERT(nvarchar(MAX), a.Contravension7)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2015].[dbo].[Base de Contravenciones] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravencion is not null
and a.IdActas=b.NroActa
and YEAR(b.FechaInfraccion) = 2015
and upper(CONVERT(nvarchar(MAX), a.Contravencion)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2016A].[dbo].[Base de Contravenciones] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravencion is not null
and a.IdActas=b.NroActa
and YEAR(b.FechaInfraccion) = 2016
and upper(CONVERT(nvarchar(MAX), a.Contravencion)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2016B].[dbo].[Base de Contravenciones] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravencion is not null
and a.IdActas=b.NroActa
and YEAR(b.FechaInfraccion) = 2016
and upper(CONVERT(nvarchar(MAX), a.Contravencion)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2017A].[dbo].[Base de Contravenciones] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravencion is not null
and a.IdActas=b.NroActa
and YEAR(b.FechaInfraccion) = 2017
and upper(CONVERT(nvarchar(MAX), a.Contravencion)) = Upper(c.Descripcion)
union
select distinct b.Id,c.Id
from [BasedeActas2018A].[dbo].[Base de Contravenciones] a, GuardiaComunal.dbo.Acts b,GuardiaComunal.dbo.Contraventions c
where a.Contravencion is not null
and a.IdActas=b.NroActa
and YEAR(b.FechaInfraccion) = 2018
and upper(CONVERT(nvarchar(MAX), a.Contravencion)) = Upper(c.Descripcion)

insert into @T2 (IdAct, IdContravencion)
select distinct IdAct, IdContravencion from @T1


insert into GuardiaComunal.dbo.ContraventionActs (Contravention_Id, Act_Id)
select IdContravencion, IdAct  from @T2
