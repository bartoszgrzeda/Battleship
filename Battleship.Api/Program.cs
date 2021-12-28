using Battleship.Api.Extensions;
using Battleship.Infrastructure.EF;
using Battleship.Infrastructure.Services;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Battleship.Infrastructure.IoC;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .Build();

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());
builder.Host.ConfigureContainer<ContainerBuilder>(x => x.RegisterModule(new ContainerModule(configuration)));

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<BattleshipContext>();

var app = builder.Build();

app.UseCustomExceptionHandler();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var dataInitializer = app.Services.GetService<IDataInitializer>();
await dataInitializer.SeedAsync();

app.UseHttpsRedirection();

//app.UseAuthorization();

app.MapControllers();

app.Run();