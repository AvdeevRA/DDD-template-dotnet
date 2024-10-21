using DDD.DataAccess.Entities.Calendar;
using DDD.DataAccess.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.DataAccess.Configurations.Calendar;

public class EventConfiguration : IEntityTypeConfiguration<EventEntity>
{
    public void Configure(EntityTypeBuilder<EventEntity> builder)
    {
        builder.HasKey(e => e.Id);

        builder.Property(e => e.Name).IsRequired();

        builder
            .HasMany(e => e.Clients)
            .WithMany(c => c.Events)
            .UsingEntity<EventClientEntity>(
                l => l.HasOne<ClientEntity>().WithMany().HasForeignKey(e => e.ClientId),
                r => r.HasOne<EventEntity>().WithMany().HasForeignKey(e => e.EventId)
            );
    }
}
