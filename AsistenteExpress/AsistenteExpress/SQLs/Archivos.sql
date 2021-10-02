USE [AsistentExpress]
GO

/****** Object:  Table [dbo].[Archivos]    Script Date: 01/10/2021 11:12:52 p. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Archivos](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProceso] [int] NOT NULL,
	[Usuario] [nvarchar](250) NULL,
	[NameFile] [nvarchar](250) NULL,
	[ruta] [nvarchar](250) NULL,
	[typeFile] [nvarchar](200) NULL,
	[FechaCarga] [datetime] NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Archivos] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[Archivos] ADD  DEFAULT ((1)) FOR [Estatus]
GO


