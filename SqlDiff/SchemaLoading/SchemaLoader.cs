using System.Data.SqlClient;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    public sealed class SchemaLoader
    {
        private readonly ILogger logger;

        public SchemaLoader(ILogger logger)
        {
            this.logger = logger;
        }

        public Schema LoadSchema(SqlConnectionStringBuilder connectionString)
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

                ColumnLoader columnLoader = new ColumnLoader(logger);

                foreach (var table in tables)
                {
                    var columns = columnLoader.Load(connection, table);
                    table.AddColumns(columns);
                }

                var views = LoadViews(connection);
                var triggers = LoadTriggers(connection);
                return new Schema(tables, views, triggers);
            }
        }

        private View[] LoadViews(SqlConnection connection)
        {
            var viewLoader = new ViewLoader(logger);
            return viewLoader.Load(connection);
        }

        private Trigger[] LoadTriggers(SqlConnection connection)
        {
            var viewLoader = new TriggerLoader(logger);
            return viewLoader.Load(connection);
        }
    }
}