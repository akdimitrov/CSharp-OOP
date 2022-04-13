using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Stealer
{
    class Spy
    {
        public string StealFieldInfo(string className,params string[] fieldNames)
        {
            StringBuilder result = new StringBuilder();
            Type type = Type.GetType(className);
            FieldInfo[] fields = type.GetFields((BindingFlags)60);

            Console.WriteLine($"Class under investigation: {className}");
            foreach (var field in fields)
            {

                if (fieldNames.Contains(field.Name))
                {
                    result.AppendLine($"{field.Name} = {field.GetValue(field.Name)}");
                }
            }

            return result.ToString().TrimEnd();
        }
    }
}
