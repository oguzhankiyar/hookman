using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class ReceiverEntityConfiguration : IEntityTypeConfiguration<ReceiverEntity>
    {
        public void Configure(EntityTypeBuilder<ReceiverEntity> builder)
        {
            builder
                .ToTable(TableConstants.ReceiverTableName, TableConstants.SchemaName)
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}