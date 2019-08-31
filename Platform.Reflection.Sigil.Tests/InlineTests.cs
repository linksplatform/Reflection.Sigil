using System;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using Xunit;
using TheSigil = Sigil;

namespace Platform.Reflection.Sigil.Tests
{
    public static class InlineTests
    {
        [Fact]
        public static void SimpleInlineTest()
        {
            var disassembledOuterMethod = TheSigil.Disassembler<Action>.Disassemble(OuterMethod);
            var emiter = TheSigil.Emit<Action>.NewDynamicMethod();

            foreach (var operation in disassembledOuterMethod)
            {
                if (operation.OpCode == OpCodes.Nop)
                {
                    continue;
                }
                if (operation.OpCode == OpCodes.Call)
                {
                    var firstParameter = operation.Parameters.First();
                    if (firstParameter is MethodInfo methodInfo)
                    {
                        if (methodInfo.Name == nameof(InnerMethod))
                        {
                            var disassembledInnerMethod = TheSigil.Disassembler<Action>.Disassemble(InnerMethod);
                            var i = 0;
                            foreach (var innerOperation in disassembledInnerMethod)
                            {
                                // There is no way to replay operation at emiter
                                // emiter.Replay(innerOperation)

                                // There is also no way to rewrite operations in the disassembledOuterMethod
                                // disassembledOuterMethod[i++] = innerOperation;
                            }

                            // So the only way (but it is not practical for now) is to use:
                            emiter = disassembledInnerMethod.EmitAll();
                            // That means we just use InnerMethod method directly, 
                            // but if OuterMethod will contain something else we will fail with current support of disassembling in the Sigil.
                        }
                    }
                }
            }

            Action result = emiter.CreateDelegate();
            result();
        }

        private static void InnerMethod()
        {
            Console.WriteLine("Inner method.");
        }

        private static void OuterMethod()
        {
            InnerMethod();
        }
    }
}
