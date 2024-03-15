using FluentValidation;
using ProductService.Data;
using ProductService.Dtos;
using ProductService.Validators;

namespace ProductService;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container
    public void ConfigureServices(IServiceCollection services)
    {
        var dbConnectionString = Configuration.GetConnectionString("ProductDb");
        
        if(dbConnectionString == null)
        {
            throw new ArgumentNullException("ProductDb connection string is not found");
        }

        RegisterValidators(services);
        services.AddControllers();
        services.RegisterDataServices(dbConnectionString);
        services.AddExceptionHandler<GlobalExceptionHandler>();
    }
    
    private void RegisterValidators(IServiceCollection services)
    {
        services.AddValidatorsFromAssembly(typeof(CreateProductDtoValidator).Assembly);
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseDeveloperExceptionPage();
        }
        
        app.UseHttpsRedirection();
        app.UseExceptionHandler( opt => {});
        app.UseRouting();
        app.UseAuthorization();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
            endpoints.MapGet("/",
                async context =>
                {
                    await context.Response.WriteAsync("Welcome to running ASP.NET Core on AWS Lambda");
                });
        });
    }
}