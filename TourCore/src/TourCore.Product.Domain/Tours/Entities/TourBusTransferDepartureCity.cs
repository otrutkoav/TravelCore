using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferDepartureCity : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int CityId { get; protected set; }

        protected TourBusTransferDepartureCity() { }

        internal TourBusTransferDepartureCity(int cityId)
        {
            if (cityId <= 0)
                throw new DomainException("Departure city identifier must be positive.");
            CityId = cityId;
        }
    }
}
