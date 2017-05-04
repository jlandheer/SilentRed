using System;

namespace SilentRed.Infrastructure.Core
{
    public abstract class SilentRedException : Exception
    {
        protected SilentRedException() { }
        protected SilentRedException(string message) : base(message) { }
        protected SilentRedException(string message, Exception inner) : base(message, inner) { }
    }
}
