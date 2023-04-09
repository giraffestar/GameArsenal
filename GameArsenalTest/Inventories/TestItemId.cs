using System;

namespace GameArsenalTest.Inventories
{
    public sealed class TestItemIdGenerator
    {
        private int raw;

        public TestItemIdGenerator(int startValue)
        {
            this.raw = startValue;
        }

        public TestItemId Generate()
        {
            return new TestItemId(this.raw++);
        }
    }

    public readonly struct TestItemId : IEquatable<TestItemId>
    {
        public static readonly TestItemId Invalid = new TestItemId(0);

        private readonly int raw;

        public TestItemId(int raw)
        {
            this.raw = raw;
        }

        public static bool operator ==(TestItemId lhs, TestItemId rhs)
        {
            return lhs.Equals(rhs);
        }

        public static bool operator !=(TestItemId lhs, TestItemId rhs)
        {
            return !(lhs == rhs);
        }

        public bool Equals(TestItemId other)
        {
            return raw == other.raw;
        }

        public override bool Equals(object obj)
        {
            return obj is TestItemId other && Equals(other);
        }

        public override int GetHashCode()
        {
            return raw;
        }

        public override string ToString()
        {
            return $"({nameof(TestItemId)}:{this.raw})";
        }
    }
}