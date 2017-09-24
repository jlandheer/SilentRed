using System;
using System.Collections.Generic;
using SimpleInjector;

namespace SilentRed.Infrastructure.SimpleInjector
{
    public class SimpleInjectorRegistratorAndActivator
    {
        public void ActivateAll()
        {
            if (_locked)
            {
                throw new InvalidOperationException("Cannot activate more than one time.");
            }

            _locked = true;
            foreach (var type in _types)
            {
                _container.GetInstance(type);
            }
        }

        public void Register<T>()
            where T : class
        {
            if (_locked)
            {
                throw new InvalidOperationException("Cannot Register types after activation.");
            }

            _container.RegisterSingleton<T>();
            _types.Add(typeof(T));
        }

        public SimpleInjectorRegistratorAndActivator(Container container)
        {
            _container = container;
            _types = new List<Type>();
            _locked = false;
        }

        private readonly Container _container;
        private readonly List<Type> _types;
        private bool _locked;
    }
}
