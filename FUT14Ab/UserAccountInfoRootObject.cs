using System.Collections.Generic;

namespace FUT14AB
{
    public class SkuAccessList
    {
        public int __invalid_name__391A0001 { get; set; }
        public int? FFA14PS3 { get; set; }
    }

    public class UserClubList
    {
        public string year { get; set; }
        public int established { get; set; }
        public string clubName { get; set; }
        public int teamId { get; set; }
        public string platform { get; set; }
        public string clubAbbr { get; set; }
        public int divisionOnline { get; set; }
        public int badgeId { get; set; }
        public int lastAccessTime { get; set; }
        public SkuAccessList skuAccessList { get; set; }
    }

    public class Persona
    {
        public int personaId { get; set; }
        public string personaName { get; set; }
        public int returningUser { get; set; }
        public List<UserClubList> userClubList { get; set; }
    }

    public class UserAccountInfo
    {
        public List<Persona> personas { get; set; }
    }

    public class UserAccountInfoRootObject
    {
        public UserAccountInfo userAccountInfo { get; set; }
    }
}
