using System.Net;

namespace FUT14AB
{
    public class AccountData
    {
        private static AccountData _instance;

        public static string NucId;
        public static string PersonaId;
        public static string PersonaName;
        public static string XutSid;
        public static string WebPhishingToken;

        //InGame Infos
        public static string Credits;


        public static CookieContainer CookieContainer = new CookieContainer();

        private AccountData() { }
        public static AccountData Instance => _instance ?? (_instance = new AccountData());
    }
}
