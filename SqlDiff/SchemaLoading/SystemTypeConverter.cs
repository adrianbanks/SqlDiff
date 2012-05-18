using System;
using System.Data;

namespace AdrianBanks.SqlDiff.SchemaLoading
{
    internal static class SystemTypeConverter
    {
         internal static DbType Convert(string systemTypeName)
         {
             // http://msdn.microsoft.com/en-us/library/cc716729.aspx
             switch (systemTypeName)
             {
                 case "bigint":
                     return DbType.Int64;
                 case "binary":
                     return DbType.Binary;
                 case "bit":
                     return DbType.Boolean;
                 case "char":
                     return DbType.AnsiStringFixedLength;
                 case "date":
                     return DbType.Date;
                 case "datetime":
                     return DbType.DateTime;
                 case "datetime2":
                     return DbType.DateTime2;
                 case "datetimeoffset":
                     return DbType.DateTimeOffset;
                 case "decimal":
                     return DbType.Decimal;
                 case "float":
                     return DbType.Double;
                 case "image":
                     return DbType.Binary;
                 case "int":
                     return DbType.Int32;
                 case "money":
                     return DbType.Decimal;
                 case "nchar":
                     return DbType.StringFixedLength;
                 case "ntext":
                     return DbType.String;
                 case "numeric":
                     return DbType.Decimal;
                 case "nvarchar":
                     return DbType.String;
                 case "real":
                     return DbType.Single;
                 case "smalldatetime":
                     return DbType.DateTime;
                 case "smallint":
                     return DbType.Int16;
                 case "smallmoney":
                     return DbType.Decimal;
                 case "sql_variant":
                     return DbType.Object;
                 case "text":
                     return DbType.String;
                 case "time":
                     return DbType.Time;
                 case "timestamp":
                     return DbType.Binary;
                 case "tinyint":
                     return DbType.Byte;
                 case "uniqueidentifier":
                     return DbType.Guid;
                 case "varbinary":
                     return DbType.Binary;
                 case "varchar":
                     return DbType.AnsiString;
                 case "xml":
                     return DbType.Xml;
                 case "hierarchyid":
                 case "geometry":
                 case "geography":
                 case "sysname":
                 default:
                     throw new InvalidOperationException("Unsupported type: " + systemTypeName);
             }
         }
    }
}