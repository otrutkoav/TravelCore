using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Railway.TrainSchedules.Commands;
using TourCore.Application.Railway.TrainSchedules.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Railway.TrainSchedules;

namespace TourCore.Api.Legacy.Controllers.Railway
{
    /// <summary>
    /// Контроллер для работы с расписаниями ЖД-переездов.
    /// </summary>
    [RoutePrefix("api/railway/schedules")]
    public class TrainSchedulesController : ApiController
    {
        private readonly ICommandHandler<CreateTrainScheduleCommand, TrainScheduleDto> _createHandler;
        private readonly ICommandHandler<UpdateTrainScheduleCommand, TrainScheduleDto> _updateHandler;
        private readonly IQueryHandler<GetTrainSchedulesQuery, PagedResponseDto<TrainScheduleListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetTrainScheduleByIdQuery, TrainScheduleDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера расписаний ЖД-переездов.
        /// </summary>
        /// <param name="createHandler">Обработчик создания расписания ЖД-переезда.</param>
        /// <param name="updateHandler">Обработчик обновления расписания ЖД-переезда.</param>
        /// <param name="getListHandler">Обработчик получения списка расписаний ЖД-переездов.</param>
        /// <param name="getByIdHandler">Обработчик получения расписания ЖД-переезда по идентификатору.</param>
        public TrainSchedulesController(
            ICommandHandler<CreateTrainScheduleCommand, TrainScheduleDto> createHandler,
            ICommandHandler<UpdateTrainScheduleCommand, TrainScheduleDto> updateHandler,
            IQueryHandler<GetTrainSchedulesQuery, PagedResponseDto<TrainScheduleListItemDto>> getListHandler,
            IQueryHandler<GetTrainScheduleByIdQuery, TrainScheduleDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список расписаний ЖД-переездов.
        /// </summary>
        /// <remarks>
        /// Возвращает список расписаний железнодорожных переездов
        /// с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Расписание определяет:
        /// - период действия маршрута
        /// - время отправления и прибытия
        /// - дни недели выполнения рейса
        /// - длительность маршрута
        /// - дополнительное примечание
        ///
        /// Не создает конкретные поездки — только шаблон расписания.
        ///
        /// Фильтрация:
        /// - query.filter.railwayTransferId — фильтр по идентификатору ЖД-переезда.
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
        /// - railwayTransferId
        /// - dateFrom
        /// - dateTo
        /// - timeFrom
        /// - timeTo
        /// - daysOnRoad
        /// - remark
        ///
        /// Поле daysOfWeek доступно только для чтения и не поддерживает сортировку.
        /// </remarks>
        /// <param name="query">
        /// Параметры фильтрации, пагинации и сортировки расписаний ЖД-переездов.
        /// </param>
        /// <returns>Страница списка расписаний ЖД-переездов.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetTrainSchedulesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetTrainSchedulesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить расписание ЖД-переезда по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные конкретного расписания ЖД-переезда.
        /// Используется для просмотра или открытия формы редактирования расписания.
        /// </remarks>
        /// <param name="id">Идентификатор расписания ЖД-переезда.</param>
        /// <returns>Данные расписания ЖД-переезда.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetTrainScheduleByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать расписание ЖД-переезда.
        /// </summary>
        /// <remarks>
        /// Создает новое расписание для ЖД-переезда.
        ///
        /// Позволяет задать:
        /// - базовый ЖД-переезд
        /// - период действия
        /// - время отправления и прибытия
        /// - дни недели в legacy-формате "1234567"
        /// - длительность в пути
        /// - примечание
        /// </remarks>
        /// <param name="command">Данные нового расписания ЖД-переезда.</param>
        /// <returns>Созданное расписание ЖД-переезда.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateTrainScheduleCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить расписание ЖД-переезда.
        /// </summary>
        /// <remarks>
        /// Обновляет параметры расписания ЖД-переезда:
        /// период действия, время, дни недели, длительность в пути и примечание.
        ///
        /// Не влияет на уже сформированные поездки, если они будут реализованы позже.
        /// </remarks>
        /// <param name="id">Идентификатор расписания ЖД-переезда.</param>
        /// <param name="command">Новые данные расписания ЖД-переезда.</param>
        /// <returns>Обновленное расписание ЖД-переезда.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateTrainScheduleCommand command)
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