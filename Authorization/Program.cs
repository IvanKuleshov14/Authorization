using Application;
using Application.Telegram;
using Application.Telegram.Interfaces;
using Infrastructure;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors();

var ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<AuthorizationDbContext>(options => options.UseNpgsql(ConnectionString));

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddOpenApi();

var telegramToken = builder.Configuration["TELEGRAM_BOT_TOKEN"];
if (string.IsNullOrEmpty(telegramToken))
{
    throw new Exception("Токен телеграм бота не найден");
}

builder.Services.AddApplication(telegramToken);
builder.Services.AddInfrastructure();

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
