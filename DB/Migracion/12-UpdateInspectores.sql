

DECLARE @MyCursor CURSOR;
DECLARE @Id int;
DECLARE @Legajo int;
BEGIN
    SET @MyCursor = CURSOR FOR
select a.Id, b.IdInspector
from [GuardiaComunal].dbo.Inspectors a, [BasedeActas2018A].dbo.Inspectores b
where UPPER( RTRIM(a.Apellido) + a.Nombre) = UPPER(  b.Apellido)      

    OPEN @MyCursor 
    FETCH NEXT FROM @MyCursor 
    INTO @Id,@Legajo

    WHILE @@FETCH_STATUS = 0
    BEGIN
      update [GuardiaComunal].dbo.Inspectors
	  set Legajo = @Legajo
	  where id = @Id

      FETCH NEXT FROM @MyCursor 
      INTO @Id,@Legajo
    END; 

    CLOSE @MyCursor ;
    DEALLOCATE @MyCursor;
END;