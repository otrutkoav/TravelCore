using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Finance.Rates.Commands;
using TourCore.Application.Finance.Rates.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Finance.Rates;

namespace TourCore.Api.Legacy.Controllers.Finance
{
    /// <summary>
    /// Контроллер для работы с валютами.
    /// </summary>
    [RoutePrefix("api/finance/rates")]
    public class RatesController : ApiController
    {
        private readonly ICommandHandler<CreateRateCommand, RateDto> _createHandler;
        private readonly ICommandHandler<UpdateRateCommand, RateDto> _updateHandler;
        private readonly IQueryHandler<GetRatesQuery, PagedResponseDto<RateListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRateByIdQuery, RateDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера валют.
        /// </summary>
        /// <param name="createHandler">Обработчик создания валюты.</param>
        /// <param name="updateHandler">Обработчик обновления валюты.</param>
        /// <param name="getListHandler">Обработчик получения списка валют.</param>
        /// <param name="getByIdHandler">Обработчик получения валюты по идентификатору.</param>
        public RatesController(
            ICommandHandler<CreateRateCommand, RateDto> createHandler,
            ICommandHandler<UpdateRateCommand, RateDto> updateHandler,
            IQueryHandler<GetRatesQuery, PagedResponseDto<RateListItemDto>> getListHandler,
            IQueryHandler<GetRateByIdQuery, RateDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список валют.
        /// </summary>
        /// <remarks>
        /// Возвращает список валют и расчетных единиц с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по коду, названию и ISO-коду валюты.
        /// - query.filter.isMain — фильтр по признаку основной валюты.
        /// - query.filter.isNational — фильтр по признаку национальной валюты.
        /// - query.filter.showInSearch — фильтр по признаку отображения валюты в поиске.
        ///
        /// Пагинация:
        /// - query.page — номер страницы. Минимальное значение: 1. По умолчанию: 1.
        /// - query.pageSize — размер страницы. По умолчанию: 20. Максимальное значение: 100.
        ///
        /// Сортировка:
        /// - query.sortBy — поле сортировки.
        /// - query.sortDirection — направление сортировки: 0 — по возрастанию, 1 — по убыванию.
        ///
        /// Допустимые значения query.sortBy:
        /// - id
        /// - code
        /// - name
        /// - isoCode
        /// - isMain
        /// - isNational
        /// - showInSearch
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка валют.</param>
        /// <returns>Страница списка валют.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetRatesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetRatesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить валюту по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной валюты по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования валюты.
        /// </remarks>
        /// <param name="id">Идентификатор валюты.</param>
        /// <returns>Данные валюты.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRateByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новую валюту.
        /// </summary>
        /// <remarks>
        /// Добавляет новую валюту или расчетную единицу в справочник.
        /// Код валюты должен быть уникальным.
        /// ISO код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные новой валюты.</param>
        /// <returns>Созданная валюта.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRateCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующую валюту.
        /// </summary>
        /// <remarks>
        /// Изменяет данные валюты по указанному идентификатору.
        /// Код валюты должен быть уникальным.
        /// ISO код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор валюты.</param>
        /// <param name="command">Новые данные валюты.</param>
        /// <returns>Обновленная валюта.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRateCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            command.Id = id;

            var result = await _updateHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }
    }
}