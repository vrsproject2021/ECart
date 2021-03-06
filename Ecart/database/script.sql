USE [master]
GO
/****** Object:  Database [ecommercedb]    Script Date: 14-01-2022 10:51:45 ******/
CREATE DATABASE [ecommercedb]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ecommercedb', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ecommercedb.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'ecommercedb_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL15.SQLEXPRESS\MSSQL\DATA\ecommercedb_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT
GO
ALTER DATABASE [ecommercedb] SET COMPATIBILITY_LEVEL = 150
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ecommercedb].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ecommercedb] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ecommercedb] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ecommercedb] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ecommercedb] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ecommercedb] SET ARITHABORT OFF 
GO
ALTER DATABASE [ecommercedb] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ecommercedb] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ecommercedb] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ecommercedb] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ecommercedb] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ecommercedb] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ecommercedb] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ecommercedb] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ecommercedb] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ecommercedb] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ecommercedb] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ecommercedb] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ecommercedb] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ecommercedb] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ecommercedb] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ecommercedb] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ecommercedb] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ecommercedb] SET RECOVERY FULL 
GO
ALTER DATABASE [ecommercedb] SET  MULTI_USER 
GO
ALTER DATABASE [ecommercedb] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ecommercedb] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ecommercedb] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ecommercedb] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [ecommercedb] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [ecommercedb] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
ALTER DATABASE [ecommercedb] SET QUERY_STORE = OFF
GO
USE [ecommercedb]
GO
/****** Object:  User [WebDatabaseUser]    Script Date: 14-01-2022 10:51:45 ******/
CREATE USER [WebDatabaseUser] FOR LOGIN [IIS APPPOOL\DefaultAppPool] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [WebDatabaseUser]
GO
/****** Object:  Table [dbo].[tblcart]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcart](
	[cartid] [int] IDENTITY(1,1) NOT NULL,
	[productid] [int] NULL,
	[userid] [int] NULL,
	[addeddate] [date] NULL,
	[ispurchased] [bit] NULL,
 CONSTRAINT [PK_tblcart] PRIMARY KEY CLUSTERED 
(
	[cartid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcategory]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcategory](
	[categoryid] [int] IDENTITY(1,1) NOT NULL,
	[category] [varchar](200) NULL,
	[userid] [int] NULL,
	[createddate] [date] NULL,
	[status] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblcategory] PRIMARY KEY CLUSTERED 
(
	[categoryid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblcoupon]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblcoupon](
	[couponid] [int] IDENTITY(1,1) NOT NULL,
	[couponcode] [varchar](200) NULL,
	[discountamount] [decimal](18, 4) NULL,
	[userid] [int] NULL,
	[createddate] [date] NULL,
	[isactive] [bit] NULL,
 CONSTRAINT [PK_tblcoupon] PRIMARY KEY CLUSTERED 
(
	[couponid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tbllogin]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbllogin](
	[userid] [int] IDENTITY(1,1) NOT NULL,
	[username] [varchar](100) NOT NULL,
	[password] [varchar](max) NOT NULL,
	[userrole] [varchar](50) NULL,
 CONSTRAINT [PK_tbllogin] PRIMARY KEY CLUSTERED 
(
	[userid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblproduct]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblproduct](
	[productid] [int] IDENTITY(1,1) NOT NULL,
	[productname] [varchar](150) NULL,
	[categoryid] [int] NULL,
	[price] [decimal](18, 4) NULL,
	[imageurl] [varchar](max) NULL,
	[userid] [int] NULL,
	[createddate] [date] NULL,
	[status] [varchar](50) NULL,
 CONSTRAINT [PK_tblproduct] PRIMARY KEY CLUSTERED 
(
	[productid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblpurchase]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblpurchase](
	[purchaseid] [int] IDENTITY(1,1) NOT NULL,
	[invoicenumber] [varchar](max) NULL,
	[totalamount] [decimal](18, 4) NULL,
	[discountamount] [decimal](18, 4) NULL,
	[userid] [int] NULL,
	[createddate] [date] NULL,
 CONSTRAINT [PK_tblpurchase] PRIMARY KEY CLUSTERED 
(
	[purchaseid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblpurchasedetails]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblpurchasedetails](
	[purchasedetailsid] [int] IDENTITY(1,1) NOT NULL,
	[purchaseid] [int] NULL,
	[productid] [int] NULL,
	[quantity] [int] NULL,
	[amount] [decimal](18, 4) NULL,
 CONSTRAINT [PK_tblpurchasedetails] PRIMARY KEY CLUSTERED 
(
	[purchasedetailsid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblregistration]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblregistration](
	[registrationid] [int] IDENTITY(1,1) NOT NULL,
	[userid] [int] NOT NULL,
	[name] [varchar](100) NOT NULL,
	[addressline1] [varchar](max) NOT NULL,
	[addresslin2] [varchar](max) NOT NULL,
	[city] [varchar](100) NULL,
	[country] [varchar](150) NULL,
	[phone] [varchar](50) NOT NULL,
	[pin] [varchar](50) NOT NULL,
	[email] [varchar](500) NOT NULL,
	[isactive] [bit] NOT NULL,
	[createddate] [date] NOT NULL,
 CONSTRAINT [PK_tblregistration] PRIMARY KEY CLUSTERED 
(
	[registrationid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[tblstockdetails]    Script Date: 14-01-2022 10:51:45 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tblstockdetails](
	[stockid] [int] IDENTITY(1,1) NOT NULL,
	[productid] [int] NULL,
	[availablequantity] [int] NULL,
 CONSTRAINT [PK_tblstockdetails] PRIMARY KEY CLUSTERED 
(
	[stockid] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[tblcart] ON 

INSERT [dbo].[tblcart] ([cartid], [productid], [userid], [addeddate], [ispurchased]) VALUES (1, 4, 3, NULL, NULL)
INSERT [dbo].[tblcart] ([cartid], [productid], [userid], [addeddate], [ispurchased]) VALUES (2, 4, 3, NULL, NULL)
INSERT [dbo].[tblcart] ([cartid], [productid], [userid], [addeddate], [ispurchased]) VALUES (3, 5, 6, NULL, NULL)
INSERT [dbo].[tblcart] ([cartid], [productid], [userid], [addeddate], [ispurchased]) VALUES (4, 11, 6, NULL, NULL)
INSERT [dbo].[tblcart] ([cartid], [productid], [userid], [addeddate], [ispurchased]) VALUES (5, 12, 6, NULL, NULL)
SET IDENTITY_INSERT [dbo].[tblcart] OFF
GO
SET IDENTITY_INSERT [dbo].[tblcategory] ON 

INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (3, N'vegetables', 5, CAST(N'2021-12-16' AS Date), N'A')
INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (4, N'Pulses', 5, CAST(N'2021-12-16' AS Date), N'A')
INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (5, N'test1', 2, CAST(N'2021-12-03' AS Date), N'A')
INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (6, N'Fast Food', 5, CAST(N'2021-12-16' AS Date), N'A')
INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (8, N'Fruits', 5, CAST(N'2021-12-03' AS Date), N'A')
INSERT [dbo].[tblcategory] ([categoryid], [category], [userid], [createddate], [status]) VALUES (10, N'Home Appliances', 5, CAST(N'2021-12-10' AS Date), N'A')
SET IDENTITY_INSERT [dbo].[tblcategory] OFF
GO
SET IDENTITY_INSERT [dbo].[tbllogin] ON 

INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (1, N'anandu@gmail.com', N'svkLRj9nYEgZo7gWDJD5IQ==', N'Admin')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (2, N'algy@gmail.com', N'Wj7VoVOy/+7ytLNAvim/Yw==', N'Admin')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (3, N'anandujay18@gmail.com', N'Wj7VoVOy/+7ytLNAvim/Yw==', N'User')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (4, N'test@gmail.com', N'Wj7VoVOy/+7ytLNAvim/Yw==', N'User')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (5, N'jithin@gmail.com', N'NYxjWUPBKvynU+f2k4XBsQ==', N'Admin')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (6, N'anandhu@gmail.com', N'9DincyZXpQ0S/uhKD1F6qQ==', N'User')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (7, N'jithingmail.com', N'jithin', N'Admin')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (9, N'customer@gmail.com', N'e/PN+7Lc71b5wkzt2gAMIAYeRU+GizR0j8O5f8g4c6U=', N'User')
INSERT [dbo].[tbllogin] ([userid], [username], [password], [userrole]) VALUES (11, N'user@gmail.com', N'gWVTSMpHnTHlFMsGWNJmLw==', N'User')
SET IDENTITY_INSERT [dbo].[tbllogin] OFF
GO
SET IDENTITY_INSERT [dbo].[tblproduct] ON 

INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (4, N'Test', 3, CAST(120.0000 AS Decimal(18, 4)), N'../Image/cat-22158472955.jpg', 2, CAST(N'2021-12-01' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (5, N'Test', 3, CAST(120.0000 AS Decimal(18, 4)), N'../Image/cat-22101432433.jpg', 2, CAST(N'2021-12-01' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (6, N'carrot', 3, CAST(45.0000 AS Decimal(18, 4)), N'../Image/cat-12138556903.jpg', 2, CAST(N'2021-12-02' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (7, N'final', 5, CAST(400.0000 AS Decimal(18, 4)), N'../Image/cat-42153188865.jpg', 2, CAST(N'2021-12-03' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (8, N'cauli', 3, CAST(30.0000 AS Decimal(18, 4)), N'../Image/cauli2117032617.jpg', 5, CAST(N'2021-12-03' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (9, N'Basumathi Rice long Grain', 4, CAST(110.0000 AS Decimal(18, 4)), N'../Image/rice2128571534.jpg', 5, CAST(N'2021-12-03' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (10, N'Grapes', 8, CAST(110.0000 AS Decimal(18, 4)), N'../Image/thumb32107038665.GIF', 5, CAST(N'2021-12-03' AS Date), NULL)
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (11, N'Cauliflower', 3, CAST(35.0000 AS Decimal(18, 4)), N'../Image/cauli2146239992.jpg', 5, CAST(N'2021-12-10' AS Date), N'A')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (12, N'Brinjal', 3, CAST(35.0000 AS Decimal(18, 4)), N'../Image/brin2149013822.jpg', 5, CAST(N'2021-12-10' AS Date), N'A')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (13, N'Grapes', 8, CAST(50.0000 AS Decimal(18, 4)), N'../Image/thumb32153348738.GIF', 5, CAST(N'2021-12-10' AS Date), N'D')
INSERT [dbo].[tblproduct] ([productid], [productname], [categoryid], [price], [imageurl], [userid], [createddate], [status]) VALUES (14, N'Basumathi Rice long Grain', 4, CAST(50.0000 AS Decimal(18, 4)), N'../Image/rice2108184341.jpg', 5, CAST(N'2021-12-10' AS Date), N'A')
SET IDENTITY_INSERT [dbo].[tblproduct] OFF
GO
SET IDENTITY_INSERT [dbo].[tblpurchase] ON 

INSERT [dbo].[tblpurchase] ([purchaseid], [invoicenumber], [totalamount], [discountamount], [userid], [createddate]) VALUES (1, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, 3, CAST(N'2021-12-03' AS Date))
INSERT [dbo].[tblpurchase] ([purchaseid], [invoicenumber], [totalamount], [discountamount], [userid], [createddate]) VALUES (2, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, 3, CAST(N'2021-12-03' AS Date))
INSERT [dbo].[tblpurchase] ([purchaseid], [invoicenumber], [totalamount], [discountamount], [userid], [createddate]) VALUES (3, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, 6, CAST(N'2021-12-21' AS Date))
INSERT [dbo].[tblpurchase] ([purchaseid], [invoicenumber], [totalamount], [discountamount], [userid], [createddate]) VALUES (4, NULL, CAST(0.0000 AS Decimal(18, 4)), NULL, 11, CAST(N'2021-12-22' AS Date))
SET IDENTITY_INSERT [dbo].[tblpurchase] OFF
GO
SET IDENTITY_INSERT [dbo].[tblpurchasedetails] ON 

INSERT [dbo].[tblpurchasedetails] ([purchasedetailsid], [purchaseid], [productid], [quantity], [amount]) VALUES (2, NULL, NULL, 20, NULL)
INSERT [dbo].[tblpurchasedetails] ([purchasedetailsid], [purchaseid], [productid], [quantity], [amount]) VALUES (3, NULL, NULL, 5, NULL)
SET IDENTITY_INSERT [dbo].[tblpurchasedetails] OFF
GO
SET IDENTITY_INSERT [dbo].[tblregistration] ON 

INSERT [dbo].[tblregistration] ([registrationid], [userid], [name], [addressline1], [addresslin2], [city], [country], [phone], [pin], [email], [isactive], [createddate]) VALUES (1, 3, N'Anandu jayakumar', N'vayalar', N'cherthala', N'Alappuzha', N'India', N'+918921261058', N'50', N'anandujay18@gmail.com', 1, CAST(N'2021-12-02' AS Date))
INSERT [dbo].[tblregistration] ([registrationid], [userid], [name], [addressline1], [addresslin2], [city], [country], [phone], [pin], [email], [isactive], [createddate]) VALUES (2, 6, N'Name', N'Add', N'Add', N'kochi', N'india', N'7878787878', N'898989', N'cust@gmail.com', 1, CAST(N'2021-12-10' AS Date))
INSERT [dbo].[tblregistration] ([registrationid], [userid], [name], [addressline1], [addresslin2], [city], [country], [phone], [pin], [email], [isactive], [createddate]) VALUES (3, 11, N'User', N'Kkd', N'Kochi', N'Kochi', N'india', N'4545454545', N'234234', N'user@gmail.com', 1, CAST(N'2021-12-22' AS Date))
SET IDENTITY_INSERT [dbo].[tblregistration] OFF
GO
SET IDENTITY_INSERT [dbo].[tblstockdetails] ON 

INSERT [dbo].[tblstockdetails] ([stockid], [productid], [availablequantity]) VALUES (2, 6, 4)
INSERT [dbo].[tblstockdetails] ([stockid], [productid], [availablequantity]) VALUES (5, 9, 55)
INSERT [dbo].[tblstockdetails] ([stockid], [productid], [availablequantity]) VALUES (6, 10, 50)
INSERT [dbo].[tblstockdetails] ([stockid], [productid], [availablequantity]) VALUES (7, 11, 30)
INSERT [dbo].[tblstockdetails] ([stockid], [productid], [availablequantity]) VALUES (8, 12, 20)
SET IDENTITY_INSERT [dbo].[tblstockdetails] OFF
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tbllogin]    Script Date: 14-01-2022 10:51:45 ******/
ALTER TABLE [dbo].[tbllogin] ADD  CONSTRAINT [IX_tbllogin] UNIQUE NONCLUSTERED 
(
	[username] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
SET ANSI_PADDING ON
GO
/****** Object:  Index [IX_tblregistration]    Script Date: 14-01-2022 10:51:45 ******/
ALTER TABLE [dbo].[tblregistration] ADD  CONSTRAINT [IX_tblregistration] UNIQUE NONCLUSTERED 
(
	[email] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, SORT_IN_TEMPDB = OFF, IGNORE_DUP_KEY = OFF, ONLINE = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
GO
ALTER TABLE [dbo].[tblcart]  WITH CHECK ADD  CONSTRAINT [FK_tblcart_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblcart] CHECK CONSTRAINT [FK_tblcart_tbllogin]
GO
ALTER TABLE [dbo].[tblcart]  WITH CHECK ADD  CONSTRAINT [FK_tblcart_tblproduct] FOREIGN KEY([productid])
REFERENCES [dbo].[tblproduct] ([productid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblcart] CHECK CONSTRAINT [FK_tblcart_tblproduct]
GO
ALTER TABLE [dbo].[tblcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblcategory_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblcategory] CHECK CONSTRAINT [FK_tblcategory_tbllogin]
GO
ALTER TABLE [dbo].[tblcoupon]  WITH CHECK ADD  CONSTRAINT [FK_tblcoupon_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblcoupon] CHECK CONSTRAINT [FK_tblcoupon_tbllogin]
GO
ALTER TABLE [dbo].[tblproduct]  WITH CHECK ADD  CONSTRAINT [FK_tblproduct_tblcategory] FOREIGN KEY([categoryid])
REFERENCES [dbo].[tblcategory] ([categoryid])
GO
ALTER TABLE [dbo].[tblproduct] CHECK CONSTRAINT [FK_tblproduct_tblcategory]
GO
ALTER TABLE [dbo].[tblproduct]  WITH CHECK ADD  CONSTRAINT [FK_tblproduct_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
GO
ALTER TABLE [dbo].[tblproduct] CHECK CONSTRAINT [FK_tblproduct_tbllogin]
GO
ALTER TABLE [dbo].[tblpurchase]  WITH CHECK ADD  CONSTRAINT [FK_tblpurchase_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblpurchase] CHECK CONSTRAINT [FK_tblpurchase_tbllogin]
GO
ALTER TABLE [dbo].[tblpurchasedetails]  WITH CHECK ADD  CONSTRAINT [FK_tblpurchasedetails_tblproduct] FOREIGN KEY([productid])
REFERENCES [dbo].[tblproduct] ([productid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblpurchasedetails] CHECK CONSTRAINT [FK_tblpurchasedetails_tblproduct]
GO
ALTER TABLE [dbo].[tblpurchasedetails]  WITH CHECK ADD  CONSTRAINT [FK_tblpurchasedetails_tblpurchase] FOREIGN KEY([purchaseid])
REFERENCES [dbo].[tblpurchase] ([purchaseid])
GO
ALTER TABLE [dbo].[tblpurchasedetails] CHECK CONSTRAINT [FK_tblpurchasedetails_tblpurchase]
GO
ALTER TABLE [dbo].[tblregistration]  WITH CHECK ADD  CONSTRAINT [FK_tblregistration_tbllogin] FOREIGN KEY([userid])
REFERENCES [dbo].[tbllogin] ([userid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblregistration] CHECK CONSTRAINT [FK_tblregistration_tbllogin]
GO
ALTER TABLE [dbo].[tblstockdetails]  WITH CHECK ADD  CONSTRAINT [FK_tblstockdetails_tblproduct] FOREIGN KEY([productid])
REFERENCES [dbo].[tblproduct] ([productid])
ON UPDATE CASCADE
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[tblstockdetails] CHECK CONSTRAINT [FK_tblstockdetails_tblproduct]
GO
USE [master]
GO
ALTER DATABASE [ecommercedb] SET  READ_WRITE 
GO
