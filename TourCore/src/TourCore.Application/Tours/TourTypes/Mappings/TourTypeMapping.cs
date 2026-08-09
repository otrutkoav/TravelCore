using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;
namespace TourCore.Application.Tours { internal static class TourTypeMapping { public static TourTypeDto ToDto(this TourType x)=>new TourTypeDto{Id=x.Id,Name=x.Name,ParentId=x.ParentId,CreatedAt=x.CreatedAt,UpdatedAt=x.UpdatedAt}; } }
