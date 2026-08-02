namespace TourCore.Search.Infrastructure.SqlServer.Queries
{
    internal static class TourSearchSql
    {
        public const string Hotels = @"
SELECT h.Id,h.CountryId,c.Name CountryName,h.CityId,ci.Name CityName,h.Code,h.Name,h.Stars,
       h.CategoryId,hc.Name CategoryName,h.ResortId,r.Name ResortName,h.Address,h.Rank,h.SortOrder
FROM Hotels h
JOIN Countries c ON c.Id=h.CountryId
JOIN Cities ci ON ci.Id=h.CityId
LEFT JOIN HotelCategories hc ON hc.Id=h.CategoryId
LEFT JOIN Resorts r ON r.Id=h.ResortId
WHERE h.CountryId=@countryId AND (@cityId IS NULL OR h.CityId=@cityId)
ORDER BY h.CityId,h.SortOrder,h.Id";

        public const string Transfers = @"
SELECT t.Id,t.CityId,t.Name,t.DurationText,t.PlaceFrom,t.PlaceTo,t.ShowOrder
FROM Transfers t JOIN Cities c ON c.Id=t.CityId
WHERE t.CityId IS NOT NULL AND c.CountryId=@countryId AND (@cityId IS NULL OR t.CityId=@cityId)
ORDER BY t.CityId,t.ShowOrder,t.Id";

        public const string Routes = @"
SELECT CAST(1 AS int) Kind,ch.Id RouteId,s.Id ScheduleId,ch.FlightNumber Name,
       ch.DepartureCityId,dc.Name DepartureCityName,ch.ArrivalCityId,ac.Name ArrivalCityName,
       s.DateFrom,s.DateTo,s.DaysOfWeek DaysOfWeek,s.TimeFrom,s.TimeTo,
       CAST(CASE WHEN s.IsNextDayArrival=1 THEN 1 ELSE 0 END AS smallint) DaysOnRoad,
       ch.AirlineCode CarrierCode,ch.FlightNumber Number
FROM CharterSeasons s JOIN Charters ch ON ch.Id=s.CharterId
JOIN Cities dc ON dc.Id=ch.DepartureCityId JOIN Cities ac ON ac.Id=ch.ArrivalCityId
UNION ALL
SELECT CAST(2 AS int),bt.Id,bs.Id,bt.Name,bt.CityFromId,dc.Name,bt.CityToId,ac.Name,
       bs.DateFrom,bs.DateTo,bs.DaysOfWeek,bs.TimeFrom,bs.TimeTo,bs.DaysOnRoad,NULL,NULL
FROM BusSchedules bs JOIN BusTransfers bt ON bt.Id=bs.BusTransferId
JOIN Cities dc ON dc.Id=bt.CityFromId JOIN Cities ac ON ac.Id=bt.CityToId
UNION ALL
SELECT CAST(3 AS int),rt.Id,ts.Id,rt.Name,rt.CityFromId,dc.Name,rt.CityToId,ac.Name,
       ts.DateFrom,ts.DateTo,ts.DaysOfWeek,ts.TimeFrom,ts.TimeTo,ts.DaysOnRoad,NULL,NULL
FROM TrainSchedules ts JOIN RailwayTransfers rt ON rt.Id=ts.RailwayTransferId
JOIN Cities dc ON dc.Id=rt.CityFromId JOIN Cities ac ON ac.Id=rt.CityToId";
    }
}
