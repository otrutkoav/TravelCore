using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TourCore.Infrastructure.Postgres.Migrations
{
    /// <inheritdoc />
    public partial class SeedStandardServiceTypes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
INSERT INTO ""ServiceTypes""
(""Id"",""Name"",""NameEn"",""Code"",""IsDuration"",""IsRoute"",""CreatedAt"",""UpdatedAt"") VALUES
(1,'Авиаперелет','Flight','А_П',FALSE,TRUE,NOW(),NULL),
(2,'Трансфер','Transfer',NULL,FALSE,TRUE,NOW(),NULL),
(3,'Отель/Круиз','Hotel/Cruise','HOTEL',TRUE,TRUE,NOW(),NULL),
(4,'Экскурсия','Excursion',NULL,FALSE,FALSE,NOW(),NULL),
(5,'Виза','Visa',NULL,TRUE,FALSE,NOW(),NULL),
(6,'Страховка','Insurance',NULL,TRUE,FALSE,NOW(),NULL),
(7,'Паром','Ship',NULL,TRUE,TRUE,NOW(),NULL),
(8,'Доп услуга в отеле/круизе','Add.service in hotel/cruise','ADHOT',TRUE,FALSE,NOW(),NULL),
(9,'Доп услуга на пароме','Add.service in ship','ADSHIP',TRUE,FALSE,NOW(),NULL),
(10,'Штраф','en_Штраф',NULL,TRUE,FALSE,NOW(),NULL),
(12,'Доплаты к авиаперелетам','Add.service in flight','ADFLIGHT',FALSE,FALSE,NOW(),NULL),
(13,'Доплаты к отелям','Add.service in hotel','ADHOTEL',TRUE,FALSE,NOW(),NULL),
(14,'Автобусный переезд','Bus transfer','BUS',TRUE,TRUE,NOW(),NULL),
(15,'Железнодорожный переезд','Railway transfer','TRAIN',TRUE,TRUE,NOW(),NULL);
SELECT setval(pg_get_serial_sequence('""ServiceTypes""','Id'),15,true);");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DELETE FROM ""ServiceTypes"" WHERE ""Id"" IN (1,2,3,4,5,6,7,8,9,10,12,13,14,15)");
        }
    }
}
