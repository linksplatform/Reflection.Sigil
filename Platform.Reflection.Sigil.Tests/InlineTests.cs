using Xunit;
using SigilVNext = Sigil;
using PlatformReflectionSigil = Platform.Reflection.Sigil;

namespace Platform.Reflection.Sigil.Tests
{
    public static class InlineTests
    {
        [Fact]
        public static void SimpleInlineTest()
        {
            //SigilVNext.Disassembler
        }

        private static void InnerMethod()
        {
        }

        private static void OuterMethod()
        {
            InnerMethod();
        }
    }
}
