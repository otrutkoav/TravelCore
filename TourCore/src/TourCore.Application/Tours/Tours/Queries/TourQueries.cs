using TourCore.Application.Common.Queries;
namespace TourCore.Application.Tours { public class GetTourByIdQuery{public GetTourByIdQuery(int id){Id=id;}public int Id{get;}} public class GetToursQuery:PagedQuery { public string Search{get;set;} public int? Status{get;set;} public int? CurrencyId{get;set;} } }
