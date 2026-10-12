using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourRailwayTransferDepartureCity : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int CityId { get; protected set; }

        protected TourRailwayTransferDepartureCity() { }

        internal TourRailwayTransferDepartureCity(int id)
        {
            if (id <= 0)
                throw new DomainException("Departure city identifier must be positive.");
            CityId = id;
        }
    }
}
