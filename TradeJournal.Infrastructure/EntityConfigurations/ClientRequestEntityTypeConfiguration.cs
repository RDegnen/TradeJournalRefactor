
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TradeJournal.Infrastructure.Idempotency;

namespace TradeJournal.Infrastructure.EntityConfigurations;

public class ClientRequestEntityTypeConfiguration : IEntityTypeConfiguration<ClientRequest>
{
  public void Configure(EntityTypeBuilder<ClientRequest> requestConfiguration)
  {
    requestConfiguration.ToTable("requests");
  }
}
