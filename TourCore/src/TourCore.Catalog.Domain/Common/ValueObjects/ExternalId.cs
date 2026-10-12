using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourCore.Domain.Common.ValueObjects
{
    public class ExternalId
    {
        public string Source { get; }
        public string Value { get; }

        public ExternalId(string source, string value)
        {
            Source = source;
            Value = value;
        }
    }
}
