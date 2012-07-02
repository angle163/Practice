using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using Practice.Extension;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Core
{
    /// <summary>
    /// The module scanner
    /// </summary>
    public class YafModuleScanner
    {
        #region Public Methods

        /// <summary>
        /// The get modules.
        /// </summary>
        /// <param name="pattern"> The pattern. </param>
        /// <returns></returns>
        [NotNull]
        public IEnumerable<Assembly> GetModules([NotNull] string pattern)
        {
            var files = GetMatchingFiles(pattern).ToList();

            return GetValidateAssemblies(files).ToList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// The clean path.
        /// </summary>
        /// <param name="path"> The path. </param>
        /// <returns> The clean path. </returns>
        [NotNull]
        private static string CleanPath([NotNull] string path)
        {
            if (!Path.IsPathRooted(path))
            {
                path = Path.Combine(GetAppBaseDirectory(), path);
            }

            return Path.GetFullPath(path);
        }

        /// <summary>
        /// The get app base directory.
        /// </summary>
        /// <returns> The get app base directory. </returns>
        [NotNull]
        private static string GetAppBaseDirectory()
        {
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string searchPath = AppDomain.CurrentDomain.RelativeSearchPath;

            return searchPath.IsNotSet() ? baseDirectory : Path.Combine(baseDirectory, searchPath);
        }

        /// <summary>
        /// The get matching files.
        /// </summary>
        /// <param name="pattern"> The pattern. </param>
        /// <returns></returns>
        [NotNull]
        private static IEnumerable<string> GetMatchingFiles([NotNull] string pattern)
        {
            string path = CleanPath(Path.GetDirectoryName(pattern));
            string glob = Path.GetFileName(pattern);

            return Directory.GetFiles(path, glob);
        }

        /// <summary>
        /// The load and validate assemblies.
        /// </summary>
        /// <param name="filenames"> The filenames. </param>
        /// <returns></returns>
        private static IEnumerable<Assembly> GetValidateAssemblies([NotNull] IEnumerable<string> filenames)
        {
            CodeContract.ArgumentNotNull(filenames, "filenames");

            foreach (var assemblyFile in filenames.Where(File.Exists))
            {
                Assembly assembly;

                try
                {
                    assembly = Assembly.LoadFrom(assemblyFile);
                }
                catch (BadImageFormatException)
                {
                    // fail on native images...
                    continue;
                }

                yield return assembly;
            }
        }

        #endregion
    }
}