using System.Collections.Generic;

namespace FUT14AB
{
    public class Currency
    {
        public string name { get; set; }
        public int funds { get; set; }
        public int finalFunds { get; set; }
    }

    public class UnopenedPacks
    {
        public int preOrderPacks { get; set; }
        public int recoveredPacks { get; set; }
    }

    public class CreditsRootObject
    {
        public int credits { get; set; }
        public List<Currency> currencies { get; set; }
        public UnopenedPacks unopenedPacks { get; set; }
    }
}
