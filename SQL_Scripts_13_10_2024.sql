USE [AECommerceDB]
GO
/****** Object:  Table [dbo].[Cozunurluk]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[CPU]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[GPU]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[Hakkimizda]    Script Date: 29.11.2024 12:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Hakkimizda](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Vizyon] [nvarchar](1000) NULL,
	[Misyon] [nvarchar](1000) NULL,
	[Hikaye] [nvarchar](1000) NULL,
	[Telefon] [varchar](20) NULL,
	[Adres] [nvarchar](250) NULL,
	[Email] [nvarchar](100) NULL,
	[CalismaGunleri] [nvarchar](250) NULL,
	[UpdatedAt] [datetime] NOT NULL,
	[UpdatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Hakkimizda] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[IsletimSistemi]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[Kategori]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[Kullanici]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[KullaniciKart]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[KullaniciTuru]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[Laptop]    Script Date: 29.11.2024 12:51:14 ******/
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
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Notebook] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Monitor]    Script Date: 29.11.2024 12:51:14 ******/
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
	[HDR] [varchar](20) NULL,
	[Boyut] [nvarchar](100) NULL,
	[Agirlik] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Monitor] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Mouse]    Script Date: 29.11.2024 12:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Mouse](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MouseAdi] [nvarchar](100) NOT NULL,
	[Fiyat] [decimal](18, 2) NOT NULL,
	[Renk] [nvarchar](100) NOT NULL,
	[DPI] [nvarchar](50) NOT NULL,
	[TusSayisi] [nvarchar](20) NOT NULL,
	[BaglantiOzellikleri] [nvarchar](100) NOT NULL,
	[Boyut] [nvarchar](100) NULL,
	[Agirlik] [nvarchar](100) NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_Mouse] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[RAM]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[SSD]    Script Date: 29.11.2024 12:51:14 ******/
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
/****** Object:  Table [dbo].[UrunResmi]    Script Date: 29.11.2024 12:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunResmi](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[MouseId] [int] NULL,
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
/****** Object:  Table [dbo].[UrunTakip]    Script Date: 29.11.2024 12:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunTakip](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[MouseId] [int] NULL,
	[Adet] [int] NULL,
	[Favori] [bit] NULL,
	[SepetDurum] [bit] NULL,
	[SiparisDurum] [int] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
	[UpdatedAt] [datetime] NULL,
	[UpdatedBy] [int] NULL,
 CONSTRAINT [PK_UrunTakip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[UrunYorum]    Script Date: 29.11.2024 12:51:14 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UrunYorum](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[LaptopId] [int] NULL,
	[MonitorId] [int] NULL,
	[MouseId] [int] NULL,
	[Yorum] [nvarchar](500) NULL,
	[YorumPuan] [smallint] NULL,
	[CreatedAt] [datetime] NOT NULL,
	[CreatedBy] [int] NOT NULL,
 CONSTRAINT [PK_UrunYorum] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[YenilemeHizi]    Script Date: 29.11.2024 12:51:14 ******/
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
SET IDENTITY_INSERT [dbo].[Cozunurluk] ON 

INSERT [dbo].[Cozunurluk] ([Id], [CozunurlukId], [CozunurlukAdi], [CreatedAt], [CreatedBy]) VALUES (1, 23, N'15,6" FHD 1920x1080', CAST(N'2024-09-16T15:31:02.700' AS DateTime), 1)
INSERT [dbo].[Cozunurluk] ([Id], [CozunurlukId], [CozunurlukAdi], [CreatedAt], [CreatedBy]) VALUES (2, 23, N'17,3" FHD 1920x1080', CAST(N'2024-09-16T15:31:38.823' AS DateTime), 1)
INSERT [dbo].[Cozunurluk] ([Id], [CozunurlukId], [CozunurlukAdi], [CreatedAt], [CreatedBy]) VALUES (3, 29, N'16" WQXGA 2560x1600', CAST(N'2024-09-22T17:24:11.733' AS DateTime), 1)
INSERT [dbo].[Cozunurluk] ([Id], [CozunurlukId], [CozunurlukAdi], [CreatedAt], [CreatedBy]) VALUES (5, 23, N'24" FHD 1920x1080', CAST(N'2024-09-22T17:26:33.397' AS DateTime), 1)
INSERT [dbo].[Cozunurluk] ([Id], [CozunurlukId], [CozunurlukAdi], [CreatedAt], [CreatedBy]) VALUES (6, 24, N'31,5" QHD 2560x1440', CAST(N'2024-09-22T17:27:56.467' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Cozunurluk] OFF
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
SET IDENTITY_INSERT [dbo].[Hakkimizda] ON 

INSERT [dbo].[Hakkimizda] ([Id], [Vizyon], [Misyon], [Hikaye], [Telefon], [Adres], [Email], [CalismaGunleri], [UpdatedAt], [UpdatedBy]) VALUES (1, N'AECommerce olarak vizyonumuz, teknoloji dünyasında yenilikçi çözümler sunarak müşterilerimize en üst düzey performansı ve kaliteyi sağlamaktır. Teknolojinin hızla gelişen dinamiklerini takip ediyor ve bu doğrultuda en son teknoloji ürünlerini, rekabetçi fiyatlarla sunarak sektörde lider bir e-ticaret platformu olmayı hedefliyoruz.', N'Misyonumuz, teknolojiye meraklı tüm kullanıcıların ihtiyaç duyduğu bilgisayar ve donanım ürünlerini en güvenilir şekilde sunmak, satış sonrası destek hizmetleriyle müşteri memnuniyetini en üst seviyede tutmaktır. Kaliteli ürün yelpazemiz ve uzman ekibimizle, her müşterimizin bireysel ihtiyaçlarına uygun çözümler üretmeyi hedefliyoruz.', N'AECommerce, teknolojiye duyduğumuz tutku ve piyasanın ihtiyaçlarına duyarlı olma isteğimizle kuruldu. Bilgisayarlar, monitörler, oyun aksesuarları ve daha birçok teknoloji ürünüyle kullanıcıların günlük hayatını kolaylaştırmak, performanslarını artırmak ve onların dijital deneyimlerini bir üst seviyeye taşımak için bu yolculuğa başladık. Bugün ise hem amatör teknoloji tutkunlarına hem de profesyonellere hitap eden geniş bir ürün yelpazesi ile hizmet veriyoruz.', N'05556667788', N'Test Mahallesi Test Caddesi No : 99/9 Altındağ/Ankara', N'aecommerce@aec.com.tr', N'Hafta içi 09:00 - 18:30', CAST(N'2024-10-13T16:06:12.153' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Hakkimizda] OFF
GO
SET IDENTITY_INSERT [dbo].[IsletimSistemi] ON 

INSERT [dbo].[IsletimSistemi] ([Id], [IsletimSistemiId], [IsletimSistemiAdi], [CreatedAt], [CreatedBy]) VALUES (1, 18, N'Windows 11 Home Single Language', CAST(N'2024-09-16T17:19:29.480' AS DateTime), 1)
INSERT [dbo].[IsletimSistemi] ([Id], [IsletimSistemiId], [IsletimSistemiAdi], [CreatedAt], [CreatedBy]) VALUES (2, 28, N'FreeDos(İşletim sistemi bulunmamaktadır.)', CAST(N'2024-09-16T17:20:23.433' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[IsletimSistemi] OFF
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
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (19, N'Linux', 6, CAST(N'2024-09-16T15:39:15.107' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (20, N'144 Hz', 7, CAST(N'2024-09-16T14:57:59.167' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (21, N'165 Hz', 7, CAST(N'2024-09-16T14:57:50.903' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (22, N'180 Hz', 7, CAST(N'2024-09-29T14:17:42.867' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (23, N'1920x1080', 8, CAST(N'2024-09-14T20:17:49.283' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (24, N'2560x1440', 8, CAST(N'2024-09-22T17:23:52.520' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (25, N'Depolama', 1, CAST(N'2024-09-15T18:59:24.943' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (26, N'500 GB', 25, CAST(N'2024-09-15T20:15:28.800' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (27, N'1 TB', 25, CAST(N'2024-09-15T20:15:41.347' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (28, N'FreeDos', 6, CAST(N'2024-09-16T15:39:07.673' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (29, N'2560x1600', 8, CAST(N'2024-09-22T17:23:43.163' AS DateTime), 1)
INSERT [dbo].[Kategori] ([Id], [KategoriAdi], [MainKategoriId], [CreatedAt], [CreatedBy]) VALUES (30, N'Mouse', NULL, CAST(N'2024-09-26T15:45:02.890' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Kategori] OFF
GO
SET IDENTITY_INSERT [dbo].[Kullanici] ON 

INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (1, N'Ali', N'Çiftçi', N'alicftci', N'357100', N'ali.cftci@aec.com.tr', N'05551112233', N'Ankara', 1, CAST(N'2024-10-02T16:52:45.037' AS DateTime), 1)
INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (2, N'Personel', N'Test', N'personeltest', N'12345', N'personeltest@aec.com.tr', N'05556667788', N'İstanbul', 2, CAST(N'2024-09-29T15:13:21.643' AS DateTime), 1)
INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (20, N'Müşteri', N'test', N'musteritest', N'12345', N'musteri@aec.com.tr', N'05545556677', N'Test Mahallesi Test Caddesi No:99 Altındağ / Ankara', 3, CAST(N'2024-10-13T17:00:44.943' AS DateTime), 20)
INSERT [dbo].[Kullanici] ([Id], [Ad], [Soyad], [KullaniciAdi], [Sifre], [Email], [Telefon], [Adres], [KullaniciTuruId], [CreatedAt], [CreatedBy]) VALUES (31, N'Musteri', N'Test2', N'musteritest2', N'12345', N'musteritest2@aec.com.tr', N'05552224466', N'Test2 Mahallesi Test2 Caddesi No:99 Altındağ / Ankara', 3, CAST(N'2024-10-02T14:25:02.517' AS DateTime), NULL)
SET IDENTITY_INSERT [dbo].[Kullanici] OFF
GO
SET IDENTITY_INSERT [dbo].[KullaniciKart] ON 

INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (1, 1, N'ZiraatBank', N'1111222233334444', N'ALİ ÇİFTÇİ', 12, 29, 999, CAST(N'2024-10-03T16:18:10.463' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (2, 1, N'EnPara', N'9999888877776666', N'ALİ ÇİFTÇİ', 12, 26, 111, CAST(N'2024-10-03T21:39:54.823' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (8, 2, N'Vakıfbank Kartım', N'1111222233334444', N'Personel Test', 12, 28, 321, CAST(N'2024-09-10T20:33:31.717' AS DateTime), 1)
INSERT [dbo].[KullaniciKart] ([Id], [KullaniciId], [KartAdi], [KartNumarasi], [KartAdSoyad], [KartSKTAy], [KartSKTYil], [KartCVVKodu], [CreatedAt], [CreatedBy]) VALUES (10, 20, N'GARANTİ KARTIM', N'4444555566667777', N'Müşteri Test', 7, 30, 555, CAST(N'2024-10-13T16:41:09.863' AS DateTime), 20)
SET IDENTITY_INSERT [dbo].[KullaniciKart] OFF
GO
SET IDENTITY_INSERT [dbo].[KullaniciTuru] ON 

INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (1, N'Admin', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (2, N'Personel', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (3, N'Müşteri', CAST(N'2024-08-30T12:00:00.000' AS DateTime), 1)
INSERT [dbo].[KullaniciTuru] ([Id], [TurAdi], [CreatedAt], [CreatedBy]) VALUES (6, N'Diğer', CAST(N'2024-09-29T15:18:45.767' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[KullaniciTuru] OFF
GO
SET IDENTITY_INSERT [dbo].[Laptop] ON 

INSERT [dbo].[Laptop] ([Id], [LaptopAdi], [Fiyat], [IsletimSistemiId], [IslemciId], [EkranKartiId], [BellekId], [DepolamaId], [CozunurlukId], [YenilemeHiziId], [Klavye], [Boyut], [Agirlik], [Batarya], [CreatedAt], [CreatedBy]) VALUES (1, N'MONSTER Abra A5 V20.4.1 15,6" Oyun Bilgisayarı', CAST(33399.00 AS Decimal(18, 2)), 1, 1, 1, 1, 2, 1, 1, N'RGB Tek Bölge Aydınlatmalı Klavye (Türkçe Q)', N'360.2 x 243.5 x 23.0 mm', N'2.00 Kg', N'4 Hücreli 62,32Wh', CAST(N'2024-09-20T20:31:42.177' AS DateTime), 1)
INSERT [dbo].[Laptop] ([Id], [LaptopAdi], [Fiyat], [IsletimSistemiId], [IslemciId], [EkranKartiId], [BellekId], [DepolamaId], [CozunurlukId], [YenilemeHiziId], [Klavye], [Boyut], [Agirlik], [Batarya], [CreatedAt], [CreatedBy]) VALUES (2, N'MONSTER Tulpar T7 V26.1.5 17,3" Oyun Bilgisayarı', CAST(48999.00 AS Decimal(18, 2)), 1, 2, 2, 2, 2, 2, 1, N'RGB 4 Bölge Aydınlatmalı Klavye (Türkçe Q)', N'396mm x 276mm x 26.8mm', N'2,35Kg', N'4 Hücreli 54wh', CAST(N'2024-09-20T20:36:15.263' AS DateTime), 1)
INSERT [dbo].[Laptop] ([Id], [LaptopAdi], [Fiyat], [IsletimSistemiId], [IslemciId], [EkranKartiId], [BellekId], [DepolamaId], [CozunurlukId], [YenilemeHiziId], [Klavye], [Boyut], [Agirlik], [Batarya], [CreatedAt], [CreatedBy]) VALUES (3, N'MONSTER Tulpar T6 V1.2.5 16" Oyun Bilgisayarı', CAST(105499.00 AS Decimal(18, 2)), 1, 3, 3, 3, 2, 3, 2, N'RGB 4 Bölge Aydınlatmalı Klavye (Türkçe Q)', N'358.44 x 266.8 x 26 mm', N'2,5 Kg', N'4 Hücreli 80 WH', CAST(N'2024-09-20T20:42:35.523' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Laptop] OFF
GO
SET IDENTITY_INSERT [dbo].[Monitor] ON 

INSERT [dbo].[Monitor] ([Id], [MonitorAdi], [Fiyat], [CozunurlukId], [YenilemeHiziId], [EkranYapisi], [HDR], [Boyut], [Agirlik], [CreatedAt], [CreatedBy]) VALUES (1, N'MONSTER Aryond A24 V2 180 Hz Gaming Monitör', CAST(5499.00 AS Decimal(18, 2)), 5, 4, N'24" Çerçevesiz Flat Ekran 16:9', N'HDR10', N'527.04mm x 296.46mm', N'6.00 Kg', CAST(N'2024-09-22T18:07:42.073' AS DateTime), 1)
INSERT [dbo].[Monitor] ([Id], [MonitorAdi], [Fiyat], [CozunurlukId], [YenilemeHiziId], [EkranYapisi], [HDR], [Boyut], [Agirlik], [CreatedAt], [CreatedBy]) VALUES (2, N'MONSTER Aryond A32 V2 180 Hz Oyuncu Monitörü', CAST(6999.00 AS Decimal(18, 2)), 6, 5, N'İnce Kenarlı Curved (1500R) Geniş Ekran 31,5" 16:9', N'HDR10', N'711 × 538,9 × 209 mm', N'8,40 Kg', CAST(N'2024-09-22T18:07:51.857' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Monitor] OFF
GO
SET IDENTITY_INSERT [dbo].[Mouse] ON 

INSERT [dbo].[Mouse] ([Id], [MouseAdi], [Fiyat], [Renk], [DPI], [TusSayisi], [BaglantiOzellikleri], [Boyut], [Agirlik], [CreatedAt], [CreatedBy]) VALUES (1, N'MONSTER Pusat Business Pro Kablosuz Mouse - Gümüş', CAST(299.00 AS Decimal(18, 2)), N'Gümüş', N'1600 DPI', N'4 Adet', N'Çift bağlantı modu (2.4GHz Wireless + Bluetooth sürümü : 5.1)', N'103 x 60,2 x 35mm', N'75 gr', CAST(N'2024-09-26T17:55:10.540' AS DateTime), 1)
INSERT [dbo].[Mouse] ([Id], [MouseAdi], [Fiyat], [Renk], [DPI], [TusSayisi], [BaglantiOzellikleri], [Boyut], [Agirlik], [CreatedAt], [CreatedBy]) VALUES (2, N'MONSTER Pusat Business Pro Kablosuz Mouse', CAST(299.00 AS Decimal(18, 2)), N'Siyah', N'1600 DPI', N'4 Adet', N'Çift bağlantı modu (2.4GHz Wireless + Bluetooth sürümü : 5.1)', N'103 x 60,2 x 35mm', N'75 gr', CAST(N'2024-09-26T18:02:01.670' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[Mouse] OFF
GO
SET IDENTITY_INSERT [dbo].[RAM] ON 

INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (1, 16, N'16 GB (2X8GB) DDR4 1.2V 3200MHz SODIMM', CAST(N'2024-09-22T12:20:30.007' AS DateTime), 1)
INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (2, 17, N'32 GB (2X16GB) DDR5 1.1V 4800MHz SODIMM', CAST(N'2024-09-22T12:21:52.713' AS DateTime), 1)
INSERT [dbo].[RAM] ([Id], [BellekId], [BellekAdi], [CreatedAt], [CreatedBy]) VALUES (3, 17, N'32 GB (2X16GB) DDR5 1.1V 5600MHz SODIMM', CAST(N'2024-09-22T12:22:52.140' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[RAM] OFF
GO
SET IDENTITY_INSERT [dbo].[SSD] ON 

INSERT [dbo].[SSD] ([Id], [DepolamaId], [DepolamaAdi], [CreatedAt], [CreatedBy]) VALUES (1, 26, N'500GB M.2 SSD PCIe 4.0 x4 (Okuma: 3500 MB/s - Yazma: 1625 MB/s)', CAST(N'2024-09-15T20:16:50.613' AS DateTime), 1)
INSERT [dbo].[SSD] ([Id], [DepolamaId], [DepolamaAdi], [CreatedAt], [CreatedBy]) VALUES (2, 27, N'1TB M.2 SSD PCIe 4.0 x4 (Okuma: 4125 MB/s - Yazma: 2950 MB/s)', CAST(N'2024-09-15T20:17:33.687' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[SSD] OFF
GO
SET IDENTITY_INSERT [dbo].[UrunResmi] ON 

INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (1, 1, NULL, NULL, N'Laptop_1\1.png', CAST(537780.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:28:00.073' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (2, 1, NULL, NULL, N'Laptop_1\2.png', CAST(954848.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:28:00.100' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (3, 1, NULL, NULL, N'Laptop_1\3.png', CAST(747977.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:28:00.107' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (4, 1, NULL, NULL, N'Laptop_1\4.png', CAST(386175.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:28:00.110' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (5, 2, NULL, NULL, N'Laptop_2\1.png', CAST(814734.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:38:05.260' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (6, 2, NULL, NULL, N'Laptop_2\2.png', CAST(1138920.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:38:05.267' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (7, 2, NULL, NULL, N'Laptop_2\3.png', CAST(819522.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:38:05.270' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (8, 2, NULL, NULL, N'Laptop_2\4.png', CAST(588014.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:38:05.273' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (9, 3, NULL, NULL, N'Laptop_3\1.png', CAST(569288.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:43:02.677' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (10, 3, NULL, NULL, N'Laptop_3\2.png', CAST(1250954.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:43:02.687' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (11, 3, NULL, NULL, N'Laptop_3\3.png', CAST(810361.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:43:02.690' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (12, 3, NULL, NULL, N'Laptop_3\4.png', CAST(666603.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-20T20:43:02.693' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (14, NULL, 1, NULL, N'Monitor_1\1.png', CAST(907187.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T17:42:05.753' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (15, NULL, 1, NULL, N'Monitor_1\2.png', CAST(652844.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T17:42:05.777' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (16, NULL, 1, NULL, N'Monitor_1\3.png', CAST(237098.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T17:42:05.780' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (17, NULL, 1, NULL, N'Monitor_1\4.png', CAST(378220.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T17:42:05.783' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (18, NULL, 2, NULL, N'Monitor_2\1.png', CAST(757220.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T18:07:16.513' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (19, NULL, 2, NULL, N'Monitor_2\2.png', CAST(753794.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T18:07:16.517' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (20, NULL, 2, NULL, N'Monitor_2\3.png', CAST(141283.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T18:07:16.520' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (21, NULL, 2, NULL, N'Monitor_2\4.png', CAST(283748.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-22T18:07:16.523' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (27, NULL, NULL, 1, N'Mouse_1\1.png', CAST(721221.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T17:55:42.310' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (28, NULL, NULL, 1, N'Mouse_1\2.png', CAST(495039.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T17:55:42.313' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (29, NULL, NULL, 1, N'Mouse_1\3.png', CAST(309718.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T17:55:42.317' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (30, NULL, NULL, 1, N'Mouse_1\4.png', CAST(1087607.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T17:55:42.320' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (31, NULL, NULL, 2, N'Mouse_2\1.png', CAST(517155.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T18:03:53.550' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (32, NULL, NULL, 2, N'Mouse_2\2.png', CAST(569076.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T18:03:53.557' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (33, NULL, NULL, 2, N'Mouse_2\3.png', CAST(689924.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T18:03:53.557' AS DateTime), 1)
INSERT [dbo].[UrunResmi] ([Id], [LaptopId], [MonitorId], [MouseId], [ResimUrl], [ResimBoyutu], [ResimTuru], [CreatedAt], [CreatedBy]) VALUES (34, NULL, NULL, 2, N'Mouse_2\4.png', CAST(898371.00 AS Decimal(18, 2)), N'image/png', CAST(N'2024-09-26T18:03:53.560' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[UrunResmi] OFF
GO
SET IDENTITY_INSERT [dbo].[UrunTakip] ON 

INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (24, 2, NULL, NULL, 1, 1, 0, NULL, CAST(N'2024-10-13T16:49:29.593' AS DateTime), 20, CAST(N'2024-10-13T16:50:36.123' AS DateTime), 20)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (25, NULL, NULL, 1, 1, NULL, 0, NULL, CAST(N'2024-10-13T16:49:32.313' AS DateTime), 20, CAST(N'2024-10-13T16:49:39.800' AS DateTime), 20)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (26, 1, NULL, NULL, 1, 1, 0, NULL, CAST(N'2024-10-13T16:49:44.853' AS DateTime), 20, CAST(N'2024-10-13T16:50:36.117' AS DateTime), 20)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (27, 1, NULL, NULL, 1, NULL, NULL, 3, CAST(N'2024-10-13T16:50:36.117' AS DateTime), 20, CAST(N'2024-10-13T16:50:41.873' AS DateTime), 1)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (28, 2, NULL, NULL, 1, NULL, NULL, 2, CAST(N'2024-10-13T16:50:36.123' AS DateTime), 20, CAST(N'2024-10-13T16:50:40.573' AS DateTime), 1)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (29, NULL, 2, NULL, 2, NULL, 0, NULL, CAST(N'2024-10-13T16:51:03.337' AS DateTime), 20, CAST(N'2024-10-13T16:51:19.807' AS DateTime), 20)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (30, NULL, 2, NULL, 2, NULL, NULL, 1, CAST(N'2024-10-13T16:51:19.810' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (31, NULL, NULL, 2, NULL, 1, NULL, NULL, CAST(N'2024-10-13T16:59:47.670' AS DateTime), 20, NULL, NULL)
INSERT [dbo].[UrunTakip] ([Id], [LaptopId], [MonitorId], [MouseId], [Adet], [Favori], [SepetDurum], [SiparisDurum], [CreatedAt], [CreatedBy], [UpdatedAt], [UpdatedBy]) VALUES (32, NULL, 1, NULL, 1, 1, 1, NULL, CAST(N'2024-10-17T16:30:26.867' AS DateTime), 20, CAST(N'2024-10-17T16:30:31.440' AS DateTime), 20)
SET IDENTITY_INSERT [dbo].[UrunTakip] OFF
GO
SET IDENTITY_INSERT [dbo].[UrunYorum] ON 

INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (7, 1, NULL, NULL, N'Ürün çok güzel paketlenmiş. Dış koli zaten koruma yapmış. İçindekibilgisayarın kutusu köpüğü çok iyi ve düzgün bir şekilde geldi. Flashbellek, garanti belgeleri ve monster çantası harika. Teşekkür ederim.Oğlum çok mutlu oldu.', 4, CAST(N'2024-10-04T20:04:11.377' AS DateTime), 20)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (8, 1, NULL, NULL, N'Kurulumu nasıl yapcaz bakalım', 2, CAST(N'2024-10-04T20:05:13.500' AS DateTime), 31)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (9, NULL, 1, NULL, N'Sağlam bir şekilde elime ulaştı daha açılımını yapmadım şu anda her şeygüzel görünüyor zamanla iyi mi kötü mü göreceğiz, teşekkürler.', 4, CAST(N'2024-10-04T20:28:58.637' AS DateTime), 31)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (10, NULL, 1, NULL, N'Hızlı geldi çok güzel kutulamışlardı', 5, CAST(N'2024-10-04T20:33:29.840' AS DateTime), 20)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (15, NULL, 2, NULL, N'Kargo hasarsız ve hızlı bir şekilde elimize ulaştı', 4, CAST(N'2024-10-04T20:51:26.700' AS DateTime), 20)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (16, NULL, NULL, 2, N'Sağlam bir şekilde elime ulaştı daha açılımını yapmadım şu anda her şeygüzel görünüyor zamanla iyi mi kötü mü göreceğiz, teşekkürler.', 3, CAST(N'2024-10-07T19:51:06.897' AS DateTime), 31)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (17, 3, NULL, NULL, N'gerçekten çok kaliteli bir ürün herkese tavsiye ederim', 3, CAST(N'2024-10-13T16:55:54.063' AS DateTime), 20)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (18, NULL, 2, NULL, N'Tam bir fiyat performans ürünü tavsiye ederim', 4, CAST(N'2024-10-13T16:56:43.947' AS DateTime), 20)
INSERT [dbo].[UrunYorum] ([Id], [LaptopId], [MonitorId], [MouseId], [Yorum], [YorumPuan], [CreatedAt], [CreatedBy]) VALUES (19, NULL, NULL, 1, N'eh işte idare eder', 1, CAST(N'2024-10-13T16:59:06.383' AS DateTime), 20)
SET IDENTITY_INSERT [dbo].[UrunYorum] OFF
GO
SET IDENTITY_INSERT [dbo].[YenilemeHizi] ON 

INSERT [dbo].[YenilemeHizi] ([Id], [YenilemeHiziId], [YenilemeHiziAdi], [CreatedAt], [CreatedBy]) VALUES (1, 20, N'144Hz IPS Mat LED Ekran', CAST(N'2024-09-16T14:58:18.187' AS DateTime), 1)
INSERT [dbo].[YenilemeHizi] ([Id], [YenilemeHiziId], [YenilemeHiziAdi], [CreatedAt], [CreatedBy]) VALUES (2, 21, N'165Hz IPS Mat LED Ekran', CAST(N'2024-09-16T14:58:53.873' AS DateTime), 1)
INSERT [dbo].[YenilemeHizi] ([Id], [YenilemeHiziId], [YenilemeHiziAdi], [CreatedAt], [CreatedBy]) VALUES (4, 22, N'180Hz FAST IPS Mat Flat Ekran', CAST(N'2024-09-22T17:42:47.840' AS DateTime), 1)
INSERT [dbo].[YenilemeHizi] ([Id], [YenilemeHiziId], [YenilemeHiziAdi], [CreatedAt], [CreatedBy]) VALUES (5, 22, N'180Hz VA Parlamaz Geniş Ekran', CAST(N'2024-09-22T17:43:15.037' AS DateTime), 1)
SET IDENTITY_INSERT [dbo].[YenilemeHizi] OFF
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
ALTER TABLE [dbo].[Hakkimizda]  WITH CHECK ADD  CONSTRAINT [FK_Hakkimizda_Kullanici] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Hakkimizda] CHECK CONSTRAINT [FK_Hakkimizda_Kullanici]
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
ALTER TABLE [dbo].[Mouse]  WITH CHECK ADD  CONSTRAINT [FK_Mouse_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[Mouse] CHECK CONSTRAINT [FK_Mouse_Kullanici]
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
ALTER TABLE [dbo].[UrunResmi]  WITH CHECK ADD  CONSTRAINT [FK_UrunResmi_Mouse] FOREIGN KEY([MouseId])
REFERENCES [dbo].[Mouse] ([Id])
GO
ALTER TABLE [dbo].[UrunResmi] CHECK CONSTRAINT [FK_UrunResmi_Mouse]
GO
ALTER TABLE [dbo].[UrunTakip]  WITH CHECK ADD  CONSTRAINT [FK_UrunTakip_Kullanici] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[UrunTakip] CHECK CONSTRAINT [FK_UrunTakip_Kullanici]
GO
ALTER TABLE [dbo].[UrunTakip]  WITH CHECK ADD  CONSTRAINT [FK_UrunTakip_Kullanici1] FOREIGN KEY([UpdatedBy])
REFERENCES [dbo].[Kullanici] ([Id])
GO
ALTER TABLE [dbo].[UrunTakip] CHECK CONSTRAINT [FK_UrunTakip_Kullanici1]
GO
ALTER TABLE [dbo].[UrunTakip]  WITH CHECK ADD  CONSTRAINT [FK_UrunTakip_Laptop] FOREIGN KEY([LaptopId])
REFERENCES [dbo].[Laptop] ([Id])
GO
ALTER TABLE [dbo].[UrunTakip] CHECK CONSTRAINT [FK_UrunTakip_Laptop]
GO
ALTER TABLE [dbo].[UrunTakip]  WITH CHECK ADD  CONSTRAINT [FK_UrunTakip_Monitor] FOREIGN KEY([MonitorId])
REFERENCES [dbo].[Monitor] ([Id])
GO
ALTER TABLE [dbo].[UrunTakip] CHECK CONSTRAINT [FK_UrunTakip_Monitor]
GO
ALTER TABLE [dbo].[UrunTakip]  WITH CHECK ADD  CONSTRAINT [FK_UrunTakip_Mouse] FOREIGN KEY([MouseId])
REFERENCES [dbo].[Mouse] ([Id])
GO
ALTER TABLE [dbo].[UrunTakip] CHECK CONSTRAINT [FK_UrunTakip_Mouse]
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
ALTER TABLE [dbo].[UrunYorum]  WITH CHECK ADD  CONSTRAINT [FK_UrunYorum_Mouse] FOREIGN KEY([MouseId])
REFERENCES [dbo].[Mouse] ([Id])
GO
ALTER TABLE [dbo].[UrunYorum] CHECK CONSTRAINT [FK_UrunYorum_Mouse]
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
