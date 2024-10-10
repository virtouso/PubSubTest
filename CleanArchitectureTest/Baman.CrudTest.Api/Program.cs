using System.Reflection;
using Baman.CrudTest.Application;
using Baman.CrudTest.Common.Swagger;
using Baman.CrudTest.Infrastructure.Models;
using Baman.CrudTest.Infrastructure.ModelsRepository;
using Baman.CrudTest.Infrastructure.ModelsRepository.Events;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors();
builder.Services.AddControllersWithViews();
builder.Services.AddSwaggerVersion(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerDoc(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.AddSwaggerVersion(builder.Configuration, Assembly.GetExecutingAssembly());
builder.Services.InstallMediatr();
builder.Services.AddDbContext<CustomerDbContext>(optionsAction => { optionsAction.UseSqlServer("DefaultConnection"); });

builder.Services.AddScoped<ICustomerRepository, CustomerSqlRepository>();
builder.Services.AddScoped<ICustomerEventsRepository,CustomerEventSqlRepository>();
var app = builder.Build();


if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");

    app.UseHsts();
}

// in real project better to limit this to debug mode or define new debug modes. 
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<CustomerDbContext>();
    context.Database.Migrate();
    context.Database.EnsureCreated();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSwaggerDoc(builder.Environment);
app.UseRouting();
app.UseCors(options => options.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();