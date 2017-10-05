namespace Domain.Interfaces
{
    public interface IUpdateReader
    {
        string GetUserId(Update update);
        string GetActionData(Update update);
        bool IsInlineQuery(Update update);
        string GetCommand(Update update);
        string GetArgument(Update update);
        int GetMessageId(Update update);
    }
}