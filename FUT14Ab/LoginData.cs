using System.Collections.Generic;

namespace FUT14AB
{
    public class LoginData
    {
        private static LoginData _instance;

        public static string Email;
        public static string Password;
        public static string SecurityHash;
        public static string Platform;

        public static List<string> UrLs = new List<string>();

        private LoginData() { }
        public static LoginData Instance => _instance ?? (_instance = new LoginData());
    }
}
