namespace OK.Hookman.Persistence.SqlServer.Config
{
    public class HookmanSqlServerPersistenceConfig
    {
        public string ConnectionString { get; set; }

        public HookmanSqlServerPersistenceConfig()
        {

        }

        public HookmanSqlServerPersistenceConfig(string connectionString)
        {
            this.ConnectionString = connectionString;
        }
    }
}