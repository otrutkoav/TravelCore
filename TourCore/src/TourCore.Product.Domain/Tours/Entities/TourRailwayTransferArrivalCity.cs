using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    public class TourRailwayTransferArrivalCity : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int CityId { get; protected set; }

        protected TourRailwayTransferArrivalCity() { }

        internal TourRailwayTransferArrivalCity(int id)
        {
            if (id <= 0)
                throw new DomainException("Arrival city identifier must be positive.");
            CityId = id;
        }
    }
}
