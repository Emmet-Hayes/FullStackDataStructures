using FullStackDataStructureVisualizer.Server.Data;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// Get the database password from the environment
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

// Configure DbContext with Npgsql and include the secret password
var defaultConnection = builder.Configuration.GetConnectionString("DefaultConnection") + $"Password={dbPassword};";
builder.Services.AddDbContext<GraphDbContext>(options =>
    options.UseNpgsql(defaultConnection));
builder.Services.AddDbContext<TreeDbContext>(options =>
    options.UseNpgsql(defaultConnection));
builder.Services.AddDbContext<LListDbContext>(options =>
    options.UseNpgsql(defaultConnection));
builder.Services.AddDbContext<AArrayDbContext>(options =>
    options.UseNpgsql(defaultConnection));
builder.Services.AddDbContext<DDictDbContext>(options =>
    options.UseNpgsql(defaultConnection));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseDefaultFiles();
app.UseStaticFiles();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

//app.MapFallbackToFile("/index.html");

app.Run();
