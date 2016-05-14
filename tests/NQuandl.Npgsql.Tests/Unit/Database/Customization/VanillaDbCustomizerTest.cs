using NQuandl.Npgsql.Services.Database.Customization;
using Xunit;

namespace NQuandl.Npgsql.Tests.Unit.Database.Customization
{
    public class VanillaDbCustomizerTests
    {
        [Fact]
        public void DoesNotCustomizeAnything()
        {
            var dbCustomizer = new VanillaDbCustomizer();
            dbCustomizer.Customize(null);
        }
    }
}