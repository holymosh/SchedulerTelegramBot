using System.Collections.Generic;
using Domain.Interfaces;
using Domain.TelegramEntities;

namespace Domain.Entities
{
    public class ButtonFactoryMethod:IButtonFactoryMethod
    {
        public InlineKeyboardMarkup CreateStartMenu(bool IsRegistered)
        {
            return IsRegistered ? 
                CreateMenuForAuthorizedUser() :
                CreateMenuForUnauthorizedUser();
        }

        private InlineKeyboardMarkup CreateMenuForAuthorizedUser()
        {
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Выйти из группы", "/groups"),
            };
            IList<InlineKeyboardButton> currentButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Какая сейчас пара?", "/current"),
            };
            IList<InlineKeyboardButton> nameButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Как зовут препода, у которого сейчас пара?", "/name"),
            };
            IList<InlineKeyboardButton> nextButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Какие пары остались?", "/next"),
            };
            IList<InlineKeyboardButton> weekButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Расписание на эту неделю", "/week"),
            };
            IList<InlineKeyboardButton> downloadButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("залить расписание", "/download"),
            };
            IList<InlineKeyboardButton> messageButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("сообщение для группы", "/message"),
            };
            IList<InlineKeyboardButton> fileButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("инструкция по заполнению и файл с форматом расписания", "/file")
            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>()
            {
                groupButton,
                currentButton,
                nameButton,
                nextButton,
                weekButton,
                downloadButton,
                messageButton,
                fileButton
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);
        }

        private InlineKeyboardMarkup CreateMenuForUnauthorizedUser()
        {
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>()
            {
                new InlineKeyboardButton("Присоединиться к группе", "/join"),

            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>()
            {
                groupButton
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);

        }
    }
}
