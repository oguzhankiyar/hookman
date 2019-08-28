using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class HookEntityConfiguration : IEntityTypeConfiguration<HookEntity>
    {
        public void Configure(EntityTypeBuilder<HookEntity> builder)
        {
            builder
                .ToTable(TableConstants.HookTableName, TableConstants.SchemaName)
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        
            builder
                .HasOne(x => x.Event)
                .WithMany(x => x.Hooks)
                .HasForeignKey(x => x.EventId);
                
            builder
                .HasOne(x => x.Sender)
                .WithMany(x => x.Hooks)
                .HasForeignKey(x => x.SenderId);

            builder
                .HasOne(x => x.Status)
                .WithMany()
                .HasForeignKey(x => x.StatusId);
        }
    }
}