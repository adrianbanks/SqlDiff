using System.Collections.Generic;
using System.Data;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal sealed class TableLoader
    {
        private readonly ILogger logger;

        public TableLoader(ILogger logger)
        {
            this.logger = logger;
        }

        public Table[] Load(IDbConnection connection)
        {
            var tables = new List<Table>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT [object_id], [name] FROM sys.tables ORDER BY [name]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int objectId = reader.GetInt32(reader.GetOrdinal("object_id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        logger.Verbose("  Loaded table '{0}'", name);

                        var table = new Table(name) {ObjectId = objectId};
                        tables.Add(table);
                    }
                }
            }

            tables.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            return tables.ToArray();
        }
    }
}
