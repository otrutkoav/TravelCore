namespace TourCore.Infrastructure.SqlServer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedStandardServiceTypes : DbMigration
    {
        public override void Up()
        {
            Sql(@"
SET IDENTITY_INSERT [dbo].[ServiceTypes] ON;
INSERT INTO [dbo].[ServiceTypes]
([Id],[Name],[NameEn],[Code],[IsDuration],[IsRoute],[CreatedAt],[UpdatedAt]) VALUES
(1,N'Авиаперелет',N'Flight',N'А_П',0,1,GETUTCDATE(),NULL),
(2,N'Трансфер',N'Transfer',NULL,0,1,GETUTCDATE(),NULL),
(3,N'Отель/Круиз',N'Hotel/Cruise',N'HOTEL',1,1,GETUTCDATE(),NULL),
(4,N'Экскурсия',N'Excursion',NULL,0,0,GETUTCDATE(),NULL),
(5,N'Виза',N'Visa',NULL,1,0,GETUTCDATE(),NULL),
(6,N'Страховка',N'Insurance',NULL,1,0,GETUTCDATE(),NULL),
(7,N'Паром',N'Ship',NULL,1,1,GETUTCDATE(),NULL),
(8,N'Доп услуга в отеле/круизе',N'Add.service in hotel/cruise',N'ADHOT',1,0,GETUTCDATE(),NULL),
(9,N'Доп услуга на пароме',N'Add.service in ship',N'ADSHIP',1,0,GETUTCDATE(),NULL),
(10,N'Штраф',N'en_Штраф',NULL,1,0,GETUTCDATE(),NULL),
(12,N'Доплаты к авиаперелетам',N'Add.service in flight',N'ADFLIGHT',0,0,GETUTCDATE(),NULL),
(13,N'Доплаты к отелям',N'Add.service in hotel',N'ADHOTEL',1,0,GETUTCDATE(),NULL),
(14,N'Автобусный переезд',N'Bus transfer',N'BUS',1,1,GETUTCDATE(),NULL),
(15,N'Железнодорожный переезд',N'Railway transfer',N'TRAIN',1,1,GETUTCDATE(),NULL);
SET IDENTITY_INSERT [dbo].[ServiceTypes] OFF;
DBCC CHECKIDENT ('[dbo].[ServiceTypes]', RESEED, 15);");
        }
        
        public override void Down()
        {
            Sql("DELETE FROM [dbo].[ServiceTypes] WHERE [Id] IN (1,2,3,4,5,6,7,8,9,10,12,13,14,15)");
        }
    }
}
