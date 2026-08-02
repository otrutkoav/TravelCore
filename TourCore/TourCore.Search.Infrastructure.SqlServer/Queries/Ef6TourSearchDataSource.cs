using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Threading;
using System.Threading.Tasks;
using TourCore.Search.Application.Abstractions;
using TourCore.Search.Application.Models;
using TourCore.Search.Contracts;
using TourCore.Search.Infrastructure.SqlServer.Persistence;

namespace TourCore.Search.Infrastructure.SqlServer.Queries
{
    public sealed class Ef6TourSearchDataSource : ITourSearchDataSource
    {
        private readonly SearchReadDbContext _db;
        public Ef6TourSearchDataSource(SearchReadDbContext db) { _db = db; }

        public async Task<SearchDataSet> LoadAsync(TourSearchRequest request, CancellationToken cancellationToken)
        {
            var cityValue = request.DestinationCityId.HasValue
                ? (object)request.DestinationCityId.Value
                : DBNull.Value;
            var hotels = await _db.Database.SqlQuery<SearchHotelRow>(TourSearchSql.Hotels,
                new SqlParameter("@countryId", request.DestinationCountryId),
                new SqlParameter("@cityId", cityValue)).ToListAsync(cancellationToken);
            var transfers = await _db.Database.SqlQuery<SearchTransferRow>(TourSearchSql.Transfers,
                new SqlParameter("@countryId", request.DestinationCountryId),
                new SqlParameter("@cityId", cityValue)).ToListAsync(cancellationToken);
            var routes = request.DepartureCityId == -1
                ? new List<SearchRouteRow>()
                : await _db.Database.SqlQuery<SearchRouteRow>(TourSearchSql.Routes).ToListAsync(cancellationToken);
            return new SearchDataSet { Hotels = hotels, Routes = routes, Transfers = transfers };
        }
    }
}
