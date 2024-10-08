using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mvc.Project.DAL.Data;
using Mvc.Project.PL.Extensions;
using Mvc.Project.PL.MappingProfiles;

namespace Mvc.Project.PL
{
    public class Startup
    { 
        public IConfiguration _configuration { get; }
    
        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllersWithViews() // Register Bulit-In-Services Requierd For Mvc
                    .AddRazorRuntimeCompilation();  // To Enable Run Time Changes

            // Add (AppDbContext) and (DbContextOptions) to DI continer and (ConnectionString)
            services.AddDbContext<ApplicationDbContext>(options =>
                 options.UseSqlServer(_configuration.GetConnectionString("DefaulteConnection")));

            // Main Class To Add DI To All Repository
            services.AddApplicationServices();

            // Allow DI for Mapper
            //services.AddAutoMapper(m=>m.AddProfile(new EmployeeProfile()));
            //services.AddAutoMapper(m => m.AddProfile(new DepartmentProfile()));
            //services.AddAutoMapper(M => M.AddProfiles(new List<Profile> {new EmployeeProfile()
            //                                                            ,new DepartmentProfile()
            //                                                            ,new UserProfile()
            //                                                            ,new RoleProfile()}));
            services.AddAutoMapper(typeof(MappingProfile));
             

                                                                                             //(services.AddAuthentication();)
            //Allow DI For Register Securtiy (Main Services(User Manger,SignInManger,RoleManger) and Manor Services and Defult Configration)
            services.AddIdentity<IdentityUser, IdentityRole>(option =>
            {
                /// Password Configration
                ///option.Password

                ///User Configration
                ///option.User
                
               
            }).AddEntityFrameworkStores<ApplicationDbContext>() // Add DI For stores
              .AddDefaultTokenProviders();  // Add Token for Reset Password

            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(option =>
                    {
                        option.LoginPath = "Account/LogIn";
                        option.AccessDeniedPath = "Home/Error";
                    });


        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Account}/{action=LogIn}/{id?}");
            });
        }
    }
}
