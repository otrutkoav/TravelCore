using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Допустимая продолжительность шаблона проживания в ночах.</summary>
    public class TourHotelDuration : Entity
    {
        public int TourHotelId { get; protected set; }
        public virtual TourHotel TourHotel { get; protected set; }
        public int Nights { get; protected set; }

        protected TourHotelDuration() { }

        internal TourHotelDuration(int nights)
        {
            if (nights <= 0)
                throw new DomainException("Hotel duration must be greater than zero.");

            Nights = nights;
        }
    }
}
