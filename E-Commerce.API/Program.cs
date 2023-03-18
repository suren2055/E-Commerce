using E_Commerce.API.Concrete;
using E_Commerce.API.Extensions;
using MongoDB.Driver;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);

try
{
    builder.Host.UseSerilog((ctx, lc) 
        => lc.WriteTo.Console().ReadFrom.Configuration(ctx.Configuration));
}
catch (Exception e)
{
    Console.WriteLine(e);
    throw;
}


builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
app.UseSerilogRequestLogging();
app.UseHsts();
using(var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<EFDBContext>();
    context.Database.EnsureCreated();
}
app.UseSwagger();
app.UseSwaggerUI();
//app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();
app.Run();