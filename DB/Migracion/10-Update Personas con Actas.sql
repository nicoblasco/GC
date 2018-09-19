DECLARE 
@T1 TABLE (
DNI nvarchar(MAX),
Apellido nvarchar(MAX),
Nombre nvarchar(MAX))

insert into @T1 (DNI,Apellido, Nombre)
select distinct  DNI,Apellido,Nombre from GuardiaComunal.dbo.Acts where DNI not in (select DNI from  GuardiaComunal.dbo.People)
and Dni not  like '%[^0-9]%' 
and Apellido is not null
and Nombre is not null


insert into GuardiaComunal.dbo.People (DNI,Apellido,Nombre)
select distinct  CONVERT (numeric(15),DNI),Apellido,Nombre from @T1
