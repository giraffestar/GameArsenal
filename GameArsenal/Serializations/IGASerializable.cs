namespace GameArsenal.Serializations
{
    public interface IGASerializable
    {
        int WriteTo(GASerializer serializer);
        void ReadFrom(GADeserializer deserializer);
        int GetSize();
    }
}