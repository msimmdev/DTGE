namespace DTGE.Common.Interfaces
{
    public interface IGameSerializable
    {
        IGameSerializationData GetSerializationData();
        void PopulateSerializationData(IGameSerializationData data);
    }
}
