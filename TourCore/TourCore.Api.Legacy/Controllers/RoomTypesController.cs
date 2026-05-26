using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.RoomTypes.Commands;
using TourCore.Application.Hotels.RoomTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Hotels.RoomTypes;

namespace TourCore.Api.Legacy.Controllers.Hotels
{
    /// <summary>
    /// Контроллер для работы с типами номеров.
    /// </summary>
    [RoutePrefix("api/hotels/room-types")]
    public class RoomTypesController : ApiController
    {
        private readonly ICommandHandler<CreateRoomTypeCommand, RoomTypeDto> _createHandler;
        private readonly ICommandHandler<UpdateRoomTypeCommand, RoomTypeDto> _updateHandler;
        private readonly IQueryHandler<GetRoomTypesQuery, PagedResponseDto<RoomTypeListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера типов номеров.
        /// </summary>
        /// <param name="createHandler">Обработчик создания типа номера.</param>
        /// <param name="updateHandler">Обработчик обновления типа номера.</param>
        /// <param name="getListHandler">Обработчик получения списка типов номеров.</param>
        /// <param name="getByIdHandler">Обработчик получения типа номера по идентификатору.</param>
        public RoomTypesController(
            ICommandHandler<CreateRoomTypeCommand, RoomTypeDto> createHandler,
            ICommandHandler<UpdateRoomTypeCommand, RoomTypeDto> updateHandler,
            IQueryHandler<GetRoomTypesQuery, PagedResponseDto<RoomTypeListItemDto>> getListHandler,
            IQueryHandler<GetRoomTypeByIdQuery, RoomTypeDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список типов номеров.
        /// </summary>
        /// <remarks>
        /// Возвращает список типов номеров из справочника с поддержкой фильтрации, пагинации и сортировки.
        /// Используется в карточке отеля, настройках номеров, ценах и фильтрах поиска.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по коду, названию и английскому названию типа номера.
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
        /// - nameEn
        /// - places
        /// - extraPlaces
        /// - sortOrder
        /// - description
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка типов номеров.</param>
        /// <returns>Страница списка типов номеров.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetRoomTypesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetRoomTypesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить тип номера по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного типа номера по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования типа номера.
        /// </remarks>
        /// <param name="id">Идентификатор типа номера.</param>
        /// <returns>Данные типа номера.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRoomTypeByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый тип номера.
        /// </summary>
        /// <remarks>
        /// Добавляет новый тип номера в справочник.
        /// Код типа номера должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные нового типа номера.</param>
        /// <returns>Созданный тип номера.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRoomTypeCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий тип номера.
        /// </summary>
        /// <remarks>
        /// Изменяет данные типа номера по указанному идентификатору.
        /// Используется для редактирования справочника типов номеров.
        /// Код типа номера должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор типа номера.</param>
        /// <param name="command">Новые данные типа номера.</param>
        /// <returns>Обновленный тип номера.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRoomTypeCommand command)
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