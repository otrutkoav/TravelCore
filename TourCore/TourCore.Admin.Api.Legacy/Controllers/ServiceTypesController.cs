using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Services.ServiceTypes.Commands;
using TourCore.Application.Services.ServiceTypes.Queries;
using TourCore.Contracts.Common;
using TourCore.Contracts.Services.ServiceTypes;

namespace TourCore.Admin.Api.Legacy.Controllers.Services
{
    /// <summary>
    /// Контроллер для работы с типами услуг.
    /// </summary>
    [RoutePrefix("api/v1/services/service-types")]
    public class ServiceTypesController : ApiController
    {
        private readonly ICommandHandler<CreateServiceTypeCommand, ServiceTypeDto> _createHandler;
        private readonly ICommandHandler<UpdateServiceTypeCommand, ServiceTypeDto> _updateHandler;
        private readonly ICommandHandler<DeleteServiceTypeCommand, bool> _deleteHandler;
        private readonly IQueryHandler<GetServiceTypesQuery, PagedResponseDto<ServiceTypeListItemDto>> _listHandler;
        private readonly IQueryHandler<GetServiceTypeByIdQuery, ServiceTypeDto> _getByIdHandler;

        /// <summary>Создает экземпляр контроллера типов услуг.</summary>
        /// <param name="createHandler">Обработчик создания типа услуги.</param>
        /// <param name="updateHandler">Обработчик обновления типа услуги.</param>
        /// <param name="deleteHandler">Обработчик удаления типа услуги.</param>
        /// <param name="listHandler">Обработчик получения списка типов услуг.</param>
        /// <param name="getByIdHandler">Обработчик получения типа услуги по идентификатору.</param>
        public ServiceTypesController(
            ICommandHandler<CreateServiceTypeCommand, ServiceTypeDto> createHandler,
            ICommandHandler<UpdateServiceTypeCommand, ServiceTypeDto> updateHandler,
            ICommandHandler<DeleteServiceTypeCommand, bool> deleteHandler,
            IQueryHandler<GetServiceTypesQuery, PagedResponseDto<ServiceTypeListItemDto>> listHandler,
            IQueryHandler<GetServiceTypeByIdQuery, ServiceTypeDto> getByIdHandler)
        {
            _createHandler = createHandler;
            _updateHandler = updateHandler;
            _deleteHandler = deleteHandler;
            _listHandler = listHandler;
            _getByIdHandler = getByIdHandler;
        }

        /// <summary>Получить список типов услуг.</summary>
        /// <remarks>
        /// Возвращает страницу справочника типов услуг с поддержкой фильтрации, пагинации и сортировки.
        ///
        /// Фильтрация:
        /// - query.filter.search — поиск по названию, английскому названию и коду;
        /// - query.filter.isDuration — отбор типов услуг по наличию продолжительности;
        /// - query.filter.isRoute — отбор маршрутных или немаршрутных типов услуг.
        ///
        /// Пагинация:
        /// - query.page — номер страницы. Минимальное значение: 1. По умолчанию: 1;
        /// - query.pageSize — размер страницы. По умолчанию: 20. Максимальное значение: 100.
        ///
        /// Сортировка:
        /// - query.sortBy — поле сортировки;
        /// - query.sortDirection — направление: 0 — по возрастанию, 1 — по убыванию.
        ///
        /// Допустимые значения query.sortBy: id, name, nameEn, code, isDuration, isRoute.
        /// </remarks>
        /// <param name="query">Параметры фильтрации, пагинации и сортировки.</param>
        /// <returns>Страница типов услуг.</returns>
        [HttpGet, Route("")]
        public async Task<IHttpActionResult> Get([FromUri] GetServiceTypesQuery query)
        {
            return Ok(await _listHandler.Handle(query ?? new GetServiceTypesQuery(), CancellationToken.None));
        }

        /// <summary>Получить тип услуги по идентификатору.</summary>
        /// <remarks>
        /// Возвращает полные данные типа услуги для просмотра или редактирования.
        /// Если запись не существует, возвращается ошибка service_type_not_found.
        /// </remarks>
        /// <param name="id">Идентификатор типа услуги.</param>
        /// <returns>Данные типа услуги.</returns>
        [HttpGet, Route("{id:int}")]
        public async Task<IHttpActionResult> GetById(int id)
        {
            return Ok(await _getByIdHandler.Handle(new GetServiceTypeByIdQuery(id), CancellationToken.None));
        }

        /// <summary>Создать тип услуги.</summary>
        /// <remarks>
        /// Добавляет новый тип услуги в справочник.
        ///
        /// Поля запроса:
        /// - name — обязательное название, не более 50 символов;
        /// - nameEn — необязательное английское название, не более 50 символов;
        /// - code — необязательный уникальный код, не более 10 символов; нормализуется в верхний регистр;
        /// - isDuration — имеет ли услуга продолжительность;
        /// - isRoute — является ли услуга маршрутной.
        /// </remarks>
        /// <param name="command">Данные нового типа услуги.</param>
        /// <returns>Созданный тип услуги.</returns>
        [HttpPost, Route("")]
        public async Task<IHttpActionResult> Create([FromBody] CreateServiceTypeCommand command)
        {
            if (command == null) throw new ValidationException(new[] { "Тело запроса не передано." });
            return Ok(await _createHandler.Handle(command, CancellationToken.None));
        }

        /// <summary>Обновить тип услуги.</summary>
        /// <remarks>
        /// Полностью обновляет основные параметры существующего типа услуги.
        /// Идентификатор берется из маршрута; значение id из тела запроса не используется.
        /// Ограничения полей совпадают с методом создания, а code должен оставаться уникальным.
        /// </remarks>
        /// <param name="id">Идентификатор изменяемого типа услуги.</param>
        /// <param name="command">Новые данные типа услуги.</param>
        /// <returns>Обновленный тип услуги.</returns>
        [HttpPut, Route("{id:int}")]
        public async Task<IHttpActionResult> Update(int id, [FromBody] UpdateServiceTypeCommand command)
        {
            if (command == null) throw new ValidationException(new[] { "Тело запроса не передано." });
            command.Id = id;
            return Ok(await _updateHandler.Handle(command, CancellationToken.None));
        }

        /// <summary>Удалить тип услуги.</summary>
        /// <remarks>
        /// Физически удаляет тип услуги из справочника и при успехе возвращает 204 No Content.
        /// Если запись не существует, возвращается ошибка service_type_not_found.
        /// Используемый другими сущностями тип услуги не удаляется при наличии ограничивающих внешних ключей.
        /// </remarks>
        /// <param name="id">Идентификатор удаляемого типа услуги.</param>
        /// <returns>Пустой ответ со статусом 204.</returns>
        [HttpDelete, Route("{id:int}")]
        public async Task<IHttpActionResult> Delete(int id)
        {
            await _deleteHandler.Handle(new DeleteServiceTypeCommand(id), CancellationToken.None);
            return StatusCode(System.Net.HttpStatusCode.NoContent);
        }
    }
}
