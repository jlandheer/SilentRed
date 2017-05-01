using System;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace SilentRed.Infrastructure
{
    public static class RuntimeLibrariesExtensions
    {
        public static bool IsCandidateCompilationLibrary(this RuntimeLibrary compilationLibrary)
        {
            if (compilationLibrary == null) throw new ArgumentNullException(nameof(compilationLibrary));

            return compilationLibrary.Name == "Specify"
                   || compilationLibrary.Dependencies.Any(d => d.Name.StartsWith("Specify"));
        }

        public static Assembly Load(this RuntimeLibrary library)
        {
            if (library == null) throw new ArgumentNullException(nameof(library));

            return Assembly.Load(new AssemblyName(library.Name));
        }
    }
}
