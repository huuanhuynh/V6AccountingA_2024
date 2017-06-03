USE [master]
GO

IF EXISTS(select * from sys.databases where name='V6Assistant')
DROP DATABASE [V6Assistant]
GO

CREATE DATABASE [V6Assistant]
GO

ALTER DATABASE [V6Assistant] SET COMPATIBILITY_LEVEL = 100
GO

IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [V6Assistant].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO

ALTER DATABASE [V6Assistant] SET ANSI_NULL_DEFAULT OFF 
GO

ALTER DATABASE [V6Assistant] SET ANSI_NULLS OFF 
GO

ALTER DATABASE [V6Assistant] SET ANSI_PADDING OFF 
GO

ALTER DATABASE [V6Assistant] SET ANSI_WARNINGS OFF 
GO

ALTER DATABASE [V6Assistant] SET ARITHABORT OFF 
GO

ALTER DATABASE [V6Assistant] SET AUTO_CLOSE OFF 
GO

ALTER DATABASE [V6Assistant] SET AUTO_CREATE_STATISTICS ON 
GO

ALTER DATABASE [V6Assistant] SET AUTO_SHRINK OFF 
GO

ALTER DATABASE [V6Assistant] SET AUTO_UPDATE_STATISTICS ON 
GO

ALTER DATABASE [V6Assistant] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO

ALTER DATABASE [V6Assistant] SET CURSOR_DEFAULT  GLOBAL 
GO

ALTER DATABASE [V6Assistant] SET CONCAT_NULL_YIELDS_NULL OFF 
GO

ALTER DATABASE [V6Assistant] SET NUMERIC_ROUNDABORT OFF 
GO

ALTER DATABASE [V6Assistant] SET QUOTED_IDENTIFIER OFF 
GO

ALTER DATABASE [V6Assistant] SET RECURSIVE_TRIGGERS OFF 
GO

ALTER DATABASE [V6Assistant] SET  DISABLE_BROKER 
GO

ALTER DATABASE [V6Assistant] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO

ALTER DATABASE [V6Assistant] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO

ALTER DATABASE [V6Assistant] SET TRUSTWORTHY OFF 
GO

ALTER DATABASE [V6Assistant] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO

ALTER DATABASE [V6Assistant] SET PARAMETERIZATION SIMPLE 
GO

ALTER DATABASE [V6Assistant] SET READ_COMMITTED_SNAPSHOT OFF 
GO

ALTER DATABASE [V6Assistant] SET HONOR_BROKER_PRIORITY OFF 
GO

ALTER DATABASE [V6Assistant] SET  READ_WRITE 
GO

ALTER DATABASE [V6Assistant] SET RECOVERY FULL 
GO

ALTER DATABASE [V6Assistant] SET  MULTI_USER 
GO

ALTER DATABASE [V6Assistant] SET PAGE_VERIFY CHECKSUM  
GO

ALTER DATABASE [V6Assistant] SET DB_CHAINING OFF 
GO

