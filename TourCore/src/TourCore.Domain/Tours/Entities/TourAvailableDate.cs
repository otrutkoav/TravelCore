using System;
using TourCore.Shared.Primitives;
namespace TourCore.Domain.Tours { public class TourAvailableDate:Entity { public int TourId{get;protected set;} public DateTime Date{get;protected set;} public virtual Tour Tour{get;protected set;} protected TourAvailableDate(){} internal TourAvailableDate(DateTime date){Date=date.Date;} } }
