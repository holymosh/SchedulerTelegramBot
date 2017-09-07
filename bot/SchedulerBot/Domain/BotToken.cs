using System.Xml;

namespace Domain
{
    public class BotToken
    {
        public static string Token { get; private set; }

        public BotToken()
        {
            Token = "token";
            XmlReader reader = XmlReader.Create("token.xml");
            while (reader.Read())
            {
                switch (reader.Name)
                {
                    case "token": Token = reader.ReadElementContentAsString();
                        break;
                }
            }
        }
    }
}
