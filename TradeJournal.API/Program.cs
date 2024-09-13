using Microsoft.EntityFrameworkCore;
using TradeJournal.API.Application.Queries;
using TradeJournal.Domain.Aggregates.JournalAggregate;
using TradeJournal.Domain.Aggregates.TradeAggregate;
using TradeJournal.Domain.Aggregates.TagAggregate;
using TradeJournal.Infrastructure;
using TradeJournal.Infrastructure.Idempotency;
using TradeJournal.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TradeJournalContext>(options =>
  options.UseMySQL(builder.Configuration.GetConnectionString("MySql")!), ServiceLifetime.Scoped);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IJournalRepository, JournalRepository>();
builder.Services.AddScoped<ITradeRepository, TradeRepository>();
builder.Services.AddScoped<ITagRepository, TagRepository>();
builder.Services.AddScoped<IRequestManager, RequestManager>();
builder.Services.AddMediatR(cfg =>
{
  cfg.RegisterServicesFromAssembly(typeof(Program).Assembly);
});
builder.Services.AddScoped<IJournalQueries, JournalQueries>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }