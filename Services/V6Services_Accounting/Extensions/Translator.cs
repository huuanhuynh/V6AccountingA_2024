using System;
using System.Collections.Generic;
using System.Linq;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Accounting.Extensions
{
    public static class Translator
    {
        public static IList<FieldIndex> ToFieldIndeses(this IList<byte> fields)
        {
            return fields
                .Select(f => (FieldIndex)Enum.Parse(typeof(FieldIndex), f + ""))
                .ToList();
        }
    }
}
