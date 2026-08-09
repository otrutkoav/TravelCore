using System;
using System.Collections.Generic;
namespace TourCore.Application.Tours
{
 public abstract class TourCommandBase { public string Name{get;set;} public int CurrencyId{get;set;} public int DateMode{get;set;} public DateTime? AvailableDateFrom{get;set;} public DateTime? AvailableDateTo{get;set;} public ICollection<DateTime> AvailableDates{get;set;}=new List<DateTime>(); public ICollection<int> TourTypeIds{get;set;}=new List<int>(); public string DescriptionUrl{get;set;} public string DescriptionHtml{get;set;} public string BookingConditionsHtml{get;set;} }
 public class CreateTourCommand:TourCommandBase{}
 public class UpdateTourCommand:TourCommandBase { public int Id{get;set;} public int Status{get;set;} }
 public class DeleteTourCommand { public DeleteTourCommand(int id){Id=id;} public int Id{get;} }
}
