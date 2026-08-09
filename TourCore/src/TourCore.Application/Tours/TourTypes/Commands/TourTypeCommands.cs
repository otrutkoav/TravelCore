namespace TourCore.Application.Tours
{
 public class CreateTourTypeCommand { public string Name{get;set;} public int? ParentId{get;set;} }
 public class UpdateTourTypeCommand { public int Id{get;set;} public string Name{get;set;} public int? ParentId{get;set;} }
 public class DeleteTourTypeCommand { public DeleteTourTypeCommand(int id){Id=id;} public int Id{get;} }
}
