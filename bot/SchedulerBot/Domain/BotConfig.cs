﻿using System.IO;
using System.Xml;

namespace Domain
{
    public class BotConfig
    {
        public static string Token { get; private set; }
        public static string Url { get; private set; }
        public static string SendMessage { get; set; }
        public BotConfig()
        {
            var localPath = @"C:\scheduler\bot\SchedulerBot\SchedulerBot\botconfig.xml";
            var productionPath = "botconfig.xml";
            var realPath = localPath;
            if (!File.Exists(localPath))
            {
                realPath = productionPath;
            }
            XmlReader reader = XmlReader.Create(realPath);
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "token": Token = reader.ReadElementContentAsString();
                        break;
                    case "url": Url = reader.ReadElementContentAsString();
                        break;
                    case "send": SendMessage = reader.ReadElementContentAsString();
                        break;
                }
            }
        }
    }
}
