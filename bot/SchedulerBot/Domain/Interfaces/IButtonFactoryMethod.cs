using Domain.TelegramEntities;

namespace Domain.Interfaces
{
    public interface IButtonFactoryMethod
    {
        InlineKeyboardMarkup CreateStartMenu(bool IsRegistered);
        InlineKeyboardMarkup CreateInlineInviteButton(string groupName , int groupId);
        AnswerInlineQuery CreateInlineAnswer(Update update, string groupName);
    }
}