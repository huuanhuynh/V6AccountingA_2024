using System;
using System.Collections.Generic;


namespace V6Soft.Common.ModelFactory.Managers
{
    /// <summary>
    ///     When implemented, provides methods to load model definitions.
    /// </summary>
    public interface IModelDefinitionManager
    {
        /// <summary>
        ///     Gets model definition from cached memory if LoadAll method was called 
        ///     earlier. Otherwise, reads data source and load definitions with
        ///     specified name.
        ///     <para/> Returns null if there is no result.
        /// </summary>
        /// <param name="modelName">Model definition name, must not be null.</param>
        ModelDefinition this[string modelName] { get; }
        
        /// <summary>
        ///     Gets model definition from cached memory if LoadAll method was called 
        ///     earlier. Otherwise, reads data source and load definitions with
        ///     specified name.
        ///     <para/> 
        ///     Returns null if there is no result or 
        ///     <paramref name="definitionIndex"/> is out of range.
        /// </summary>
        /// <param name="definitionIndex">Model definition index.</param>
        ModelDefinition this[ushort definitionIndex] { get; }

        /// <summary>
        ///     Adds new fields to model.
        ///     <para/>Returns true if successfull, false if not.
        /// </summary>
        /// <param name="modelIndex">Model index.</param>
        /// <param name="newFields">New field definition.</param>
        /// <returns></returns>
        bool AddField(ushort modelIndex, IList<ModelFieldDefinition> newFields);

        /// <summary>
        ///     Gets model definition from cached memory if LoadAll method was called 
        ///     earlier. Otherwise, reads data source and load definitions with
        ///     specified name.
        ///     <para/> Returns null if there is no result.
        /// </summary>
        /// <param name="modelName">Model definition name, must not be null.</param>
        ModelDefinition Load(string modelName);

        /// <summary>
        ///     Gets model definition from cached memory if LoadAll method was called 
        ///     earlier. Otherwise, reads data source and load definitions with
        ///     specified name.
        ///     <para/> Returns null if there is no result.
        /// </summary>
        /// <param name="definitionIndex">Model definition index.</param>
        ModelDefinition Load(ushort definitionIndex);
        
        /// <summary>
        ///     Reads data source to loads all model definitions and caches to memory.
        ///     <para/> Returns null if there is no result.
        /// </summary>
        System.Collections.Generic.IList<ModelDefinition> LoadAll();

        /// <summary>
        ///     Gets definition mapping at spefified <paramref name="index"/>.
        ///     <para/>Returns null if there is no mapping or <param name="index"/>
        ///     is out of range.
        /// </summary>
        ModelMap GetMapping(ushort index);

        /// <summary>
        ///     Gets a copy of definition mapping list.
        /// </summary>
        IList<ModelMap> GetAllMappings();
    }
}
