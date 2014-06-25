using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Procent.DependencyInjection.app
{
    public class PoorMansContainer
    {
        readonly Dictionary<Type, Type> _registrations = new Dictionary<Type, Type>();

        public void RegisterType<T>()
        {
            _registrations.Add(typeof(T), typeof(T));
        }

        public void Register<TInterface, TImplementation>()
            where TImplementation : TInterface
        {
            _registrations.Add(typeof(TInterface), typeof(TImplementation));
        }

        public T Resolve<T>()
        {
            return (T)Resolve(typeof(T));
        }

        public object Resolve(Type t)
        {
            Type implementationType;
            bool implementationFound = _registrations.TryGetValue(t, out implementationType);
            if (implementationFound == false)
            {
                throw new TypeNotRegisteredException(t);
            }

            ConstructorInfo[] ctors = implementationType.GetConstructors();
            if (ctors.Length == 0)
            {
                throw new NoConstructorFoundException(t);
            }
            if (ctors.Length > 1)
            {
                throw new MultipleConstructorFoundException(t);
            }

            object[] ctorParams = ctors[0].GetParameters()
                .Select(x => Resolve(x.ParameterType))
                .ToArray();

            return Activator.CreateInstance(implementationType, ctorParams);
        }
    }

    public abstract class ResolveException : Exception
    {
        public readonly Type Type;

        public ResolveException(Type type, string message, Exception innerException = null)
            : base(message, innerException)
        {
            Type = type;
        }
    }

    public class TypeNotRegisteredException : ResolveException
    {
        public TypeNotRegisteredException(Type type)
            : base(type, string.Format("Type {0} not registered in the container", type.Name))
        {
        }
    }

    public class NoConstructorFoundException : ResolveException
    {
        public NoConstructorFoundException(Type type)
            : base(type, string.Format("Type {0} does not have any constructors", type.Name))
        {
        }
    }

    public class MultipleConstructorFoundException : ResolveException
    {
        public MultipleConstructorFoundException(Type type)
            : base(type, string.Format("Type {0} has more than one constructor", type.Name))
        {
        }
    }
}