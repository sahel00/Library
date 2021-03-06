USE [master]
GO
/****** Object:  Database [Biblioteca]    Script Date: 25/01/2021 23:53:13 ******/
CREATE DATABASE [Biblioteca]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Biblioteca', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Biblioteca.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Biblioteca_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.MSSQLSERVER\MSSQL\DATA\Biblioteca_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [Biblioteca] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Biblioteca].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Biblioteca] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Biblioteca] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Biblioteca] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Biblioteca] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Biblioteca] SET ARITHABORT OFF 
GO
ALTER DATABASE [Biblioteca] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [Biblioteca] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Biblioteca] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Biblioteca] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Biblioteca] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Biblioteca] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Biblioteca] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Biblioteca] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Biblioteca] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Biblioteca] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Biblioteca] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Biblioteca] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Biblioteca] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Biblioteca] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Biblioteca] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Biblioteca] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Biblioteca] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Biblioteca] SET RECOVERY FULL 
GO
ALTER DATABASE [Biblioteca] SET  MULTI_USER 
GO
ALTER DATABASE [Biblioteca] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Biblioteca] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Biblioteca] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Biblioteca] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Biblioteca] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'Biblioteca', N'ON'
GO
ALTER DATABASE [Biblioteca] SET QUERY_STORE = OFF
GO
USE [Biblioteca]
GO
/****** Object:  Table [dbo].[Libri]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Libri](
	[Codice] [nvarchar](50) NOT NULL,
	[Autore] [nvarchar](50) NOT NULL,
	[Titolo] [nvarchar](50) NOT NULL,
	[Editore] [nvarchar](50) NOT NULL,
	[Anno] [int] NULL,
	[Luogo] [nvarchar](50) NULL,
	[Pagine] [nvarchar](50) NOT NULL,
	[Classificazione] [nvarchar](50) NOT NULL,
	[Collocazione] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Libri] PRIMARY KEY CLUSTERED 
(
	[Codice] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Prestiti]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Prestiti](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CodiceLibro] [nvarchar](50) NOT NULL,
	[MatricolaStudente] [int] NOT NULL,
	[Data_Inizio_Prestito] [datetime] NOT NULL,
	[Data_Fine_Prestito] [datetime] NOT NULL,
	[Riportato] [bit] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Studenti]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Studenti](
	[Matricola] [int] IDENTITY(11332,1) NOT NULL,
	[Nome] [nvarchar](50) NOT NULL,
	[Cognome] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Classe] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](100) NULL,
	[Ruolo] [nvarchar](50) NULL,
 CONSTRAINT [PK_Studenti] PRIMARY KEY CLUSTERED 
(
	[Matricola] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Prestiti] ADD  DEFAULT ((0)) FOR [Riportato]
GO
ALTER TABLE [dbo].[Studenti] ADD  DEFAULT ('USER') FOR [Ruolo]
GO
ALTER TABLE [dbo].[Prestiti]  WITH CHECK ADD  CONSTRAINT [prestiti_codice_libro_FK] FOREIGN KEY([CodiceLibro])
REFERENCES [dbo].[Libri] ([Codice])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prestiti] CHECK CONSTRAINT [prestiti_codice_libro_FK]
GO
ALTER TABLE [dbo].[Prestiti]  WITH CHECK ADD  CONSTRAINT [prestiti_matricola_studenti_FK] FOREIGN KEY([MatricolaStudente])
REFERENCES [dbo].[Studenti] ([Matricola])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[Prestiti] CHECK CONSTRAINT [prestiti_matricola_studenti_FK]
GO
/****** Object:  StoredProcedure [dbo].[spAggiungiPrestito]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAggiungiPrestito]
@Codice NVARCHAR(50),
@Matricola int,
@DataInizio DateTime,
@DataFine DateTime
AS
BEGIN
	SET NOCOUNT ON;
	INSERT INTO Prestiti(CodiceLibro, MatricolaStudente, Data_Inizio_Prestito, Data_Fine_Prestito) 
	Values(@Codice, @Matricola, @DataInizio, @DataFine)
END
GO
/****** Object:  StoredProcedure [dbo].[spAllLibri]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spAllLibri]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * from Libri
END
GO
/****** Object:  StoredProcedure [dbo].[spAllStudenti]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spAllStudenti]
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT Nome, Cognome, Email, Classe from Studenti
	Where Ruolo = 'USER'
END
GO
/****** Object:  StoredProcedure [dbo].[spDetailLibro]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spDetailLibro] @Codice Nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	Select * from Libri where Codice = @Codice
    
END
GO
/****** Object:  StoredProcedure [dbo].[spDetailStudente]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spDetailStudente] @Email Nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT MATRICOLA, NOME, COGNOME, EMAIL, CLASSE from Studenti Where Email = @Email
END
GO
/****** Object:  StoredProcedure [dbo].[spEliminaLibro]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spEliminaLibro] @Codice Nvarchar(50)
AS
BEGIN

	SET NOCOUNT ON;
	DELETE FROM Libri WHERE Codice = @Codice;

END
GO
/****** Object:  StoredProcedure [dbo].[spEliminaStudente]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spEliminaStudente] @Email NVARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    DELETE FROM Studenti WHERE Email = @Email;
END
GO
/****** Object:  StoredProcedure [dbo].[spLibriPrestito]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spLibriPrestito]
AS
BEGIN
	
	SET NOCOUNT ON;
	Select * from Prestiti Inner Join Libri on Libri.Codice = Prestiti.CodiceLibro
	Where Prestiti.Riportato = 0
   
END
GO
/****** Object:  StoredProcedure [dbo].[spLogin]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spLogin]
	 @Email NVARCHAR(50), @Password NVARCHAR(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT * FROM Studenti Where Email = @Email And Password = @Password
END
GO
/****** Object:  StoredProcedure [dbo].[spModificaLibro]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spModificaLibro]
@Codice Nvarchar(50),
@Autore Nvarchar(50),
@Titolo Nvarchar(50),
@Editore Nvarchar(50),
@Anno Int,
@Luogo Nvarchar(50),
@Pagine Nvarchar(50),
@Classificazione Nvarchar(50),
@Collocazione Nvarchar(50),
@CodiceDaModicare Nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    SET NOCOUNT ON;
	UPDATE Libri
	SET Codice = @Codice,
	Autore = @Autore,
	Titolo = @Titolo,
	Editore = @Editore,
	Anno = @Anno,
	Luogo = @Luogo,
	Pagine = @Pagine,
	Classificazione = @Classificazione,
	Collocazione = @Collocazione
	WHERE Codice = @CodiceDaModicare 
END
GO
/****** Object:  StoredProcedure [dbo].[spModificaPrestito]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spModificaPrestito]
@CodiceLibro NVARCHAR(50),
@MatricolaStudente INT,
@DataInizio DATETIME,
@DataFine DATETIME,
@Id INT,
@Riportato Bit
AS
BEGIN

	SET NOCOUNT ON;

    UPDATE Prestiti 
	SET CodiceLibro = @CodiceLibro,
	MatricolaStudente = @MatricolaStudente,
	Data_Inizio_Prestito = @DataInizio,
	 Data_Fine_Prestito = @DataFine,
	 Riportato = @Riportato
	 Where Id =@Id
END
GO
/****** Object:  StoredProcedure [dbo].[spModificaStudente]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[spModificaStudente]
@Nome Nvarchar(50),
@Cognome Nvarchar(50),
@Email Nvarchar(50),
@EmailDaModicare Nvarchar(50),
@Classe Nvarchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE Studenti
	SET Nome = @Nome,
	Cognome = @Cognome,
	Email = @Email,
	Classe = @Classe
	WHERE Email = @EmailDaModicare 
END
GO
/****** Object:  StoredProcedure [dbo].[spPrestitiStorico]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spPrestitiStorico]
AS
BEGIN
	SET NOCOUNT ON;

    
	SELECT * From Studenti 
	Inner Join Prestiti On Studenti.Matricola = Prestiti.MatricolaStudente
	Where Riportato = 1
END
GO
/****** Object:  StoredProcedure [dbo].[spPrestitoScaduto]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spPrestitoScaduto]
@Data DateTime
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select * from Studenti join prestiti  on Studenti.Matricola =  prestiti.MatricolaStudente
	where Prestiti.data_fine_prestito < @Data 
                     
END
GO
/****** Object:  StoredProcedure [dbo].[spRegistrazione]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spRegistrazione]
	 @Nome NVARCHAR(50), @Cognome NVARCHAR(50), @Email NVARCHAR(50), @Classe NVARCHAR(50), @Password NVARCHAR(100)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO Studenti(NOME, COGNOME,EMAIL, CLASSE, PASSWORD) VALUES(@Nome, @Cognome, @Email, @Classe, @Password)
END
GO
/****** Object:  StoredProcedure [dbo].[spRiportaLibro]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spRiportaLibro]
@Id int
AS
BEGIN
	
	SET NOCOUNT ON;

    Update Prestiti Set Riportato = 1 
	Where Id = @Id

END
GO
/****** Object:  StoredProcedure [dbo].[spStudentiPrestiti]    Script Date: 25/01/2021 23:53:13 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[spStudentiPrestiti]
AS
BEGIN

	SET NOCOUNT ON;

	Select * from Studenti 
	Inner Join Prestiti On Studenti.Matricola = Prestiti.MatricolaStudente
	Inner Join Libri On Libri.Codice = Prestiti.CodiceLibro
	Where Prestiti.Riportato = 0
END
GO
USE [master]
GO
ALTER DATABASE [Biblioteca] SET  READ_WRITE 
GO
