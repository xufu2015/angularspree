using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace Common.Helpers
{
    [AttributeUsage(AttributeTargets.Parameter)]
    public sealed class ValidatedNotNullAttribute : Attribute
    {
    }
    public static class Guard
    {
        public static Guid GuidNotEmpty(Guid value, string name)
        {
            if (value == Guid.Empty)
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0} must not be empty.", (object)name));
            return value;
        }

        public static Guid GuidNotEmpty(Guid? value, string name)
        {
            if (!value.HasValue)
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0} must not be null.", (object)name));
            return Guard.GuidNotEmpty(value.Value, name);
        }

        public static void StringNotNullOrEmpty(string value, string name)
        {
            if (string.IsNullOrEmpty(value))
                throw new ArgumentException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "{0} must not be null or empty.", (object)name));
        }

        public static void ObjectNotNull([ValidatedNotNull] object value, string name)
        {
            if (value == null)
                throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The object {0} must not be null.", (object)name));
        }

        public static TObject ArgumentNotNull<TObject>([ValidatedNotNull] TObject arg, string name, string exceptionMessage = null)
        {
            if ((object)arg != null)
                return arg;
            if (string.IsNullOrEmpty(exceptionMessage))
                throw new ArgumentNullException(name);
            throw new ArgumentNullException(name, exceptionMessage);
        }

        public static TEnum EnumArgumentNotInvalid<TEnum>(TEnum enumArgument, TEnum invalidEnum, string name)
        {
            string exceptionMessage = string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The enum {0} must not be equal to {1}", (object)name, (object)invalidEnum);
            Guard.ArgumentNotInvalid(enumArgument.Equals((object)invalidEnum), name, exceptionMessage);
            return enumArgument;
        }

        public static void ArgumentNotInvalid(bool invalidCondition, string name, string exceptionMessage)
        {
            if (invalidCondition)
            {
                exceptionMessage = string.Join(Environment.NewLine, string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The argument {0} is not valid.", (object)name), exceptionMessage);
                throw new ArgumentException(exceptionMessage, name);
            }
        }

        public static T NotDefault<T>(T value, string name)
        {
            if (default(T).Equals((object)value))
                throw new InvalidOperationException(string.Format((IFormatProvider)CultureInfo.InvariantCulture, "The value of {0} should not be {1}", (object)name, (object)default(T)));
            return value;
        }

        public static void InvalidOperationException(bool condition, string message)
        {
            if (condition)
                throw new InvalidOperationException(message);
        }
    }
}
