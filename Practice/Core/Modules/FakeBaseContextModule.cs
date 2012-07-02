using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using Autofac;
using Autofac.Core;
using Practice.Extension;
using Practice.Types;
using Practice.Types.Annotation;
using Practice.Types.Interface;

namespace Practice.Core.Modules
{
    /// <summary>
    /// The module for all singleton scoped items...
    /// </summary>
    public class FakeBaseContainerModule : IModule, IHaveComponentRegistry
    {
        #region Properties

        /// <summary>
        ///   Gets or sets ExtensionAssemblies.
        /// </summary>
        public IList<Assembly> ExtensionAssemblies { get; protected set; }

        /// <summary>
        ///   Gets or sets ComponentRegistry.
        /// </summary>
        public IComponentRegistry ComponentRegistry { get; set; }

        #endregion

        #region Implemented Interfaces

        #region IModule

        /// <summary>
        /// Apply the module to the component registry.
        /// </summary>
        /// <param name="componentRegistry">
        /// Component registry to apply configuration to.
        /// </param>
        public void Configure([NotNull] IComponentRegistry componentRegistry)
        {
            CodeContract.ArgumentNotNull(componentRegistry, "componentRegistry");

            ComponentRegistry = componentRegistry;

            ExtensionAssemblies =
                new YafModuleScanner().GetModules("*.dll").OrderByDescending(x => x.GetAssemblySortOrder()).ToList();

            RegisterBasicBindings();
            RegisterWebAbstractions();
        }

        #endregion

        #endregion

        #region Methods

        /// <summary>
        /// The register basic bindings.
        /// </summary>
        private void RegisterBasicBindings()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<AutoFacServiceLocatorProvider>().AsSelf()
                .As<IServiceLocator>().As<IInjectService>().InstancePerLifetimeScope();

            // Http Application Base
            builder.RegisterType<CurrentHttpApplicationStateBaseProvider>().SingleInstance().PreserveExistingDefaults();
            builder.Register(k => k.Resolve<CurrentHttpApplicationStateBaseProvider>().Instance)
                .ExternallyOwned().PreserveExistingDefaults();

            this.UpdateRegistry(builder);
        }

        /// <summary>
        /// The register web abstractions.
        /// </summary>
        private void RegisterWebAbstractions()
        {
            var builder = new ContainerBuilder();

            builder.Register(c => new HttpContextWrapper(HttpContext.Current))
                .As<HttpContextBase>().InstancePerYafContext();

            builder.Register(c => c.Resolve<HttpContextBase>().Request)
                .As<HttpRequestBase>().InstancePerYafContext();

            builder.Register(c => c.Resolve<HttpContextBase>().Response)
                .As<HttpResponseBase>().InstancePerYafContext();

            builder.Register(c => c.Resolve<HttpContextBase>().Server)
                .As<HttpServerUtilityBase>().InstancePerYafContext();

            builder.Register(c => c.Resolve<HttpContextBase>().Session)
                .As<HttpSessionStateBase>().InstancePerYafContext();

            this.UpdateRegistry(builder);
        }

        #endregion
    }
}