using System;
using System.Collections.Generic;



namespace V6Soft.Common.Utils.DependencyInjection
{
    public interface IDependencyInjector : IDisposable
    {
        /// <summary>
        ///     Registers a type mapping. 
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested.</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned.</typeparam>
        void RegisterType<TFrom, TTo>() where TTo : TFrom;
        
        /// <summary>
        ///     Registers a type mapping with specified constructor parameters.
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested.</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned.</typeparam>
        /// <param name="constructorParams">Constructor parameters</param>
        void RegisterType<TFrom, TTo>(params object[] constructorParams)
            where TTo : TFrom;

        /// <summary>
        /// Returns true if a type is registered in the container.
        /// </summary>
        /// <typeparam name="T">Type that will be checked for.</typeparam>
        /// <returns></returns>
        bool IsRegistered<T>();

        /// <summary>
        /// Register a singleton type mapping.
        /// </summary>
        /// <typeparam name="TFrom">Type that will be requested.</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned.</typeparam>
        void RegisterSingletonType<TFrom, TTo>() where TTo : TFrom;

        /// <summary>
        /// Register a mapping for two types to a singleton.
        /// </summary>
        /// <typeparam name="TFrom1">First type that will be requested.</typeparam>
        /// /// <typeparam name="TFrom2">Second type that will be requested.</typeparam>
        /// <typeparam name="TTo">Type that will actually be returned.</typeparam>
        void RegisterSingletonType<TFrom1, TFrom2, TTo>() where TTo : TFrom1, TFrom2;

        /// <summary>
        /// Registers a singleton type.
        /// </summary>
        /// <typeparam name="T">The type to register.</typeparam>
        void RegisterSingletonType<T>();

        /// <summary>
        /// Register an instance.
        /// </summary>
        /// <param name="instance">Object to be returned.</param>
        /// <typeparam name="T">Type of instance to register (may be an implemented interface instead of the full type).</typeparam>
        void RegisterInstance<T>(T instance);

        /// <summary>
        /// Resolve an instance of the default requested type.
        /// </summary>
        /// <typeparam name="T">Type of object to get from the container.</typeparam>
        /// <returns>The retrieved object.</returns>
        T Resolve<T>();

        /// <summary>
        /// Resolve an instance of the default requested type.
        /// </summary>
        /// <param name="type">Type of object to get from the container.</param>
        /// <returns>The retrieved object.</returns>
        object Resolve(Type type);

        /// <summary>
        /// Creates an instance of child injector.
        /// </summary>
        IDependencyInjector CreateChildInjector();

        /// <summary>
        /// Returns instances of all registered types requested.
        /// <para/> This method is useful if you've registered multiple types with the same 
        /// System.Type but different names.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        IEnumerable<T> ResolveAll<T>();


        /// <summary>
        /// Returns instances of all registered types requested.
        /// <para/> This method is useful if you've registered multiple types with the same 
        /// System.Type but different names.
        /// </summary>
        IEnumerable<object> ResolveAll(Type type);
    }
}
