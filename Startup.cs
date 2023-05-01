using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OnBusyness.Behaviour.Error;
// using OnBusyness.Services.UserService;
using System.Text;
using OnBusyness.Services.EmailService;
using Quartz;


namespace OnBusyness {
    public class Startup {
        public Startup(IConfiguration configuration, IWebHostEnvironment env) {
            Configuration = configuration;
            _env = env;
        }

        public IConfiguration Configuration { get; set; }
        public IWebHostEnvironment _env { get; set; }

        #region  Configure Services
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services) {
            //* ---------- NOTE CONCEPT -------
            // services.Add(new ServiceDescriptor(serviceType: typeof(object), factory: sp => new object(), lifetime: ServiceLifetime.Scoped));
            // services.AddScoped<object>(sp => new object());  ---THIS IS ALTERNATIVE OF ABOVE
            //* -------------------------------
            //! SHOULD NewtonSoft is not default after 3.0 so done this for backwards compatibilty
            services.AddControllers().AddNewtonsoftJson();
            //cross origin resource sharing
            services.AddCors();
            // ! SHOULD must have to pass options else MVC routing not work
            services.AddMvc(options => options.EnableEndpointRouting = false).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });
            //used in error handling
            services.AddSingleton<IWebHostEnvironment>(sp => _env);

            //ASP.NET Core applications access the HTTPContext through the IHttpContextAccessor interface. The HttpContextAccessor class implements it.
            services.AddHttpContextAccessor();

            //! Custom Services - added 
            services.AddSingleton<IEmailService, EmailService>(); //email service

            //! we can access appSettings options in file or we can say registerd that serction in IOPtions
            services.Configure<JobsOptions.MailSenderJobOptions>(Configuration.GetSection(JobsOptions.MailSenderJobOptions.MailSenderJobOption));

            services.AddQuartz(options => {
                options.UseMicrosoftDependencyInjectionJobFactory();
                JobKey key = new JobKey("emailJob");
                options.AddJob<EmailSenderJob>(x => x.WithIdentity("emailJob"));
                options.AddTrigger(opt => {
                    opt.ForJob(key)
                    .WithIdentity("emailTrigger")
                    .WithSimpleSchedule(x => x
                        .WithIntervalInSeconds(5)
                        .RepeatForever()
                    );
                });
            });
            services.AddQuartzHostedService(opt => {
                opt.WaitForJobsToComplete = true;
            });


        }
        #endregion

        #region  Configure App
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if( env.IsDevelopment() ) {
                app.UseDeveloperExceptionPage();
            }
            else {
                app.UseHsts();
            }
            // ! Exception-handling delegates should be called early in the pipeline, so they can catch exceptions that occur in later stages of the pipeline.
            app.UseMiddleware(typeof(ErrorHandeling));
            // ! SHOULD ALWAYS BE HERE
            app.UseCors(
                options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader()
            );
            app.UseHttpsRedirection();
            // first check authentication of user
            app.UseAuthentication();
            //then check authorization
            app.UseAuthorization();
            //then open the doors 
            app.UseMvc();


        }
        #endregion
    }
}

