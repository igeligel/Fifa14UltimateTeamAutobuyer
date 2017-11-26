using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Web.Script.Serialization;
using System.Data;

using System.Threading;

namespace FUT14AB
{
    public class EaWebApi
    {
        public string Credits()
        {
            const string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/user/credits";
            const string post = "";

            var req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            CreditsRootObject returnedResponse = new JavaScriptSerializer().Deserialize<CreditsRootObject>(wichtig);
            string credits = returnedResponse.credits.ToString();

            return credits;
        }

        public List<string[]> GetTradepile()
        {
            const string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/tradepile";
            const string post = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            List<string> assetId = new List<string>();
            List<string> ressourceId = new List<string>();
            List<string> itemType = new List<string>();
            List<string> rating = new List<string>();
            List<string> currentBid = new List<string>();
            List<string> buyNowPrice = new List<string>();
            List<string> expires = new List<string>();

            TradepileRootObject returnedResponse = new JavaScriptSerializer().Deserialize<TradepileRootObject>(wichtig);
            foreach (var item in returnedResponse.auctionInfo)
            {
                assetId.Add(item.itemData.assetId);
                ressourceId.Add(item.itemData.resourceId);
                itemType.Add(item.itemData.itemType);
                rating.Add(item.itemData.rating);
                currentBid.Add(item.currentBid);
                buyNowPrice.Add(item.buyNowPrice);
                expires.Add(item.expires);
            }

            List<string[]> row = new List<string[]>();

            for (int j = 0; j < assetId.Count; j++)
            {
                row.Add(new[] { assetId[j], ressourceId[j], itemType[j], rating[j], currentBid[j], buyNowPrice[j], expires[j]});
            }

            return row;
        }

        public List<string[]> GetWatchlist()
        {
            string URL = "https://utas.s2.fut.ea.com/ut/game/fifa14/watchlist";
            string POST = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(POST);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            List<string> assetId = new List<string>();
            List<string> ressourceId = new List<string>();
            List<string> itemType = new List<string>();
            List<string> rating = new List<string>();
            List<string> currentBid = new List<string>();
            List<string> buyNowPrice = new List<string>();
            List<string> expires = new List<string>();

            Watchlist.WatchListRootObject returnedResponse = new JavaScriptSerializer().Deserialize<Watchlist.WatchListRootObject>(wichtig);
            foreach (var item in returnedResponse.auctionInfo)
            {
                assetId.Add(item.itemData.assetId);
                ressourceId.Add(item.itemData.resourceId);
                itemType.Add(item.itemData.itemType);
                rating.Add(item.itemData.rating);
                currentBid.Add(item.currentBid);
                buyNowPrice.Add(item.buyNowPrice);
                expires.Add(item.expires);
            }

            List<string[]> row = new List<string[]>();

            for (int j = 0; j < assetId.Count; j++)
            {
                row.Add(new[]
                {
                    assetId[j], ressourceId[j], itemType[j], rating[j],
                    currentBid[j], buyNowPrice[j], expires[j]
                });
            }

            return row;
        }

        public string PlayerSearch(string pos, string cS , string micr , string lev, string team, string minb, string type, string maxb, string macr, string leag, string nat, string start, string num)
        {
            string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/transfermarket?";
            if (pos != "")
            {
                url += "pos=" + pos;
            }
            if (cS != "")
            {
                url += "&playStyle=" +cS;
            }
            if (micr != "")
            {
                url += "&micr=" + micr;
            }
            if (lev != "")
            {
                url += "&lev=" + lev;
            }
            if (team != "")
            {
                url += "&team=" + team;
            }
            if (minb != "")
            {
                url += "&minb=" + minb;
            }
            if (type != "")
            {
                url += "&type=" + type;
            }
            if (maxb != "")
            {
                url += "&maxb=" + maxb;
            }
            if (macr != "")
            {
                url += "&macr=" + macr;
            }
            if (leag != "")
            {
                url += "&leag=" + leag;
            }
            if (nat != "")
            {
                url += "&nat=" + nat;
            }
            if (start != "" | num != "")
            {
                url += "&num=" + num + "&start=" + start;
            }
            else
            {
                url += "&num=16&start=0";
            }

            string POST = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(POST);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream, Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            return wichtig;
        }

        public string PostBid(string price, string tradeId)
        {
            var url = "https://utas.s2.fut.ea.com/ut/game/fifa14/trade/" + tradeId + "/bid";
            var post = "{\"bid\":" + price + "}";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "PUT");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();
            return wichtig;
        }

        public string PurchasedItems(string hilfId)
        {
            string URL = "https://utas.s2.fut.ea.com/ut/game/fifa14/purchased/items";
            string POST = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(POST);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            string ids = "";

            PI.PruchasedItemsRootObject returnedResponse = new JavaScriptSerializer().Deserialize<PI.PruchasedItemsRootObject>(wichtig);
            foreach (var item in returnedResponse.itemData)
            {
                if (hilfId == item.id)
                {
                    ids = item.id;
                }
            }
            return ids;
        }

        public void MoveToTp(string id)
        {
            const string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/item";
            string post = "{\"itemData\":[{\"id\":\""+ id  +"\",\"pile\":\"trade\"}]}";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "PUT");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            str.ReadToEnd();
            stream.Close();
        }

        public void SellOnTp(string id, string price)
        {
            const string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/auctionhouse";
            int hilfprice = ValueSell(Convert.ToInt32(price));
            int startBid = Convert.ToInt16(price) - hilfprice;
            string post = "{\"buyNowPrice\":" + price + ",\"startingBid\":" + startBid.ToString() + ",\"duration\":3600,\"itemData\":{\"id\":" + id + "}}";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "POST");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            str.ReadToEnd();
            stream.Close();
        }

        public List<string> ExpiredTradeIDsFromTp()
        {
            const string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/tradepile";
            const string post = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();


            List<string> tradeIDs = new List<string>();
            try
            {
                var returnedResponse = new JavaScriptSerializer().Deserialize<TradepileRootObject>(wichtig);
                foreach (var item in returnedResponse.auctionInfo)
                {
                    if (item.expires == "-1" && item.tradeState == "closed")
                    {
                        tradeIDs.Add(item.tradeId);
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
            return tradeIDs;
        }

        public void RemoveExpiredItems(string tradeId)
        {
            string url = "https://utas.s2.fut.ea.com/ut/game/fifa14/trade/" + tradeId;
            const string post = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(url);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "DELETE");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(post);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            str.ReadToEnd();
            stream.Close();
        }

        public void ResellTp()
        {
            string URL = "https://utas.s2.fut.ea.com/ut/game/fifa14/tradepile";
            string POST = "";

            HttpWebRequest req = (HttpWebRequest)WebRequest.Create(URL);
            req.MediaType = "HTTP/1.1";

            req.CookieContainer = AccountData.CookieContainer;

            req.Method = "POST";
            req.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:18.0) Gecko/20100101 Firefox/18.0";
            req.ContentType = "application/json; charset=UTF-8;";
            req.Headers.Add("X-HTTP-Method-Override", "GET");
            req.Headers.Add("X-UT-PHISHING-TOKEN", AccountData.WebPhishingToken);
            req.Headers.Add("X-UT-SID", AccountData.XutSid);

            ASCIIEncoding encoding = new ASCIIEncoding();
            byte[] loginDataBytes = encoding.GetBytes(POST);
            req.ContentLength = loginDataBytes.Length;
            Stream stream = req.GetRequestStream();
            stream.Write(loginDataBytes, 0, loginDataBytes.Length);
            stream.Close();
            HttpWebResponse res = (HttpWebResponse)req.GetResponse();

            Stream dataStream = res.GetResponseStream();
            StreamReader str = new StreamReader(dataStream ?? throw new InvalidOperationException(), Encoding.UTF8);
            var wichtig = str.ReadToEnd();
            stream.Close();

            List<string> iD = new List<string>();
            List<string> buyNowPrice = new List<string>();

            TradepileRootObject returnedResponse = new JavaScriptSerializer().Deserialize<TradepileRootObject>(wichtig);
            foreach (var item in returnedResponse.auctionInfo)
            {
                if (item.bidState == "none" && item.expires == "-1" && item.tradeState == "expired")
                {
                    iD.Add(item.itemData.id);
                    buyNowPrice.Add(item.buyNowPrice);
                }
            }
            for (int i = 0; i < iD.Count; i++)
            {
                int hilf = Convert.ToInt32(buyNowPrice[i]);
                int newPrice = hilf - (ValueDown(hilf));
                SellOnTp(iD[i], newPrice.ToString());
            }

        }



        //Hilfmethoden

        public int Pricecheck(string pos, string cS, string lev, string team, string maxb, string nat, string resId)
        {
            int hilf = 0;
            string type = "player";
            List<string> iD = new List<string>();
            List<int> prices = new List<int>();

            //1.Abfrage
            string response = PlayerSearch(pos, cS, "", lev, team, "", type, "", "", "", nat, "0", "12");
            SResponse.SResonseRootObject returnedResponse = new JavaScriptSerializer().Deserialize<SResponse.SResonseRootObject>(response);
            try
            {
                foreach (var item in returnedResponse.auctionInfo)
                {
                    if (item.itemData.resourceId == resId)
                    {
                        iD.Add(item.itemData.id);
                        prices.Add(item.buyNowPrice);
                    }
                    hilf += 1;
                }
            }
            catch (Exception)
            {
                // ignored
            }

            int durchläufe = 1;

            //Bis ins unendliche
            while (hilf != 0)
            {
                hilf = 0;
                string start = (durchläufe * 12).ToString();
                response = PlayerSearch(pos, cS, "", lev, team, "", type, "", "", "", nat, start, "13");
                SResponse.SResonseRootObject ret = new JavaScriptSerializer().Deserialize<SResponse.SResonseRootObject>(response);
                try
                {
                    foreach (var item in ret.auctionInfo)
                    {
                        if (item.itemData.resourceId == resId)
                        {
                            prices.Add(item.buyNowPrice);
                            iD.Add(item.itemData.id);
                        }
                        hilf += 1;
                    }
                }
                catch (Exception)
                {
                    // ignored
                }
                durchläufe += 1;
                Thread.Sleep(700);
            }
            DataTable d = new DataTable();
            d.Columns.Add("iD", typeof(string));
            d.Columns.Add("buyNowPrice", typeof(int));
            
            for (var i = 0; i < prices.Count; i++)
            {
                if (prices[i] != 0)
                {
                    d.Rows.Add(iD[i], prices[i]);
                }
            }
            EaMath math = new EaMath();
            int price = math.AvgPrice(d);
            d.Rows.Add("Durchschnittspreis", price);
            return price;
        }

        public List<string> SearchForBuy(string pos, string cS, string lev, string team, string maxb, string nat, string resId, string sell)
        {
            List<string> message = new List<string>();
            string tradeId;
            string buyNow;
            int hilf = 0;
            Thread.Sleep(500);
            string response = PlayerSearch(pos, cS, "", lev, team, "", "player", maxb, "", "", nat, "0", "12");
            SResponse.SResonseRootObject returnedResponse = new JavaScriptSerializer().Deserialize<SResponse.SResonseRootObject>(response);
            try
            {
                foreach (var item in returnedResponse.auctionInfo)
                {
                    if (item.itemData.resourceId == resId)
                    {
                        var hilfId = item.itemData.id;
                        tradeId = item.tradeId;
                        buyNow = item.buyNowPrice.ToString();
                        string texthilf = PostBid(buyNow, tradeId);
                        string id = PurchasedItems(hilfId);
                        MoveToTp(id);
                        SellOnTp(id, sell);
                        message.Add(texthilf);
                    }
                    hilf += 1;
                    Thread.Sleep(350);
                }
            }
            catch (Exception)
            {
                // ignored
            }

            int durchläufe = 1;

            //Durchlaufen mehrerer Seiten
            while (hilf != 0)
            {
                hilf = 0;
                string start = (durchläufe * 12).ToString();
                response = PlayerSearch(pos, cS, "", lev, team, "", "player", maxb, "", "", nat, start, "13");
                SResponse.SResonseRootObject ret = new JavaScriptSerializer().Deserialize<SResponse.SResonseRootObject>(response);
                try
                {
                    foreach (var item in ret.auctionInfo)
                    {
                        if (item.itemData.resourceId == resId)
                        {
                            var hilfId = item.itemData.id;
                            tradeId = item.tradeId;
                            buyNow = item.buyNowPrice.ToString();
                            string texthilf = PostBid(buyNow, tradeId);
                            string id = PurchasedItems(hilfId);
                            MoveToTp(id);
                            SellOnTp(id, sell);
                            message.Add(texthilf);
                        }
                        hilf += 1;
                    }
                    durchläufe += 1;
                    Thread.Sleep(350);
                }
                catch (Exception)
                {
                    // ignored
                }
            }
            return message;
        }

        public int ValueSell(int input)
        {
            int betrag = 0;
            if (input <= 1000)
            {
                betrag = 50;
            }
            else if (input <= 10000)
            {
                betrag = 100;
            }
            else if (input <= 50000)
            {
                betrag = 250;
            }
            else if (input <= 100000)
            {
                betrag = 500;
            }
            else if (input >= 100000)
            {
                betrag = 1000;
            }
            return betrag;
        }

        public string GetBuyMessage(string message)
        {
            if (message != "{\"debug\":\"\",\"string\":\"Not enough credit\",\"reason\":\"\",\"code\":\"470\"}" && message!= "{\"debug\":\"\",\"string\":\"Permission Denied\",\"reason\":\"\",\"code\":\"461\"}")
            {
                try
                {
                    string buyNowPrice = "";
                    BP.BoughtPlayerRootObject returnedResponse = new JavaScriptSerializer().Deserialize<BP.BoughtPlayerRootObject>(message);
                    foreach (var item in returnedResponse.auctionInfo)
                    {
                        buyNowPrice = item.buyNowPrice;
                    }
                    return buyNowPrice;
                }
                catch (Exception)
                {
                    return "";
                }
            }
            else
            {
                return "";
            }
        }

        public int ValueDown(int input)
        {
            int output = 0;
            if (input <= 1000)
            {
                if (input == 200 || input == 0)
                {
                    output = 0;
                }
                else
                {
                    output = 50;
                }
            }
            else if (input <= 10000)
            {
                output = 100;
            }
            else if (input <= 50000)
            {
                output = 250;
            }
            else if (input <= 100000)
            {
                output = 500;
            }
            else if (input > 100000)
            {
                output = 1000;
            }
            return output;
        }
    }
}
