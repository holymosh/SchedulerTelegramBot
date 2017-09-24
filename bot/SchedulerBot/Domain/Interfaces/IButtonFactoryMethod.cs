using Domain.TelegramEntities;

namespace Domain.Interfaces
{
    public interface IButtonFactoryMethod
    {
        InlineKeyboardMarkup CreateStartMenu(bool IsRegistered);
        InlineKeyboardMarkup CreateInviteButton();
    }
}