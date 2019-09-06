using System.Linq;
using Microsoft.AspNet.OData.Builder;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using TestDecimalSkipToken.Models;

namespace TestDecimalSkipToken
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddMvcCore();
            services.AddOData();
        }

        public void Configure(IApplicationBuilder app)
        {
            app.UseMvc(routeBuilder =>
            {
                routeBuilder.Count().Expand().Filter().MaxTop(null).OrderBy().Select().SkipToken();
                routeBuilder.EnableDependencyInjection();

                // this works
                // var modelBuilder = new ODataConventionModelBuilder();

                // this causes an error when navigating the generated skiptoken link
                var modelBuilder = new ODataConventionModelBuilder().EnableLowerCamelCase();

                modelBuilder.EntitySet<Employee>("Employees");
                routeBuilder.MapODataServiceRoute("odata", "api", modelBuilder.GetEdmModel());
            });

            app.UseHttpsRedirection();
        }
    }
}