using System;
using System.Collections.Generic;
using System.Linq;

using FieldIndex = V6Soft.Models.Core.Constants.DefinitionIndex.Field;


namespace V6Soft.Services.Assistant.Extensions
{
    /// <summary>
    ///     Translator for requests from AssistantService
    /// </summary>
    public static class Translator
    {

        /// <summary>
        ///     Translate to fieldIndex
        /// </summary>
        public static IList<FieldIndex> ToFieldIndeses(this IList<byte> fields)
        {
            return fields
                .Select(f => (FieldIndex)Enum.Parse(typeof(FieldIndex), f + ""))
                .ToList();
        }
    }
}
