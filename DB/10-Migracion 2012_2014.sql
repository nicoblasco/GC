select * from [dbo].[Actas2012_2014A]


Declare
--Old Base de datos bkp
@IdActa varchar(200),
@ApellidoOld varchar(150),
@FechaEmicion varchar(150),
@Zona nvarchar(MAX),
@FechadeCarga nvarchar(255),
@LicenciaRetenidaOld bit,
@Inspector  nvarchar(255),
@InspectorName nvarchar(255),
@InspectorLastName nvarchar(255),
@Vehiculo nvarchar(255),
@Marca nvarchar(255),
@DNIOld   nvarchar(255),
@NombreOld  nvarchar(255),
@NumLicencia  nvarchar(255),
@DominioA   nvarchar(255),
@DominioM nvarchar(255),
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


Declare ccActs cursor GLOBAL
	FOR select IdActa,Apellido,FechaEmicion, Zona, FechadeCarga, LicenciaRetenida, Inspector,
	substring(Inspector, 1,charindex(' ',Inspector)) as InspectorLastName  ,substring(Inspector, charindex(' ',Inspector),len(Inspector)-charindex(' ',Inspector)+1) InspectorName,
	Vehiculo, Marca, DNI, Nombre,NumLicencia, [Dominio A], [Dominio M]
    from [dbo].[Actas2012_2014A]
	Open ccActs
fetch ccActs into @IdActa, @ApellidoOld,@FechaEmicion, @Zona,@FechadeCarga, @LicenciaRetenidaOld, @Inspector,@InspectorLastName,@InspectorName,@Vehiculo, @Marca, @DNIOld,@NombreOld,@NumLicencia,@DominioA,@Dominio
while(@@fetch_status=0)
begin
	--TipoDeActa
	if (@ApellidoOld = null or @ApellidoOld ='Oficio')
		set @TipoDeActa ='Oficio'
	else
	  set @TipoDeActa ='Documentada'	

	--Acta
	set @NroActa=@IdActa

	--FechaInfraccion
	set @FechaInfraccion =Convert(varchar(30),@FechaEmicion,102)

	--Tanda
	set @Tanda =1

	--Calle (Las calles es imposible mapearlas, se van a guardar en entre calles)
	set @Calle = null

	--Altura
	set @Altura = null

	--EntreCalle *******La Zona no esta bien
	set @EntreCalle= @Zona

	--Barrio
	set @Barrio = null

	--FechaEnvioAlJuzgado
	set @FechaEnvioAlJuzgado = null

	--ActaAdjunta
	set @ActaAdjunta=null

	--FechaCarga
	if (@FechadeCarga is not null)
		set @FechaCarga=Convert(varchar(30),@FechadeCarga,102)
	else
		set @FechaCarga= null

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
	 set @Detalle= null

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


	--print 'InspectorId: '+ CAST(@InspectorId AS VARCHAR)
	--print 'Dominio: '+ CAST(@DomainId AS VARCHAR)

	--print 'TipoDeActa: '+@TipoDeActa	
	--print 'NroActa:' +@NroActa
	----print 'FechaInfraccion:' + CONVERT( VARCHAR(24),@FechaInfraccion,102)
	----print 'Tanda: '+@Tanda
	--print 'Calle: ' + @Calle
	--print 'Altura: '+@Altura
	--print 'EntreCalle: '+@EntreCalle 
	--insert ino GuardiaComunal.dbo.
fetch ccActs into @IdActa, @ApellidoOld,@FechaEmicion, @Zona,@FechadeCarga, @LicenciaRetenidaOld, @Inspector,@InspectorLastName,@InspectorName,@Vehiculo, @Marca,, @DNIOld,@NombreOld,@NumLicencia,@DominioA,@Dominio
end
close ccActs
deallocate ccActs
GO