using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using Practice.Extension;
using Practice.Pattern;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Core
{
    /// <summary>
    /// The auto fac service locator provider.
    /// </summary>
    public class AutoFacServiceLocatorProvider : IServiceLocator, IInjectService
    {
        #region Constants and Fields

        /// <summary>
        ///   The default flags.
        /// </summary>
        private const BindingFlags DefaultFlags = BindingFlags.Public | BindingFlags.SetProperty | BindingFlags.Instance;

        /// <summary>
        /// The _injection cache.
        /// </summary>
        private static readonly IThreadSafeDictionary<KeyValuePair<Type, Type>, IList<PropertyInfo>> _injectionCache =
            new ThreadSafeDictionary<KeyValuePair<Type, Type>, IList<PropertyInfo>>();

        #endregion

        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="AutoFacServiceLocatorProvider"/> class.
        /// </summary>
        /// <param name="container"> The container. </param>
        public AutoFacServiceLocatorProvider([NotNull] ILifetimeScope container)
        {
            CodeContract.ArgumentNotNull(container, "container");

            Container = container;
        }

        #endregion

        #region Properties

        /// <summary>
        ///   Gets or sets Container.
        /// </summary>
        public ILifetimeScope Container { get; set; }

        #endregion

        #region Implemented Interfaces

        #region IInjectService

        /// <summary>
        /// Inject an object with services.
        /// </summary>
        /// <typeparam name="TAttribute">
        /// TAttribute is the attribute that marks properties to inject to.
        /// </typeparam>
        /// <param name="instance">
        /// the object to inject
        /// </param>
        public void InjectMarked<TAttribute>(object instance) where TAttribute : Attribute
        {
            CodeContract.ArgumentNotNull(instance, "instance");

            // Container.InjectUnsetProperties(instance);
            Type type = instance.GetType();
            Type attributeType = typeof(TAttribute);

            var keyPair = new KeyValuePair<Type, Type>(type, attributeType);

            IList<PropertyInfo> properties;

            if (!_injectionCache.TryGetValue(keyPair, out properties))
            {
                // find them...
                properties =
                    type.GetProperties(DefaultFlags).Where(
                        p =>
                        p.GetSetMethod(false) != null && p.GetIndexParameters().Count() == 0 &&
                        p.IsDefined(attributeType, true)).
                        ToList();

                _injectionCache.MergeSafe(keyPair, properties);
            }

            foreach (PropertyInfo injectProp in properties)
            {
                object serviceInstance;

                if (injectProp.PropertyType == typeof(ILogger))
                {
                    // we're getting the logger via the logger factory...
                    serviceInstance = Container.Resolve<ILoggerProvider>().Create(injectProp.DeclaringType);
                }
                else
                {
                    serviceInstance = Container.Resolve(injectProp.PropertyType);
                }

                // set value is super slow... best not to use it very much.
                injectProp.SetValue(instance, serviceInstance, null);
            }
        }

        #endregion

        #region IServiceLocator

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <returns> The get. </returns>
        public object Get(Type serviceType)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");

            return Container.Resolve(serviceType);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <param name="parameters"> The parameters. </param>
        /// <returns> The get. </returns>
        /// <exception cref="NotSupportedException">
        /// <c>NotSupportedException</c>.
        /// </exception>
        public object Get(Type serviceType, IEnumerable<IServiceLocationParameter> parameters)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");
            CodeContract.ArgumentNotNull(parameters, "parameters");

            return Container.Resolve(serviceType, ConvertToAutofacParameters(parameters));
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <param name="named"> The named. </param>
        /// <returns> The get. </returns>
        public object Get(Type serviceType, string named)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");
            CodeContract.ArgumentNotNull(named, "named");

            return Container.ResolveNamed(named, serviceType);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <param name="named"> The named. </param>
        /// <param name="parameters"> The parameters. </param>
        /// <returns> The get. </returns>
        public object Get(Type serviceType, string named, IEnumerable<IServiceLocationParameter> parameters)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");
            CodeContract.ArgumentNotNull(named, "named");
            CodeContract.ArgumentNotNull(parameters, "parameters");

            return Container.ResolveNamed(named, serviceType, ConvertToAutofacParameters(parameters));
        }

        /// <summary>
        /// The try get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <param name="instance"> The instance. </param>
        /// <returns> The try get. </returns>
        public bool TryGet(Type serviceType, [NotNull] out object instance)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");

            return Container.TryResolve(out instance);
        }

        /// <summary>
        /// The try get.
        /// </summary>
        /// <param name="serviceType"> The service type. </param>
        /// <param name="named"> The named. </param>
        /// <param name="instance"> The instance. </param>
        /// <returns> The try get. </returns>
        public bool TryGet(Type serviceType, string named, [NotNull] out object instance)
        {
            CodeContract.ArgumentNotNull(serviceType, "serviceType");
            CodeContract.ArgumentNotNull(named, "named");

            return Container.TryResolve(out instance);
        }

        #endregion

        #region IServiceProvider

        /// <summary>
        /// Gets the service object of the specified type.
        /// </summary>
        /// <returns>
        ///  A service object of type <paramref name="serviceType"/>.
        ///   -or- 
        ///   null if there is no service object of type <paramref name="serviceType"/>. 
        /// </returns>
        /// <param name="serviceType">
        /// An object that specifies the type of service object to get.
        /// </param>
        /// <filterpriority>2</filterpriority>
        [CanBeNull]
        public object GetService([NotNull] Type serviceType)
        {
            object instance;

            return TryGet(serviceType, out instance) ? instance : null;
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The convert to autofac parameters.
        /// </summary>
        /// <param name="parameters"> The parameters. </param>
        /// <exception cref="NotSupportedException">
        /// <c>NotSupportedException</c>.
        /// </exception>
        [NotNull]
        private static IEnumerable<Parameter> ConvertToAutofacParameters(
            [NotNull] IEnumerable<IServiceLocationParameter> parameters)
        {
            CodeContract.ArgumentNotNull(parameters, "parameters");

            var autoParams = new List<Parameter>();

            foreach (IServiceLocationParameter parameter in parameters)
            {
                if (parameter is NamedParameter)
                {
                    var param = parameter as NamedParameter;
                    autoParams.Add(new Autofac.NamedParameter(param.Name, param.Value));
                }
                else if (parameter is TypedParameter)
                {
                    var param = parameter as TypedParameter;
                    autoParams.Add(new Autofac.TypedParameter(param.Type, param.Value));
                }
                else
                {
                    throw new NotSupportedException(
                        "Parameter Type of {0} is not supported.".FormatWith(parameter.GetType()));
                }
            }

            return autoParams;
        }

        #endregion
    }
}