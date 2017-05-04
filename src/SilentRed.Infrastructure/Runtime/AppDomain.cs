using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace SilentRed.Infrastructure.Runtime
{
    public class AppDomain
    {
        public static IEnumerable<Assembly> GetAssemblies()
        {
            return DependencyContext.Default.RuntimeLibraries
                                    .Where(i => i.IsCandidateCompilationLibrary())
                                    .Select(i => i.Load())
                                    .ToArray();
        }

        public static AppDomain CurrentDomain { get; }

        static AppDomain()
        {
            CurrentDomain = new AppDomain();
        }
    }
}
