using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Transfers.TransferDirections.Commands;
using TourCore.Application.Transfers.TransferDirections.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Transfers.TransferDirections;

namespace TourCore.Api.Legacy.Controllers.Transfers
{
    /// <summary>
    /// Контроллер для работы с направлениями трансферов.
    /// </summary>
    [RoutePrefix("api/transfers/directions")]
    public class TransferDirectionsController : ApiController
    {
        private readonly ICommandHandler<CreateTransferDirectionCommand, TransferDirectionDto> _createHandler;
        private readonly ICommandHandler<UpdateTransferDirectionCommand, TransferDirectionDto> _updateHandler;
        private readonly IQueryHandler<GetTransferDirectionsQuery, PagedResponseDto<TransferDirectionListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetTransferDirectionByIdQuery, TransferDirectionDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера направлений трансферов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания направления трансфера.</param>
        /// <param name="updateHandler">Обработчик обновления направления трансфера.</param>
        /// <param name="getListHandler">Обработчик получения списка направлений трансферов.</param>
        /// <param name="getByIdHandler">Обработчик получения направления трансфера по идентификатору.</param>
        public TransferDirectionsController(
            ICommandHandler<CreateTransferDirectionCommand, TransferDirectionDto> createHandler,
            ICommandHandler<UpdateTransferDirectionCommand, TransferDirectionDto> updateHandler,
            IQueryHandler<GetTransferDirectionsQuery, PagedResponseDto<TransferDirectionListItemDto>> getListHandler,
            IQueryHandler<GetTransferDirectionByIdQuery, TransferDirectionDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список направлений трансферов.
        /// </summary>
        /// <remarks>
        /// Возвращает справочник направлений трансферов
        /// с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Используется при настройке вариантов трансфера.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию направления трансфера.
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
        /// </remarks>
        /// <param name="query">
        /// Параметры фильтрации, пагинации и сортировки списка направлений трансферов.
        /// </param>
        /// <returns>Страница списка направлений трансферов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetTransferDirectionsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetTransferDirectionsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить направление трансфера по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного направления трансфера по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования направления.
        /// </remarks>
        /// <param name="id">Идентификатор направления трансфера.</param>
        /// <returns>Данные направления трансфера.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetTransferDirectionByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать направление трансфера.
        /// </summary>
        /// <remarks>
        /// Создает новое направление трансфера.
        /// Направление используется как справочник при создании вариантов трансфера.
        /// </remarks>
        /// <param name="command">Данные нового направления трансфера.</param>
        /// <returns>Созданное направление трансфера.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateTransferDirectionCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить направление трансфера.
        /// </summary>
        /// <remarks>
        /// Обновляет название существующего направления трансфера.
        /// </remarks>
        /// <param name="id">Идентификатор направления трансфера.</param>
        /// <param name="command">Новые данные направления трансфера.</param>
        /// <returns>Обновленное направление трансфера.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateTransferDirectionCommand command)
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