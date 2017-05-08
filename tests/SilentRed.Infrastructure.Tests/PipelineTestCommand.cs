using System;
using System.Collections.Generic;
using SilentRed.Infrastructure.Command;

namespace SilentRed.Infrastructure.Tests
{
    public class PipelineTestCommand : ICommand
    {
        public void Add(Type type)
        {
            _typesVisited.Add(type);
        }

        public List<Type> TypesVisited() => _typesVisited;
        private readonly List<Type> _typesVisited = new List<Type>();
    }
}
