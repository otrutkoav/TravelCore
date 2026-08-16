using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourRailwayTransferDepartureCountry : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int CountryId { get; protected set; }

        protected TourRailwayTransferDepartureCountry() { }

        internal TourRailwayTransferDepartureCountry(int id)
        {
            if (id <= 0)
                throw new DomainException("Departure country identifier must be positive.");
            CountryId = id;
        }
    }
}
