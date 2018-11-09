--Liberaciones
DECLARE 
@T1 TABLE (
NroLiberacion nvarchar(MAX),
FechaDeLiberacion datetime,
NroJuzgado nvarchar(MAX),
FechaCarga datetime,
Convenio nvarchar(MAX),
Cuotas int,
Acarreo decimal(18, 2),
NroRecibo nvarchar(MAX),
Importe decimal(18, 2),
MontoEnCuotas decimal(18, 2),
FechaEmisionRecibo datetime,
Enable bit,
ActaId int,
LiberationPlaceId int,
PersonId int,
UsuarioId int)

insert into @T1
--2012
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,l.IdActa, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2012].[dbo].[02 Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2011,2012)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2013
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2013].[dbo].[03 Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2012,2013)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2014
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2013].[dbo].[03 Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2012,2013)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2014
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2014].[dbo].[04 Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2012,2013,2014)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2015
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2015].[dbo].[05 Liberados]l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2014,2015)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2015 B
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2015].[dbo].[06 Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2014,2015)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2016
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2016A].[dbo].[Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2015,2016)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2016 B
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2016B].[dbo].[Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2015,2016)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2017
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2017A].[dbo].[Liberados]l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2016,2017)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null
UNION
--2018
select l.IdLiberado as NroLiberacion, isnull(l.Fecha,FechaDeCarga) as FechaDeLiberacion, l.NumJuzgado as NroJuzgado, Isnull(l.FechaDeCarga,l.Fecha) as FechaCarga,
l.Convenio as Convenio, isnull(l.CantCuotas,0) as Cuotas, isnull(l.Acarreo,0) as Acarreo, isnull(l.NroRecibo,0) as NroRecibo, 
isnull(l.Importe,0) as Importe, isnull(l.MontoCuotas,0) as MontoEnCuotas, isnull(l.FechaEmision, l.FechaDeCarga) as FechaEmisionRecibo, 
1 as Enable,a.Id, lp.Id,
p.Id as PersonId, (select UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin') as UsuarioId
from [BasedeActas2018A].[dbo].[Liberados] l, GuardiaComunal.dbo.People p,  GuardiaComunal.dbo.Acts a, GuardiaComunal.dbo.LiberationPlaces lp
where l.DNI=p.DNI
and a.NroActa = l.IdActa
and YEAR (a.FechaCarga ) in (2016,2017,2018)
and lp.Descripcion = l.LiberadoDesde
and l.FechaEmision is not null


insert into GuardiaComunal.dbo.Liberations (NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,
NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable,ActaId,LiberationPlaceId,PersonId ,UsuarioId )
select NroLiberacion,FechaDeLiberacion,NroJuzgado,FechaCarga,Convenio,Cuotas,Acarreo,
NroRecibo,Importe,MontoEnCuotas,FechaEmisionRecibo,Enable,ActaId,LiberationPlaceId,PersonId ,UsuarioId 
from @T1

--SELECT ActaId, count(*)
--FROM @T1
--GROUP BY ActaId
--HAVING count(*) > 1

--select * from @T1
--where ActaId=31950