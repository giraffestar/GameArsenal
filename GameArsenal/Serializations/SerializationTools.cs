using System;
using System.Collections.Generic;
using System.Text;

namespace GameArsenal.Serializations
{
    public static class SerializationTools
    {
        public static int GetSize(this string serializable)
        {
            return string.IsNullOrEmpty(serializable) ? 0 : Encoding.UTF8.GetByteCount(serializable);
        }

        public static int Write<T>(this GASerializer serializer, in List<T> serializableList) where T : IGASerializable, new()
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            serializer.Write(serializableList.Count);

            foreach (var serializable in serializableList)
            {
                sizeInBytes += serializer.Write(serializable);
            }

            return sizeInBytes;
        }

        public static void Read<T>(this GADeserializer deserializer, out List<T> serializableList) where T : IGASerializable, new()
        {
            serializableList = new List<T>();

            deserializer.Read(out int count);
            for (var i = 0; i < count; i++)
            {
                deserializer.Read(out T deserializable);
                serializableList.Add(deserializable);
            }
        }

        public static int GetSize<T>(this List<T> serializableList) where T : IGASerializable
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += serializable.GetSize();
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<string> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += serializable.GetSize();
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<bool> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(bool);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<byte> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(byte);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<char> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(char);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<decimal> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(decimal);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<double> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(double);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<float> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(float);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<int> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(int);
            }

            return sizeInBytes;
        }

        public static int GetSize(this List<long> serializableList)
        {
            if (serializableList == null)
            {
                throw new ArgumentException($"{nameof(serializableList)} cannot be null");
            }

            var sizeInBytes = sizeof(int);
            foreach (var serializable in serializableList)
            {
                sizeInBytes += sizeof(long);
            }

            return sizeInBytes;
        }
    }
}