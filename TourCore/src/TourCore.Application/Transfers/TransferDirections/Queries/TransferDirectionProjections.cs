using System;
using System.Linq.Expressions;
using TourCore.Contracts.Transfers.TransferDirections;
using TourCore.Domain.Transfers.Entities;

namespace TourCore.Application.Transfers.TransferDirections.Queries
{
    internal static class TransferDirectionProjections
    {
        public static readonly Expression<Func<TransferDirection, TransferDirectionListItemDto>> ListItem =
            x => new TransferDirectionListItemDto
            {
                Id = x.Id,
                Name = x.Name
            };
    }
}