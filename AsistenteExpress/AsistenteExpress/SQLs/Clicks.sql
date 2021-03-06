USE [AsistentExpress]
GO

/****** Object:  Table [dbo].[Clicks]    Script Date: 29/09/2021 12:30:19 a. m. ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Clicks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IdProceso] [int] NOT NULL,
	[Count] [int] NOT NULL,
	[Estatus] [bit] NOT NULL,
 CONSTRAINT [PK_Clicks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO


