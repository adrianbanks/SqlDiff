using System.Data;
using System.Collections.Generic;
using AdrianBanks.SqlDiff.SchemaItems;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal sealed class ColumnLoader
    {
        private readonly ILogger logger;

        public ColumnLoader(ILogger logger)
        {
            this.logger = logger;
        }

        public Column[] Load(IDbConnection connection, Table table)
        {
            var columns = new List<Column>();

            using (var command = connection.CreateCommand())
            {
                command.CommandText = string.Format("SELECT C.[object_id], C.[name], T.[name] AS [system_type], C.[is_nullable], C.[is_identity], C.[collation_name], C.[max_length], C.[precision], C.[scale] FROM sys.columns C, sys.types T WHERE C.[system_type_id] = T.[user_type_id] AND [object_id] = {0} ORDER BY [name]", table.ObjectId);

                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        int objectId = reader.GetInt32(reader.GetOrdinal("object_id"));
                        string name = reader.GetString(reader.GetOrdinal("name"));
                        string systemTypeName = reader.GetString(reader.GetOrdinal("system_type"));
                        bool isNullable = reader.GetBoolean(reader.GetOrdinal("is_nullable"));
                        bool isIdentity = reader.GetBoolean(reader.GetOrdinal("is_identity"));
                        int collationOrdinal = reader.GetOrdinal("collation_name");
                        string collation = reader.IsDBNull(collationOrdinal) ? null : reader.GetString(collationOrdinal);
                        int maxLength = reader.GetInt16(reader.GetOrdinal("max_length"));
                        int precision = reader.GetByte(reader.GetOrdinal("precision"));
                        int scale = reader.GetByte(reader.GetOrdinal("scale"));
                        logger.Verbose("    Loaded column '{0}'", name);
                        DbType dataType = SystemTypeConverter.Convert(systemTypeName);

                        var column = new Column(name, dataType, isNullable, isIdentity, collation, maxLength, precision, scale) {ObjectId = objectId};
                        columns.Add(column);
                    }
                }
            }

            columns.Sort((x, y) => string.CompareOrdinal(x.Name, y.Name));
            return columns.ToArray();
        }
    }
}