using System.Collections.Generic;
namespace TourCore.Application.Tours
{
 public class CreateTourServiceCommand { public int TourId{get;set;} public int ServiceTypeId{get;set;} public string Name{get;set;} public int? ParentServiceId{get;set;} public bool IsHidden{get;set;} public bool IsRemovable{get;set;} public bool IsOptional{get;set;} }
 public class UpdateTourServiceCommand:CreateTourServiceCommand { public int Id{get;set;} }
 public class DeleteTourServiceCommand { public DeleteTourServiceCommand(int tourId,int id){TourId=tourId;Id=id;} public int TourId{get;} public int Id{get;} }
 public class ReorderTourServicesCommand { public int TourId{get;set;} public ICollection<int> ServiceIds{get;set;}=new List<int>(); }
}
