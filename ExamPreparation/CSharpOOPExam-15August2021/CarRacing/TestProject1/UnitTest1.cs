using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using NUnit.Framework;

using CarRacing;
using CarRacing.Repositories;

// ReSharper disable InconsistentNaming
// ReSharper disable CheckNamespace

public class Tests_000_003
{
    // MUST exist within project, otherwise a Compile Time Error will be thrown.
    private static readonly Assembly ProjectAssembly = typeof(StartUp).Assembly;

    [Test]
    public void ValidateBeginRaceCommandMethodInController()
    {
        var controller = CreateObjectInstance(GetType("Controller"));

        var addCarArguments = new object[] { "SuperCar", "BMW", "M8", "JN1HJ01P0MT518872", 617 };
        InvokeMethod(controller, "AddCar", addCarArguments);

        var addRacerArguments = new object[] { "ProfessionalRacer", "Atanas", "JN1HJ01P0MT518872" };
        InvokeMethod(controller, "AddRacer", addRacerArguments);

        var addCarArguments2 = new object[] { "SuperCar", "Audi", "RS6", "5N1AN08W86C526409", 591 };
        InvokeMethod(controller, "AddCar", addCarArguments2);

        var addRacerArguments2 = new object[] { "ProfessionalRacer", "Shopoff", "5N1AN08W86C526409" };
        InvokeMethod(controller, "AddRacer", addRacerArguments2);

        var startRaceArguments = new object[] { "Atanas", "Shopoff" };
        var actualResult = InvokeMethod(controller, "BeginRace", startRaceArguments);

        var expectedResult = "Atanas has just raced against Shopoff! Atanas is the winner!";

        var validActualResultWithoutNewLines = RemoveNewLines(actualResult.ToString());
        var validExpectedResultWithoutNewLines = RemoveNewLines(expectedResult);

        Assert.AreEqual(validExpectedResultWithoutNewLines, validActualResultWithoutNewLines);
    }

    private static string RemoveNewLines(string content)
    {
        return content.Replace("\r", "")
            .Replace("\n", "");
    }

    private static object InvokeMethod(object obj, string methodName, object[] parameters)
    {
        try
        {
            var result = obj.GetType()
                .GetMethod(methodName)
                .Invoke(obj, parameters);

            return result;
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static object CreateObjectInstance(Type type, params object[] parameters)
    {
        try
        {
            var desiredConstructor = type.GetConstructors()
                .FirstOrDefault(x => x.GetParameters().Any());

            if (desiredConstructor == null)
            {
                return Activator.CreateInstance(type, parameters);
            }

            var instances = new List<object>();

            foreach (var parameterInfo in desiredConstructor.GetParameters())
            {
                var currentInstance = Activator.CreateInstance(GetType(parameterInfo.Name.Substring(1)));

                instances.Add(currentInstance);
            }

            return Activator.CreateInstance(type, instances.ToArray());
        }
        catch (TargetInvocationException e)
        {
            return e.InnerException.Message;
        }
    }

    private static Type GetType(string name)
    {
        var type = ProjectAssembly
            .GetTypes()
            .FirstOrDefault(t => t.Name.Contains(name));

        return type;
    }
}