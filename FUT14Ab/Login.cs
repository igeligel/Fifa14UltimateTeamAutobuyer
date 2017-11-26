using System;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web.Script.Serialization;

namespace FUT14AB
{
    public class Login
    {
        public CookieContainer CookieContainer = new CookieContainer();


        public void StartLogin()
        {
            var initialLogin = "http://www.easports.com/uk/fifa/football-club/ultimate-team";
            string iframeUrl = "http://www.easports.com/iframe/fut/?locale=en_GB&baseShowoffUrl=http%3A%2F%2Fwww.easports.com%2Fpl%2Ffifa%2Ffootball-club%2Fultimate-team%2Fshow-off&guest_app_uri=http%3A%2F%2Fwww.easports.com%2Fpl%2Ffifa%2Ffootball-club%2Fultimate-team";

            //Schritt 1
            string firstlocation = login_1(initialLogin); //RedirectRequest
            //MessageBox.Show(firstlocation);

            //Schritt 2
            string secondlocation = login_2(firstlocation, initialLogin); //RedirectRequest
            //MessageBox.Show(secondlocation);

            //Schritt 3
            string loginlocation = login_3(secondlocation, firstlocation); //RedirectRequest
            //MessageBox.Show(loginlocation);

            //Schritt 4
            string authedLocation = login_4(loginlocation, secondlocation); //LoginRequest
            //MessageBox.Show(authedLocation);
            //MessageBox.Show("Anzahl der Cookies: " + login._cookieContainer.Count.ToString());

            //Schritt 5
            //MessageBox.Show("authedLocation: " + authedLocation);
            //MessageBox.Show("loginlocation: " + loginlocation);
            string nextlocation = login_5(authedLocation, loginlocation); //RedirectRequest
            //MessageBox.Show("nextlocation: " + nextlocation);

            //Schritt 6
            string secondNextLocation = login_6(nextlocation, authedLocation); //RedirectRequest
            //MessageBox.Show(secondNextLocation);

            //Schritt 7
            string aa = login_7(iframeUrl, secondNextLocation); //RedirectRequest

            //Schritt 8
            string next = login_8(aa); //RedirectRequest

            //Schritt 9
            string showOffUrl = login_9(next, aa); //9

            //Schritt 10
            AccountData.NucId = GetEaswId(showOffUrl, next);

            //Schritt 11
            GetAccountInfo(showOffUrl);

            //Schritt 12
            Auth(showOffUrl);

            //Schritt 13
            GetWebPhishingToken(showOffUrl, LoginData.SecurityHash, AccountData.XutSid);

            AccountData.CookieContainer = CookieContainer;
        }

        //Schritt 1 // Cookies sammeln + Redirect
        public string login_1(string url)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            //request2.Referer = sourceUrl; //Es gibt keinen Referrer
            request2.CookieContainer = CookieContainer;
            string location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            LoginData.UrLs.Add(location);
            return location;
        }

        //Schritt 2 // Cookies sammeln + Redirect
        public string login_2(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            string location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            LoginData.UrLs.Add(location);
            return location;
        }

        //Schritt 3 // Cookies sammeln + Redirect
        public string login_3(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!String.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            string location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 4 // Login
        public string login_4(string url, string Ref)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.Method = "POST";
            var content = "email=" + LoginData.Email + "&password=" + LoginData.Password + "&_eventId=submit&_rememberMe=on&facebookAuth=";
            byte[] contentBytes = Encoding.UTF8.GetBytes(content);
            request.ContentLength = contentBytes.Length;
            request.CookieContainer = CookieContainer;
            request.ContentType = "application/x-www-form-urlencoded";
            var contentStream = request.GetRequestStream();
            contentStream.Write(contentBytes, 0, contentBytes.Length);
            contentStream.Close();
            string redirectUrl;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                CookieContainer.Add(response.Cookies);
                redirectUrl = response.Headers["Location"];
            }
            return redirectUrl;
        }

        //Schritt 5 // RedirectRequest
        public string login_5(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!string.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            string location;
            using (var response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 6 //
        public string login_6(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!String.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            String location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 7 //
        public string login_7(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!String.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            String location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 8 //
        public string login_8(string url, string Ref = "")
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!String.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            String location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 9 //
        public string login_9(string url, string Ref)
        {
            var request2 = (HttpWebRequest)WebRequest.Create(url);
            request2.AllowAutoRedirect = false;
            if (!String.IsNullOrEmpty(Ref))
                request2.Referer = Ref;
            request2.CookieContainer = CookieContainer;
            String location;
            using (HttpWebResponse response2 = (HttpWebResponse)request2.GetResponse())
            {
                location = response2.Headers["Location"];
                CookieContainer.Add(response2.Cookies);
            }
            return location;
        }

        //Schritt 10 //
        public string GetEaswId(string url, string Ref)
        {
            var request = (HttpWebRequest)WebRequest.Create(url);
            request.AllowAutoRedirect = false;
            request.Referer = Ref;
            request.CookieContainer = CookieContainer;
            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream ?? throw new InvalidOperationException()))
                    {
                        var getString = reader.ReadToEnd();
                        Match match = Regex.Match(getString, @".*var EASW_ID = \'(.*?)\'.*", RegexOptions.IgnoreCase);
                        String token = match.Value;
                        string id = Regex.Match(token, @"\d+").Value;
                        return id;
                    }
                }
            }
        }

        //Schritt 11 // AccountInfos abfangen, unter anderem: personaID, personaName (unter AccountInfo Singleton)
        public void GetAccountInfo(string referer)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.easports.com/iframe/fut/p/ut/game/fifa14/user/accountinfo?_=");
            request.CookieContainer = CookieContainer;
            request.Referer = referer;
            ExtendHttpHeader(ref request);

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream ?? throw new InvalidOperationException()))
                    {
                        var retStr = reader.ReadToEnd();
                        UserAccountInfoRootObject returnedResponse = new JavaScriptSerializer().Deserialize<UserAccountInfoRootObject>(retStr);
                        foreach (var item in returnedResponse.userAccountInfo.personas)
                        {
                            AccountData.PersonaId = item.personaId.ToString();
                            AccountData.PersonaName = item.personaName;
                        }
                    }
                }
            }
        }

        //Schritt 12 // Authentification; Abfangen der XutSid (unter AccountInfo Singleton)
        public void Auth(string showOffUrl)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.easports.com/iframe/fut/p/ut/auth");

            string content = "{ \"isReadOnly\": false, \"sku\": \"FUT14WEB\", \"clientVersion\": 1, \"nuc\": " + AccountData.NucId + ", \"nucleusPersonaId\": " + AccountData.PersonaId + ", \"nucleusPersonaDisplayName\": \"" + AccountData.PersonaName + "\", \"nucleusPersonaPlatform\": \"" + LoginData.Platform + "\", \"locale\": \"en-GB\", \"method\": \"authcode\", \"priorityLevel\":4, \"identification\": { \"authCode\": \"\" } }";
            //var content = JsonConvert.SerializeObject(authorizationViewModel);
            var contentBytes = Encoding.UTF8.GetBytes(content);
            request.ContentLength = contentBytes.Length;
            request.AllowAutoRedirect = false;
            request.ContentType = "application/json; charset=UTF-8;";
            request.Method = "POST";
            request.Referer = showOffUrl;
            ExtendHttpHeader(ref request);
            request.CookieContainer = CookieContainer;

            var contenStream = request.GetRequestStream();
            contenStream.Write(contentBytes, 0, contentBytes.Length);
            contenStream.Close();
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                CookieContainer.Add(response.Cookies);
                using (var responseStream = response.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream ?? throw new InvalidOperationException()))
                    {
                        Session returnedResponse = new JavaScriptSerializer().Deserialize<Session>(reader.ReadToEnd());
                        foreach (var item in returnedResponse.Sid)
                        {
                            AccountData.XutSid += item.ToString();
                        }
                    }
                }
            }
        }

        //Schritt 13 // Security Answer + SID
        public void GetWebPhishingToken(string referer, string secAnswer, string xutSessionId)
        {
            var request = (HttpWebRequest)WebRequest.Create("http://www.easports.com/iframe/fut/p/ut/game/fifa14/phishing/validate");
            request.CookieContainer = CookieContainer;
            request.Referer = referer;
            request.Method = "POST";


            ExtendHttpHeader(ref request);
            request.Headers.Add("X-UT-SID", xutSessionId);


            var content = "answer=" + secAnswer;
            byte[] contentBytes = Encoding.Default.GetBytes(content);

            request.ContentLength = contentBytes.Length;

            var contenStream = request.GetRequestStream();

            contenStream.Write(contentBytes, 0, contentBytes.Length);
            contenStream.Close();

            request.AllowAutoRedirect = false;
            using (var response = (HttpWebResponse)request.GetResponse())
            {
                using (var responseStream = response.GetResponseStream())
                {
                    CookieContainer.Add(response.Cookies);
                    using (var reader = new StreamReader(responseStream ?? throw new InvalidOperationException()))
                    {
                        var getString = reader.ReadToEnd();
                        //var pk = JsonConvert.DeserializeObject<PhishingKey>(getString);
                        PhishingKey returnedResponse = new JavaScriptSerializer().Deserialize<PhishingKey>(getString);
                        foreach (var item in returnedResponse.token)
                        {
                            AccountData.WebPhishingToken += item.ToString();
                        }
                    }
                }
            }
        }



        //Zusatzklassen

        private void ExtendHttpHeader(ref HttpWebRequest request)
        {
            request.UserAgent = "Mozilla/5.0 (Windows NT 6.1; WOW64; rv:23.0) Gecko/20100101 Firefox/23.0";
            request.Headers.Add("X-Requested-With", "XMLHttpRequest");
            request.Headers.Add("X-UT-Embed-Error", "true");
            request.Headers.Add("X-UT-Route", "https://utas.s2.fut.ea.com:443"); //evtl. bei xbox pc anders?! checken wenn zeit
            request.Headers.Add("Easw-Session-Data-Nucleus-Id", AccountData.NucId);
        }

        public class Session
        {
            public string ServerTime { get; set; }
            public object LastOnlineTime { get; set; }
            public string Sid { get; set; }
            public string IpPort { get; set; }
        }

        public class PhishingKey
        {
            public string debug { get; set; }
            public string @string { get; set; }
            public string reason { get; set; }
            public string token { get; set; }
            public string code { get; set; }
        }
       
    }
}
