using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TourCore.Application.Abstractions;
using TourCore.Application.Avia.AirClasses.Commands;
using TourCore.Application.Avia.AirClasses.Queries;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Common.Models;
using TourCore.Contracts.Avia.AirClasses;
using TourCore.Contracts.Common;

namespace TourCore.Admin.Api.Controllers.Avia
{
    /// <summary>
    /// Контроллер для работы с классами обслуживания.
    /// </summary>
    [ApiController]
    [Route("api/v1/avia/air-classes")]
    public class AirClassesController : ControllerBase
    {
        private readonly ICommandHandler<CreateAirClassCommand, AirClassDto> _createHandler;
        private readonly ICommandHandler<UpdateAirClassCommand, AirClassDto> _updateHandler;
        private readonly IQueryHandler<GetAirClassesQuery, PagedResponseDto<AirClassListItemDto>> _getListHandler;
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
            IQueryHandler<GetAirClassesQuery, PagedResponseDto<AirClassListItemDto>> getListHandler,
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
        /// Возвращает список классов обслуживания с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по коду, названию и английскому названию класса обслуживания.
        /// - query.filter.group — поиск по группе класса обслуживания.
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
        /// - group
        /// - sortOrder
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки списка классов обслуживания.</param>
        /// <returns>Страница списка классов обслуживания.</returns>
        [HttpGet]
        [Route("")]
        public async Task<IActionResult> Get([FromQuery] GetAirClassesQuery query)
        {
            var result = await _getListHandler.Handle(
                query ?? new GetAirClassesQuery(),
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
        public async Task<IActionResult> GetById(int id)
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
        public async Task<IActionResult> Create([FromBody] CreateAirClassCommand command)
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
        public async Task<IActionResult> Update(int id, [FromBody] UpdateAirClassCommand command)
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