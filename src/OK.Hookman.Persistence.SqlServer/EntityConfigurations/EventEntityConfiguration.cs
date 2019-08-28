using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class EventEntityConfiguration : IEntityTypeConfiguration<EventEntity>
    {
        public void Configure(EntityTypeBuilder<EventEntity> builder)
        {
            builder
                .ToTable(TableConstants.EventTableName, TableConstants.SchemaName)
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();

            builder
                .HasOne(x => x.Sender)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.SenderId);
                
            builder
                .HasOne(x => x.Receiver)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.ReceiverId);
                
            builder
                .HasOne(x => x.Action)
                .WithMany(x => x.Events)
                .HasForeignKey(x => x.ActionId);
        }
    }
}