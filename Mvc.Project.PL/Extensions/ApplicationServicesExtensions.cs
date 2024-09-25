using Microsoft.Extensions.DependencyInjection;
using Mvc.Project.BLL.Interfaces;
using Mvc.Project.BLL.Repositories;
using Mvc.Project.PL.Servies.EmailSender;

namespace Mvc.Project.PL.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services )
        {
            // Add DI To (IDepartmentRepository)
            /* services.AddScoped<IDepartmentRepository, DepartmentRepository>();*/ //(Go To UnitOfWork)
            // Add DI To (IEmployeeRepository)
            /*services.AddScoped<IEmployeeRepository, EmployeeRepository>(); *///(Go To UnitOfWork)

            services.AddTransient<IEmailSender, EmailSender>();
            services.AddScoped<IUintOfWork, UintOfWork>();

            return services;
        }
    }
}
