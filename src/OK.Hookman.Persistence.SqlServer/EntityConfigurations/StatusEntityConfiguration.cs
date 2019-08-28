using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OK.Hookman.Core.Enums;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.Constants;

namespace OK.Hookman.Persistence.SqlServer.EntityConfigurations
{
    public class StatusEntityConfiguration : IEntityTypeConfiguration<StatusEntity>
    {
        public void Configure(EntityTypeBuilder<StatusEntity> builder)
        {
            builder
                .ToTable(TableConstants.StatusTableName, TableConstants.SchemaName)
                .HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .IsRequired()
                .ValueGeneratedNever();

            builder.HasData(
                new StatusEntity() { Id = (int)StatusEnum.Created, Name = StatusEnum.Created.ToString(), IsDeleted = false },
                new StatusEntity() { Id = (int)StatusEnum.Sending, Name = StatusEnum.Sending.ToString(), IsDeleted = false },
                new StatusEntity() { Id = (int)StatusEnum.Sent, Name = StatusEnum.Sent.ToString(), IsDeleted = false },
                new StatusEntity() { Id = (int)StatusEnum.Failed, Name = StatusEnum.Failed.ToString(), IsDeleted = false },
                new StatusEntity() { Id = (int)StatusEnum.Canceled, Name = StatusEnum.Canceled.ToString(), IsDeleted = false });
        }
    }
}