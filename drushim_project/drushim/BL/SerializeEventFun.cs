using System;
using Newtonsoft.Json;

namespace drushim.BL
{
    public static class JsonHelper
    {
        public static T DeserializeWithConversions<T>(string json)
        {
            try
            {
                var jsonObject = JsonConvert.DeserializeObject<T>(json);

                return jsonObject;

              
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        private static object ConvertValue(Type targetType, string value)
        {
            try
            {
                if (targetType == typeof(int))
                {
                    return ConvertValue<int>(value);
                }
                if (targetType == typeof(double))
                {
                    return ConvertValue<double>(value);
                }
                if (targetType == typeof(bool))
                {
                    return ConvertValue<bool>(value);
                }

                return Convert.ChangeType(value, targetType);
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public static T ConvertValue<T>(string value)
        {
            try
            {
                if (string.IsNullOrEmpty(value))
                    return default(T);

                if (typeof(T) == typeof(int))
                {
                    if (int.TryParse(value, out int intValue))
                    {
                        return (T)(object)intValue;
                    }
                    else
                    {
                        return (T)(object)0; 
                    }
                }
                if (typeof(T) == typeof(double))
                {
                    if (double.TryParse(value, out double doubleValue))
                    {
                        return (T)(object)doubleValue;
                    }
                    else
                    {
                        return (T)(object)0.0; 
                    }
                }

                if (typeof(T) == typeof(bool))
                {
                    if (bool.TryParse(value, out bool boolValue))
                    {
                        return (T)(object)boolValue;
                    }
                    else
                    {
                        return (T)(object)false; 
                    }
                }

                return (T)Convert.ChangeType(value, typeof(T));
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
