using System;
using System.Collections.Generic;
using Sigil;
using Platform.Exceptions;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Reflection.Sigil
{
    public static class DelegateHelpers
    {
        public static TDelegate Compile<TDelegate>(Action<Emit<TDelegate>> emitCode)
        {
            var @delegate = default(TDelegate);
            try
            {
                var emiter = Emit<TDelegate>.NewDynamicMethod();
                emitCode(emiter);
                @delegate = emiter.CreateDelegate();
            }
            catch (Exception exception)
            {
                exception.Ignore();
            }
            finally
            {
                if (EqualityComparer<TDelegate>.Default.Equals(@delegate, default))
                {
                    var factory = new NotSupportedExceptionDelegateFactory<TDelegate>();
                    @delegate = factory.Create();
                }
            }
            return @delegate;
        }
    }
}
