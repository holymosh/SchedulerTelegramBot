﻿using System.Collections.Generic;
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
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Выйти из группы", "/exit")
            };
            IList<InlineKeyboardButton> joinButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Пригласить одногруппника", "/invite")
            };
            IList<InlineKeyboardButton> currentButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Какая сейчас пара?", "/current")
            };
            IList<InlineKeyboardButton> nameButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Как зовут препода, у которого сейчас пара?", "/name")
            };
            IList<InlineKeyboardButton> nextButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Какие пары остались?", "/next")
            };
            IList<InlineKeyboardButton> weekButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Расписание на эту неделю", "/week")
            };
            IList<InlineKeyboardButton> downloadButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("залить расписание", "/download")
            };
            IList<InlineKeyboardButton> messageButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("сообщение для группы", "/message")
            };
            IList<InlineKeyboardButton> fileButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("инструкция по заполнению и файл с форматом расписания", "/file")
            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                groupButton,
                joinButton,
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
            IList<InlineKeyboardButton> groupButton = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton("Присоединиться к группе", "/join")

            };

            IList<IList<InlineKeyboardButton>> listOfListOfButtons = new List<IList<InlineKeyboardButton>>
            {
                groupButton
            };
            return new InlineKeyboardMarkup(listOfListOfButtons);

        }

        private InlineKeyboardMarkup CreateUsualInviteButton(string groupName)
        {
            var inviteButton = new InlineKeyboardButton("Присоединиться к группе");
            inviteButton.callback_data = $"/join/{groupName}";
            inviteButton.url = "t.me/SchedulerLoDBot";
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
    }
}
