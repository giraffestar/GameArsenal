using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GameArsenal.Serializations
{
    public class GADeserializer : IDisposable
    {
        private readonly BinaryReader reader;

        public GADeserializer(MemoryStream memoryStream)
        {
            this.reader = new BinaryReader(memoryStream, Encoding.UTF8);
        }

        public void Read<T>(out T deserializable) where T : IGASerializable, new()
        {
            deserializable = new T();
            deserializable.ReadFrom(this);
        }

        public void Read(out string deserializable)
        {
            var sizeInBytes = this.reader.ReadInt32();

            if (sizeInBytes == -1)
            {
                deserializable = null;
            }
            else if (sizeInBytes == 0)
            {
                deserializable = string.Empty;
            }
            else
            {
                var utf8 = new UTF8Encoding();
                deserializable = utf8.GetString(this.reader.ReadBytes(sizeInBytes));
            }
        }

        public void Read(out bool deserializable)
        {
            deserializable = this.reader.ReadBoolean();
        }

        public void Read(out byte deserializable)
        {
            deserializable = this.reader.ReadByte();
        }

        public void Read(out sbyte deserializable)
        {
            deserializable = this.reader.ReadSByte();
        }

        public void Read(out char deserializable)
        {
            deserializable = this.reader.ReadChar();
        }

        public void Read(out short deserializable)
        {
            deserializable = this.reader.ReadInt16();
        }

        public void Read(out ushort deserializable)
        {
            deserializable = this.reader.ReadUInt16();
        }

        public void Read(out int deserializable)
        {
            deserializable = this.reader.ReadInt32();
        }

        public void Read(out uint deserializable)
        {
            deserializable = this.reader.ReadUInt32();
        }

        public void Read(out long deserializable)
        {
            deserializable = this.reader.ReadInt64();
        }

        public void Read(out ulong deserializable)
        {
            deserializable = this.reader.ReadUInt64();
        }

        public void Read(out float deserializable)
        {
            deserializable = this.reader.ReadSingle();
        }

        public void Read(out double deserializable)
        {
            deserializable = this.reader.ReadDouble();
        }

        public void Read(out decimal deserializable)
        {
            deserializable = this.reader.ReadDecimal();
        }

        public void Read(out List<string> serializableList)
        {
            serializableList = new List<string>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out string deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<bool> serializableList)
        {
            serializableList = new List<bool>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out bool deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<byte> serializableList)
        {
            serializableList = new List<byte>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out byte deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<char> serializableList)
        {
            serializableList = new List<char>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out char deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<decimal> serializableList)
        {
            serializableList = new List<decimal>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out decimal deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<double> serializableList)
        {
            serializableList = new List<double>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out double deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<float> serializableList)
        {
            serializableList = new List<float>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out float deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<int> serializableList)
        {
            serializableList = new List<int>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out int deserializable);
                serializableList.Add(deserializable);
            }
        }

        public void Read(out List<long> serializableList)
        {
            serializableList = new List<long>();

            Read(out int count);
            for (var i = 0; i < count; i++)
            {
                Read(out long deserializable);
                serializableList.Add(deserializable);
            }
        }


        public void Dispose()
        {
            this.reader.Dispose();
        }
    }
}