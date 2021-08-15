namespace DTGE.Common.Interfaces
{
    public interface IGameSerializable
    {
        IGameDto GetDto();
        void UseDto(IGameDto data, IResolver resolver);
    }
}
