using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.Airlines.Commands;
using TourCore.Application.Avia.Airlines.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.Airlines;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с авиакомпаниями.
    /// </summary>
    [RoutePrefix("api/avia/airlines")]
    public class AirlinesController : ApiController
    {
        private readonly ICommandHandler<CreateAirlineCommand, AirlineDto> _createHandler;
        private readonly ICommandHandler<UpdateAirlineCommand, AirlineDto> _updateHandler;
        private readonly IQueryHandler<GetAirlinesQuery, ListResult<AirlineListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAirlineByIdQuery, AirlineDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера авиакомпаний.
        /// </summary>
        /// <param name="createHandler">Обработчик создания авиакомпании.</param>
        /// <param name="updateHandler">Обработчик обновления авиакомпании.</param>
        /// <param name="getListHandler">Обработчик получения списка авиакомпаний.</param>
        /// <param name="getByIdHandler">Обработчик получения авиакомпании по идентификатору.</param>
        public AirlinesController(
            ICommandHandler<CreateAirlineCommand, AirlineDto> createHandler,
            ICommandHandler<UpdateAirlineCommand, AirlineDto> updateHandler,
            IQueryHandler<GetAirlinesQuery, ListResult<AirlineListItemDto>> getListHandler,
            IQueryHandler<GetAirlineByIdQuery, AirlineDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список авиакомпаний.
        /// </summary>
        /// <remarks>
        /// Возвращает список авиакомпаний.
        /// Используется в авиаблоке, рейсах, расписании, перелетах и настройках поставщиков авиаперевозки.
        /// </remarks>
        /// <returns>Список авиакомпаний.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetAirlinesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить авиакомпанию по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одной авиакомпании по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования авиакомпании.
        /// </remarks>
        /// <param name="id">Идентификатор авиакомпании.</param>
        /// <returns>Данные авиакомпании.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAirlineByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новую авиакомпанию.
        /// </summary>
        /// <remarks>
        /// Добавляет новую авиакомпанию в справочник.
        /// Код авиакомпании должен быть уникальным.
        /// ICAO-код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные новой авиакомпании.</param>
        /// <returns>Созданная авиакомпания.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAirlineCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующую авиакомпанию.
        /// </summary>
        /// <remarks>
        /// Изменяет данные авиакомпании по указанному идентификатору.
        /// Код авиакомпании должен быть уникальным.
        /// ICAO-код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор авиакомпании.</param>
        /// <param name="command">Новые данные авиакомпании.</param>
        /// <returns>Обновленная авиакомпания.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAirlineCommand command)
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