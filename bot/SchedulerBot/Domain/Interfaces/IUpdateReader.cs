namespace Domain.Interfaces
{
    public interface IUpdateReader
    {
        string GetUserId(Update update);
        string GetCommand(Update update);
    }
}