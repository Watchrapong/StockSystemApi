using Microsoft.EntityFrameworkCore;
using StockSystem.Api;
using StockSystem.Api.Data;
using StockSystem.Api.Exception;

var builder = WebApplication.CreateBuilder(args);

var AllowSpecificOrigins = "AllowOrigin";
builder.Services.AddCors(options =>
{
    options.AddPolicy(AllowSpecificOrigins, options => options.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("Content-Disposition"));
});

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

builder.Services.AddDependency(builder.Configuration);

builder.Services.AddExceptionHandler<AppExceptionHandler>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(AllowSpecificOrigins);
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();