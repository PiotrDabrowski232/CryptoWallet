using System.ComponentModel;
using System.Reflection;

namespace CryptoWallet.Logic.Extensions
{
    public static class EnumExtension
    {
        public static string GetEnumDescription(this Enum value)
        {
            var type = value.GetType();

            var field = type.GetField(value.ToString());

            if (field != null)
            {
                var attribute = field.GetCustomAttribute<DescriptionAttribute>();

                if (attribute != null)
                {
                    return attribute.Description;
                }
            }
            return value.ToString();
        }
    }
}
