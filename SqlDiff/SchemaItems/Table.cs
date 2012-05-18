using System;
using System.Collections.Generic;
using System.Diagnostics;

namespace AdrianBanks.SqlDiff.SchemaItems
{
    [DebuggerDisplay("Name = {name}")]
    public sealed class Table : IEquatable<Table>
    {
        internal int ObjectId{get;set;}

        public string Name{get {return name;}}
        private readonly string name;

        public Column[] Columns{get {return columns.ToArray();}}
        private readonly List<Column> columns;

        public Table(string name)
        {
            this.name = name;
            this.columns = new List<Column>();
        }

        public void AddColumns(IEnumerable<Column> columnsToAdd)
        {
            columns.AddRange(columnsToAdd);
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Table);
        }

        public bool Equals(Table other)
        {
            return Equals(this, other);
        }

        private static bool Equals(Table x, Table y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.name.Equals(y.name)
                   && x.columns.Count == y.columns.Count
                   && x.columns.AreEquivalent(y.columns);
        }

        public override int GetHashCode()
        {
            return HashCodeUtil.GetHashCode(name, columns.Count, columns);
        }
    }
}