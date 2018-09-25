using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Extensions
{
    public static class EnumExtensions
    {
        public static string GetDescription(this Enum enumValue)
        {
            DescriptionAttribute[] customAttributes = (DescriptionAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false);
            return customAttributes.Length != 0 ? customAttributes[0].Description : enumValue.ToString();
        }

        //public static string GetTranslatable(this Enum enumValue)
        //{
        //    TranslatableAttribute[] customAttributes = (TranslatableAttribute[])enumValue.GetType().GetField(enumValue.ToString()).GetCustomAttributes(typeof(TranslatableAttribute), false);
        //    return customAttributes.Length != 0 ? customAttributes[0].MessageCode : enumValue.ToString();
        //}
    }
}
