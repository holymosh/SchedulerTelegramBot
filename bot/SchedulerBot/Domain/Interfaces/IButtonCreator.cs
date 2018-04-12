using Domain.TelegramEntities;

namespace Domain.Interfaces
{
    public interface IButtonCreator
    {
        InlineKeyboardMarkup CreateStartMenu(bool IsRegistered ,string messageId);
        InlineKeyboardMarkup CreateInlineInviteButton(string groupName , int groupId);
        AnswerInlineQuery CreateInlineAnswer(Update update, string groupName);
        InlineKeyboardMarkup CreateBackButton(string messageId);
        InlineKeyboardMarkup CreateWeekButtons(string messageId);
        InlineKeyboardMarkup CreateBackToWeekButton(string messageId);
    }
}