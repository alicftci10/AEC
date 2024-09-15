USE [AECommerceDB]
GO
/****** Object:  Table [dbo].[Cozunurluk]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Cozunurluk](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[CozunurlukId] [int] NOT NULL,
	[CozunurlukAdi] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Cozunurluk] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[CPU]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CPU](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IslemciSerisiId] [int] NOT NULL,
	[IslemciMimarisi] [nvarchar](50) NOT NULL,
	[IslemciAdi] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_CPU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[GPU]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GPU](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[EkranKartiSerisiId] [int] NOT NULL,
	[EkranKartiAdi] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_GPU] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IsletimSistemi]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[IsletimSistemi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[IsletimSistemiId] [int] NOT NULL,
	[IsletimSistemiAdi] [nvarchar](100) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_OperatingSystem] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kategori]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kategori](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KategoriAdi] [nvarchar](100) NOT NULL,
	[MainKategoriId] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Kategori] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Kullanici]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Kullanici](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Ad] [nvarchar](50) NOT NULL,
	[Soyad] [nvarchar](50) NOT NULL,
	[KullaniciAdi] [nvarchar](50) NOT NULL,
	[Sifre] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](100) NOT NULL,
	[Telefon] [varchar](20) NULL,
	[Adres] [nvarchar](250) NULL,
	[KullaniciTuruId] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Kullanici] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KullaniciKart]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KullaniciKart](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[KullaniciId] [int] NOT NULL,
	[KartAdi] [nvarchar](50) NOT NULL,
	[KartNumarasi] [varchar](16) NOT NULL,
	[KartAdSoyad] [nvarchar](100) NOT NULL,
	[KartSKTAy] [int] NOT NULL,
	[KartSKTYil] [int] NOT NULL,
	[KartCVVKodu] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NULL,
 CONSTRAINT [PK_Card] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[KullaniciTuru]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[KullaniciTuru](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[TurAdi] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_KullaniciTipi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Laptop]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Laptop](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopAdi] [nvarchar](100) NOT NULL,
	[Fiyat] [decimal](18, 2) NOT NULL,
	[IsletimSistemiId] [int] NOT NULL,
	[IslemciId] [int] NOT NULL,
	[EkranKartiId] [int] NOT NULL,
	[BellekId] [int] NOT NULL,
	[DepolamaId] [int] NOT NULL,
	[CozunurlukId] [int] NOT NULL,
	[YenilemeHiziId] [int] NOT NULL,
	[Klavye] [nvarchar](100) NULL,
	[Boyut] [nvarchar](100) NULL,
	[Agirlik] [nvarchar](100) NULL,
	[Batarya] [nvarchar](100) NULL,
	[UrunKodu] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Notebook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitor]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Monitor](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MonitorAdi] [nvarchar](100) NOT NULL,
	[Fiyat] [decimal](18, 2) NOT NULL,
	[CozunurlukId] [int] NOT NULL,
	[YenilemeHiziId] [int] NOT NULL,
	[EkranYapisi] [nvarchar](100) NULL,
	[HDR] [bit] NULL,
	[Boyut] [nvarchar](100) NULL,
	[Agirlik] [nvarchar](100) NULL,
	[UrunKodu] [int] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Monitor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RAM]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[RAM](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[BellekId] [int] NOT NULL,
	[BellekAdi] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_RAM] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Sepet]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Sepet](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[Adet] [int] NOT NULL,
	[SepetOnay] [bit] NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Sepet] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[SSD]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SSD](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[DepolamaId] [int] NOT NULL,
	[DepolamaAdi] [nvarchar](200) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_SSD] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunResmi]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunResmi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[ResimUrl] [nvarchar](300) NOT NULL,
	[ResimBoyutu] [decimal](18, 2) NOT NULL,
	[ResimTuru] [nvarchar](20) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_UrunResmi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunYorum]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunYorum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[Yorum] [nvarchar](500) NOT NULL,
	[YorumPuan] [smallint] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_UrunYorum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YenilemeHizi]    Script Date: 15.09.2024 20:33:01 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[YenilemeHizi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[YenilemeHiziId] [int] NOT NULL,
	[YenilemeHiziAdi] [nvarchar](50) NOT NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_YenilemeHizi] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[CPU] ON 

INSERT [dbo].[CPU] ([Id], [IslemciSerisiId], [IslemciMimarisi], [IslemciAdi], [CreatedAt], [CreatedBy]) VALUES (1, 12, N'12. Nesil Alder Lake', N'Intel® Alder Lake Core™ i5-12450H 8C/12T; 12MB L3; E-CORE Max 3.30GHZ P-CORE Max 4.40GHZ; 45W', CAST(N'2024-09-15T13:54:34.433' AS DateTime), 1)
INSERT [dbo].[CPU] ([Id], [IslemciSerisiId], [IslemciMimarisi], [IslemciAdi], [CreatedAt], [CreatedBy]) VALUES (2, 13, N'13. Nesil Raptor Lake', N'Intel® Raptor Lake Core™ i7-13700H 14C/20T; 24MB L3; E-CORE Max 3.70GHZ P-CORE Max 5.00 GHZ; 45W', CAST(N'2024-09-15T13:54:23.703' AS DateTime), 1)
INSERT [dbo].[CPU] ([Id], [IslemciSerisiId], [IslemciMimarisi], [IslemciAdi], [CreatedAt], [CreatedBy]) VALUES (3, 14, N'14. Nesil Raptor Lake Refresh', N'Intel® Raptor Lake Refresh Core™ i9-14900HX 24/32T; 36MB L3; E-CORE Max 4.1 GHZ P-CORE Max 5.8 GHZ; 55W', CAST(N'2024-09-15T14:00:15.560' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[CPU] OFF
GO
SET IDENTITY_INSERT [dbo].[GPU] ON 

INSERT [dbo].[GPU] ([Id], [EkranKartiSerisiId], [EkranKartiAdi], [CreatedAt], [CreatedBy]) VALUES (1, 9, N'NVIDIA® GeForce® RTX4050 Max-Performance 6GB GDDR6 96-Bit DX12 (En Yüksek Grafik Gücü;95 Watt + 10 Watt DB 2.0)', CAST(N'2024-09-15T15:05:01.897' AS DateTime), 1)
INSERT [dbo].[GPU] ([Id], [EkranKartiSerisiId], [EkranKartiAdi], [CreatedAt], [CreatedBy]) VALUES (2, 10, N'NVIDIA® GeForce® RTX4060 Max-Performance 8GB GDDR6 128-Bit DX12 (En Yüksek Grafik Gücü;80 Watt + 25 Watt DB.2.0)', CAST(N'2024-09-15T15:05:29.107' AS DateTime), 1)
INSERT [dbo].[GPU] ([Id], [EkranKartiSerisiId], [EkranKartiAdi], [CreatedAt], [CreatedBy]) VALUES (3, 11, N'NVIDIA® GeForce® RTX4080 Max-Performance 12GB GDDR6 192-Bit DX12 (En Yüksek Grafik Gücü;150 Watt + 25 Watt DB.2.0)', CAST(N'2024-09-15T15:12:45.063' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[GPU] OFF
GO
SET IDENTITY_INSERT [dbo].[Kategori] ON 

INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (1, N'Laptop', NULL, CAST(N'2024-09-14T20:03:46.710' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (2, N'Monitör', NULL, CAST(N'2024-09-14T20:03:56.597' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (3, N'Ekran Kartı Donanımları', 1, CAST(N'2024-09-14T20:04:39.127' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (4, N'İşlemci Donanımları', 1, CAST(N'2024-09-14T20:05:23.630' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (5, N'Bellek', 1, CAST(N'2024-09-14T20:06:36.947' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (6, N'İşletim Sistemi', 1, CAST(N'2024-09-14T20:06:51.393' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (7, N'Yenileme Hızı', 2, CAST(N'2024-09-14T20:07:31.973' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (8, N'Çözünürlük', 2, CAST(N'2024-09-14T20:08:00.770' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (9, N'RTX 4050', 3, CAST(N'2024-09-15T15:04:33.803' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (10, N'RTX 4060', 3, CAST(N'2024-09-15T15:04:25.010' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (11, N'RTX 4080', 3, CAST(N'2024-09-15T13:59:14.887' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (12, N'i5', 4, CAST(N'2024-09-14T20:10:09.867' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (13, N'i7', 4, CAST(N'2024-09-14T20:10:26.727' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (14, N'i9', 4, CAST(N'2024-09-14T20:10:45.330' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (15, N'8 GB', 5, CAST(N'2024-09-14T20:11:43.067' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (16, N'16 GB', 5, CAST(N'2024-09-14T20:11:57.457' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (17, N'32 GB', 5, CAST(N'2024-09-14T20:12:08.210' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (18, N'Windows', 6, CAST(N'2024-09-14T20:12:52.783' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (19, N'FreeDos', 6, CAST(N'2024-09-14T20:13:30.340' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (20, N'120 Hz', 7, CAST(N'2024-09-14T20:14:41.227' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (21, N'144 Hz', 7, CAST(N'2024-09-14T20:14:53.847' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (22, N'180 Hz', 7, CAST(N'2024-09-14T20:15:32.793' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (23, N'1920x1080', 8, CAST(N'2024-09-14T20:17:49.283' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (24, N'2560x1440', 8, CAST(N'2024-09-14T20:18:26.643' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (25, N'Depolama', 1, CAST(N'2024-09-15T18:59:24.943' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (26, N'500 GB', 25, CAST(N'2024-09-15T20:15:28.800' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (27, N'1 TB', 25, CAST(N'2024-09-15T20:15:41.347' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Kategori] OFF
GO
SET IDENTITY_INSERT [dbo].[Kullanici] ON 

INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (1, N'Ali', N'Çiftçi', N'alicftci', N'357100', N'ali.cftci@aec.com.tr', N'05546813361', N'Ankara', 1, CAST(N'2024-09-08T17:25:56.730' AS DateTime), 1)
INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (2, N'Personel', N'Test', N'personeltest', N'12345', N'personeltest@aec.com.tr', N'05556667788', N'İstanbul', 2, CAST(N'2024-09-13T17:28:00.950' AS DateTime), 1)
INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (20, N'Müşteri', N'test', N'musteritest', N'12345', N'musteri@aec.com.tr', N'05545556677', N'Test Mahallesi Test Caddesi No:99 Altındağ / Ankara', 3, CAST(N'2024-09-14T18:42:44.460' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Kullanici] OFF
GO
SET IDENTITY_INSERT [dbo].[KullaniciKart] ON 

INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (1, 1, N'ZiraatBank', N'1111222233334444', N'ALİ ÇİFTÇİ', 12, 29, 999, CAST(N'2024-09-09T15:11:49.673' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (2, 1, N'EnPara', N'9999888877776666', N'ALİ ÇİFTÇİ', 12, 26, 111, CAST(N'2024-09-07T00:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (8, 2, N'Vakıfbank Kartım', N'1111222233334444', N'Personel Test', 12, 28, 321, CAST(N'2024-09-10T20:33:31.717' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (10, 20, N'GARANTİ KARTIM', N'4444555566667777', N'Müşteri Test', 7, 30, 555, CAST(N'2024-09-10T20:43:42.977' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[KullaniciKart] OFF
GO
SET IDENTITY_INSERT [dbo].[KullaniciTuru] ON 

INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (1, N'Admin', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (2, N'Personel', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (3, N'Müşteri', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (6, N'deneme', CAST(N'2024-09-11T19:22:54.700' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[KullaniciTuru] OFF
GO
SET IDENTITY_INSERT [dbo].[RAM] ON 

INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (1, 15, N'8 GB DDR4 1.2V 3200MHz SODIMM', CAST(N'2024-09-15T18:13:47.410' AS DateTime), 1)
INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (2, 16, N'16 GB DDR5 1.1V 4800MHz SODIMM', CAST(N'2024-09-15T18:16:10.720' AS DateTime), 1)
INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (3, 16, N'16 GB DDR5 1.1V 5600MHz SODIMM', CAST(N'2024-09-15T18:15:18.123' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[RAM] OFF
GO
SET IDENTITY_INSERT [dbo].[SSD] ON 

INSERT [dbo].[SSD] ([Id], [DepolamaId], [DepolamaAdi], [CreatedAt], [CreatedBy]) VALUES (1, 26, N'500GB M.2 SSD PCIe 4.0 x4 (Okuma: 3500 MB/s - Yazma: 1625 MB/s)', CAST(N'2024-09-15T20:16:50.613' AS DateTime), 1)
INSERT [dbo].[SSD] ([Id], [DepolamaId], [DepolamaAdi], [CreatedAt], [CreatedBy]) VALUES (2, 27, N'1TB M.2 SSD PCIe 4.0 x4 (Okuma: 4125 MB/s - Yazma: 2950 MB/s)', CAST(N'2024-09-15T20:17:33.687' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[SSD] OFF
GO
ALTER TABLE [dbo].[Cozunurluk]  WITH CHECK ADD  CONSTRAINT [FK_Cozunurluk_Kategori] FOREIGN KEY([CozunurlukId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[Cozunurluk] CHECK CONSTRAINT [FK_Cozunurluk_Kategori]
GO
ALTER TABLE [dbo].[Cozunurluk]  WITH CHECK ADD  CONSTRAINT [FK_Cozunurluk_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Cozunurluk] CHECK CONSTRAINT [FK_Cozunurluk_Kullanici]
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_Kategori] FOREIGN KEY([IslemciSerisiId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_Kategori]
GO
ALTER TABLE [dbo].[CPU]  WITH CHECK ADD  CONSTRAINT [FK_CPU_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[CPU] CHECK CONSTRAINT [FK_CPU_Kullanici]
GO
ALTER TABLE [dbo].[GPU]  WITH CHECK ADD  CONSTRAINT [FK_GPU_Kategori] FOREIGN KEY([EkranKartiSerisiId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[GPU] CHECK CONSTRAINT [FK_GPU_Kategori]
GO
ALTER TABLE [dbo].[GPU]  WITH CHECK ADD  CONSTRAINT [FK_GPU_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[GPU] CHECK CONSTRAINT [FK_GPU_Kullanici]
GO
ALTER TABLE [dbo].[IsletimSistemi]  WITH CHECK ADD  CONSTRAINT [FK_IsletimSistemi_Kategori] FOREIGN KEY([IsletimSistemiId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[IsletimSistemi] CHECK CONSTRAINT [FK_IsletimSistemi_Kategori]
GO
ALTER TABLE [dbo].[IsletimSistemi]  WITH CHECK ADD  CONSTRAINT [FK_IsletimSistemi_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[IsletimSistemi] CHECK CONSTRAINT [FK_IsletimSistemi_Kullanici]
GO
ALTER TABLE [dbo].[Kategori]  WITH CHECK ADD  CONSTRAINT [FK_Kategori_Kategori] FOREIGN KEY([MainKategoriId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[Kategori] CHECK CONSTRAINT [FK_Kategori_Kategori]
GO
ALTER TABLE [dbo].[Kategori]  WITH CHECK ADD  CONSTRAINT [FK_Kategori_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Kategori] CHECK CONSTRAINT [FK_Kategori_Kullanici]
GO
ALTER TABLE [dbo].[Kullanici]  WITH CHECK ADD  CONSTRAINT [FK_Kullanici_KullaniciTuru] FOREIGN KEY([KullaniciTuruId])
REFERENCES [dbo].[KullaniciTuru] ([Id])
GO
ALTER TABLE [dbo].[Kullanici] CHECK CONSTRAINT [FK_Kullanici_KullaniciTuru]
GO
ALTER TABLE [dbo].[KullaniciKart]  WITH CHECK ADD  CONSTRAINT [FK_KullaniciKart_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[KullaniciKart] CHECK CONSTRAINT [FK_KullaniciKart_Kullanici]
GO
ALTER TABLE [dbo].[KullaniciKart]  WITH CHECK ADD  CONSTRAINT [FK_KullaniciKart_KullaniciId] FOREIGN KEY([KullaniciId])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[KullaniciKart] CHECK CONSTRAINT [FK_KullaniciKart_KullaniciId]
GO
ALTER TABLE [dbo].[KullaniciTuru]  WITH CHECK ADD  CONSTRAINT [FK_KullaniciTuru_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[KullaniciTuru] CHECK CONSTRAINT [FK_KullaniciTuru_Kullanici]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_Cozunurluk] FOREIGN KEY([CozunurlukId])
REFERENCES [dbo].[Cozunurluk] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_Cozunurluk]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_CPU] FOREIGN KEY([IslemciId])
REFERENCES [dbo].[CPU] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_CPU]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_GPU] FOREIGN KEY([EkranKartiId])
REFERENCES [dbo].[GPU] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_GPU]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_IsletimSistemi] FOREIGN KEY([IsletimSistemiId])
REFERENCES [dbo].[IsletimSistemi] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_IsletimSistemi]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_Kullanici]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_RAM] FOREIGN KEY([BellekId])
REFERENCES [dbo].[RAM] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_RAM]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_SSD] FOREIGN KEY([DepolamaId])
REFERENCES [dbo].[SSD] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_SSD]
GO
ALTER TABLE [dbo].[Laptop]  WITH CHECK ADD  CONSTRAINT [FK_Laptop_YenilemeHizi] FOREIGN KEY([YenilemeHiziId])
REFERENCES [dbo].[YenilemeHizi] ([Id])
GO
ALTER TABLE [dbo].[Laptop] CHECK CONSTRAINT [FK_Laptop_YenilemeHizi]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Cozunurluk] FOREIGN KEY([CozunurlukId])
REFERENCES [dbo].[Cozunurluk] ([Id])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_Cozunurluk]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_Kullanici]
GO
ALTER TABLE [dbo].[Monitor]  WITH CHECK ADD  CONSTRAINT [FK_Monitor_YenilemeHizi] FOREIGN KEY([YenilemeHiziId])
REFERENCES [dbo].[YenilemeHizi] ([Id])
GO
ALTER TABLE [dbo].[Monitor] CHECK CONSTRAINT [FK_Monitor_YenilemeHizi]
GO
ALTER TABLE [dbo].[RAM]  WITH CHECK ADD  CONSTRAINT [FK_RAM_Kategori] FOREIGN KEY([BellekId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[RAM] CHECK CONSTRAINT [FK_RAM_Kategori]
GO
ALTER TABLE [dbo].[RAM]  WITH CHECK ADD  CONSTRAINT [FK_RAM_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[RAM] CHECK CONSTRAINT [FK_RAM_Kullanici]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Kullanici]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Laptop] FOREIGN KEY([LaptopId])
REFERENCES [dbo].[Laptop] ([Id])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Laptop]
GO
ALTER TABLE [dbo].[Sepet]  WITH CHECK ADD  CONSTRAINT [FK_Sepet_Monitor] FOREIGN KEY([MonitorId])
REFERENCES [dbo].[Monitor] ([Id])
GO
ALTER TABLE [dbo].[Sepet] CHECK CONSTRAINT [FK_Sepet_Monitor]
GO
ALTER TABLE [dbo].[SSD]  WITH CHECK ADD  CONSTRAINT [FK_SSD_Kategori] FOREIGN KEY([DepolamaId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[SSD] CHECK CONSTRAINT [FK_SSD_Kategori]
GO
ALTER TABLE [dbo].[SSD]  WITH CHECK ADD  CONSTRAINT [FK_SSD_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[SSD] CHECK CONSTRAINT [FK_SSD_Kullanici]
GO
ALTER TABLE [dbo].[UrunResmi]  WITH CHECK ADD  CONSTRAINT [FK_UrunResmi_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[UrunResmi] CHECK CONSTRAINT [FK_UrunResmi_Kullanici]
GO
ALTER TABLE [dbo].[UrunResmi]  WITH CHECK ADD  CONSTRAINT [FK_UrunResmi_Laptop] FOREIGN KEY([LaptopId])
REFERENCES [dbo].[Laptop] ([Id])
GO
ALTER TABLE [dbo].[UrunResmi] CHECK CONSTRAINT [FK_UrunResmi_Laptop]
GO
ALTER TABLE [dbo].[UrunResmi]  WITH CHECK ADD  CONSTRAINT [FK_UrunResmi_Monitor] FOREIGN KEY([MonitorId])
REFERENCES [dbo].[Monitor] ([Id])
GO
ALTER TABLE [dbo].[UrunResmi] CHECK CONSTRAINT [FK_UrunResmi_Monitor]
GO
ALTER TABLE [dbo].[UrunYorum]  WITH CHECK ADD  CONSTRAINT [FK_UrunYorum_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[UrunYorum] CHECK CONSTRAINT [FK_UrunYorum_Kullanici]
GO
ALTER TABLE [dbo].[UrunYorum]  WITH CHECK ADD  CONSTRAINT [FK_UrunYorum_Laptop] FOREIGN KEY([LaptopId])
REFERENCES [dbo].[Laptop] ([Id])
GO
ALTER TABLE [dbo].[UrunYorum] CHECK CONSTRAINT [FK_UrunYorum_Laptop]
GO
ALTER TABLE [dbo].[UrunYorum]  WITH CHECK ADD  CONSTRAINT [FK_UrunYorum_Monitor] FOREIGN KEY([MonitorId])
REFERENCES [dbo].[Monitor] ([Id])
GO
ALTER TABLE [dbo].[UrunYorum] CHECK CONSTRAINT [FK_UrunYorum_Monitor]
GO
ALTER TABLE [dbo].[YenilemeHizi]  WITH CHECK ADD  CONSTRAINT [FK_YenilemeHizi_Kategori] FOREIGN KEY([YenilemeHiziId])
REFERENCES [dbo].[Kategori] ([Id])
GO
ALTER TABLE [dbo].[YenilemeHizi] CHECK CONSTRAINT [FK_YenilemeHizi_Kategori]
GO
ALTER TABLE [dbo].[YenilemeHizi]  WITH CHECK ADD  CONSTRAINT [FK_YenilemeHizi_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[YenilemeHizi] CHECK CONSTRAINT [FK_YenilemeHizi_Kullanici]
GO
