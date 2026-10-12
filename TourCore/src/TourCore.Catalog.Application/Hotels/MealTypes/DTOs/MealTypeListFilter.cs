namespace TourCore.Application.Hotels.MealTypes.DTOs
{
    public class MealTypeListFilter
    {
        /// <summary>
        /// Поиск по названию, английскому названию, коду и глобальному коду типа питания.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Поиск по глобальному коду типа питания.
        /// </summary>
        public string GlobalCode { get; set; }
    }
}