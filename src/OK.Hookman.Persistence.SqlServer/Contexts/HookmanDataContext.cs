using Microsoft.EntityFrameworkCore;
using OK.Hookman.Persistence.Core.Entities;
using OK.Hookman.Persistence.SqlServer.EntityConfigurations;

namespace OK.Hookman.Persistence.SqlServer.Contexts
{
    public class HookmanDataContext : DbContext
    {
        public virtual DbSet<ActionEntity> Actions { get; set; }
        public virtual DbSet<EventEntity> Events { get; set; }
        public virtual DbSet<HookEntity> Hooks { get; set; }
        public virtual DbSet<ReceiverEntity> Receivers { get; set; }
        public virtual DbSet<SenderEntity> Senders { get; set; }
        public virtual DbSet<StatusEntity> Statuses { get; set; }

        public HookmanDataContext(DbContextOptions<HookmanDataContext> options) : base(options)
        {
        }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
            modelBuilder.ApplyConfiguration(new ActionEntityConfiguration());
            modelBuilder.ApplyConfiguration(new EventEntityConfiguration());
            modelBuilder.ApplyConfiguration(new HookEntityConfiguration());
            modelBuilder.ApplyConfiguration(new ReceiverEntityConfiguration());
            modelBuilder.ApplyConfiguration(new SenderEntityConfiguration());
            modelBuilder.ApplyConfiguration(new StatusEntityConfiguration());
        }
    }
}