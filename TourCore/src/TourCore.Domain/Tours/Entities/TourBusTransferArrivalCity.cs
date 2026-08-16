using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferArrivalCity : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int CityId { get; protected set; }

        protected TourBusTransferArrivalCity() { }

        internal TourBusTransferArrivalCity(int cityId)
        {
            if (cityId <= 0)
                throw new DomainException("Arrival city identifier must be positive.");
            CityId = cityId;
        }
    }
}
