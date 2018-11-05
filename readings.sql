CREATE TABLE [dbo].[readings] (
    [Id]          INT            IDENTITY (1, 1) NOT NULL,
    [CO]          FLOAT (53)     NOT NULL,
    [Humidity]    FLOAT (53)     NOT NULL,
    [OwnerId]     INT            NOT NULL,
    [ReadingTime] BIGINT         NOT NULL,
    [Status]      NVARCHAR (MAX) NOT NULL,
    [Temp]        FLOAT (53)     NOT NULL,
    CONSTRAINT [PK_dbo.readings] PRIMARY KEY CLUSTERED ([Id] ASC),
    CONSTRAINT [FK_dbo.readings_dbo.devices_OwnerId] FOREIGN KEY ([OwnerId]) REFERENCES [dbo].[devices] ([Id])
);

GO
CREATE NONCLUSTERED INDEX [IX_OwnerId]
    ON [dbo].[readings]([OwnerId] ASC);

GO
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(1, 1, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 1)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(2, 2, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 2)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(3, 3, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 3)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(4, 4, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 4)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(5, 5, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 5)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(6, 6, 1, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 6)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(1, 1, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 1)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(2, 2, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 2)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(3, 3, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 3)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(4, 4, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 4)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(5, 5, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 5)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(6, 6, 2, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 6)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(1, 1, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 1)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(2, 2, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 2)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(3, 3, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 3)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(4, 4, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 4)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(5, 5, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 5)
	
INSERT INTO [dbo].[readings] ([CO], [Humidity], [OwnerId], [ReadingTime], [Status], [Temp])
	VALUES(6, 6, 3, DATEDIFF_BIG( microsecond, '00010101', GETUTCDATE() ) * 10 + ( DATEPART( NANOSECOND, GETUTCDATE() ) % 1000 ) / 100, 'good', 6)

GO