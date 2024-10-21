using DDD.DataAccess.Entities.Calendar;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DDD.DataAccess.Configurations.Calendar;

public class EventClientConfiguration : IEntityTypeConfiguration<EventClientEntity>
{
    public void Configure(EntityTypeBuilder<EventClientEntity> builder)
    {
        builder.HasKey(e => new { e.EventId, e.ClientId });
    }
}
