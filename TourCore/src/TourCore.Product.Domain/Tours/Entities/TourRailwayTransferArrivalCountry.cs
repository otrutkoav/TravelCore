using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourRailwayTransferArrivalCountry : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int CountryId { get; protected set; }

        protected TourRailwayTransferArrivalCountry() { }

        internal TourRailwayTransferArrivalCountry(int id)
        {
            if (id <= 0)
                throw new DomainException("Arrival country identifier must be positive.");
            CountryId = id;
        }
    }
}
