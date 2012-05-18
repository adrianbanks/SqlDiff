using System.Data;
using System.Collections.Generic;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal sealed class TriggerLoader
    {
        private readonly ILogger logger;

        public TriggerLoader(ILogger logger)
        {
            this.logger = logger;
        }

        public Trigger[] Load(IDbConnection connection)
        {
            var triggers = new List<Trigger>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT T.[object_id], T.[name], M.[definition] FROM sys.triggers T, sys.sql_modules M WHERE T.[object_id] = M.[object_id]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int objectId = reader.GetInt32(reader.GetOrdinal("object_id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        int definitionOrdinal = reader.GetOrdinal("definition");
                        string definition = reader.IsDBNull(definitionOrdinal) ? null : reader.GetString(definitionOrdinal);
                        
                        var view = new Trigger(name, definition) {ObjectId = objectId};
                        triggers.Add(view);
                    }
                }
            }

            triggers.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            return triggers.ToArray();
        }
    }
}