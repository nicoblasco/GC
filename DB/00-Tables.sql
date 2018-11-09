USE [GuardiaComunal]
GO

/****** Object:  Table [dbo].[__MigrationHistory]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[__MigrationHistory](
	[MigrationId] [nvarchar](150) NOT NULL,
	[ContextKey] [nvarchar](300) NOT NULL,
	[Model] [varbinary](max) NOT NULL,
	[ProductVersion] [nvarchar](32) NOT NULL,
 CONSTRAINT [PK_dbo.__MigrationHistory] PRIMARY KEY CLUSTERED 
(
	[MigrationId] ASC,
	[ContextKey] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Acts]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Acts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TipoDeActa] [nvarchar](max) NOT NULL,
	[NroActa] [nvarchar](max) NOT NULL,
	[FechaInfraccion] [datetime] NOT NULL,
	[Tanda] [int] NOT NULL,
	[Calle] [nvarchar](max) NOT NULL,
	[Altura] [nvarchar](max) NOT NULL,
	[EntreCalle] [nvarchar](max) NOT NULL,
	[Barrio] [nvarchar](max) NOT NULL,
	[FechaEnvioAlJuzgado] [datetime] NOT NULL,
	[ActaAdjunta] [nvarchar](max) NULL,
	[FechaCarga] [datetime] NOT NULL,
	[Color] [nvarchar](max) NULL,
	[NroMotor] [nvarchar](max) NULL,
	[NroChasis] [nvarchar](max) NULL,
	[EstadoVehiculo] [nvarchar](max) NULL,
	[FechaEstado] [datetime] NOT NULL,
	[TipoAgente] [nvarchar](max) NOT NULL,
	[VehiculoRetenido] [bit] NOT NULL,
	[LicenciaRetenida] [bit] NOT NULL,
	[TicketAlcoholemia] [bit] NOT NULL,
	[ResultadoAlcoholemia] [nvarchar](max) NULL,
	[TicketAlcoholemiaAdjunto] [nvarchar](max) NULL,
	[Informe] [nvarchar](max) NULL,
	[InformeAdjunto] [nvarchar](max) NULL,
	[Detalle] [nvarchar](max) NULL,
	[Enable] [bit] NOT NULL,
	[InspectorId] [int] NULL,
	[PoliceId] [int] NULL,
	[DomainId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[VehicleBrandId] [int] NULL,
	[VehicleModelId] [int] NULL,
	[VehicleTypeId] [int] NOT NULL,
	[StreetId] [int] NULL,
	[NighborhoodId] [int] NULL,
	[DNI] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[NroLicencia] [nvarchar](max) NULL,
	[Dominio] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Acts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Audits]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Audits](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Accion] [nvarchar](max) NULL,
	[Fecha] [datetime] NOT NULL,
	[UsuarioId] [int] NOT NULL,
	[WindowId] [int] NOT NULL,
	[Clave] [nvarchar](max) NULL,
	[Entidad] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Audits] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ContraventionActs]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ContraventionActs](
	[Contravention_Id] [int] NOT NULL,
	[Act_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ContraventionActs] PRIMARY KEY CLUSTERED 
(
	[Contravention_Id] ASC,
	[Act_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Contraventions]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Contraventions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NroArticulo] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Contraventions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Domains]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Domains](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Domains] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Inspectors]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Inspectors](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Inspectors] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[LiberationPlaces]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[LiberationPlaces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.LiberationPlaces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Liberations]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Liberations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[NroLiberacion] [nvarchar](max) NOT NULL,
	[FechaDeLiberacion] [datetime] NOT NULL,
	[NroJuzgado] [nvarchar](max) NULL,
	[FechaCarga] [datetime] NOT NULL,
	[Convenio] [nvarchar](max) NULL,
	[Cuotas] [int] NOT NULL,
	[Acarreo] [decimal](18, 2) NOT NULL,
	[NroRecibo] [nvarchar](max) NOT NULL,
	[Importe] [decimal](18, 2) NOT NULL,
	[MontoEnCuotas] [decimal](18, 2) NOT NULL,
	[FechaEmisionRecibo] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
	[ActaId] [int] NOT NULL,
	[LiberationPlaceId] [int] NOT NULL,
	[PersonId] [int] NOT NULL,
	[UsuarioId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Liberations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Modules]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Modules](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Modules] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Nighborhoods]    Script Date: 23/10/2018 13:09:10 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Nighborhoods](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[Direccion] [nvarchar](max) NULL,
	[Altura] [nvarchar](max) NULL,
	[Partida] [nvarchar](max) NULL,
	[Barrio] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Zona] [nvarchar](max) NULL,
	[NombreCorto] [nvarchar](max) NULL,
	[Codigo] [int] NOT NULL,
	[Habitantes2010] [nvarchar](max) NULL,
	[ProyecionHabitantes2016] [nvarchar](max) NULL,
	[ProyecionHabitantes2020] [nvarchar](max) NULL,
	[Detalle] [nvarchar](max) NULL,
	[Latitud] [nvarchar](max) NULL,
	[Longitud] [nvarchar](max) NULL,
	[Perimetro] [nvarchar](max) NULL,
	[Area] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Nighborhoods] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[ObservationActs]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ObservationActs](
	[Observation_Id] [int] NOT NULL,
	[Act_Id] [int] NOT NULL,
 CONSTRAINT [PK_dbo.ObservationActs] PRIMARY KEY CLUSTERED 
(
	[Observation_Id] ASC,
	[Act_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Observations]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Observations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.Observations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[People]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[People](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [nvarchar](max) NOT NULL,
	[Apellido] [nvarchar](max) NOT NULL,
	[Nombre] [nvarchar](max) NOT NULL,
	[Calle] [nvarchar](max) NULL,
	[Altura] [nvarchar](max) NULL,
	[Barrio] [nvarchar](max) NULL,
	[EntreCalle] [nvarchar](max) NULL,
	[Partido] [nvarchar](max) NULL,
	[StreetId] [int] NULL,
	[NighborhoodId] [int] NULL,
 CONSTRAINT [PK_dbo.People] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Permissions]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Permissions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Consulta] [bit] NOT NULL,
	[AltaModificacion] [bit] NOT NULL,
	[Baja] [bit] NOT NULL,
	[Fecha] [datetime] NOT NULL,
	[RolId] [int] NOT NULL,
	[WindowId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Permissions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Police]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Police](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DNI] [nvarchar](max) NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
	[PoliceStationId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Police] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[PoliceStations]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[PoliceStations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Direccion] [nvarchar](max) NULL,
	[Altura] [nvarchar](max) NULL,
	[Partida] [nvarchar](max) NULL,
	[Barrio] [nvarchar](max) NULL,
	[Sede] [nvarchar](max) NULL,
	[Detalle] [nvarchar](max) NULL,
	[Latitud] [nvarchar](max) NULL,
	[Longitud] [nvarchar](max) NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.PoliceStations] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Rols]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Rols](
	[RolId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Descripcion] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Rols] PRIMARY KEY CLUSTERED 
(
	[RolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Streets]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Streets](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Baja] [nvarchar](max) NULL,
	[DescripcionOficial] [nvarchar](max) NULL,
	[Codigo] [nvarchar](max) NULL,
	[DescripcionGoogle] [nvarchar](max) NULL,
 CONSTRAINT [PK_dbo.Streets] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Usuarios]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Usuarios](
	[UsuarioId] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [nvarchar](max) NULL,
	[Apellido] [nvarchar](max) NULL,
	[Nombreusuario] [nvarchar](max) NULL,
	[Contraseña] [nvarchar](max) NULL,
	[Enable] [bit] NOT NULL,
	[RolId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Usuarios] PRIMARY KEY CLUSTERED 
(
	[UsuarioId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleBrands]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleBrands](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
	[VehicleTypeId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.VehicleBrands] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleModels]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
	[VehicleBrandId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.VehicleModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[VehicleTypes]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehicleTypes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[FechaAlta] [datetime] NOT NULL,
	[Enable] [bit] NOT NULL,
 CONSTRAINT [PK_dbo.VehicleTypes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Windows]    Script Date: 23/10/2018 13:09:11 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Windows](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Descripcion] [nvarchar](max) NULL,
	[Enable] [bit] NOT NULL,
	[Url] [nvarchar](max) NULL,
	[Orden] [nvarchar](max) NULL,
	[ModuleId] [int] NOT NULL,
 CONSTRAINT [PK_dbo.Windows] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Domains_TipoDeDominio_Id] FOREIGN KEY([DomainId])
REFERENCES [dbo].[Domains] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Domains_TipoDeDominio_Id]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Inspectors_Inspector_Id] FOREIGN KEY([InspectorId])
REFERENCES [dbo].[Inspectors] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Inspectors_Inspector_Id]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Nighborhoods_NighborhoodId] FOREIGN KEY([NighborhoodId])
REFERENCES [dbo].[Nighborhoods] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Nighborhoods_NighborhoodId]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Police_Police_Id] FOREIGN KEY([PoliceId])
REFERENCES [dbo].[Police] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Police_Police_Id]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Streets_StreetId] FOREIGN KEY([StreetId])
REFERENCES [dbo].[Streets] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Streets_StreetId]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.Usuarios_User_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.Usuarios_User_UsuarioId]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.VehicleBrands_VehicleBrand_Id] FOREIGN KEY([VehicleBrandId])
REFERENCES [dbo].[VehicleBrands] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.VehicleBrands_VehicleBrand_Id]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.VehicleModels_VehicleModel_Id] FOREIGN KEY([VehicleModelId])
REFERENCES [dbo].[VehicleModels] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.VehicleModels_VehicleModel_Id]
GO

ALTER TABLE [dbo].[Acts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Acts_dbo.VehicleTypes_VehicleType_Id] FOREIGN KEY([VehicleTypeId])
REFERENCES [dbo].[VehicleTypes] ([Id])
GO

ALTER TABLE [dbo].[Acts] CHECK CONSTRAINT [FK_dbo.Acts_dbo.VehicleTypes_VehicleType_Id]
GO

ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Audits_dbo.Usuarios_User_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO

ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_dbo.Audits_dbo.Usuarios_User_UsuarioId]
GO

ALTER TABLE [dbo].[Audits]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Audits_dbo.Windows_Window_Id] FOREIGN KEY([WindowId])
REFERENCES [dbo].[Windows] ([Id])
GO

ALTER TABLE [dbo].[Audits] CHECK CONSTRAINT [FK_dbo.Audits_dbo.Windows_Window_Id]
GO

ALTER TABLE [dbo].[ContraventionActs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContraventionActs_dbo.Acts_Act_Id] FOREIGN KEY([Act_Id])
REFERENCES [dbo].[Acts] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ContraventionActs] CHECK CONSTRAINT [FK_dbo.ContraventionActs_dbo.Acts_Act_Id]
GO

ALTER TABLE [dbo].[ContraventionActs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ContraventionActs_dbo.Contraventions_Contravention_Id] FOREIGN KEY([Contravention_Id])
REFERENCES [dbo].[Contraventions] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ContraventionActs] CHECK CONSTRAINT [FK_dbo.ContraventionActs_dbo.Contraventions_Contravention_Id]
GO

ALTER TABLE [dbo].[Liberations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Liberations_dbo.Acts_Acta_Id] FOREIGN KEY([ActaId])
REFERENCES [dbo].[Acts] ([Id])
GO

ALTER TABLE [dbo].[Liberations] CHECK CONSTRAINT [FK_dbo.Liberations_dbo.Acts_Acta_Id]
GO

ALTER TABLE [dbo].[Liberations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Liberations_dbo.LiberationPlaces_LiberationPlace_Id] FOREIGN KEY([LiberationPlaceId])
REFERENCES [dbo].[LiberationPlaces] ([Id])
GO

ALTER TABLE [dbo].[Liberations] CHECK CONSTRAINT [FK_dbo.Liberations_dbo.LiberationPlaces_LiberationPlace_Id]
GO

ALTER TABLE [dbo].[Liberations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Liberations_dbo.People_Person_Id] FOREIGN KEY([PersonId])
REFERENCES [dbo].[People] ([Id])
GO

ALTER TABLE [dbo].[Liberations] CHECK CONSTRAINT [FK_dbo.Liberations_dbo.People_Person_Id]
GO

ALTER TABLE [dbo].[Liberations]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Liberations_dbo.Usuarios_User_UsuarioId] FOREIGN KEY([UsuarioId])
REFERENCES [dbo].[Usuarios] ([UsuarioId])
GO

ALTER TABLE [dbo].[Liberations] CHECK CONSTRAINT [FK_dbo.Liberations_dbo.Usuarios_User_UsuarioId]
GO

ALTER TABLE [dbo].[ObservationActs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObservationActs_dbo.Acts_Act_Id] FOREIGN KEY([Act_Id])
REFERENCES [dbo].[Acts] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ObservationActs] CHECK CONSTRAINT [FK_dbo.ObservationActs_dbo.Acts_Act_Id]
GO

ALTER TABLE [dbo].[ObservationActs]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ObservationActs_dbo.Observations_Observation_Id] FOREIGN KEY([Observation_Id])
REFERENCES [dbo].[Observations] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[ObservationActs] CHECK CONSTRAINT [FK_dbo.ObservationActs_dbo.Observations_Observation_Id]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_dbo.People_dbo.Nighborhoods_NighborhoodId] FOREIGN KEY([NighborhoodId])
REFERENCES [dbo].[Nighborhoods] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_dbo.People_dbo.Nighborhoods_NighborhoodId]
GO

ALTER TABLE [dbo].[People]  WITH CHECK ADD  CONSTRAINT [FK_dbo.People_dbo.Streets_StreetId] FOREIGN KEY([StreetId])
REFERENCES [dbo].[Streets] ([Id])
GO

ALTER TABLE [dbo].[People] CHECK CONSTRAINT [FK_dbo.People_dbo.Streets_StreetId]
GO

ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Permissions_dbo.Rols_Role_RolId] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([RolId])
GO

ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_dbo.Permissions_dbo.Rols_Role_RolId]
GO

ALTER TABLE [dbo].[Permissions]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Permissions_dbo.Windows_Window_Id] FOREIGN KEY([WindowId])
REFERENCES [dbo].[Windows] ([Id])
GO

ALTER TABLE [dbo].[Permissions] CHECK CONSTRAINT [FK_dbo.Permissions_dbo.Windows_Window_Id]
GO

ALTER TABLE [dbo].[Police]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Police_dbo.PoliceStations_PoliceStationId] FOREIGN KEY([PoliceStationId])
REFERENCES [dbo].[PoliceStations] ([Id])
GO

ALTER TABLE [dbo].[Police] CHECK CONSTRAINT [FK_dbo.Police_dbo.PoliceStations_PoliceStationId]
GO

ALTER TABLE [dbo].[Usuarios]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Usuarios_dbo.Rols_RolId] FOREIGN KEY([RolId])
REFERENCES [dbo].[Rols] ([RolId])
GO

ALTER TABLE [dbo].[Usuarios] CHECK CONSTRAINT [FK_dbo.Usuarios_dbo.Rols_RolId]
GO

ALTER TABLE [dbo].[VehicleBrands]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VehicleBrands_dbo.VehicleTypes_VehicleTypeId] FOREIGN KEY([VehicleTypeId])
REFERENCES [dbo].[VehicleTypes] ([Id])
GO

ALTER TABLE [dbo].[VehicleBrands] CHECK CONSTRAINT [FK_dbo.VehicleBrands_dbo.VehicleTypes_VehicleTypeId]
GO

ALTER TABLE [dbo].[VehicleModels]  WITH CHECK ADD  CONSTRAINT [FK_dbo.VehicleModels_dbo.VehicleBrands_VehicleBrandId] FOREIGN KEY([VehicleBrandId])
REFERENCES [dbo].[VehicleBrands] ([Id])
GO

ALTER TABLE [dbo].[VehicleModels] CHECK CONSTRAINT [FK_dbo.VehicleModels_dbo.VehicleBrands_VehicleBrandId]
GO

ALTER TABLE [dbo].[Windows]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Windows_dbo.Modules_Module_Id] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Modules] ([Id])
GO

ALTER TABLE [dbo].[Windows] CHECK CONSTRAINT [FK_dbo.Windows_dbo.Modules_Module_Id]
GO

