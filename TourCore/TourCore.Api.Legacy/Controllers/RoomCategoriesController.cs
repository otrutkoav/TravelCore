using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.RoomCategories.Commands;
using TourCore.Application.Hotels.RoomCategories.Queries;
using TourCore.Contracts.Hotels.RoomCategories;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы с категориями номеров.
    /// </summary>
    [RoutePrefix("api/hotels/room-categories")]
    public class RoomCategoriesController : ApiController
    {
        private readonly ICommandHandler<CreateRoomCategoryCommand, RoomCategoryDto> _createHandler;
        private readonly ICommandHandler<UpdateRoomCategoryCommand, RoomCategoryDto> _updateHandler;
        private readonly IQueryHandler<GetRoomCategoriesQuery, ListResult<RoomCategoryListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRoomCategoryByIdQuery, RoomCategoryDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера категорий номеров.
        /// </summary>
        /// <param name="createHandler">Обработчик создания категории номера.</param>
        /// <param name="updateHandler">Обработчик обновления категории номера.</param>
        /// <param name="getListHandler">Обработчик получения списка категорий номеров.</param>
        /// <param name="getByIdHandler">Обработчик получения категории номера по идентификатору.</param>
        public RoomCategoriesController(
            ICommandHandler<CreateRoomCategoryCommand, RoomCategoryDto> createHandler,
            ICommandHandler<UpdateRoomCategoryCommand, RoomCategoryDto> updateHandler,
            IQueryHandler<GetRoomCategoriesQuery, ListResult<RoomCategoryListItemDto>> getListHandler,
            IQueryHandler<GetRoomCategoryByIdQuery, RoomCategoryDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список категорий номеров.
        /// </summary>
        /// <remarks>
        /// Возвращает список категорий номеров из справочника.
        /// Используется в карточке отеля, настройках номеров, ценах и фильтрах поиска.
        /// </remarks>
        /// <returns>Список категорий номеров.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetRoomCategoriesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить категорию номера по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной категории номера по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования категории номера.
        /// </remarks>
        /// <param name="id">Идентификатор категории номера.</param>
        /// <returns>Данные категории номера.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRoomCategoryByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новую категорию номера.
        /// </summary>
        /// <remarks>
        /// Добавляет новую категорию номера в справочник.
        /// Код категории номера должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные новой категории номера.</param>
        /// <returns>Созданная категория номера.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRoomCategoryCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующую категорию номера.
        /// </summary>
        /// <remarks>
        /// Изменяет данные категории номера по указанному идентификатору.
        /// Используется для редактирования справочника категорий номеров.
        /// Код категории номера должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор категории номера.</param>
        /// <param name="command">Новые данные категории номера.</param>
        /// <returns>Обновленная категория номера.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRoomCategoryCommand command)
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