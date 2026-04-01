using System;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Vista.Shared
{
    public static class EnumExtensions
    {
        public static string GetDisplayName(this Enum? enumValue)
        {
            if (enumValue == null)
                return string.Empty;

            var memberInfo = enumValue.GetType().GetMember(enumValue.ToString());
            if (memberInfo.Length == 0)
                return string.Empty;

            var displayAttr = memberInfo[0].GetCustomAttribute<DisplayAttribute>();
            return displayAttr?.GetName() ?? string.Empty;
        }
    }
}
