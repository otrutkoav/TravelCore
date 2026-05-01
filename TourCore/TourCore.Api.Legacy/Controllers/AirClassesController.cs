using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Avia.AirClasses.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.AirClasses;

namespace TourCore.Api.Legacy.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с классами обслуживания.
    /// </summary>
    [RoutePrefix("api/avia/air-classes")]
    public class AirClassesController : ApiController
    {
        private readonly ICommandHandler<CreateAirClassCommand, AirClassDto> _createHandler;
        private readonly ICommandHandler<UpdateAirClassCommand, AirClassDto> _updateHandler;
        private readonly IQueryHandler<GetAirClassesQuery, ListResult<AirClassListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetAirClassByIdQuery, AirClassDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера классов обслуживания.
        /// </summary>
        /// <param name="createHandler">Обработчик создания класса обслуживания.</param>
        /// <param name="updateHandler">Обработчик обновления класса обслуживания.</param>
        /// <param name="getListHandler">Обработчик получения списка классов обслуживания.</param>
        /// <param name="getByIdHandler">Обработчик получения класса обслуживания по идентификатору.</param>
        public AirClassesController(
            ICommandHandler<CreateAirClassCommand, AirClassDto> createHandler,
            ICommandHandler<UpdateAirClassCommand, AirClassDto> updateHandler,
            IQueryHandler<GetAirClassesQuery, ListResult<AirClassListItemDto>> getListHandler,
            IQueryHandler<GetAirClassByIdQuery, AirClassDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список классов обслуживания.
        /// </summary>
        /// <remarks>
        /// Возвращает список классов обслуживания в авиа.
        /// Используется в авиаблоке, настройках перелетов, тарифах и отображении условий перевозки.
        /// </remarks>
        /// <returns>Список классов обслуживания.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetAirClassesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить класс обслуживания по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного класса обслуживания по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования класса обслуживания.
        /// </remarks>
        /// <param name="id">Идентификатор класса обслуживания.</param>
        /// <returns>Данные класса обслуживания.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetAirClassByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый класс обслуживания.
        /// </summary>
        /// <remarks>
        /// Добавляет новый класс обслуживания в справочник.
        /// Код класса обслуживания должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные нового класса обслуживания.</param>
        /// <returns>Созданный класс обслуживания.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateAirClassCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий класс обслуживания.
        /// </summary>
        /// <remarks>
        /// Изменяет данные класса обслуживания по указанному идентификатору.
        /// Код класса обслуживания должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор класса обслуживания.</param>
        /// <param name="command">Новые данные класса обслуживания.</param>
        /// <returns>Обновленный класс обслуживания.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateAirClassCommand command)
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