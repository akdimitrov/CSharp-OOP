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
            object typeInstance = Activator.CreateInstance(type);

            StringBuilder result = new StringBuilder();
            result.AppendLine($"Class under investigation: {className}");
            foreach (var field in fields.Where(x => fieldNames.Contains(x.Name)))
            {
                result.AppendLine($"{field.Name} = {field.GetValue(typeInstance)}");
            }

            return result.ToString().TrimEnd();
        }

        public string AnalyzeAccessModifiers(string className)
        {
            Type type = Type.GetType(className);
            FieldInfo[] fields = type.GetFields(BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static);
            MethodInfo[] publicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.Public);
            MethodInfo[] nonPublicMethods = type.GetMethods(BindingFlags.Instance | BindingFlags.NonPublic);
            StringBuilder result = new StringBuilder();

            foreach (var field in fields)
            {
                result.AppendLine($"{field.Name} must be private!");
            }

            foreach (var method in nonPublicMethods.Where(x => x.Name.StartsWith("get")))
            {
                result.AppendLine($"{method.Name} have to be public!");
            }

            foreach (var method in publicMethods.Where(x => x.Name.StartsWith("set")))
            {
                result.AppendLine($"{method.Name} have to be private");
            }

            return result.ToString().TrimEnd();
        }

        public string RevealPrivateMethods(string className)
        {
            Type type = Type.GetType(className);
            MethodInfo[] privateMethods = type.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Instance);

            StringBuilder result = new StringBuilder();
            result.AppendLine($"All Private Methods of Class: {className}");
            result.AppendLine($"Base Class: {type.BaseType.Name}");
            foreach (var method in privateMethods)
            {
                result.AppendLine(method.Name);
            }

            return result.ToString().TrimEnd();
        }
    }
}
