using System;
using Sigil;
using Platform.Interfaces;

#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace Platform.Reflection.Sigil
{
    public class NotSupportedExceptionDelegateFactory<TDelegate> : IFactory<TDelegate>
    {
        public TDelegate Create()
        {
            var emiter = Emit<TDelegate>.NewDynamicMethod();
            emiter.NewObject<NotSupportedException>();
            emiter.Throw();
            return emiter.CreateDelegate();
        }
    }
}
