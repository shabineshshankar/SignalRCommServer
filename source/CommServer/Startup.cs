using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CommServer
{
    public class Startup
    {
        private IConfiguration _configuration { get; }

        public Startup(IConfiguration Configuration)
        {
            _configuration = Configuration;

        }

        public void ConfigureServices(IServiceCollection services)
        {
            
            services.AddCors(options => options.AddPolicy("CorsPolicy", builder =>
            {
                builder.SetIsOriginAllowed(origin => true)
                    .AllowAnyMethod()
                    .AllowCredentials()
                    .AllowAnyHeader();
            }));

            services.AddSignalR();

        }

        public void Configure(IApplicationBuilder app)
        {
            
            app.UseRouting();
            app.UseCors("CorsPolicy");
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<CommunicationHub>("/commHub");
            });


        }
    }
}
