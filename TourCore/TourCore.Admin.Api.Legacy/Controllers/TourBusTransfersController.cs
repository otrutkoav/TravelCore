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
    /// <summary>Управление автобусным переездом в составе тура.</summary>
    /// <remarks>
    /// Одна услуга тура содержит одну конфигурацию автобусного переезда. Идентификаторы
    /// справочников сохраняются без внешних ключей на страны, города, переезды, места
    /// посадки и транспорт.
    /// </remarks>
    [RoutePrefix("api/v1/tours/{tourId:int}/services/{serviceId:int}/bus-transfer")]
    public class TourBusTransfersController : ApiController
    {
        private readonly IQueryHandler<GetTourBusTransferQuery, TourBusTransferDto> _get;
        private readonly ICommandHandler<ReplaceTourBusTransferCommand, TourBusTransferDto> _replace;

        /// <summary>Создаёт контроллер настроек автобусного переезда.</summary>
        /// <param name="get">Обработчик чтения настроек.</param>
        /// <param name="replace">Обработчик полной замены настроек.</param>
        public TourBusTransfersController(
            IQueryHandler<GetTourBusTransferQuery, TourBusTransferDto> get,
            ICommandHandler<ReplaceTourBusTransferCommand, TourBusTransferDto> replace
        )
        {
            _get = get;
            _replace = replace;
        }

        /// <summary>Получить настройки автобусного переезда выбранной услуги тура.</summary>
        /// <remarks>
        /// До первого сохранения возвращается пустая конфигурация с полными суточными
        /// диапазонами времени. Места посадки и транспорт могут быть не ограничены.
        /// </remarks>
        /// <param name="tourId">Идентификатор тура.</param>
        /// <param name="serviceId">Идентификатор услуги автобусного переезда.</param>
        /// <returns>Текущие ограничения автобусного переезда.</returns>
        [HttpGet]
        [Route("")]
        [ResponseType(typeof(TourBusTransferDto))]
        public async Task<IHttpActionResult> Get(int tourId, int serviceId)
        {
            return Ok(
                await _get.Handle(
                    new GetTourBusTransferQuery(tourId, serviceId),
                    CancellationToken.None
                )
            );
        }

        /// <summary>Создать или полностью заменить настройки автобусного переезда.</summary>
        /// <remarks>
        /// Операция атомарно заменяет страны и города отправления и прибытия, допустимые
        /// записи BusTransfers, места посадки, транспорт и временные диапазоны. Цены,
        /// ценовые пакеты и поставщики не участвуют. После сохранения увеличиваются
        /// Revision услуги и тура.
        /// </remarks>
        /// <param name="tourId">Идентификатор тура.</param>
        /// <param name="serviceId">Идентификатор услуги автобусного переезда.</param>
        /// <param name="command">Полная новая конфигурация автобусного переезда.</param>
        /// <returns>Сохранённая конфигурация.</returns>
        [HttpPut]
        [Route("")]
        [ResponseType(typeof(TourBusTransferDto))]
        public async Task<IHttpActionResult> Replace(
            int tourId,
            int serviceId,
            [FromBody] ReplaceTourBusTransferCommand command
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
