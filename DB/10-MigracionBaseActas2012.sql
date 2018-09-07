

Declare
@IdActa int,
@FechaEmicion datetime,
@FechadeCarga datetime,
@ApellidoOld nvarchar(50),
@NombreOld nvarchar(50),
@Domicilio nvarchar(50),
@Numero int,
@BarrioOld nvarchar(50),
@Localidad nvarchar(50),
@Partido nvarchar(50),
@DNIOld nvarchar(50),
@NumLicencia nvarchar(50),
@Zona nvarchar(255),
@InspectorLastName nvarchar(255),
@InspectorName nvarchar(255),
@Inspector nvarchar(255),
@Contravension nvarchar(MAX),
@ContravensionOtras nvarchar(MAX),
@Observaciones ntext,
@Liberado bit,
@LicenciaRetenidaOld bit,
@Vehiculo nvarchar(50),
@Marca nvarchar(50),
@DominioA nvarchar(50),
@DominioM nvarchar(255),
@SinEspecificar nvarchar(50),
--New Parametros de la nueva base de datos
@NroActa nvarchar(MAX),
@TipoDeActa nvarchar(MAX),
@FechaInfraccion datetime,
@Tanda int,
@Calle nvarchar(MAX),
@Altura nvarchar(MAX),
@EntreCalle nvarchar(MAX),
@Barrio  nvarchar(MAX),
@FechaEnvioAlJuzgado datetime,
@ActaAdjunta nvarchar(MAX),
@FechaCarga nvarchar(MAX),
@Color nvarchar(MAX),
@NroMotor nvarchar(MAX),
@NroChasis nvarchar(MAX),
@EstadoVehiculo nvarchar(MAX),
@FechaEstado datetime,
@TipoAgente nvarchar(MAX), 
@VehiculoRetenido bit,
@LicenciaRetenida bit,
@TicketAlcoholemia bit,
@ResultadoAlcoholemia nvarchar(MAX),
@TicketAlcoholemiaAdjunto nvarchar(MAX),
@Informe nvarchar(MAX),
@InformeAdjunto nvarchar(MAX),
@Detalle nvarchar(MAX),
@Enable bit,
@InspectorId int,
@PoliceId int,
@DomainId int,
@UsuarioId int, 
@VehicleBrandId int,
@VehicleModelId int, 
@VehicleTypeId int,
@VehicleTypeIdAuto int, 
@StreetId int,
@NighborhoodId int,
@DNI nvarchar(MAX),
@Nombre nvarchar(MAX),
@Apellido nvarchar(MAX),
@NroLicencia nvarchar(MAX),
@Dominio nvarchar(MAX)

select @DomainId = Id from GuardiaComunal.dbo.Domains where Descripcion='Viejo'
select @UsuarioId = UsuarioId from GuardiaComunal.dbo.Usuarios where Nombreusuario ='admin'
select @VehicleTypeIdAuto = Id from GuardiaComunal.dbo.VehicleTypes where Descripcion ='Auto'
set @SinEspecificar='SIN ESPECIFICAR'

Declare ccActs cursor GLOBAL
FOR select IdActa,FechaEmicion,FechadeCarga,Apellido, Nombre, Domicilio, Numero, Barrio, Localidad, Partido,DNI,  NumLicencia, Zona, Inspector,
    substring(Inspector, 1,charindex(' ',Inspector)) as InspectorLastName  ,substring(Inspector, charindex(' ',Inspector),len(Inspector)-charindex(' ',Inspector)+1) InspectorName,
	Contravension,ContravensionOtras,Observaciones,	Liberado, LicenciaRetenida, Vehiculo, Marca, Replace( [Dominio A],'-','') DominioA,Replace( [Dominio M],'-','') DominioM
    from [dbo].[01 Base de Actas]
	Open ccActs
fetch ccActs into @IdActa,@FechaEmicion,@FechadeCarga,@ApellidoOld,@NombreOld,@Domicilio,@Numero,@BarrioOld,@Localidad,@Partido,@DNIOld,@NumLicencia,@Zona,@Inspector, @InspectorLastName,
@InspectorName,@Contravension,@ContravensionOtras,@Observaciones,@Liberado,@LicenciaRetenidaOld,@Vehiculo,@Marca,@DominioA,@DominioM
while(@@fetch_status=0)
begin
	--TipoDeActa
	if (@ApellidoOld = null or @ApellidoOld ='Oficio')
		set @TipoDeActa ='Oficio'
	else
	  set @TipoDeActa ='Documentada'	

	  	--Acta
	set @NroActa=CONVERT(nvarchar(255),@IdActa) 

		--FechaInfraccion
	set @FechaInfraccion =@FechaEmicion

		--Tanda
	set @Tanda =1

	--Calle 
	if (@Domicilio is not null)
		set @Calle = @Domicilio
	else
		set @Calle = @SinEspecificar

		--Altura
	if (@Numero is not null)
		set @Altura = CONVERT(nvarchar(255),@Numero)
	else
		set @Altura = 'S/N'

		--EntreCalle
	if (@Zona is not null)
		set @EntreCalle= @Zona
	else
		set @EntreCalle= @SinEspecificar

		--Barrio
	if (@BarrioOld is not null)
		set @Barrio = @BarrioOld
	else
		set @Barrio = @SinEspecificar

	--FechaEnvioAlJuzgado
	set @FechaEnvioAlJuzgado = null

		--ActaAdjunta
	set @ActaAdjunta=null

	--FechaCarga
	if (@FechadeCarga is not null)
		set @FechaCarga=Convert(varchar(30),@FechadeCarga,102)
	else
		set @FechaCarga= @FechaEmicion

	--Color
	set @Color= null

	--NroMotor
	set @NroMotor=null

	--NroChasis
	set @NroChasis = null

	--EstadoVehiculo
	set @EstadoVehiculo= null

	--FechaEstado
	set @FechaEstado = null

	--TipoAgente **********(Se supone que son inspectores)
	set @TipoAgente ='Inspector'

	--VehiculoRetenido
	set @VehiculoRetenido = 0

	--LicenciaRetenida
	if (@LicenciaRetenidaOld is null)
		set @LicenciaRetenida= 0
	else
	    set @LicenciaRetenida= @LicenciaRetenidaOld

	--TicketAlcoholemia
	set @TicketAlcoholemia=0

	--ResultadoAlcoholemia
	set @ResultadoAlcoholemia = null

	--TicketAlcoholemiaAdjunto
	set @TicketAlcoholemiaAdjunto= null

	--Informe
	set @Informe=null

	--InformeAdjunto
	set @InformeAdjunto=null	

	--Detalle
	set @Detalle = null

	--Enable
	set @Enable =1

	--InspectorId
	  --Busco el inspector por apellido y nombre	  
	  if (@Inspector is not null)	 
			if Exists(	select * from GuardiaComunal.dbo.Inspectors where Upper(Apellido) = upper(@InspectorLastName) and Upper(Nombre) = upper(@InspectorName))
				select @InspectorId = Id
				from GuardiaComunal.dbo.Inspectors
				where Upper(Apellido) = upper(@InspectorLastName)
				and Upper(Nombre) = upper(@InspectorName);
			else
				insert into GuardiaComunal.dbo.Inspectors (Nombre,Apellido,FechaAlta, Enable) values (upper(@InspectorName), upper(@InspectorLastName), GETDATE(),1)
				select Id = @InspectorId from GuardiaComunal.dbo.Inspectors where Upper(Apellido) = upper(@InspectorLastName) and Upper(Nombre) = upper(@InspectorName)

	--PoliceId
		set @PoliceId=null

	
	--DomainId
		--(Lo puse arriba porque no cambia)	
		--select Id =@DomainId from GuardiaComunal.dbo.Domains where Descripcion='Viejo'
	
	--UsuarioId
		--Lo puse Arriba
	
	--VehicleTypeId
	 if (@Vehiculo is not null)
		--BEGIN
			if (exists (select * from GuardiaComunal.dbo.VehicleTypes where UPPER( Descripcion) = UPPER(@Vehiculo)))
				select  @VehicleTypeId = id from GuardiaComunal.dbo.VehicleTypes where UPPER( Descripcion) = UPPER(@Vehiculo)
			else
				set @VehicleTypeId= @VehicleTypeIdAuto
		--END	
	 else
	  set @VehicleTypeId= @VehicleTypeIdAuto --Default Auto

	--VehicleBrandId
	if (@Marca is not null)
		if (exists (select * from GuardiaComunal.dbo.VehicleBrands where upper(Descripcion) =UPPER(@Marca)))
			select @VehicleBrandId= Id from GuardiaComunal.dbo.VehicleBrands where upper(Descripcion) =UPPER(@Marca)
		else
				insert into GuardiaComunal.dbo.VehicleBrands (Descripcion, FechaAlta, Enable, VehicleTypeId)
				values (upper(@Marca), GETDATE(),1,@VehicleTypeId)
	else
		set @VehicleBrandId = null

	--VehicleModelId
		set @VehicleModelId= null

	--StreetId
	 set @StreetId = null

	 --NighborhoodId
	 set @NighborhoodId = null

	 --DNI
	 if (@DNIOld is not null)
	  set @DNI = @DNIOld
	else
	 set @DNI= null

	 --Nombre
	if (@NombreOld is not null)
		set @Nombre = @NombreOld
	else
		set @Nombre = null

	 --Apellido
	if (@ApellidoOld is not null)
		set @Apellido = @ApellidoOld
	else
		set @Apellido = null

	 --NroLicencia
	if (@NumLicencia is not null)
		set @NroLicencia = @NumLicencia
	else
		set @NroLicencia = null

	 --Dominio
	if (@DominioA is not null)
		set @Dominio = @DominioA
	else
		if (@DominioM is not null)
			set @Dominio = @DominioM
		else
		   set @Dominio = null

	--Contravenciones

		SELECT Split.a.value('.', 'VARCHAR(100)') AS ColName  
		FROM  
		(
			 SELECT CONVERT(XML, '<M>' + REPLACE(ColName, ',', '</M><M>') + '</M>') AS ColName  
			 FROM  (SELECT @ContravensionOtras AS ColName) TableName
		 ) AS A CROSS APPLY ColName.nodes ('/M') AS Split(a)


INSERT INTO GuardiaComunal.[dbo].[Acts]
           ([TipoDeActa],[NroActa],[FechaInfraccion],[Tanda],[Calle],[Altura],[EntreCalle],[Barrio],[FechaEnvioAlJuzgado]
           ,[ActaAdjunta],[FechaCarga],[Color],[NroMotor],[NroChasis],[EstadoVehiculo],[FechaEstado],[TipoAgente],[VehiculoRetenido]
           ,[LicenciaRetenida],[TicketAlcoholemia],[ResultadoAlcoholemia],[TicketAlcoholemiaAdjunto],[Informe],[InformeAdjunto]
           ,[Detalle],[Enable],[InspectorId],[PoliceId],[DomainId],[UsuarioId],[VehicleBrandId],[VehicleModelId],[VehicleTypeId]
           ,[StreetId],[NighborhoodId],[DNI],[Nombre],[Apellido],[NroLicencia],[Dominio])
     VALUES (@TipoDeActa,@NroActa,@FechaInfraccion,@Tanda,@Calle,@Altura,@EntreCalle,@Barrio,@FechaEnvioAlJuzgado,
			 @ActaAdjunta,@FechaCarga,@Color,@NroMotor,@NroChasis,@EstadoVehiculo,@FechaEstado,@TipoAgente,@VehiculoRetenido,
			 @LicenciaRetenida,@TicketAlcoholemia,@ResultadoAlcoholemia,@TicketAlcoholemiaAdjunto,@Informe,@InformeAdjunto,
			 @Detalle,@Enable,@InspectorId,@PoliceId,@DomainId,@UsuarioId,@VehicleBrandId,@VehicleModelId,@VehicleTypeId,
			 @StreetId,@NighborhoodId,@DNI,@Nombre,@Apellido,@NroLicencia,@Dominio)


fetch ccActs into @IdActa,@FechaEmicion,@FechadeCarga,@ApellidoOld,@NombreOld,@Domicilio,@Numero,@BarrioOld,@Localidad,@Partido,@DNIOld,@NumLicencia,@Zona,@Inspector,@InspectorLastName,
@InspectorName,@Contravension,@ContravensionOtras,@Observaciones,@Liberado,@LicenciaRetenidaOld,@Vehiculo,@Marca,@DominioA,@DominioM
end
close ccActs
deallocate ccActs
GO