using Clinic.Common.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Clinic.Common.Authentication;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Clinic.Common.Helpers.API;
using Microsoft.Bot.Builder;
using Clinic.Api.Bots;
using Microsoft.Bot.Builder.Integration.AspNet.Core;
using Clinic.Bot;
using Clinic.Common.Helpers.Bot.ConversationRefrence;
using Microsoft.Bot.Connector.Authentication;
using Clinics.Bot;

namespace Clinic.Api
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

            services.AddDbContext<MeetingsContext>(options => options.UseSqlServer(
                Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddScoped<IDoctorHelper, DoctorHelper>();
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();
            services.AddScoped<IMessageHelper, MessageHelper>();
            services.AddScoped<IConversationReferencesHelper, ConversationReferencesHelper>();

            // Create the Bot Framework Authentication to be used with the Bot Adapter.
            services.AddSingleton<BotFrameworkAuthentication, ConfigurationBotFrameworkAuthentication>();

            // Create the Bot Adapter with error handling enabled.
            services.AddSingleton<IBotFrameworkHttpAdapter, AdapterWithErrorHandler>();


            services.AddTransient<IBot,BotTest>();


            services.AddControllers();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Clinic.Api", Version = "v1" });
            });

            



              // For Identity  
            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<MeetingsContext>()
                .AddDefaultTokenProviders();





        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
             if (env.IsDevelopment())
             {
                 app.UseDeveloperExceptionPage();
                 app.UseSwagger();
                 app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Clinic.Api v1"));
             }


            /*if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            */
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            /* app.UseEndpoints(endpoints =>
             {
                 endpoints.MapControllers();
             });
            */

            app.UseDefaultFiles()
               .UseStaticFiles()
               .UseWebSockets()
               .UseRouting()
               .UseAuthorization()
               .UseEndpoints(endpoints =>
               {
                   endpoints.MapControllers();
               });

        }
    }
}
