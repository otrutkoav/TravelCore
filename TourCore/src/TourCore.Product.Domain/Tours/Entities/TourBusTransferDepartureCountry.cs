using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourBusTransferDepartureCountry : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int CountryId { get; protected set; }

        protected TourBusTransferDepartureCountry() { }

        internal TourBusTransferDepartureCountry(int countryId)
        {
            if (countryId <= 0)
                throw new DomainException("Departure country identifier must be positive.");
            CountryId = countryId;
        }
    }
}
