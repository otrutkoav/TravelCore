using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Finance.RealCourses.Commands;
using TourCore.Application.Finance.RealCourses.Queries;
using TourCore.Contracts.Finance.RealCourses;

namespace TourCore.Api.Legacy.Controllers.Finance
{
    /// <summary>
    /// Контроллер для работы с курсами валют.
    /// </summary>
    [RoutePrefix("api/finance/real-courses")]
    public class RealCoursesController : ApiController
    {
        private readonly ICommandHandler<CreateRealCourseCommand, RealCourseDto> _createHandler;
        private readonly ICommandHandler<UpdateRealCourseCommand, RealCourseDto> _updateHandler;
        private readonly IQueryHandler<GetRealCoursesQuery, ListResult<RealCourseListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetRealCourseByIdQuery, RealCourseDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера курсов валют.
        /// </summary>
        /// <param name="createHandler">Обработчик создания курса валют.</param>
        /// <param name="updateHandler">Обработчик обновления курса валют.</param>
        /// <param name="getListHandler">Обработчик получения списка курсов валют.</param>
        /// <param name="getByIdHandler">Обработчик получения курса валют по идентификатору.</param>
        public RealCoursesController(
            ICommandHandler<CreateRealCourseCommand, RealCourseDto> createHandler,
            ICommandHandler<UpdateRealCourseCommand, RealCourseDto> updateHandler,
            IQueryHandler<GetRealCoursesQuery, ListResult<RealCourseListItemDto>> getListHandler,
            IQueryHandler<GetRealCourseByIdQuery, RealCourseDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список курсов валют.
        /// </summary>
        /// <remarks>
        /// Возвращает список курсов валют между расчетными единицами.
        /// Используется в финансовом блоке, расчетах цен, пересчете валют и отображении стоимости.
        /// </remarks>
        /// <returns>Список курсов валют.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetRealCoursesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить курс валют по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного курса валют по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования курса.
        /// </remarks>
        /// <param name="id">Идентификатор курса валют.</param>
        /// <returns>Данные курса валют.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetRealCourseByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый курс валют.
        /// </summary>
        /// <remarks>
        /// Добавляет новый курс валют для пары расчетных единиц.
        /// Исходная и целевая валюты должны существовать.
        /// Для одной пары валют и одного периода не допускается создание дубля.
        /// </remarks>
        /// <param name="command">Данные нового курса валют.</param>
        /// <returns>Созданный курс валют.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateRealCourseCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий курс валют.
        /// </summary>
        /// <remarks>
        /// Изменяет данные курса валют по указанному идентификатору.
        /// Исходная и целевая валюты должны существовать.
        /// Для одной пары валют и одного периода не допускается создание дубля.
        /// </remarks>
        /// <param name="id">Идентификатор курса валют.</param>
        /// <param name="command">Новые данные курса валют.</param>
        /// <returns>Обновленный курс валют.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateRealCourseCommand command)
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