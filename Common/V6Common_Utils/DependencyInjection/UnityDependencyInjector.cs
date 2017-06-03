using System;
using System.Collections.Generic;
using Microsoft.Practices.Unity;



namespace V6Soft.Common.Utils.DependencyInjection
{
    /// <summary>
    /// A wrapper for the Unity's dependecny injection container.
    /// </summary>
    public sealed class UnityDependencyInjector : IDependencyInjector
    {
        private readonly IUnityContainer m_UnityContainer;
        
        /// <summary>
        /// Creates a new instance of UnityDependencyInjectionContainer from an existing container.
        /// </summary>
        public UnityDependencyInjector (IUnityContainer unityContainer)
        {
            Guard.ArgumentNotNull(unityContainer, "unityContainer");

            m_UnityContainer = unityContainer;
        }

        /// <summary>
        /// Creates a new instance of UnityDependencyInjectionContainer.
        /// </summary>
        public UnityDependencyInjector()
        {
            m_UnityContainer = new UnityContainer();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.RegisterType"/>
        /// </summary>
        public void RegisterType<TFrom, TTo>() where TTo : TFrom
        {
            m_UnityContainer.RegisterType<TFrom, TTo>();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.RegisterType"/>
        /// </summary>
        public void RegisterType<TFrom, TTo>(params object[] constructorParams) 
            where TTo : TFrom
        {
            m_UnityContainer.RegisterType<TFrom, TTo>(
                new InjectionConstructor(constructorParams)
            );
        }

        /// <summary>
        /// <see cref="IDependencyInjector.IsRegistered"/>
        /// </summary>
        public bool IsRegistered<T>()
        {
            return m_UnityContainer.IsRegistered<T>();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.RegisterSingletonType"/>
        /// </summary>
        public void RegisterSingletonType<TFrom, TTo>() where TTo : TFrom
        {
            m_UnityContainer.RegisterType<TFrom, TTo>(new ContainerControlledLifetimeManager());
        }

        /// <summary>
        /// <see cref="IDependencyInjector.RegisterSingletonType"/>
        /// </summary>
        public void RegisterSingletonType<TFrom1, TFrom2, TTo>() where TTo : TFrom1, TFrom2
        {
            m_UnityContainer.RegisterType<TTo>(new ContainerControlledLifetimeManager())
                        .RegisterType<TFrom1, TTo>()
                        .RegisterType<TFrom2, TTo>();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.RegisterSingletonType"/>
        /// </summary>
        public void RegisterSingletonType<T>()
        {
            m_UnityContainer.RegisterType<T>(new ContainerControlledLifetimeManager());
        }


        /// <summary>
        /// <see cref="IDependencyInjector.RegisterInstance"/>
        /// </summary>
        public void RegisterInstance<T>(T instance)
        {
            m_UnityContainer.RegisterInstance(instance);
        }

        /// <summary>
        /// <see cref="IDependencyInjector.Resolve"/>
        /// </summary>
        public T Resolve<T>()
        {
            return m_UnityContainer.Resolve<T>();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.Resolve(Type type)"/>
        /// </summary>
        public object Resolve(Type type)
        {
            return m_UnityContainer.Resolve(type);
        }

        /// <summary>
        /// <see cref="IDependencyInjector.CreateChildInjector"/>
        /// </summary>
        public IDependencyInjector CreateChildInjector()
        {
            return new UnityDependencyInjector(m_UnityContainer.CreateChildContainer());
        }

        /// <summary>
        /// <see cref="IDependencyInjector.Dispose"/>
        /// </summary>
        public void Dispose()
        {
            m_UnityContainer.Dispose();
        }

        /// <summary>
        /// <see cref="IDependencyInjector.ResolveAll"/>
        /// </summary>
        public IEnumerable<T> ResolveAll<T>()
        {
            return m_UnityContainer.ResolveAll<T>();
        }
        
        /// <summary>
        /// <see cref="IDependencyInjector.ResolveAll"/>
        /// </summary>
        public IEnumerable<object> ResolveAll(Type type)
        {
            return m_UnityContainer.ResolveAll(type);
        }
    }
}
