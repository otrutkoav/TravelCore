using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Transfers.Transfers.Commands;
using TourCore.Application.Transfers.Transfers.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transfers.Transfers;

namespace TourCore.Api.Legacy.Controllers.Transfers
{
    /// <summary>
    /// Контроллер для работы с трансферами.
    /// </summary>
    [RoutePrefix("api/transfers")]
    public class TransfersController : ApiController
    {
        private readonly ICommandHandler<CreateTransferCommand, TransferDto> _createHandler;
        private readonly ICommandHandler<UpdateTransferCommand, TransferDto> _updateHandler;
        private readonly IQueryHandler<GetTransfersQuery, PagedResponseDto<TransferListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetTransferByIdQuery, TransferDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера трансферов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания трансфера.</param>
        /// <param name="updateHandler">Обработчик обновления трансфера.</param>
        /// <param name="getListHandler">Обработчик получения списка трансферов.</param>
        /// <param name="getByIdHandler">Обработчик получения трансфера по идентификатору.</param>
        public TransfersController(
            ICommandHandler<CreateTransferCommand, TransferDto> createHandler,
            ICommandHandler<UpdateTransferCommand, TransferDto> updateHandler,
            IQueryHandler<GetTransfersQuery, PagedResponseDto<TransferListItemDto>> getListHandler,
            IQueryHandler<GetTransferByIdQuery, TransferDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список трансферов.
        /// </summary>
        /// <remarks>
        /// Возвращает список вариантов трансфера с поддержкой фильтрации,
        /// пагинации и сортировки.
        ///
        /// Трансфер описывает маршрут или услугу перемещения:
        /// место отправления, место прибытия, длительность,
        /// привязку к городу и направлению трансфера.
        ///
        /// Используется как справочник при формировании туров,
        /// дополнительных услуг и логистических блоков.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию трансфера.
        /// - query.filter.cityId — фильтр по городу.
        /// - query.filter.directionId — фильтр по направлению трансфера.
        /// - query.filter.isMain — фильтр по признаку основного трансфера.
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
        /// - timeFrom
        /// - timeTo
        /// - durationText
        /// - placeFrom
        /// - placeTo
        /// - isMain
        /// - cityId
        /// - directionId
        /// - showOrder
        /// - autoApplyFrom
        /// - autoApplyTo
        /// </remarks>
        /// <param name="query">
        /// Параметры фильтрации, пагинации и сортировки списка трансферов.
        /// </param>
        /// <returns>Страница списка трансферов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetTransfersQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetTransfersQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить трансфер по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного трансфера по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования трансфера.
        /// </remarks>
        /// <param name="id">Идентификатор трансфера.</param>
        /// <returns>Данные трансфера.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetTransferByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать трансфер.
        /// </summary>
        /// <remarks>
        /// Создает новый вариант трансфера.
        ///
        /// Можно указать название, время, длительность,
        /// место отправления и прибытия, город, направление,
        /// ссылку, порядок отображения и признаки автоприменения.
        /// </remarks>
        /// <param name="command">Данные нового трансфера.</param>
        /// <returns>Созданный трансфер.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateTransferCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить трансфер.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры существующего трансфера.
        ///
        /// Изменяет название, время, длительность, места отправления и прибытия,
        /// город, направление, ссылку, порядок отображения и признаки автоприменения.
        /// </remarks>
        /// <param name="id">Идентификатор трансфера.</param>
        /// <param name="command">Новые данные трансфера.</param>
        /// <returns>Обновленный трансфер.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateTransferCommand command)
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