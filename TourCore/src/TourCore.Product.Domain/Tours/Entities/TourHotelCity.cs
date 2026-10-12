using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Допустимый справочный город для шаблона проживания.</summary>
    /// <remarks>CityId хранится без внешнего ключа на таблицу справочника городов.</remarks>
    public class TourHotelCity : Entity
    {
        public int TourHotelId { get; protected set; }
        public virtual TourHotel TourHotel { get; protected set; }
        public int CityId { get; protected set; }

        protected TourHotelCity() { }

        internal TourHotelCity(int cityId)
        {
            if (cityId <= 0)
                throw new DomainException("City identifier must be greater than zero.");

            CityId = cityId;
        }
    }
}
