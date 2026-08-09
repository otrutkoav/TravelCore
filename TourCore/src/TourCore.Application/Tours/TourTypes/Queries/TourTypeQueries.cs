using TourCore.Application.Common.Queries;
namespace TourCore.Application.Tours { public class GetTourTypeByIdQuery { public GetTourTypeByIdQuery(int id){Id=id;} public int Id{get;} } public class GetTourTypesQuery:PagedQuery { public string Search{get;set;} public int? ParentId{get;set;} public bool? RootOnly{get;set;} } }
