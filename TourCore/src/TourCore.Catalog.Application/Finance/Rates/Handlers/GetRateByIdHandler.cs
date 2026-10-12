using System.Threading;
using System.Threading.Tasks;
using TourCore.Application.Abstractions;
using TourCore.Application.Common.Exceptions;
using TourCore.Contracts.Finance.Rates;
using TourCore.Application.Abstractions.Persistence.Finance;
using TourCore.Application.Finance.Rates.Mappings;
using TourCore.Application.Finance.Rates.Queries;
using TourCore.Application.Common.Errors;

namespace TourCore.Application.Finance.Rates.Handlers
{
    public class GetRateByIdHandler : IQueryHandler<GetRateByIdQuery, RateDto>
    {
        private readonly IRateRepository _rateRepository;

        public GetRateByIdHandler(IRateRepository rateRepository)
        {
            _rateRepository = rateRepository;
        }

        public async Task<RateDto> Handle(GetRateByIdQuery query, CancellationToken cancellationToken)
        {
            var entity = await _rateRepository.GetByIdAsync(query.Id, cancellationToken);
            if (entity == null)
                throw new NotFoundException(ErrorMessages.RateNotFound, ErrorCode.RateNotFound);

            return entity.ToDto();
        }
    }
}