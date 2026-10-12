using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Разрешённый справочный отель для шаблона проживания.</summary>
    /// <remarks>HotelId хранится без внешнего ключа на таблицу справочника отелей.</remarks>
    public class TourHotelAllowedHotel : Entity
    {
        public int TourHotelId { get; protected set; }
        public virtual TourHotel TourHotel { get; protected set; }
        public int HotelId { get; protected set; }

        protected TourHotelAllowedHotel() { }

        internal TourHotelAllowedHotel(int hotelId)
        {
            if (hotelId <= 0)
                throw new DomainException("Hotel identifier must be greater than zero.");

            HotelId = hotelId;
        }
    }
}
