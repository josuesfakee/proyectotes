USE [AsistentExpress]
GO

/****** Object:  Table [dbo].[Procesos]    Script Date: 23/09/2021 11:05:21 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Procesos](
	[Id] [int] NOT NULL,
	[Asunto] [nvarchar](100) NULL,
	[IdCampaña] [int] NOT NULL,
	[IdPerfil] [int] NULL,
	[IdMotivo] [int] NULL,
	[IdSubMotivo] [int] NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Procesos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

