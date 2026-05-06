using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Transportation.Transports.Commands;
using TourCore.Application.Transportation.Transports.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transportation.Transports;

namespace TourCore.Api.Legacy.Controllers.Transportation
{
    /// <summary>
    /// Контроллер для работы с транспортом.
    /// </summary>
    [RoutePrefix("api/transportation/transports")]
    public class TransportsController : ApiController
    {
        private readonly ICommandHandler<CreateTransportCommand, TransportDto> _createHandler;
        private readonly ICommandHandler<UpdateTransportCommand, TransportDto> _updateHandler;
        private readonly IQueryHandler<GetTransportsQuery, PagedResponseDto<TransportListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetTransportByIdQuery, TransportDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера транспорта.
        /// </summary>
        /// <param name="createHandler">Обработчик создания транспорта.</param>
        /// <param name="updateHandler">Обработчик обновления транспорта.</param>
        /// <param name="getListHandler">Обработчик получения списка транспорта.</param>
        /// <param name="getByIdHandler">Обработчик получения транспорта по идентификатору.</param>
        public TransportsController(
            ICommandHandler<CreateTransportCommand, TransportDto> createHandler,
            ICommandHandler<UpdateTransportCommand, TransportDto> updateHandler,
            IQueryHandler<GetTransportsQuery, PagedResponseDto<TransportListItemDto>> getListHandler,
            IQueryHandler<GetTransportByIdQuery, TransportDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список транспорта.
        /// </summary>
        /// <remarks>
        /// Возвращает справочник видов транспорта с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Используется для выбора типа транспорта в маршрутах,
        /// трансферах и других логистических сущностях.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию и английскому названию транспорта.
        ///
        /// Пагинация:
        /// - query.page — номер страницы. Минимальное значение: 1. По умолчанию: 1.
        /// - query.pageSize — размер страницы. По умолчанию: 20. Максимальное значение: 100.
        ///
        /// Сортировка:
        /// - query.sortBy — поле сортировки.
        /// - query.sortDirection — направление сортировки:
        ///   0 — по возрастанию,
        ///   1 — по убыванию.
        ///
        /// Допустимые значения query.sortBy:
        /// - id
        /// - name
        /// - nameEn
        /// - seatsCount
        /// - showOrder
        /// </remarks>
        /// <param name="query">
        /// Параметры фильтрации, пагинации и сортировки списка транспорта.
        /// </param>
        /// <returns>Страница списка транспорта.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetTransportsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetTransportsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить транспорт по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного вида транспорта.
        /// Используется при редактировании или просмотре.
        /// </remarks>
        /// <param name="id">Идентификатор транспорта.</param>
        /// <returns>Данные транспорта.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetTransportByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать транспорт.
        /// </summary>
        /// <remarks>
        /// Создает новый вид транспорта.
        /// Может содержать название, вместимость и порядок отображения.
        /// </remarks>
        /// <param name="command">Данные нового транспорта.</param>
        /// <returns>Созданный транспорт.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateTransportCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить транспорт.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующего транспорта.
        /// </remarks>
        /// <param name="id">Идентификатор транспорта.</param>
        /// <param name="command">Новые данные транспорта.</param>
        /// <returns>Обновленный транспорт.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateTransportCommand command)
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