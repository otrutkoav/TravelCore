using System;
using System.Collections.Generic;

namespace TourCore.Search.Contracts
{
    /// <summary>Найденная комбинация дат, отеля, транспорта и трансферов.</summary>
    public sealed class TourCandidate
    {
        /// <summary>Детерминированный идентификатор кандидата в рамках текущей модели поиска.</summary>
        public string CandidateId { get; set; }
        /// <summary>Дата начала поездки.</summary>
        public DateTime StartDate { get; set; }
        /// <summary>Дата окончания поездки.</summary>
        public DateTime EndDate { get; set; }
        /// <summary>Продолжительность поездки в ночах.</summary>
        public int Nights { get; set; }
        /// <summary>Количество взрослых туристов.</summary>
        public int Adults { get; set; }
        /// <summary>Страна и город назначения.</summary>
        public SearchDestination Destination { get; set; }
        /// <summary>Отель назначения без комнаты и типа питания.</summary>
        public SearchHotel Hotel { get; set; }
        /// <summary>Транспорт туда и обратно. Null в режиме поиска без транспорта.</summary>
        public RoundTripTransport Transport { get; set; }
        /// <summary>Доступные трансферы города назначения.</summary>
        public IReadOnlyCollection<SearchTransfer> Transfers { get; set; }
    }
}
