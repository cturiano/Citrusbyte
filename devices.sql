CREATE TABLE [dbo].[devices] (
    [Id]               INT              IDENTITY (1, 1) NOT NULL,
    [Firmware_Version] NVARCHAR (MAX)   NOT NULL,
    [RegistrationDate] BIGINT           NOT NULL,
    [Serial]           UNIQUEIDENTIFIER NOT NULL,
    CONSTRAINT [PK_dbo.devices] PRIMARY KEY CLUSTERED ([Id] ASC)
);

GO
CREATE UNIQUE NONCLUSTERED INDEX [IX_Serial]
    ON [dbo].[devices]([Serial] ASC);
	
GO
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('A-12345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())
	
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('B-22345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())
	
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('C-32345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())
	
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('D-42345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())
	
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('E-52345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())
	
INSERT INTO [dbo].[devices] (Firmware_Version, RegistrationDate, Serial)
	VALUES('F-62345', DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, NEWID())

GO