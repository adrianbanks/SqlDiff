using System.Diagnostics;

namespace AdrianBanks.SqlDiff.Comparison
{
    [DebuggerDisplay("Comparison = {comparisonType}, Object = {objectType}, Name = {name}")]
    public sealed class ComparisonObject
    {
        public ComparisonType ComparisonType{get {return comparisonType;}}
        private readonly ComparisonType comparisonType;

        public ObjectType ObjectType{get {return objectType;}}
        private readonly ObjectType objectType;

        public string Name{get {return name;}}
        private readonly string name;

        internal ComparisonObject(ComparisonType comparisonType, ObjectType objectType, string name)
        {
            this.comparisonType = comparisonType;
            this.objectType = objectType;
            this.name = name;
        }
    }
}