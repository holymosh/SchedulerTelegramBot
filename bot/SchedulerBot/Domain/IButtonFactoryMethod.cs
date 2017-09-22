using Domain.TelegramEntities;

namespace Domain
{
    public interface IButtonFactoryMethod
    {
        InlineKeyboardMarkup CreateStartMenu(bool IsRegistered);
    }
}