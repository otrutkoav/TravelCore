using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;

namespace TourCore.Domain.Tours
{
    /// <summary>Разрешённая запись внешнего справочника BusTransfers.</summary>
    public class TourBusTransferOption : Entity
    {
        public int TourBusTransferId { get; protected set; }
        public virtual TourBusTransfer TourBusTransfer { get; protected set; }
        public int BusTransferId { get; protected set; }

        protected TourBusTransferOption() { }

        internal TourBusTransferOption(int busTransferId)
        {
            if (busTransferId <= 0)
                throw new DomainException("Bus transfer identifier must be positive.");
            BusTransferId = busTransferId;
        }
    }
}
