using DataAccess.DBContexts.HRMSDB;
using DataAccess.Services.Interfaces;
using DataAccess.Services;
using Microsoft.EntityFrameworkCore;
using Common.Constants;
using Common.DataTransferObjects.Version;
using ApiConfiguration;
using DataAccess.UnitOfWorks.HRMSDB;

var builder = WebApplication.CreateBuilder(args);

ApiServices.ConfigureServices(builder.Services);

/*DBContext Registration*/
builder.Services.AddDbContextPool<HRMSDBContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("HRMSDB")));

/*UoW Registration*/
builder.Services.AddScoped<IHRMSDBUnitOfWork, HRMSDBUnitOfWork>();

builder.Services.AddScoped<IDbContextChangeTrackingService, DbContextChangeTrackingService>();

var app = builder.Build();

// Use CORS policy
app.UseCors("AllowSpecificOrigin");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();

app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers(); // Map controllers
    endpoints.MapGet("/", async context =>
    {
        context.Response.ContentType = ApiHomePageConstant.ContentType;
        await context.Response.WriteAsync(
            string.Format(
                ApiHomePageConstant.ContentFormat,
                "HRMSApi",
                app.Environment.EnvironmentName,
                context.Request.Scheme,
                context.Request.Host.Value,
                VersionDetail.DisplayVersion()
            )
        );
    });
});

app.Run();
