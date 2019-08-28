using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class SenderEntityConfiguration : IEntityTypeConfiguration<SenderEntity>
    {
        public void Configure(EntityTypeBuilder<SenderEntity> builder)
        {
            builder
                .ToTable(TableConstants.SenderTableName, TableConstants.SchemaName)
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}