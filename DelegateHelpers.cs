using System;
using System.Collections.Generic;
using Sigil;

namespace Platform.Reflection.Sigil
{
    public static class DelegateHelpers
    {
        public static void Compile<TDelegate>(out TDelegate @delegate, Action<Emit<TDelegate>> emitCode)
        {
            @delegate = default;
            try
            {
                var emiter = Emit<TDelegate>.NewDynamicMethod();
                emitCode(emiter);
                @delegate = emiter.CreateDelegate();
            }
            finally
            {
                if (EqualityComparer<TDelegate>.Default.Equals(@delegate, default))
                {
                    var factory = new NotSupportedExceptionDelegateFactory<TDelegate>();
                    @delegate = factory.Create();
                }
            }
        }
    }
}
