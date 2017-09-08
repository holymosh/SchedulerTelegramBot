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
            XmlReader reader = XmlReader.Create("botconfig.xml");
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "token": Token = reader.ReadElementContentAsString();
                        break;
                    case "url": Url = reader.ReadElementContentAsString();
                        break;
                    case "sendMessage": SendMessage = reader.ReadElementContentAsString();
                        break;
                }
            }
        }
    }
}
