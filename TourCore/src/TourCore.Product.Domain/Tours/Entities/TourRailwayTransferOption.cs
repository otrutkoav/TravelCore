using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Разрешённая запись внешнего справочника RailwayTransfers.</summary>
    public class TourRailwayTransferOption : Entity
    {
        public int TourRailwayTransferId { get; protected set; }
        public virtual TourRailwayTransfer TourRailwayTransfer { get; protected set; }
        public int RailwayTransferId { get; protected set; }

        protected TourRailwayTransferOption() { }

        internal TourRailwayTransferOption(int id)
        {
            if (id <= 0)
                throw new DomainException("Railway transfer identifier must be positive.");
            RailwayTransferId = id;
        }
    }
}
