using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Application.Tours;
using TourCore.Contracts.Tours;
namespace TourCore.Admin.Api.Legacy.Controllers.Tours
{
    /// <summary>Управление составом услуг тура.</summary>
    /// <remarks>Работает с экземплярами услуг конкретного тура. OrderIndex назначается сервером; клиент передаёт позиции только как упорядоченный массив ID.</remarks>
    [RoutePrefix("api/v1/tours/{tourId:int}/services")]
    public class TourServicesController:ApiController
    {
        private readonly ICommandHandler<CreateTourServiceCommand,TourServiceDto> create;private readonly ICommandHandler<UpdateTourServiceCommand,TourServiceDto> update;private readonly ICommandHandler<DeleteTourServiceCommand,bool> delete;private readonly ICommandHandler<ReorderTourServicesCommand,IReadOnlyCollection<TourServiceDto>> reorder;private readonly IQueryHandler<GetTourServicesQuery,IReadOnlyCollection<TourServiceDto>> list;private readonly IQueryHandler<GetTourServiceByIdQuery,TourServiceDto> get;
        /// <summary>Создаёт контроллер состава тура.</summary>
        /// <param name="create">Обработчик добавления.</param><param name="update">Обработчик изменения.</param><param name="delete">Обработчик удаления.</param><param name="reorder">Обработчик перестановки.</param><param name="list">Обработчик списка.</param><param name="get">Обработчик получения по ID.</param>
        public TourServicesController(ICommandHandler<CreateTourServiceCommand,TourServiceDto> create,ICommandHandler<UpdateTourServiceCommand,TourServiceDto> update,ICommandHandler<DeleteTourServiceCommand,bool> delete,ICommandHandler<ReorderTourServicesCommand,IReadOnlyCollection<TourServiceDto>> reorder,IQueryHandler<GetTourServicesQuery,IReadOnlyCollection<TourServiceDto>> list,IQueryHandler<GetTourServiceByIdQuery,TourServiceDto> get){this.create=create;this.update=update;this.delete=delete;this.reorder=reorder;this.list=list;this.get=get;}
        /// <summary>Получить полный состав указанного тура.</summary>
        /// <remarks>Возвращает услуги по возрастанию OrderIndex. Для существующего пустого тура возвращается пустой массив. Возможная ошибка: tour_not_found.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><returns>Упорядоченный список услуг.</returns>
        [HttpGet,Route(""),ResponseType(typeof(IReadOnlyCollection<TourServiceDto>))]public async Task<IHttpActionResult> Get(int tourId){return Ok(await list.Handle(new GetTourServicesQuery(tourId),CancellationToken.None));}
        /// <summary>Получить услугу тура по идентификатору.</summary>
        /// <remarks>Поиск ограничен указанным туром. Для неизвестного ID или услуги другого тура возвращается tour_service_not_found.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор услуги.</param><returns>Полные данные, включая OrderIndex и Revision.</returns>
        [HttpGet,Route("{id:int}"),ResponseType(typeof(TourServiceDto))]public async Task<IHttpActionResult> GetById(int tourId,int id){return Ok(await get.Handle(new GetTourServiceByIdQuery(tourId,id),CancellationToken.None));}
        /// <summary>Добавить услугу в конец состава тура.</summary>
        /// <remarks>serviceTypeId обязателен; name обязателен и ограничен 256 символами; parentServiceId необязателен и должен ссылаться на услугу того же тура. isHidden скрывает услугу в клиентских интерфейсах; isRemovable разрешает убрать её из варианта в корзине; isOptional исключает её из первоначального расчёта до выбора. Скрытая услуга не может быть удаляемой или опциональной. Сервер назначает OrderIndex и Revision = 1, затем увеличивает Revision тура. Возможные ошибки: tour_not_found, service_type_not_found, tour_service_parent_not_found.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><param name="c">Вид, название и необязательный родитель.</param><returns>Созданная услуга с назначенными ID, OrderIndex и Revision.</returns>
        [HttpPost,Route(""),ResponseType(typeof(TourServiceDto))]public async Task<IHttpActionResult> Create(int tourId,[FromBody]CreateTourServiceCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});c.TourId=tourId;return Ok(await create.Handle(c,CancellationToken.None));}
        /// <summary>Изменить содержательные данные услуги без изменения порядка.</summary>
        /// <remarks>Заменяет ServiceTypeId, Name, ParentServiceId, IsHidden, IsRemovable и IsOptional, не изменяя OrderIndex. Скрытая услуга не может быть удаляемой или опциональной. Родителем нельзя назначить саму услугу, услугу другого тура или собственного потомка. При успехе увеличиваются Revision услуги и тура. Возможные ошибки: tour_not_found, tour_service_not_found, service_type_not_found, tour_service_parent_not_found.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор услуги.</param><param name="c">Новые данные услуги.</param><returns>Обновлённая услуга с новой Revision.</returns>
        [HttpPut,Route("{id:int}"),ResponseType(typeof(TourServiceDto))]public async Task<IHttpActionResult> Update(int tourId,int id,[FromBody]UpdateTourServiceCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});c.TourId=tourId;c.Id=id;return Ok(await update.Handle(c,CancellationToken.None));}
        /// <summary>Удалить услугу и уплотнить порядок состава.</summary>
        /// <remarks>Сервер перенумеровывает оставшиеся OrderIndex до 1..N и увеличивает Revision тура. Услугу с дочерними элементами удалить нельзя. Возможные ошибки: tour_not_found, tour_service_not_found, tour_service_has_children.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><param name="id">Идентификатор услуги.</param><returns>Пустой ответ 204 No Content.</returns>
        [HttpDelete,Route("{id:int}")]public async Task<IHttpActionResult> Delete(int tourId,int id){await delete.Handle(new DeleteTourServiceCommand(tourId,id),CancellationToken.None);return StatusCode(System.Net.HttpStatusCode.NoContent);}
        /// <summary>Изменить порядок услуг в составе тура.</summary>
        /// <remarks>ServiceIds должен содержать все услуги тура ровно по одному разу в требуемом порядке. Частичный список, повторы, неизвестные ID и услуги другого тура не принимаются. Сервер назначает OrderIndex от 1 до N. Revision тура увеличивается один раз, Revision услуг не изменяется.</remarks>
        /// <param name="tourId">Идентификатор тура.</param><param name="c">Полная последовательность ID.</param><returns>Состав в новом порядке.</returns>
        [HttpPut,Route("order"),ResponseType(typeof(IReadOnlyCollection<TourServiceDto>))]public async Task<IHttpActionResult> Reorder(int tourId,[FromBody]ReorderTourServicesCommand c){if(c==null)throw new ValidationException(new[]{"Тело запроса не передано."});c.TourId=tourId;return Ok(await reorder.Handle(c,CancellationToken.None));}
    }
}
