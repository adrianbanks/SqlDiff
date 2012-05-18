using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace AdrianBanks.SqlDiff.SchemaItems
{
    [DebuggerDisplay("Tables = {tables.Count}, Views = {views.Count}, Triggers = {triggers.Count}")]
    public sealed class Schema
    {
        public Table[] Tables{get {return tables.ToArray();}}
        private readonly List<Table> tables;

        public View[] Views{get {return views.ToArray();}}
        private readonly List<View> views;

        public Trigger[] Triggers{get {return triggers.ToArray();}}
        private readonly List<Trigger> triggers;

        public Schema(IEnumerable<Table> tables, IEnumerable<View> views, IEnumerable<Trigger> triggers)
        {
            this.tables = tables.ToList();
            this.views = views.ToList();
            this.triggers = triggers.ToList();
        }
    }
}
