using CoursePol.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SmartBreadcrumbs;
using System;




namespace CoursePol
{
    public class Startup
    {
        readonly IConfigurationRoot Configuration;

        public Startup(IHostingEnvironment env)
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(env.ContentRootPath)
                .AddJsonFile("appsettings.json")
                .Build();
        }
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
               options.UseSqlServer(
                   Configuration["ConnectionStrings:DefaultConnection"]));
            services.AddIdentity<User, IdentityRole>(opts =>
            {
                opts.Password.RequiredLength = 4;   // минимальная длина
                opts.Password.RequireNonAlphanumeric = false;   // требуются ли не алфавитно-цифровые символы
                opts.Password.RequireLowercase = false; // требуются ли символы в нижнем регистре
                opts.Password.RequireUppercase = false; // требуются ли символы в верхнем регистре
                opts.Password.RequireDigit = false; // требуются ли цифры
               
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders(); ;

            services.Configure<SecurityStampValidatorOptions>(options => { options.ValidationInterval = TimeSpan.FromMinutes(5); });
            //>>>>>>>>>>>>>>>>>>>>>>>DB>>>>>>>>>>>>>>>>>>>>>>>>>>>>
            services.AddTransient<ICourse, EFCourseRepository>();
            services.AddTransient<ICourseExercise, EFCourseExerciseRepository>();
            services.AddTransient<IExercise, EFExerciseRepository>();
            services.AddTransient<IFolower, EFFolowerRepository>();
            services.AddTransient<IUserExercisesComplete, EFUserExercisesComplete>();
            //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>

            services.UseBreadcrumbs(GetType().Assembly, options =>
            {
                options.TagName = "div";
                options.TagClasses = "myBreadcrumb";
                options.OlClasses = "breadcrumb";
                options.LiClasses = "breadcrumb-item";
                options.ActiveLiClasses = "breadcrumb-item active";
                options.SeparatorElement = "";
            });
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

            }
            app.UseAuthentication();
            app.UseStatusCodePages();
            app.UseStaticFiles();
#pragma warning disable CS0618 // Type or member is obsolete
            app.UseIdentity();
#pragma warning restore CS0618 // Type or member is obsolete
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                  name: null,
                  template: "Admin/{action}",
                   defaults: new { controller = "Admin", action = "Home" }


                  );
                routes.MapRoute(
                   name: null,
                   template: "Solutions/course{CourseID:int}/{solutionID:int?}",
                   defaults: new { controller = "Course", action = "Course" }

                   );
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });


        }
    }
}
