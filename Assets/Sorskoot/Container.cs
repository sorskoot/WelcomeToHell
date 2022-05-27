using System;
using System.Collections.Generic;
using System.Linq;

namespace Sorskoot.Ioc
{
    public class Container
    {
        internal Container(){}
        
        private readonly Dictionary<Type, RegistrationModel> registrations = new();

        public void Register<TFrom, TTo>(IocScope scope = IocScope.Singleton)
        {
            if (registrations.ContainsKey(typeof(TFrom)))
            {
                registrations.Remove(typeof(TFrom));
            }
            
            registrations.Add(typeof(TFrom), new RegistrationModel()
            {
                Scope = scope,
                Type = typeof(TTo)
            });
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        private object Resolve(Type typeToResolve)
        {
            RegistrationModel resolvedType = null;
            try
            {
                resolvedType = registrations[typeToResolve];
            }
            catch
            {
                throw new Exception($"Could not resolve type {typeToResolve.FullName}");
            }

            var firstConstructor = resolvedType.Type.GetConstructors().First();
            var constructorParameters = firstConstructor.GetParameters();

            if (!constructorParameters.Any())
            {
                return CreateInstance(resolvedType);
            }
            
            List<object> arguments = new List<object>();
            foreach (var param in constructorParameters)
            {
                Type type = param.ParameterType;
                arguments.Add(Resolve(type));
            }

            return CreateInstance(resolvedType, arguments.ToArray());
        }
        
        private object CreateInstance(RegistrationModel model, object[] arguments = null)
        {
            object returnedObj = null;
            Type typeToCreate = model.Type;

            if (model.Scope == IocScope.Transient)
            {
                returnedObj = TransientCreationService.GetInstance().GetNewObject(typeToCreate, arguments);
            }
            else if (model.Scope == IocScope.Singleton)
            {
                returnedObj = SingletonCreationService.GetInstance().GetSingleton(typeToCreate, arguments);
            }

            return returnedObj;
        }
    }
}