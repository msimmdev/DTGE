namespace DTGE.Common.Interfaces
{
    public interface IGameSerializable
    {
        IGameDto GetDto();
        void UseDto(IGameDto data, IObjectResolver resolver);
    }
}
