namespace TourCore.Domain.Services.Enums
{
    public enum ServiceTypeEnum
    {
        /// <summary>
        /// Авиаперелет
        /// </summary>
        Flight = 1,

        /// <summary>
        /// Трансфер
        /// </summary>
        Transfer = 2,

        /// <summary>
        /// Проживание (отель / круиз)
        /// </summary>
        Hotel = 3,

        /// <summary>
        /// Экскурсия
        /// </summary>
        Excursion = 4,

        /// <summary>
        /// Виза
        /// </summary>
        Visa = 5,

        /// <summary>
        /// Страховка
        /// </summary>
        Insurance = 6,

        /// <summary>
        /// Паром / круиз
        /// </summary>
        Cruise = 7,

        /// <summary>
        /// Доп. услуги в отеле
        /// </summary>
        AddHotel = 8,

        /// <summary>
        /// Доп. услуги на пароме
        /// </summary>
        AddCruise = 9,

        /// <summary>
        /// Штраф
        /// </summary>
        Penalty = 10,

        /// <summary>
        /// Коррекция цены
        /// </summary>
        PriceUpdate = 11,

        /// <summary>
        /// Доплаты к авиаперелетам
        /// </summary>
        AddCostForFlight = 12,

        /// <summary>
        /// Доплаты к отелям
        /// </summary>
        AddCostForHotel = 13,

        /// <summary>
        /// Автобусные переезды
        /// </summary>
        BusTransfer = 14,

        /// <summary>
        /// ЖД переезды
        /// </summary>
        RailwayTransfer = 15,

        /// <summary>
        /// Пользовательская услуга
        /// </summary>
        Custom = 1000
    }
}