using System.Linq;
using System.Reflection;
using Microsoft.Extensions.DependencyModel;

namespace SilentRed.Infrastructure
{
    public class AppDomain
    {
        public static AppDomain CurrentDomain { get; }

        static AppDomain()
        {
            CurrentDomain = new AppDomain();
        }

        public Assembly[] GetAssemblies()
        {
            return DependencyContext.Default.RuntimeLibraries
                                    .Where(i => i.IsCandidateCompilationLibrary())
                                    .Select(i => i.Load())
                                    .ToArray();
        }
    }
}