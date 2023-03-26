using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameArsenal.Serializations
{
    public class GASerializer : IDisposable
    {
        private readonly BinaryWriter writer;

        public GASerializer(MemoryStream memoryStream)
        {
            this.writer = new BinaryWriter(memoryStream, Encoding.UTF8);
        }

        public int Write<T>(in T serializable) where T : IGASerializable, new()
        {
            return serializable.WriteTo(this);
        }

        public int Write(in string serializable)
        {
            var sizeInBytes = sizeof(int);
            sizeInBytes += serializable.GetSize();

            if (serializable == null)
            {
                this.writer.Write(-1);
            }
            else if (serializable == string.Empty)
            {
                this.writer.Write(0);
            }
            else
            {
                var utf8 = new UTF8Encoding();
                this.writer.Write(serializable.GetSize());
                this.writer.Write(utf8.GetBytes(serializable));
            }

            return sizeInBytes;
        }

        public int Write(in bool serializable)
        {
            this.writer.Write(serializable);
            return sizeof(bool);
        }

        public int Write(in byte serializable)
        {
            this.writer.Write(serializable);
            return sizeof(byte);
        }

        public int Write(in sbyte serializable)
        {
            this.writer.Write(serializable);
            return sizeof(sbyte);
        }

        public int Write(in char serializable)
        {
            this.writer.Write(serializable);
            return sizeof(char);
        }

        public int Write(in short serializable)
        {
            this.writer.Write(serializable);
            return sizeof(short);
        }

        public int Write(in ushort serializable)
        {
            this.writer.Write(serializable);
            return sizeof(ushort);
        }

        public int Write(in int serializable)
        {
            this.writer.Write(serializable);
            return sizeof(int);
        }

        public int Write(in uint serializable)
        {
            this.writer.Write(serializable);
            return sizeof(uint);
        }

        public int Write(in long serializable)
        {
            this.writer.Write(serializable);
            return sizeof(long);
        }

        public int Write(in ulong serializable)
        {
            this.writer.Write(serializable);
            return sizeof(ulong);
        }

        public int Write(in float serializable)
        {
            this.writer.Write(serializable);
            return sizeof(float);
        }

        public int Write(in double serializable)
        {
            this.writer.Write(serializable);
            return sizeof(double);
        }

        public int Write(in decimal serializable)
        {
            this.writer.Write(serializable);
            return sizeof(decimal);
        }

        public int Write(in List<string> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<bool> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<byte> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<char> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<decimal> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<double> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<float> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<int> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public int Write(in List<long> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = Write(serializableList.Count);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += Write(serializable);
            }

            return sizeInBytes;
        }

        public void Dispose()
        {
            this.writer.Dispose();
        }
    }
}