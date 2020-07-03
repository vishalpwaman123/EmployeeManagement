
namespace EmployeeApp
{
    using BusinessModel.Interface;
    using BusinessModel.Service;
    using RepositoryModel.Interface;
    using RepositoryModel.Service;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<BusinessInterface, EmployeeBusiness>();
            services.AddTransient<RepositoryInterface, EmployeesRepository>();
            services.AddTransient<BusinessRegistrationInterface, EmployeeBusinessRegistration>();
            services.AddTransient<RepositoryRegistrationInterface, EmployeeRegistrationRepository>();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }
       
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
