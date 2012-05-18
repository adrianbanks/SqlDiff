using System.Data;
using System.Collections.Generic;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal sealed class ViewLoader
    {
        private readonly ILogger logger;

        public ViewLoader(ILogger logger)
        {
            this.logger = logger;
        }

        public View[] Load(IDbConnection connection)
        {
            var views = new List<View>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = "SELECT V.[object_id], V.[name], M.[definition] FROM sys.views V, sys.sql_modules M WHERE V.[object_id] = M.[object_id]";

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int objectId = reader.GetInt32(reader.GetOrdinal("object_id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        int definitionOrdinal = reader.GetOrdinal("definition");
                        string definition = reader.IsDBNull(definitionOrdinal) ? null : reader.GetString(definitionOrdinal);

                        var view = new View(name, definition) {ObjectId = objectId};
                        views.Add(view);
                    }
                }
            }

            views.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            return views.ToArray();
        }
    }
}