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

        public InlineKeyboardMarkup CreateInlineInviteButton(string groupName,int groupId)
        {
            var inviteButton = new InlineKeyboardButton($"Присоединить одногруппника к {groupName}")
            {
                switch_inline_query = groupId.ToString()
            };

            IList<InlineKeyboardButton> inviteLine = new List<InlineKeyboardButton>
            {
                inviteButton
            };
            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                inviteLine
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);

        }

        public AnswerInlineQuery CreateInlineAnswer(Update update, string groupName)
        {
            var answer = new AnswerInlineQuery();
            answer.inline_query_id = update.inline_query.id;
            answer.results = new List<InlineQueryResultArticle>
            {
                new InlineQueryResultArticle{id = update.inline_query.query,
                    reply_markup = CreateUsualInviteButton(update.inline_query.query),
                input_message_content = new InputTextMessageContent {message_text = $"Присоединиться к группе {groupName}"},
                title = update.inline_query.query}
            };
            return answer;
        }

        private InlineKeyboardMarkup CreateMenuForAuthorizedUser()
        {
            IList<InlineKeyboardButton> exitButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Выйти из группы", "/exit")
            };
            IList<InlineKeyboardButton> joinButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Пригласить в группу", "/invite")
            };
            IList<InlineKeyboardButton> tomorrowButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Какие завтра пары?", "/tomorrow")
            };
            IList<InlineKeyboardButton> nameButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("У кого сейчас пара?", "/name")
            };
            IList<InlineKeyboardButton> nextButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Какие пары остались?", "/next")
            };
            IList<InlineKeyboardButton> weekButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Расписание на неделю", "/week")
            };
            IList<InlineKeyboardButton> downloadButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Изменить расписание", "/download")
            };
            IList<InlineKeyboardButton> messageButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Сообщение для группы", "/message")
            };
            

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                joinButton,
                tomorrowButton,
                nameButton,
                nextButton,
                weekButton,
                downloadButton,
                messageButton,
                exitButton
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);
        }

        private InlineKeyboardMarkup CreateMenuForUnauthorizedUser()
        {
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Создать группу", "/create")
            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                groupButton
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);

        }

        private InlineKeyboardMarkup CreateUsualInviteButton(string groupId)
        {
            var redirectButton = new InlineKeyboardButton
            {
                text = "Присоединиться",
                url = $"t.me/SchedulerLoDBot?start={groupId}"
            };
            IList<InlineKeyboardButton> redirectLine = new List<InlineKeyboardButton>
            {
                redirectButton
            };
            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                redirectLine
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);
        }
    }
}
