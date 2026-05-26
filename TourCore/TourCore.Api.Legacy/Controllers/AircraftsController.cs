using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.Aircrafts.Commands;
using TourCore.Application.Avia.Aircrafts.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.Aircrafts;
using TourCore.Contracts.Common;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с воздушными судами.
    /// </summary>
    [RoutePrefix("api/avia/aircrafts")]
    public class AircraftsController : ApiController
    {
        private readonly ICommandHandler<CreateAircraftCommand, AircraftDto> _createHandler;
        private readonly ICommandHandler<UpdateAircraftCommand, AircraftDto> _updateHandler;
        private readonly IQueryHandler<GetAircraftsQuery, PagedResponseDto<AircraftListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAircraftByIdQuery, AircraftDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера воздушных судов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания воздушного судна.</param>
        /// <param name="updateHandler">Обработчик обновления воздушного судна.</param>
        /// <param name="getListHandler">Обработчик получения списка воздушных судов.</param>
        /// <param name="getByIdHandler">Обработчик получения воздушного судна по идентификатору.</param>
        public AircraftsController(
            ICommandHandler<CreateAircraftCommand, AircraftDto> createHandler,
            ICommandHandler<UpdateAircraftCommand, AircraftDto> updateHandler,
            IQueryHandler<GetAircraftsQuery, PagedResponseDto<AircraftListItemDto>> getListHandler,
            IQueryHandler<GetAircraftByIdQuery, AircraftDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список воздушных судов.
        /// </summary>
        /// <remarks>
        /// Возвращает список воздушных судов с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по коду, названию и английскому названию.
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
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка воздушных судов.</param>
        /// <returns>Страница списка воздушных судов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetAircraftsQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetAircraftsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить воздушное судно по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного воздушного судна по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования воздушного судна.
        /// </remarks>
        /// <param name="id">Идентификатор воздушного судна.</param>
        /// <returns>Данные воздушного судна.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAircraftByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новое воздушное судно.
        /// </summary>
        /// <remarks>
        /// Добавляет новое воздушное судно / тип самолета в справочник.
        /// Код воздушного судна должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные нового воздушного судна.</param>
        /// <returns>Созданное воздушное судно.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAircraftCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующее воздушное судно.
        /// </summary>
        /// <remarks>
        /// Изменяет данные воздушного судна по указанному идентификатору.
        /// Код воздушного судна должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор воздушного судна.</param>
        /// <param name="command">Новые данные воздушного судна.</param>
        /// <returns>Обновленное воздушное судно.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAircraftCommand command)
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