using System;
using System.Data.SqlClient;
using AdrianBanks.SqlDiff;
using AdrianBanks.SqlDiff.Comparison;
using AdrianBanks.SqlDiff.SchemaLoading;

namespace AdrianBanks.SqlDiffConsole
{
    internal static class Program
    {
        private static void Main()
        {
            Logger logger = new Logger("TEST");

            const string server = @"(local)";
            var right = new SqlConnectionStringBuilder(string.Format(@"Data Source={0};Initial Catalog=left;Integrated Security=SSPI", server));
            var left = new SqlConnectionStringBuilder(string.Format(@"Data Source={0};Initial Catalog=right;Integrated Security=SSPI", server));

            SchemaLoader schemaLoader = new SchemaLoader(logger);
            var leftSchema = schemaLoader.LoadSchema(left);
            var rightSchema = schemaLoader.LoadSchema(right);

            SchemaComparer comparer = new SchemaComparer(leftSchema, rightSchema);
            Report report = comparer.Compare();

            Console.WriteLine("Additions:   {0}", report.Additions.Count);
            Console.WriteLine("Differences: {0}", report.Differences.Count);
            Console.WriteLine("Equalities:  {0}", report.Equalities.Count);
            Console.WriteLine("Missings:    {0}", report.Missings.Count);

            Console.ReadLine();
        }
    }
}
