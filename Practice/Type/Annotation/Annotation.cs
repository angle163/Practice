using System;

namespace Practice.Types.Annotation
{
    /// <summary>
    /// Indicates that marked method builds string by format pattern and (optional) arguments.
    ///     Parameter, which contains format string, should be given in constructor.
    ///     The format string should be in <see cref="string.Format(IFormatProvider, string, object[])"/> -like form.
    /// </summary>
    [AttributeUsage(AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class StringFormatMethodAttribute : Attribute
    {
        /// <summary>
        /// The format parameter name.
        /// </summary>
        private readonly string _formatParameterName;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFormatMethodAttribute"/> class.
        /// Initializes new instance of StringFormatMethodAttribute.
        /// </summary>
        /// <param name="formatParameterName">Specifies which parameter of an annotated method should be treated as format-string.</param>
        public StringFormatMethodAttribute([NotNull] string formatParameterName)
        {
            _formatParameterName = formatParameterName;
        }

        /// <summary>
        /// Gets format parameter name.
        /// </summary>
        public string FormatParameterName
        {
            get { return _formatParameterName; }
        }


    }

    /// <summary>
    /// 表明标记元素的值有时可能为<c>null</c>, 在使用它前需要检查是否为<c>null</c>.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field,
        AllowMultiple = false, Inherited = true)]
    public sealed class CanBeNullAttribute : Attribute
    {
    }

    /// <summary>
    /// 表明标记元素的值可能为<c>null</c>.
    /// </summary>
    [AttributeUsage(
        AttributeTargets.Method | AttributeTargets.Parameter | AttributeTargets.Property |
        AttributeTargets.Delegate | AttributeTargets.Field,
        AllowMultiple = false, Inherited = true)]
    public sealed class NotNullAttribute : Attribute
    {
    }

    /// <summary>
    /// 表明标记方法是断言方法。
    /// Indicates that the marked method is assertion method, i.e. it halts control flow if one of the conditions is satisfied.
    ///     To set the condition, mark one of the parameters with <see cref="AssertionConditionAttribute"/> attribute.
    /// </summary>
    /// <seealso cref="AssertionConditionAttribute"/>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionMethodAttribute : Attribute
    {
    }

    /// <summary>
    /// Indicates the condition parameter of the assertion method.
    ///     The method itself should be marked by <see cref="AssertionMethodAttribute"/> attribute.
    ///     The mandatory argument of the attribute is the assertion type.
    /// </summary>
    /// <seealso cref="AssertionConditionType"/>
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
    public sealed class AssertionConditionAttribute : Attribute
    {
        /// <summary>
        /// The condition type.
        /// </summary>
        private readonly AssertionConditionType _conditionType;

        /// <summary>
        /// Initializes a new instance of the <see cref="AssertionConditionAttribute"/> class.
        /// Initialized new instance of AssertionConditionAttribute.
        /// </summary>
        /// <param name="conditionType">Specifies condition type.</param>
        public AssertionConditionAttribute(AssertionConditionType conditionType)
        {
            _conditionType = conditionType;
        }

        /// <summary>
        /// Gets condition type.
        /// </summary>
        public AssertionConditionType ConditionType
        {
            get { return _conditionType; }
        }
    }
    
    /// <summary>
    /// Specifies assertion type. If the assertion method argument statisifes the condition, then the execution continues.
    ///      Otherwise, execution is assumed to be halted.
    /// </summary>
    public enum AssertionConditionType
    {
        /// <summary>
        /// Indicates that the marked parameter should be evaluated to true.
        /// </summary>
        IsTrue = 0,
        /// <summary>
        /// Indicates that the marked parameter should be evaluated to false.
        /// </summary>
        IsFalse = 1,
        /// <summary>
        /// Indicates that the marked parameter should be evaluated to null value.
        /// </summary>
        IsNull = 2,
        /// <summary>
        /// Indicates taht the marked parameter should be evaluated to not null value.
        /// </summary>
        IsNotNull = 3,
    }
}