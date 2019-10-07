USE [gea]
GO

/****** Object:  Table [dbo].[arquivo]    Script Date: 06/10/2019 19:51:40 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[arquivo](
	[id_arquivo] [int] NOT NULL,
	[nome_arquivo] [nvarchar](200) NOT NULL,
	[caminho_arquivo] [nvarchar](200) NULL,
	[arquivo] [image] NOT NULL,
	[data_cadastro] [datetime] NOT NULL
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO


