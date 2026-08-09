using System;
using System.Collections.Generic;
using TourCore.Domain.Common.Exceptions;
using TourCore.Shared.Primitives;
namespace TourCore.Domain.Tours
{
    public class TourType : AuditableEntity
    {
        public string Name { get; protected set; } public int? ParentId { get; protected set; } public virtual TourType Parent { get; protected set; } public virtual ICollection<TourType> Children { get; protected set; } public virtual ICollection<Tour> Tours { get; protected set; }
        protected TourType(){Children=new List<TourType>();Tours=new List<Tour>();} public TourType(string name,int? parentId,DateTime createdAt):this(){SetName(name);ParentId=parentId;SetCreated(createdAt);} public void Update(string name,int? parentId,DateTime updatedAt){if(parentId==Id)throw new DomainException("Tour type cannot be its own parent.");SetName(name);ParentId=parentId;SetUpdated(updatedAt);} private void SetName(string value){if(string.IsNullOrWhiteSpace(value))throw new DomainException("Tour type name is required.");value=value.Trim();if(value.Length>128)throw new DomainException("Tour type name must be 128 characters or less.");Name=value;}
    }
}
