using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Application.Hotels.MealTypes.Commands;
using TourCore.Application.Hotels.MealTypes.Queries;
using TourCore.Contracts.Hotels.MealTypes;

namespace TourCore.Api.Legacy.Controllers
{
    /// <summary>
    /// Контроллер для работы с типами питания.
    /// </summary>
    [RoutePrefix("api/hotels/meal-types")]
    public class MealTypesController : ApiController
    {
        private readonly ICommandHandler<CreateMealTypeCommand, MealTypeDto> _createHandler;
        private readonly ICommandHandler<UpdateMealTypeCommand, MealTypeDto> _updateHandler;
        private readonly IQueryHandler<GetMealTypesQuery, ListResult<MealTypeListItemDto>> _getListHandler;
        private readonly IQueryHandler<GetMealTypeByIdQuery, MealTypeDto> _getByIdHandler;

        /// <summary>
        /// Создает экземпляр контроллера типов питания.
        /// </summary>
        /// <param name="createHandler">Обработчик создания типа питания.</param>
        /// <param name="updateHandler">Обработчик обновления типа питания.</param>
        /// <param name="getListHandler">Обработчик получения списка типов питания.</param>
        /// <param name="getByIdHandler">Обработчик получения типа питания по идентификатору.</param>
        public MealTypesController(
            ICommandHandler<CreateMealTypeCommand, MealTypeDto> createHandler,
            ICommandHandler<UpdateMealTypeCommand, MealTypeDto> updateHandler,
            IQueryHandler<GetMealTypesQuery, ListResult<MealTypeListItemDto>> getListHandler,
            IQueryHandler<GetMealTypeByIdQuery, MealTypeDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _getListHandler = getListHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>
        /// Получить список типов питания.
        /// </summary>
        /// <remarks>
        /// Возвращает список типов питания из справочника.
        /// Используется в карточке отеля, настройках размещения, ценах и фильтрах поиска.
        /// </remarks>
        /// <returns>Список типов питания.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> Get()
        {
            var result = await _getListHandler.Handle(
                new GetMealTypesQuery(),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Получить тип питания по идентификатору.
        /// </summary>
        /// <remarks>
        /// Возвращает данные одного типа питания по указанному идентификатору.
        /// Используется для просмотра или открытия формы редактирования типа питания.
        /// </remarks>
        /// <param name="id">Идентификатор типа питания.</param>
        /// <returns>Данные типа питания.</returns>
        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            var result = await _getByIdHandler.Handle(
                new GetMealTypeByIdQuery(id),
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Создать новый тип питания.
        /// </summary>
        /// <remarks>
        /// Добавляет новый тип питания в справочник.
        /// Код типа питания должен быть уникальным.
        /// Глобальный код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="command">Данные нового типа питания.</param>
        /// <returns>Созданный тип питания.</returns>
        [HttpPost]
        [Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateMealTypeCommand command)
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            var result = await _createHandler.Handle(
                command,
                CancellationToken.None);

            return Ok(result);
        }

        /// <summary>
        /// Обновить существующий тип питания.
        /// </summary>
        /// <remarks>
        /// Изменяет данные типа питания по указанному идентификатору.
        /// Используется для редактирования справочника типов питания.
        /// Код типа питания должен быть уникальным.
        /// Глобальный код, если указан, также должен быть уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор типа питания.</param>
        /// <param name="command">Новые данные типа питания.</param>
        /// <returns>Обновленный тип питания.</returns>
        [HttpPut]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateMealTypeCommand command)
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