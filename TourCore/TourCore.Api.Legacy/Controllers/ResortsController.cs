using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Geography.Resorts.Commands;
using TourCore.Application.Geography.Resorts.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Geography.Resorts;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы с курортами.
    /// </summary>
    [RoutePrefix("api/geography/resorts")]
    public class ResortsController : ApiController
    {
        private readonly ICommandHandler<CreateResortCommand, ResortDto> _createHandler;
        private readonly ICommandHandler<UpdateResortCommand, ResortDto> _updateHandler;
        private readonly IQueryHandler<GetResortsQuery, PagedResponseDto<ResortListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetResortByIdQuery, ResortDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера курортов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания курорта.</param>
        /// <param name="updateHandler">Обработчик обновления курорта.</param>
        /// <param name="getListHandler">Обработчик получения списка курортов.</param>
        /// <param name="getByIdHandler">Обработчик получения курорта по идентификатору.</param>
        public ResortsController(
            ICommandHandler<CreateResortCommand, ResortDto> createHandler,
            ICommandHandler<UpdateResortCommand, ResortDto> updateHandler,
            IQueryHandler<GetResortsQuery, PagedResponseDto<ResortListItemDto>> getListHandler,
            IQueryHandler<GetResortByIdQuery, ResortDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список курортов.
        /// </summary>
        /// <remarks>
        /// Возвращает список курортов с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию и английскому названию курорта.
        /// - query.filter.countryId — фильтр по идентификатору страны.
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
        /// - countryId
        /// - name
        /// - nameEn
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка курортов.</param>
        /// <returns>Страница списка курортов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetResortsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetResortsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить курорт по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного курорта по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования курорта.
        /// </remarks>
        /// <param name="id">Идентификатор курорта.</param>
        /// <returns>Данные курорта.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetResortByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый курорт.
        /// </summary>
        /// <remarks>
        /// Добавляет новый курорт в справочник.
        /// Курорт обязательно должен быть привязан к стране.
        /// Название курорта должно быть уникально в рамках страны.
        /// </remarks>
        /// <param name="command">Данные нового курорта.</param>
        /// <returns>Созданный курорт.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateResortCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий курорт.
        /// </summary>
        /// <remarks>
        /// Изменяет данные курорта по указанному идентификатору.
        /// Используется для редактирования справочника курортов.
        /// Страна обязательна.
        /// Название курорта должно быть уникально в рамках страны.
        /// </remarks>
        /// <param name="id">Идентификатор курорта.</param>
        /// <param name="command">Новые данные курорта.</param>
        /// <returns>Обновленный курорт.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateResortCommand command)
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