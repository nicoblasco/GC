WITH CTE AS(
   SELECT DNI,Apellido,Nombre,
       RN = ROW_NUMBER()OVER(PARTITION BY DNI ORDER BY DNI)
   FROM GuardiaComunal.dbo.People
)
delete FROM CTE WHERE RN > 1