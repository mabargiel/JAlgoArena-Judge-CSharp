using AutoMapper;
using FluentValidation.AspNetCore;
using Judge.Infrastructure.Data.Repositories;
using Judge.Infrastructure.Generators;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RestSharp;
using ServiceStack.Redis;
using Swashbuckle.AspNetCore.Swagger;

namespace Judge.API
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
            services
                .AddMvc()
                .AddFluentValidation();

            services.AddCors(); //TODO EUREKA CORS?
            services.AddAutoMapper();

#if DEBUG
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("test", new Info {Title = "Judge CSharp Api", Version = "test"});
            });
#endif
            // ---------------------
            // Dependencies Injection
            // ---------------------

            services.AddTransient<IProblemsRepository, CachedProblemsRepository>();

            //TODO EXTRACT IMPLEMENTATION. Url must be parametrized for problems (through eureka?)
            services.AddSingleton<IRestClient>(new RestClient("localhost:5002")); 

            //TODO EXTRACT IMPLEMENTATION. Cache should be deployed locally (out of containers?)
            services.AddSingleton<IRedisClient>(new RedisClient());
            services.AddTransient<ISkeletonCodeGenerator, CSharpSkeletonCodeGenerator>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/test/swagger.json", "Judge CSharp Api"); });
            }

            app.UseMvc();
        }
    }

    internal static class AddServiceExtensions
    {
        public static void AddAutoMapper(this IServiceCollection services)
        {
            var mapper = new MapperConfiguration(cfg =>
                    cfg.AddProfile(new AutoMapperProfileConfiguration()))
                .CreateMapper();

            services.AddSingleton(mapper);
        }
    }
}