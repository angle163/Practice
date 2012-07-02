using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    /// <summary>
    /// The assembly extensions.
    /// </summary>
    public static class AssemblyExtensions
    {
        #region Public Methods

        [NotNull]
        public static IEnumerable<Type> FindModules<T>([NotNull] this IEnumerable<Assembly> assemblies)
        {
            CodeContract.ArgumentNotNull(assemblies, "assemblies");

            var moduleClassTypes = new List<Type>();
            var implementedInterfaceType = typeof(T);

            // get classes...
            foreach (
                var types in
                    assemblies.Select(
                        a =>
                        a.GetExportedTypes().Where(t => !t.IsAbstract).ToList()))
            {
                moduleClassTypes.AddRange(types.Where(implementedInterfaceType.IsAssignableFrom));
            }

            return moduleClassTypes.Distinct();
        }

        [NotNull]
        public static IEnumerable<Type> FindClassesWithAttribute<T>([NotNull] this IEnumerable<Assembly> assemblies)
            where T : Attribute
        {
            CodeContract.ArgumentNotNull(assemblies, "assemblies");

            var moduleClassTypes = new List<Type>();
            var attributeType = typeof(T);

            // get classes...
            foreach (
                var types in
                    assemblies.Select(
                        a =>
                        a.GetExportedTypes().Where(
                            t => !t.IsAbstract && t.GetCustomAttributes(attributeType, true).Any()).ToList()))
            {
                moduleClassTypes.AddRange(types);
            }

            return moduleClassTypes.Distinct();
        }

        /// <summary>
        /// The get assembly sort order.
        /// </summary>
        /// <param name="assembly">
        /// The assembly.
        /// </param>
        /// <returns>
        /// The get assembly sort order.
        /// </returns>
        public static int GetAssemblySortOrder([NotNull] this Assembly assembly)
        {
            CodeContract.ArgumentNotNull(assembly, "assembly");

            var attribute =
                assembly.GetCustomAttributes(typeof(AssemblyModuleSortOrder), true).OfType<AssemblyModuleSortOrder>();

            return attribute.Any() ? attribute.First().SortOrder : 9999;
        }

        /// <summary>
        /// The find modules.
        /// </summary>
        /// <param name="assemblies">
        /// The assemblies.
        /// </param>
        /// <param name="namespaceName">
        /// The module namespace.
        /// </param>
        /// <param name="implementedInterfaceName">
        /// The module base interface.
        /// </param>
        /// <returns></returns>
        [NotNull]
        public static IEnumerable<Type> FindModules(
            [NotNull] this IEnumerable<Assembly> assemblies,
            [NotNull] string namespaceName,
            [NotNull] string implementedInterfaceName)
        {
            CodeContract.ArgumentNotNull(assemblies, "assemblies");
            CodeContract.ArgumentNotNull(namespaceName, "namespaceName");
            CodeContract.ArgumentNotNull(implementedInterfaceName, "implementedInterfaceName");

            var moduleClassTypes = new List<Type>();
            var implementedInterfaceType = Type.GetType(implementedInterfaceName);

            // get classes...
            foreach (
                var types in
                    assemblies.OfType<Assembly>().Select(
                        a =>
                        a.GetExportedTypes().Where(
                            t => t.Namespace != null && !t.IsAbstract && t.Namespace.Equals(namespaceName))
                            .ToList()))
            {
                moduleClassTypes.AddRange(types.Where(implementedInterfaceType.IsAssignableFrom));
            }

            return moduleClassTypes.Distinct();
        }

        #endregion
    }
}