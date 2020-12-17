using System;
using CO.PaymentGateway.BankClient.Client;
using CO.PaymentGateway.Business.Core.Repositories;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Business.Core.UseCases.PaymentProcess.Rules;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Commands;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Queries;
using CO.PaymentGateway.Business.Logic.UseCases.PaymentProcess.Rules;
using CO.PaymentGateway.Cache;
using CO.PaymentGateway.Data.EFContext;
using CO.PaymentGateway.Data.Repositories.PaymentProcess;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using CO.AcessControl.AcessClient;

namespace CO.PaymentGateway.HostApp
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
            services.AddDbContext<PaymentContext>();

            services.ConfigureAccessControl();
            services.AddControllers();

            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.TryAddSingleton<ICOMemoryCache, COMemoryCache>();
            services.AddMemoryCache();

            services.AddDataProtection();

            services.AddScoped<IPaymentProcessWriteRepository, PaymentProcessWriteRepository>();
            services.AddScoped<IPaymentProcessReadRepository, PaymentProcessReadRepository>();

            services.AddScoped<IPaymentProcessGetAllQuery, PaymentProcessGetAllQuery>();
            services.AddScoped<IPaymentProcessGetByIdQuery, PaymentProcessGetByIdQuery>();
            services.AddScoped<IPaymentProcessCommand, PaymentProcessCommand>();

            services.AddScoped<IPaymentProcessValidationRule, ExpirationDateIsValidRule>();
            services.AddScoped<IPaymentProcessValidationRule, PaymentDeniedTwiceRule>();
            services.AddScoped<IPaymentRuleEngine, PaymentRuleEngine>();

            services.AddHttpClient<IBankHttpClient, BankHttpClient>(client =>
            {
                client.BaseAddress = new Uri(Environment.GetEnvironmentVariable("BANKCLIENTURL"));
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "CO.PaymentGateway.HostApp", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "CO.PaymentGateway.HostApp v1"));
            }

            using (var scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<PaymentContext>())
                context.Database.EnsureCreated();

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();


            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}