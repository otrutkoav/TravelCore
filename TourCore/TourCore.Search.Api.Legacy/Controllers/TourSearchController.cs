using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TourCore.Application.Abstractions;
using TourCore.Search.Contracts;

namespace TourCore.Search.Api.Legacy.Controllers
{
    /// <summary>
    /// Выполняет поиск кандидатов тура.
    /// </summary>
    [RoutePrefix("api/v1/search/tours")]
    public sealed class TourSearchController : ApiController
    {
        private readonly IQueryHandler<TourSearchRequest, TourSearchResponse> _handler;
        /// <summary>Создаёт контроллер поиска туров.</summary>
        /// <param name="handler">Обработчик поискового запроса.</param>
        public TourSearchController(IQueryHandler<TourSearchRequest, TourSearchResponse> handler)
        {
            _handler = handler;
        }

        /// <summary>
        /// Найти кандидаты тура по основным параметрам.
        /// </summary>
        /// <remarks>
        /// Первая версия поиска формирует кандидаты из отеля, транспорта туда и обратно
        /// и доступных трансферов города назначения.
        ///
        /// Правила поиска:
        /// - departureCityId обязателен; значение -1 включает режим без транспорта;
        /// - destinationCountryId обязателен;
        /// - destinationCityId необязателен: null означает все города выбранной страны;
        /// - dates содержит от 1 до 10 дат начала поездки;
        /// - durationsInNights содержит от 1 до 10 значений от 1 до 60 ночей;
        /// - adults содержит количество взрослых от 1 до 20;
        /// - каждая дата комбинируется с каждой продолжительностью;
        /// - поддерживаются пары самолёт+отель+самолёт, автобус+отель+автобус
        ///   и поезд+отель+поезд;
        /// - смешанный транспорт и маршрут только в одну сторону не возвращаются.
        ///
        /// Цена, квоты, комнаты, питание и актуальные фильтры пока не рассчитываются.
        /// Поэтому ответ содержит PriceStatus=NotCalculated и
        /// AvailabilityStatus=NotChecked.
        /// </remarks>
        /// <param name="request">Основные параметры поиска тура.</param>
        /// <returns>Страница найденных кандидатов тура.</returns>
        [HttpPost]
        [Route("")]
        [ResponseType(typeof(TourSearchResponse))]
        public async Task<IHttpActionResult> Search([FromBody] TourSearchRequest request)
        {
            var result = await _handler.Handle(request, CancellationToken.None);
            return Ok(result);
        }
    }
}
