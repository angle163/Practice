using Autofac.Builder;
using Practice.Types;
using Practice.Types.Annotation;

namespace Practice.Extension
{
    /// <summary>
    /// The i registration builder extension.
    /// </summary>
    public static class RegistrationBuilderExtension
    {
        #region Public Methods

        /// <summary>
        /// The owned by yaf context.
        /// </summary>
        /// <param name="builder"> The builder. </param>
        /// <typeparam name="TLimit"></typeparam>
        /// <typeparam name="TActivatorData"></typeparam>
        /// <typeparam name="TRegistrationStyle"></typeparam>
        /// <returns> The instance per yaf context. </returns>
        public static IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> InstancePerYafContext
          <TLimit, TActivatorData, TRegistrationStyle>(
          [NotNull] this IRegistrationBuilder<TLimit, TActivatorData, TRegistrationStyle> builder)
        {
            CodeContract.ArgumentNotNull(builder, "builder");

            return builder.InstancePerMatchingLifetimeScope(FakeLifetimeScope.Context);
        }

        #endregion
    }
}