using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tibre.Core
{
    public abstract class SqlDataType
    {
    }

    public abstract class CharacterStringDataType : SqlDataType
    {
        public int Length {get; private set;}
        public CharacterStringDataType(int length)
        {
            Length = length;
        }
    }

    public class FixedLengthCharacterStringDataType : CharacterStringDataType
    {
        public FixedLengthCharacterStringDataType(int length)
            : base(length) { }
    }

    public class VaryingLengthCharacterStringDataType : CharacterStringDataType
    {
        public VaryingLengthCharacterStringDataType(int length)
            : base(length) { }
    }

    public class BooleanDataType : SqlDataType
    {
        
    }

    public class IntegerDataType : SqlDataType
    {
        public int Precision { get; private set; }
        public IntegerDataType(int precision)
        {
            Precision = precision;
        }
    }

    public class DecimalDataType : SqlDataType
    {
        public int Precision { get; private set; }
        public int Scale { get; private set; }
        public DecimalDataType(int precision, int scale)
        {
            Precision = precision;
            Scale = scale;
        }
    }

    public class FloatDataType : SqlDataType
    {
        public int Precision { get; private set; }
        public FloatDataType(int precision)
        {
            Precision = precision;
        }
    }

    public class DateTimeDataType : SqlDataType
    {
        public int Precision { get; private set; }
        public DateTimeDataType(int precision)
        {
            Precision = precision;
        }
    }

    public class DateDataType : SqlDataType
    {
        public DateDataType() { }
    }

    public class TimeDataType : SqlDataType
    {
        public TimeDataType() { }
    }

    public class NonGenericDataType : SqlDataType
    {
        public NonGenericDataType(string name, IEnumerable<int> args)
        {
            Name = name;
            Arguments = new List<int>(args).AsReadOnly();
        }

        public string Name { get; private set; }
        public IReadOnlyList<int> Arguments { get; private set; }
    }


}
