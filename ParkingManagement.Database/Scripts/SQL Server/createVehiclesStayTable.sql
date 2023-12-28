USE [ParkingManagement]
GO

SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[VehiclesStay](
	[Id] [nvarchar](50) NOT NULL,
	[LicensePlate] [nvarchar](50) NOT NULL,
	[EntryTime] [datetime] NOT NULL,
	[ExitTime] [datetime] NULL,
 CONSTRAINT [PK_VehiclesStay] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VehiclesStay]  WITH CHECK ADD  CONSTRAINT [FK_VehiclesStay_Vehicles] FOREIGN KEY([LicensePlate])
REFERENCES [dbo].[Vehicles] ([LicensePlate])
GO

ALTER TABLE [dbo].[VehiclesStay] CHECK CONSTRAINT [FK_VehiclesStay_Vehicles]
GO
