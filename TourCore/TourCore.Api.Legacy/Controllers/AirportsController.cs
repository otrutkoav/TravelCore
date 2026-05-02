using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.Airports.Commands;
using TourCore.Application.Avia.Airports.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.Airports;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с аэропортами.
    /// </summary>
    [RoutePrefix("api/avia/airports")]
    public class AirportsController : ApiController
    {
        private readonly ICommandHandler<CreateAirportCommand, AirportDto> _createHandler;
        private readonly ICommandHandler<UpdateAirportCommand, AirportDto> _updateHandler;
        private readonly IQueryHandler<GetAirportsQuery, ListResult<AirportListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAirportByIdQuery, AirportDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера аэропортов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания аэропорта.</param>
        /// <param name="updateHandler">Обработчик обновления аэропорта.</param>
        /// <param name="getListHandler">Обработчик получения списка аэропортов.</param>
        /// <param name="getByIdHandler">Обработчик получения аэропорта по идентификатору.</param>
        public AirportsController(
            ICommandHandler<CreateAirportCommand, AirportDto> createHandler,
            ICommandHandler<UpdateAirportCommand, AirportDto> updateHandler,
            IQueryHandler<GetAirportsQuery, ListResult<AirportListItemDto>> getListHandler,
            IQueryHandler<GetAirportByIdQuery, AirportDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список аэропортов.
        /// </summary>
        /// <remarks>
        /// Возвращает список аэропортов.
        /// Используется в авиаблоке, рейсах, расписании, маршрутах и настройках перелетов.
        /// </remarks>
        /// <returns>Список аэропортов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetAirportsQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить аэропорт по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного аэропорта по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования аэропорта.
        /// </remarks>
        /// <param name="id">Идентификатор аэропорта.</param>
        /// <returns>Данные аэропорта.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAirportByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый аэропорт.
        /// </summary>
        /// <remarks>
        /// Добавляет новый аэропорт в справочник.
        /// Аэропорт обязательно должен быть привязан к городу.
        /// Код аэропорта должен быть уникальным.
        /// ICAO-код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные нового аэропорта.</param>
        /// <returns>Созданный аэропорт.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAirportCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий аэропорт.
        /// </summary>
        /// <remarks>
        /// Изменяет данные аэропорта по указанному идентификатору.
        /// Аэропорт обязательно должен быть привязан к городу.
        /// Код аэропорта должен быть уникальным.
        /// ICAO-код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор аэропорта.</param>
        /// <param name="command">Новые данные аэропорта.</param>
        /// <returns>Обновленный аэропорт.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAirportCommand command)
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