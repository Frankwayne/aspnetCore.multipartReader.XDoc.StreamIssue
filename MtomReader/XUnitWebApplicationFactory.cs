using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;

namespace MtomReader
{

    /// <summary>
    /// Class XUnitWebApplicationFactory.
    /// Implements the <see cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{TStartup}" />
    /// </summary>
    /// <typeparam name="TStartup">The <c>Startup</c> class instance</typeparam>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Testing.WebApplicationFactory{TStartup}" />
    /// <seealso cref="https://github.com/aspnet/AspNetCore.Docs/issues/7063#issuecomment-414661566"/>
    /// <seealso cref="https://github.com/aspnet/AspNetCore.Docs/issues/7825"/>
    public class XUnitWebApplicationFactory<TStartup> : WebApplicationFactory<TStartup> where TStartup : class
    {
        protected override IWebHostBuilder CreateWebHostBuilder() => WebHost.CreateDefaultBuilder().UseStartup<Startup>();
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.UseContentRoot(".");
            base.ConfigureWebHost(builder);
        }
    }
}
