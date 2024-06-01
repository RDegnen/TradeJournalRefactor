using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TradeJournal.Infrastructure;

namespace TradeJournal.IntegrationTests;

public class TradeJournalApiFixture<TProgram> : WebApplicationFactory<TProgram> where TProgram : class
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    base.ConfigureWebHost(builder);

    builder.ConfigureServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(d =>
        d.ServiceType == typeof(DbContextOptions<TradeJournalContext>));

      if (dbContextDescriptor != null)
      {
        services.Remove(dbContextDescriptor);
      }

      services.AddDbContext<TradeJournalContext>(options =>
      {
        options.UseInMemoryDatabase("TestDb");
      });

      var sp = services.BuildServiceProvider();
      using (var scope = sp.CreateScope())
      {
        var scopedServices = scope.ServiceProvider;
        var db = scopedServices.GetRequiredService<TradeJournalContext>();
        db.Database.EnsureCreated();
      }
    });
  }
}
