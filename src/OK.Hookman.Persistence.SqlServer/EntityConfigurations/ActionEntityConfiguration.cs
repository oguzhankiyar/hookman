using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class ActionEntityConfiguration : IEntityTypeConfiguration<ActionEntity>
    {
        public void Configure(EntityTypeBuilder<ActionEntity> builder)
        {
            builder
                .ToTable(TableConstants.ActionTableName, TableConstants.SchemaName)
                .HasKey(x => x.Id);

            builder
                .Property(x => x.Id)
                .IsRequired()
                .ValueGeneratedOnAdd();
        }
    }
}