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
@StreetId int,
@NighborhoodId int,
@DNI nvarchar(MAX),
@Nombre nvarchar(MAX),
@Apellido nvarchar(MAX),
@NroLicencia nvarchar(MAX),
@Dominio nvarchar(MAX)

Declare ccActs cursor GLOBAL
	FOR select IdActa,Apellido,FechaEmicion, Zona, FechadeCarga, LicenciaRetenida, Inspector from [dbo].[Actas2012_2014A]
	Open ccActs
fetch ccActs into @IdActa, @ApellidoOld,@FechaEmicion, @Zona,@FechadeCarga, @LicenciaRetenidaOld, @Inspector
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
	--set @FechaInfraccion =Convert(varchar(30),@FechaEmicion,102)

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
	--set @FechaEnvioAlJuzgado = null

	--ActaAdjunta
	set @ActaAdjunta=null

	--FechaCarga
	--if (@FechadeCarga != null)
	--	set @FechaCarga=Convert(varchar(30),@FechadeCarga,102)
	--else
	--	set @FechaCarga= null

	--Color
	set @Color= null

	--NroMotor
	set @NroMotor=null

	--NroChasis
	set @NroChasis = null

	--EstadoVehiculo
	set @EstadoVehiculo= null

	--FechaEstado
	--set @FechaEstado = null

	--TipoAgente **********(Se supone que son inspectores)
	set @TipoAgente ='Inspector'

	--VehiculoRetenido
	set @VehiculoRetenido = 0

	--LicenciaRetenida
	if (@LicenciaRetenidaOld = null)
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


	print 'TipoDeActa: '+@TipoDeActa	
	print 'NroActa:' +@NroActa
	--print 'FechaInfraccion:' + CONVERT( VARCHAR(24),@FechaInfraccion,102)
	print 'Tanda: '+@Tanda
	print 'Calle: ' + @Calle
	print 'Altura: '+@Altura
	print 'EntreCalle: '+@EntreCalle 
	--insert ino GuardiaComunal.dbo.
fetch ccActs into @IdActa, @ApellidoOld,@FechaEmicion, @Zona,@FechadeCarga, @LicenciaRetenidaOld, @Inspector
end
close ccActs
deallocate ccActs
GO