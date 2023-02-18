using E_Commerce.API.Concrete;
using E_Commerce.API.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services.RegisterServices(builder.Configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
   
}
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