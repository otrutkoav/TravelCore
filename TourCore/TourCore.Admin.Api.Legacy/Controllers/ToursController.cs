using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Common;
using TourCore.Contracts.Tours;

namespace TourCore.Admin.Api.Legacy.Controllers.Tours
{
    /// <summary>Управление иерархическим справочником типов туров.</summary>
    [RoutePrefix("api/v1/tours/tour-types")]
    public class TourTypesController : ApiController
    {
        private readonly ICommandHandler<CreateTourTypeCommand,TourTypeDto> create;private readonly ICommandHandler<UpdateTourTypeCommand,TourTypeDto> update;private readonly ICommandHandler<DeleteTourTypeCommand,bool> delete;private readonly IQueryHandler<GetTourTypesQuery,PagedResponseDto<TourTypeListItemDto>> list;private readonly IQueryHandler<GetTourTypeByIdQuery,TourTypeDto> get;
        /// <summary>Создаёт контроллер справочника типов туров.</summary>
        /// <param name="create">Обработчик создания.</param><param name="update">Обработчик изменения.</param><param name="delete">Обработчик удаления.</param><param name="list">Обработчик списка.</param><param name="get">Обработчик получения по ID.</param>
        public TourTypesController(ICommandHandler<CreateTourTypeCommand,TourTypeDto> create,ICommandHandler<UpdateTourTypeCommand,TourTypeDto> update,ICommandHandler<DeleteTourTypeCommand,bool> delete,IQueryHandler<GetTourTypesQuery,PagedResponseDto<TourTypeListItemDto>> list,IQueryHandler<GetTourTypeByIdQuery,TourTypeDto> get){this.create=create;this.update=update;this.delete=delete;this.list=list;this.get=get;}
        /// <summary>Получить страницу типов туров.</summary><remarks>Поддерживает поиск, фильтрацию по родителю, корневым элементам и пагинацию.</remarks>
        /// <param name="q">Поиск, фильтр дерева и параметры пагинации.</param><returns>Страница кратких данных типов туров.</returns>
        [HttpGet,Route(""),ResponseType(typeof(PagedResponseDto<TourTypeListItemDto>))] public async Task<IHttpActionResult> Get([FromUri] GetTourTypesQuery q){return Ok(await list.Handle(q??new GetTourTypesQuery(),CancellationToken.None));}
        /// <summary>Получить тип тура по идентификатору.</summary><remarks>Возвращает название, родителя и даты аудита. Ошибка: tour_type_not_found.</remarks>
        /// <param name="id">Идентификатор типа.</param><returns>Полные данные типа тура.</returns>
        [HttpGet,Route("{id:int}"),ResponseType(typeof(TourTypeDto))] public async Task<IHttpActionResult> GetById(int id){return Ok(await get.Handle(new GetTourTypeByIdQuery(id),CancellationToken.None));}
        /// <summary>Создать тип тура.</summary><remarks>Name обязателен, уникален и содержит не более 128 символов. ParentId должен существовать.</remarks>
        /// <param name="c">Название и необязательный родитель.</param><returns>Созданный тип тура.</returns>
        [HttpPost,Route(""),ResponseType(typeof(TourTypeDto))] public async Task<IHttpActionResult> Create([FromBody] CreateTourTypeCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});return Ok(await create.Handle(c,CancellationToken.None));}
        /// <summary>Изменить тип тура.</summary><remarks>Циклические связи и назначение самого себя родителем запрещены.</remarks>
        /// <param name="id">Идентификатор типа.</param><param name="c">Новое название и родитель.</param><returns>Обновлённый тип тура.</returns>
        [HttpPut,Route("{id:int}"),ResponseType(typeof(TourTypeDto))] public async Task<IHttpActionResult> Update(int id,[FromBody] UpdateTourTypeCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});c.Id=id;return Ok(await update.Handle(c,CancellationToken.None));}
        /// <summary>Удалить тип тура.</summary><remarks>Тип с дочерними элементами или используемый туром удалить нельзя.</remarks>
        /// <param name="id">Идентификатор типа.</param><returns>Пустой ответ 204.</returns>
        [HttpDelete,Route("{id:int}")] public async Task<IHttpActionResult> Delete(int id){await delete.Handle(new DeleteTourTypeCommand(id),CancellationToken.None);return StatusCode(System.Net.HttpStatusCode.NoContent);}
    }

    /// <summary>Управление шаблонами туров.</summary>
    [RoutePrefix("api/v1/tours")]
    public class ToursController : ApiController
    {
        private readonly ICommandHandler<CreateTourCommand,TourDto> create;private readonly ICommandHandler<UpdateTourCommand,TourDto> update;private readonly ICommandHandler<DeleteTourCommand,bool> delete;private readonly IQueryHandler<GetToursQuery,PagedResponseDto<TourListItemDto>> list;private readonly IQueryHandler<GetTourByIdQuery,TourDto> get;
        /// <summary>Создаёт контроллер шаблонов туров.</summary>
        /// <param name="create">Обработчик создания.</param><param name="update">Обработчик изменения.</param><param name="delete">Обработчик удаления.</param><param name="list">Обработчик списка.</param><param name="get">Обработчик получения по ID.</param>
        public ToursController(ICommandHandler<CreateTourCommand,TourDto> create,ICommandHandler<UpdateTourCommand,TourDto> update,ICommandHandler<DeleteTourCommand,bool> delete,IQueryHandler<GetToursQuery,PagedResponseDto<TourListItemDto>> list,IQueryHandler<GetTourByIdQuery,TourDto> get){this.create=create;this.update=update;this.delete=delete;this.list=list;this.get=get;}
        /// <summary>Получить страницу туров.</summary><remarks>Поддерживает Search, Status, CurrencyId, Page и PageSize.</remarks>
        /// <param name="q">Фильтры и параметры пагинации.</param><returns>Страница кратких данных туров.</returns>
        [HttpGet,Route(""),ResponseType(typeof(PagedResponseDto<TourListItemDto>))] public async Task<IHttpActionResult> Get([FromUri] GetToursQuery q){return Ok(await list.Handle(q??new GetToursQuery(),CancellationToken.None));}
        /// <summary>Получить тур по идентификатору.</summary><remarks>Возвращает календарь, типы тура и форматированные HTML-поля.</remarks>
        /// <param name="id">Идентификатор тура.</param><returns>Полные данные тура.</returns>
        [HttpGet,Route("{id:int}"),ResponseType(typeof(TourDto))] public async Task<IHttpActionResult> GetById(int id){return Ok(await get.Handle(new GetTourByIdQuery(id),CancellationToken.None));}
        /// <summary>Создать тур.</summary><remarks>DateMode 1 использует диапазон; DateMode 2 — AvailableDates. Валюта и типы должны существовать. Новый тур получает статус 1.</remarks>
        /// <param name="c">Основные параметры, календарь, типы и HTML-описание.</param><returns>Созданный тур с ID от 100000000.</returns>
        [HttpPost,Route(""),ResponseType(typeof(TourDto))] public async Task<IHttpActionResult> Create([FromBody] CreateTourCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});return Ok(await create.Handle(c,CancellationToken.None));}
        /// <summary>Изменить тур.</summary><remarks>Полностью заменяет поля, статус, назначенные типы и календарь.</remarks>
        /// <param name="id">Идентификатор тура.</param><param name="c">Новое полное состояние тура.</param><returns>Обновлённый тур.</returns>
        [HttpPut,Route("{id:int}"),ResponseType(typeof(TourDto))] public async Task<IHttpActionResult> Update(int id,[FromBody] UpdateTourCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});c.Id=id;return Ok(await update.Handle(c,CancellationToken.None));}
        /// <summary>Удалить тур.</summary><remarks>Связанные конкретные даты и связи с типами удаляются вместе с туром.</remarks>
        /// <param name="id">Идентификатор тура.</param><returns>Пустой ответ 204.</returns>
        [HttpDelete,Route("{id:int}")] public async Task<IHttpActionResult> Delete(int id){await delete.Handle(new DeleteTourCommand(id),CancellationToken.None);return StatusCode(System.Net.HttpStatusCode.NoContent);}
    }
}
