namespace TourCore.Application.Avia.AirClasses.DTOs
{
    public class AirClassListFilter
    {
        /// <summary>
        /// Поиск по коду, названию и английскому названию класса обслуживания.
        /// </summary>
        public string Search { get; set; }

        /// <summary>
        /// Поиск по группе класса обслуживания.
        /// </summary>
        public string Group { get; set; }
    }
}