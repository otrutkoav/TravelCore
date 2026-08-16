using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferArrivalCountry : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int CountryId { get; protected set; }

        protected TourBusTransferArrivalCountry() { }

        internal TourBusTransferArrivalCountry(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("Arrival country identifier must be positive.");
            CountryId = countryId;
        }
    }
}
