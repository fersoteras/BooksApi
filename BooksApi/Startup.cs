using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using BooksApi.Models;
using BooksApi.Services;


namespace BooksApi
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
            //  the configuration instance to wifh appsettings.json to file's BookstoreDatabaseSettings section
            //  binds is registered in the DI container.
            //  For example, a BookstoreDatabaseSettings object's ConnectionString property is populated 
            //  with the BookstoreDatabaseSettings:ConnectionString property in appsettings.json.

            services.Configure<BookstoreDatabaseSettings>(
               Configuration.GetSection(nameof(BookstoreDatabaseSettings)));

            //The IBookstoreDatabaseSettings interface is registered in DI with a singleton service lifetime. 
            //When injected, the interface instance resolves to a BookstoreDatabaseSettings object.

            services.AddSingleton<IBookstoreDatabaseSettings>(sp =>
            sp.GetRequiredService<IOptions<BookstoreDatabaseSettings>>().Value);

            // Each service that needs to be running at request attention time should be addressed here, 
            services.AddSingleton<BookService>();

            services.AddMvc()
                                        //  .AddJsonOptions(options => options.UseMemberCasing())
                                         .SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
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
