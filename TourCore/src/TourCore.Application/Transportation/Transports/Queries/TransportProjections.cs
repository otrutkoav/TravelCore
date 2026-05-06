using System;
using System.Linq.Expressions;
using TourCore.Contracts.Transportation.Transports;
using TourCore.Domain.Transportation.Entities;

namespace TourCore.Application.Transportation.Transports.Queries
{
    internal static class TransportProjections
    {
        public static readonly Expression<Func<Transport, TransportListItemDto>> ListItem =
            x => new TransportListItemDto
            {
                Id = x.Id,
                Name = x.Name,
                NameEn = x.NameEn,
                SeatsCount = x.SeatsCount,
                ShowOrder = x.ShowOrder
            };
    }
}