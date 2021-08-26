using Coravel;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Payments.Application;
using Payments.Application.Common.Interfaces;
using Payments.Application.PaymentSystems.DataRequest;
using Payments.Domain.Entities;
using Payments.Infrastructure.Data;
using Payments.Infrastructure.PaymentSystem;
using Payments.Infrastructure.PaymentSystem.Scheduler;
using Payments.Infrastructure.PaymentSystem.Yandex.Settings;
using Serilog;
using System.Configuration;

namespace Payments.WebAPI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddLogging(builder => {
                builder.ClearProviders();
                builder.AddSerilog();
            });
            services.AddTransient<DataRequestDB>();
            services.AddTransient<ICommunicationProject, CommunicationProject>();
            services.AddApplication();
            services.AddInfrastructureData(Configuration);
            services.AddInfrastructurePaymentSystem();
            
            services.AddTransient<IPaymentsDbContext, PaymentsDbContext>();

            services.AddScheduler();
            services.AddTransient<SchedulingSendingNotificationTask>();

            services.AddControllers();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseForwardedHeaders(new ForwardedHeadersOptions
            {
                ForwardedHeaders = ForwardedHeaders.XForwardedFor |
                                   ForwardedHeaders.XForwardedProto
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();                                                                                                               

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            var provider = app.ApplicationServices;
            var time = Configuration["AppSettings:PeriodOfTime"];
            provider.UseScheduler(scheduler =>
            {
                scheduler.Schedule<SchedulingSendingNotificationTask>()
                    .Cron($"*/{time} * * * *");
            });
        }
    }
}
