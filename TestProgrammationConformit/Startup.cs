using System.Reflection;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using TestProgrammationConformit.Domains.Models;
using TestProgrammationConformit.Domains.Services;
using TestProgrammationConformit.Infrastructures;

namespace TestProgrammationConformit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<Env>();
            services.AddScoped<IService<Event, int>>(provider =>
            {
                var context = provider.GetService<ConformitContext>();
                return new EventService(context, context?.Events, 0);
            });
            services.AddScoped<IService<Comment, int>>(provider =>
            {
                var context = provider.GetService<ConformitContext>();
                return new CommentService(context, context?.Comments, 0);
            });
            services.AddScoped<IService<Stakeholder, int>>(provider =>
            {
                var context = provider.GetService<ConformitContext>();
                return new StakeholderService(context, context?.Stakeholders, 0);
            });
            services.AddSwaggerGen(options =>
                options.SwaggerDoc("v0.0.1", new OpenApiInfo{ Title = "Test Programmation Conformit", Version = "v0.0.1"}));
            services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.IgnoreNullValues = true;
                options.JsonSerializerOptions.DictionaryKeyPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
            });
            services.AddDbContext<ConformitContext>(options =>
            {
                options.UseNpgsql(Configuration.GetConnectionString("ConformitDb"),
                    sqlOptions => sqlOptions.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(swaggerUiOptions =>
                swaggerUiOptions.SwaggerEndpoint("/swagger/v0.0.1/swagger.json", "Test Programmation Conformit"));
            app.UseRouting();
            app.UseAuthorization();
            app.UseEndpoints(endpoints => endpoints.MapControllers());
        }
    }
}
