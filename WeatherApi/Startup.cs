using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Serialization;
using System.Security.Authentication;
using System.Text;
using System.Xml.Schema;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using WeatherApi.Data;
using WeatherApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Net.Http.Headers;
using WeatherApi.Controllers;
using WeatherApi.Extensions;
using WeatherApi.Helpers;
using WeatherApi.Models;

[assembly: ApiController]
namespace WeatherApi
{

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(options =>
                {
                    options.RespectBrowserAcceptHeader = true;
                    options.FormatterMappings.SetMediaTypeMappingForFormat(
                        "xml", MediaTypeHeaderValue.Parse(Constants.XmlMediaType));
                    options.FormatterMappings.SetMediaTypeMappingForFormat(
                        "json", MediaTypeHeaderValue.Parse(Constants.JsonMediaType));
                })
                .AddXmlSerializerFormatters()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2)
                .ConfigureApiBehaviorOptions(options =>
                {
                    options.SuppressInferBindingSourcesForParameters = true;
                });

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear(); //sub and idp claims flow throgh unmolested

            services.AddScoped<WeatherService>();
            services.AddAutoMapper(config => config.AddProfile<MappingProfile>());

            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("WeatherAppDb")));

            services.AddAuthentication(Constants.BearerScheme)
                .AddJwtBearer(Constants.BearerScheme, options =>
                {
                    options.Authority = Constants.Authority;
                    options.RequireHttpsMetadata = false;
                    options.Audience = Constants.Audience;
                });

            services.AddHttpClient<WeatherApiClient>(config =>
                {
                    config.BaseAddress = new Uri("https://api.apixu.com");
                });
            services.AddAuthorization();

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseCustomExceptionHandler();

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseMvc();
        }
    }
}
