using System.Linq;
using TourCore.Contracts.Tours;
using TourCore.Domain.Tours;
namespace TourCore.Application.Tours { internal static class TourMapping { public static TourDto ToDto(this Tour x)=>new TourDto{Id=x.Id,Name=x.Name,Status=(int)x.Status,Revision=x.Revision,CurrencyId=x.CurrencyId,DateMode=(int)x.DateMode,AvailableDateFrom=x.AvailableDateFrom,AvailableDateTo=x.AvailableDateTo,AvailableDates=x.AvailableDates.OrderBy(d=>d.Date).Select(d=>d.Date).ToArray(),TourTypes=x.TourTypes.OrderBy(t=>t.Name).Select(t=>new TourTypeListItemDto{Id=t.Id,Name=t.Name,ParentId=t.ParentId}).ToArray(),DescriptionUrl=x.DescriptionUrl,DescriptionHtml=x.DescriptionHtml,BookingConditionsHtml=x.BookingConditionsHtml,CreatedAt=x.CreatedAt,UpdatedAt=x.UpdatedAt}; } }
