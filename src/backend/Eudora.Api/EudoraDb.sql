USE [EudoraDb]
GO

/****** Object: Table [dbo].[users] Script Date: 28/09/2018 15:29:13 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

--representantes
IF (OBJECT_ID('[dbo].[representantes]') IS NOT NULL)
BEGIN
	DROP TABLE [dbo].[representantes]
END

CREATE TABLE [dbo].[representantes] (
    [id]					INT					NOT NULL,
    [nome]					NVARCHAR	(100)	NOT NULL,
	[telefone]				VARCHAR		(15)	NOT NULL,
	[classificacao_re]		VARCHAR		(30)	NULL,
	[tipo]					VARCHAR		(10)	NULL,
	[contato_realizado]		BIT					NOT NULL	 DEFAULT 0,
	PRIMARY KEY(id)
)

--mensagens
IF (OBJECT_ID('[dbo].[mensagens]') IS NOT NULL)
BEGIN
	DROP TABLE [dbo].[mensagens]
END

CREATE TABLE [dbo].[mensagens] (
    [id]					INT			IDENTITY (1, 1)	NOT NULL,
    [from]					VARCHAR		(15)			NOT NULL,
	[to]					VARCHAR		(15)			NOT NULL,
	[content]				NVARCHAR	(MAX)			NOT NULL,
	[sentiment]				VARCHAR		(10)			NULL,
	[date]					DATETIME2					NOT NULL,
	PRIMARY KEY(id)
)

CREATE CLUSTERED INDEX IDX_COD_REVENDEDOR ON compras (COD_REVENDEDOR);
CREATE INDEX IDX_DAT_CAPTACAO ON compras (DAT_CAPTACAO);
CREATE INDEX IDX_CATEGORIA ON compras (CATEGORIA);
CREATE INDEX IDX_COD_MATERIAL ON compras (COD_MATERIAL);


CREATE CLUSTERED INDEX IDX_COD_REVENDEDOR ON credito (COD_REVENDEDOR);  
CREATE INDEX IDX_CidadeResidencial ON credito (CidadeResidencial); 
CREATE INDEX IDX_EstadoResidencial ON credito (EstadoResidencial);   