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
    /// <summary>Управление вариантами авиаперелёта в составе тура.</summary>
    /// <remarks>Настройки принадлежат услуге тура и не создают внешних связей со справочниками.</remarks>
    [RoutePrefix("api/v1/tours/{tourId:int}/services/{serviceId:int}/flights")]
    public class TourFlightsController : ApiController
    {
        private readonly IQueryHandler<
            GetTourFlightsQuery,
            IReadOnlyCollection<TourFlightGroupDto>
        > _get;
        private readonly ICommandHandler<
            ReplaceTourFlightsCommand,
            IReadOnlyCollection<TourFlightGroupDto>
        > _replace;

        /// <summary>Создаёт контроллер настройки авиаперелётов тура.</summary>
        /// <param name="get">Обработчик чтения авиационных настроек.</param>
        /// <param name="replace">Обработчик полной замены авиационных настроек.</param>
        public TourFlightsController(
            IQueryHandler<GetTourFlightsQuery, IReadOnlyCollection<TourFlightGroupDto>> get,
            ICommandHandler<
                ReplaceTourFlightsCommand,
                IReadOnlyCollection<TourFlightGroupDto>
            > replace
        )
        {
            _get = get;
            _replace = replace;
        }

        /// <summary>Получить все варианты и перелёты авиационной услуги.</summary>
        /// <remarks>Группы и вложенные перелёты возвращаются по OrderIndex.</remarks>
        /// <param name="tourId">Идентификатор тура.</param>
        /// <param name="serviceId">Идентификатор авиационной услуги в составе тура.</param>
        /// <returns>Упорядоченный набор групп с вложенными перелётами.</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(IReadOnlyCollection<TourFlightGroupDto>))]
        public async Task<IHttpActionResult> Get(int tourId, int serviceId)
        {
            var query = new GetTourFlightsQuery(tourId, serviceId);
            return Ok(await _get.Handle(query, CancellationToken.None));
        }

        /// <summary>Полностью заменить авиационные настройки услуги.</summary>
        /// <remarks>Группы являются альтернативами, а перелёты внутри группы выполняются последовательно. Интервал задаётся у второго и последующих перелётов. ID ограничений сохраняются без внешних ключей. Revision услуги и тура увеличивается.</remarks>
        /// <param name="tourId">Идентификатор тура.</param>
        /// <param name="serviceId">Идентификатор авиационной услуги в составе тура.</param>
        /// <param name="command">Полная новая структура групп и перелётов.</param>
        /// <returns>Сохранённая структура с назначенными идентификаторами.</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(IReadOnlyCollection<TourFlightGroupDto>))]
        public async Task<IHttpActionResult> Replace(
            int tourId,
            int serviceId,
            [FromBody] ReplaceTourFlightsCommand command
        )
        {
            if (command == null)
                throw new ValidationException(new[] { "Тело запроса не передано." });

            command.TourId = tourId;
            command.TourServiceId = serviceId;

            return Ok(await _replace.Handle(command, CancellationToken.None));
        }
    }
}
