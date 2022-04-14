using System.Linq;
using System.Reflection;

namespace ValidationAttributes
{
    public static class Validator
    {
        public static bool IsValid(object obj)
        {
            PropertyInfo[] props = obj
                .GetType()
                .GetProperties()
                .Where(x => x.GetCustomAttributes(typeof(MyValidationAttribute)).Any())
                .ToArray();

            foreach (var property in props)
            {
                object value = property.GetValue(obj);
                MyValidationAttribute attribute = property.GetCustomAttribute<MyValidationAttribute>();

                bool isValid = attribute.IsValid(value);
                if (!isValid)
                {
                    return false;
                }
            }

            return true;
        }
    }
}
