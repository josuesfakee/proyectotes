USE [AsistentExpress]
GO

/****** Object:  Table [dbo].[Procesos]    Script Date: 01/10/2021 11:14:34 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Procesos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Asunto] [nvarchar](100) NULL,
	[IdCampaña] [int] NOT NULL,
	[IdPerfil] [int] NULL,
	[IdTipo] [int] NULL,
	[IdMotivo] [int] NULL,
	[IdSubMotivo] [int] NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Procesos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Procesos] ADD  DEFAULT ((1)) FOR [Estatus]
GO


