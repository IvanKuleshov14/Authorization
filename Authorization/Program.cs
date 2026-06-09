using Application;
using Application.Telegram;
using Application.Telegram.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseNpgsql(ConnectionString));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

var telegramToken = builder.Configuration["TELEGRAM_BOT_TOKEN"];
if (string.IsNullOrEmpty(telegramToken))
{
    throw new Exception("Токен телеграм бота не найден");
}

builder.Services.AddApplication(telegramToken);
builder.Services.AddInfrastructure();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Auth API",
        Version = "v1",
        Description = "Модуль двухфакторной аутентификации (Email/Telegram)"
    });

    var presentersXml = "Presenters.xml";
    var presentersXmlPath = Path.Combine(AppContext.BaseDirectory, presentersXml);
    if (File.Exists(presentersXmlPath)) options.IncludeXmlComments(presentersXmlPath);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<AuthorizationDbContext>();
    await dbContext.Database.MigrateAsync();
}

app.Run();
