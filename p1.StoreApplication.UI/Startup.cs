using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using p1.StoreApplication.Context;
using p1.StoreApplication.Logic.Repositories;
using p1.StoreApplication.Logic.Interfaces;
using p1.StoreApplication.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Rewrite;

namespace p1.StoreApplication.UI
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

      services.AddControllers();
      services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "p1.StoreApplication.UI", Version = "v1" });
      });
      services.AddDbContext<StoreApplicationDBContext>(options =>
      {
        if(!options.IsConfigured)
        {
          options.UseSqlServer(Configuration.GetConnectionString("DevDb"));
        }
      });
      services.AddScoped<ICustomerRepository, CustomerRepository>();
      services.AddScoped<IStoreRepository, StoreRepository>();
      services.AddScoped<IProductRepository, ProductRepository>();
      services.AddScoped<IOrderProductRepository, OrderProductRepository>();
      services.AddScoped<IOrderRepository, OrderRepository>();
      services.AddScoped<IModelMapper, ModelMapper>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "p1.StoreApplication.UI v1"));
      }

      app.UseStatusCodePages();

      app.UseHttpsRedirection();

      app.UseRewriter(new RewriteOptions().AddRedirect("^$", "index.html"));

      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthorization();

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapControllers();
      });
    }
  }
}
