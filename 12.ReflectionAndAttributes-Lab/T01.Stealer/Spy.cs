using System;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    class Spy
    {
        public string StealFieldInfo(string className, params string[] fieldNames)
        {
            Type type = Type.GetType(className);
            FieldInfo[] fields = type.GetFields((BindingFlags)60);
            var typeInstance = Activator.CreateInstance(type);

            StringBuilder result = new StringBuilder();
            result.AppendLine($"Class under investigation: {className}");
            foreach (var field in fields.Where(x => fieldNames.Contains(x.Name)))
            {
                result.AppendLine($"{field.Name} = {field.GetValue(typeInstance)}");
            }

            return result.ToString().TrimEnd();
        }
    }
}
