using System;
using System.Data;
using System.Diagnostics;

namespace AdrianBanks.SqlDiff.SchemaItems
{
    [DebuggerDisplay("Name = {name}, Type = {dataType}")]
    public sealed class Column : IEquatable<Column>
    {
        internal int ObjectId{get;set;}

        public string Name{get {return name;}}
        private readonly string name;

        public DbType DataType{get {return dataType;}}
        private readonly DbType dataType;

        public bool IsNullable{get {return isNullable;}}
        private readonly bool isNullable;
        
        public bool IsIdentity{get {return isIdentity;}}
        private readonly bool isIdentity;
        
        public string Collation{get {return collation;}}
        private readonly string collation;
        
        public int MaxLength{get {return maxLength;}}
        private readonly int maxLength;
        
        public int Precision{get {return precision;}}
        private readonly int precision;
        
        public int Scale{get {return scale;}}
        private readonly int scale;

        public Column(string name, DbType dataType, bool isNullable, bool isIdentity, string collation, int maxLength, int precision, int scale)
        {
            this.name = name;
            this.dataType = dataType;
            this.isNullable = isNullable;
            this.isIdentity = isIdentity;
            this.collation = collation;
            this.maxLength = maxLength;
            this.precision = precision;
            this.scale = scale;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Column);
        }

        public bool Equals(Column other)
        {
            return Equals(this, other);
        }

        private static bool Equals(Column x, Column y)
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
                   && x.dataType == y.dataType
                   && x.isNullable == y.isNullable
                   && x.isIdentity == y.isIdentity
                   && x.collation == y.collation
                   && x.maxLength == y.maxLength
                   && x.precision == y.precision
                   && x.scale == y.scale;
        }

        public override int GetHashCode()
        {
            return HashCodeUtil.GetHashCode(name, dataType, isNullable, isIdentity, collation, maxLength, precision, scale);
        }
   }
}