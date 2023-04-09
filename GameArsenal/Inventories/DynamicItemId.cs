using System;

namespace GameArsenal.Inventories
{
    public sealed class DynamicItemIdGenerator
    {
        private int raw;

        public DynamicItemIdGenerator(int startValue)
        {
            this.raw = startValue;
        }

        public DynamicItemId Generate()
        {
            return new DynamicItemId(this.raw++);
        }
    }

    public struct DynamicItemId : IEquatable<DynamicItemId>
    {
        public static readonly DynamicItemId Invalid = new DynamicItemId(0);

        private readonly int raw;

        public DynamicItemId(int raw)
        {
            this.raw = raw;
        }

        public static bool operator ==(DynamicItemId lhs, DynamicItemId rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(DynamicItemId lhs, DynamicItemId rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(DynamicItemId other)
        {
            return raw == other.raw;
        }

        public override bool Equals(object obj)
        {
            return obj is DynamicItemId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return raw;
        }

        public override string ToString()
        {
            return $"({nameof(DynamicItemId)}:{this.raw})";
        }
    }
}