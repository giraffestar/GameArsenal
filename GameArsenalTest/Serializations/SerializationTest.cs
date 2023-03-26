using GameArsenal.Serializations;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace GameArsenalTest.Serializations
{
    [TestClass]
    public class SerializationTest
    {
        public class OuterSerializationClass : IGASerializable
        {
            public int Integer;
            public bool Boolean;
            public long Long;
            public InnerSerializationClass Inner = new();
            public string NullText;
            public string EmptyText;
            public string EnglishText;
            public string KoreanText;
            public SerializationStruct Struct;
            public List<InnerSerializationClass> InnerList = new();
            public List<string> StringList = new();

            public int WriteTo(GASerializer serializer)
            {
                var sizeInBytes = serializer.Write(in this.Integer);
                sizeInBytes += serializer.Write(in this.Boolean);
                sizeInBytes += serializer.Write(in this.Long);
                sizeInBytes += serializer.Write(in this.Inner);
                sizeInBytes += serializer.Write(in this.NullText);
                sizeInBytes += serializer.Write(in this.EmptyText);
                sizeInBytes += serializer.Write(in this.EnglishText);
                sizeInBytes += serializer.Write(in this.KoreanText);
                sizeInBytes += serializer.Write(in this.Struct);
                sizeInBytes += serializer.Write(in this.InnerList);
                sizeInBytes += serializer.Write(in this.StringList);

                return sizeInBytes;
            }

            public void ReadFrom(GADeserializer deserializer)
            {
                deserializer.Read(out this.Integer);
                deserializer.Read(out this.Boolean);
                deserializer.Read(out this.Long);
                deserializer.Read(out this.Inner);
                deserializer.Read(out this.NullText);
                deserializer.Read(out this.EmptyText);
                deserializer.Read(out this.EnglishText);
                deserializer.Read(out this.KoreanText);
                deserializer.Read(out this.Struct);
                deserializer.Read(out this.InnerList);
                deserializer.Read(out this.StringList);
            }

            public int GetSize()
            {
                var sizeInBytes = sizeof(int);
                sizeInBytes += sizeof(bool);
                sizeInBytes += sizeof(long);
                sizeInBytes += this.Inner.GetSize();
                sizeInBytes += this.NullText.GetSize();
                sizeInBytes += this.EmptyText.GetSize();
                sizeInBytes += this.EnglishText.GetSize();
                sizeInBytes += this.KoreanText.GetSize();
                sizeInBytes += this.Struct.GetSize();
                sizeInBytes += this.InnerList.GetSize();
                sizeInBytes += this.StringList.GetSize();

                return sizeInBytes;
            }
        }

        public struct SerializationStruct : IGASerializable
        {
            public int Integer;

            public int WriteTo(GASerializer serializer)
            {
                var sizeInBytes = serializer.Write(this.Integer);

                return sizeInBytes;
            }

            public void ReadFrom(GADeserializer deserializer)
            {
                deserializer.Read(out this.Integer);
            }

            public int GetSize()
            {
                var sizeInBytes = sizeof(int);

                return sizeInBytes;
            }
        }

        public class InnerSerializationClass : IGASerializable
        {
            public short Short;

            public int WriteTo(GASerializer serializer)
            {
                var sizeInBytes = serializer.Write(this.Short);

                return sizeInBytes;
            }

            public void ReadFrom(GADeserializer deserializer)
            {
                deserializer.Read(out this.Short);
            }

            public int GetSize()
            {
                var sizeInBytes = sizeof(short);

                return sizeInBytes;
            }
        }

        [TestMethod]
        public void SerializerTest()
        {
            var serializable = new OuterSerializationClass();
            serializable.Integer = 100;
            serializable.Long = -100;
            serializable.Boolean = true;
            serializable.Inner.Short = 50;
            serializable.NullText = null;
            serializable.EmptyText = string.Empty;
            serializable.EnglishText = "Hello World";
            serializable.KoreanText = "헬로 월드";
            serializable.Struct.Integer = 200;
            serializable.InnerList = new List<InnerSerializationClass>()
            {
                new() {Short = 1},
                new() {Short = 2},
            };
            serializable.StringList = new List<string>()
            {
                "One",
                "Two",
            };

            var deserializable = new OuterSerializationClass();

            using (var memoryStream = new MemoryStream())
            {
                var serializer = new GASerializer(memoryStream);
                serializer.Write(serializable);

                memoryStream.Seek(0, SeekOrigin.Begin);

                var deserializer = new GADeserializer(memoryStream);
                deserializable.ReadFrom(deserializer);

                serializer.Dispose();
                deserializer.Dispose();
            }

            Assert.AreEqual(100, deserializable.Integer);
            Assert.AreEqual(-100, deserializable.Long);
            Assert.AreEqual(true, deserializable.Boolean);
            Assert.AreEqual(50, deserializable.Inner.Short);
            Assert.IsNull(deserializable.NullText);
            Assert.AreEqual(string.Empty, deserializable.EmptyText);
            Assert.AreEqual("Hello World", deserializable.EnglishText);
            Assert.AreEqual("헬로 월드", deserializable.KoreanText);
            Assert.AreEqual(200, deserializable.Struct.Integer);
            Assert.AreEqual(1, deserializable.InnerList[0].Short);
            Assert.AreEqual(2, deserializable.InnerList[1].Short);
            Assert.AreEqual("One", deserializable.StringList[0]);
            Assert.AreEqual("Two", deserializable.StringList[1]);
        }
    }
}