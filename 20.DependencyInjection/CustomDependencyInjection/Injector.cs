using System;
using System.Reflection;
using CustomDependencyInjection.Contracts;

namespace CustomDependencyInjection
{
    public class Injector
    {
        private static Injector instance;

        private Injector()
        {
        }

        public static Injector Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Injector();
                }

                return instance;
            }
        }

        public IServiceModule ServiceModule { get; set; }

        public TClass Create<TClass>()
        {
            ConstructorInfo[] constructors = typeof(TClass).GetConstructors();
            TClass instance = default(TClass);
            foreach (var constructor in constructors)
            {
                object[] parameterInstances = new object[constructor.GetParameters().Length];
                int index = 0;
                ParameterInfo[] parameters = constructor.GetParameters();
                foreach (var parameter in parameters)
                {
                    var parameterInstance = ServiceModule.GetInstance<TClass>();
                    if (parameterInstance == null)
                    {
                        Type parameterImplementationType = ServiceModule.GetMapping(parameter.ParameterType);

                        if (parameterImplementationType == null)
                        {
                            parameterInstance = ServiceModule.GetCustomMapping(parameter.ParameterType);
                        }
                        else
                        {
                            parameterInstance = InvokeWithGeneric(parameterImplementationType, "Create", this);
                        }
                    }

                    parameterInstances[index++] = parameterInstance;
                }

                instance = (TClass)Activator.CreateInstance(typeof(TClass), parameterInstances);
            }

            return instance;
        }

        private object InvokeWithGeneric(Type typeToCreate, string methodName, object instance, params object[] parameters)
        {
            var method = instance.GetType().GetMethod("Create");
            var methodGeneric = method.MakeGenericMethod(typeToCreate);
            return methodGeneric.Invoke(instance, parameters);
        }
    }
}
