using System.Data.SqlClient;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal sealed class SchemaLoader
    {
        private readonly ILogger logger;

        public SchemaLoader(ILogger logger)
        {
            this.logger = logger;
        }

        internal Schema LoadSchema(SqlConnectionStringBuilder connectionString)
        {
            logger.Verbose("Opening connection to database...");

            using (var connection = new SqlConnection(connectionString.ConnectionString))
            {
                connection.Open();
                logger.Verbose("Connection to database opened successfully");

                TableLoader tableLoader = new TableLoader(logger);
                logger.Debug("Loading tables...");
                var tables = tableLoader.Load(connection);
                logger.Debug("Tables loaded successfully");

                return new Schema(tables, null, null);
            }
        }
    }
}