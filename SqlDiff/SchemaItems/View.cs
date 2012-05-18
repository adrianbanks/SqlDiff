using System;
using System.Diagnostics;

namespace AdrianBanks.SqlDiff.SchemaItems
{
    [DebuggerDisplay("Name = {name}")]
    public sealed class View : IEquatable<View>
    {
        internal int ObjectId{get;set;}
        
        public string Name{get {return name;}}
        private readonly string name;

        public string Definition{get {return definition;}}
        private readonly string definition;

        public View(string name, string definition)
        {
            this.name = name;
            this.definition = definition;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as View);
        }

        public bool Equals(View other)
        {
            return Equals(this, other);
        }

        private static bool Equals(View x, View y)
        {
            if (ReferenceEquals(x, y))
            {
                return true;
            }

            if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            {
                return false;
            }

            return x.name == y.name
                   && x.definition == y.definition;
        }

        public override int GetHashCode()
        {
            return HashCodeUtil.GetHashCode(name, definition);
        }
    }
}