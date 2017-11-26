using System;
using System.Collections.Generic;
using System.Configuration;
using System.Windows.Forms;
using System.IO;
using System.Web.Script.Serialization;
using System.Threading;

namespace FUT14AB
{
    public partial class Form1 : Form
    {
        public Thread Thread;
        public Thread Thh;
        private readonly IList<DataBaseRootObject> _persons;
        private readonly EaWebApi _eaWebApi = new EaWebApi();
        private readonly string _filePath = ConfigurationManager.AppSettings["FilePath"];

        public Form1()
        {
            CheckForIllegalCrossThreadCalls = false;
            InitializeComponent();
            string json;
            using (var reader = new StreamReader(_filePath))
            {
                json = reader.ReadToEnd();
            }
            var d = new JavaScriptSerializer {MaxJsonLength = 86753090};

            _persons = d.Deserialize<IList<DataBaseRootObject>>(json);
            foreach (var person in _persons)
            {
                if (person.CommonName == "")
                {
                    cB_player.Items.Add(
                        person.FirstName + " " +
                        person.LastName + " (" +
                        person.Rating + ")");
                }
                else
                {
                    cB_player.Items.Add(person.CommonName + " (" +
                                        person.Rating + ")");
                }
            }

            cB_player.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            cB_player.AutoCompleteSource = AutoCompleteSource.ListItems;
            cB_player.Focus();

            var allStyles = new ChemistryStyle().GetAll();
            foreach (var chemistryStyle in allStyles)
            {
                cB_cs.Items.Add(chemistryStyle.Name);
            }
        }

        private void BT_Login_Click(object sender, EventArgs e)
        {
            Login login = new Login();
            LoginData.Email = TbMail.Text;
            LoginData.Password = TbPassword.Text;
            LoginData.SecurityHash = TbSecurity.Text;
            LoginData.Platform = "ps3";

            login.StartLogin();

            if (AccountData.WebPhishingToken != "")
            {
                lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Erfolgreich angemeldet");
            }
        }

        private void BT_LoginImport_Click(object sender, EventArgs e)
        {

        }

        private void BT_Test_Click(object sender, EventArgs a)
        {
            
        }

        private void cB_player_SelectedIndexChanged(object sender, EventArgs e)
        {
            //MessageBox.Show(cB_player.SelectedIndex.ToString());
        }

        private void BT_Add_Click(object sender, EventArgs e)
        {
            if (cB_player.SelectedIndex != -1 &&
                cB_version.SelectedIndex != -1 &&
                cB_position.SelectedIndex != -1 &&
                cB_version.SelectedIndex != -1 &&
                tb_buy.Text != "" &&
                tb_sell.Text != "")
            {
                string rat;
                var mystyle = new ChemistryStyle().GetAll();
                var chemistryStyle = mystyle[cB_cs.SelectedIndex].Id.ToString();

                var rating = Convert.ToInt32(_persons[cB_player.SelectedIndex].Rating);
                if (rating >= 75)
                {
                    rat = "gold";
                }
                else if (rating <= 64)
                {
                    rat = "bronze";
                }
                else
                {
                    rat = "silver";
                }
                int baseId = Convert.ToInt32(_persons[cB_player.SelectedIndex].bID);
                int version = Convert.ToInt32(cB_version.Items[cB_version.SelectedIndex]);
                int resId = baseId + 1610612736 + version * 50331648;
                string name;

                //Name bestimmen ;)
                if (_persons[cB_player.SelectedIndex].CommonName == "")
                {
                    name = _persons[cB_player.SelectedIndex].FirstName + " " + _persons[cB_player.SelectedIndex].LastName + " (" + _persons[cB_player.SelectedIndex].Rating + ")";
                }
                else
                {
                    name = _persons[cB_player.SelectedIndex].CommonName + " (" + _persons[cB_player.SelectedIndex].Rating + ")";
                }

                string nationId = _persons[cB_player.SelectedIndex].NationId;
                string clubId = _persons[cB_player.SelectedIndex].ClubId;
                string position = "";
                if (cB_position.SelectedIndex != -1)
                {
                    position = cB_position.Items[cB_position.SelectedIndex].ToString();
                }
                string buy = "";
                string sell = "";
                if (tb_buy.Text != "" && tb_sell.Text != "")
                {
                    buy = tb_buy.Text;
                    sell = tb_sell.Text;
                }
                object[] row = { rat, name, nationId, clubId, position,chemistryStyle, buy, sell, baseId.ToString(), resId.ToString() };
                dG_list.Rows.Add(row);
            }
            else
            {
                MessageBox.Show(@"Bitte alle Informationen angeben!");
            }
        }

        private void BT_import_Click(object sender, EventArgs e)
        {
            string pfad = string.Empty;

            string line = "";
            OpenFileDialog openFileDialog1 = new OpenFileDialog
            {
                Filter = @"txt files (*.txt)|*.txt|All files (*.*)|*.*"
            };
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
                pfad = openFileDialog1.FileName;
            if (pfad == "")
            {
            }
            else
            {
                using (StreamReader sr = new StreamReader(pfad))
                {
                    
                    line = sr.ReadToEnd();
                }
            } 
            IList<ListImportRootObject> listImport = new JavaScriptSerializer().Deserialize<IList<ListImportRootObject>>(line);
            try
            {
                foreach (ListImportRootObject listImportElement in listImport)
                {
                    object[] row = { listImportElement.art, listImportElement.name, listImportElement.nationId, listImportElement.clubId, listImportElement.position, listImportElement.c_s, 
                        listImportElement.buy, listImportElement.sell, listImportElement.bId, listImportElement.rId };
                    dG_list.Rows.Add(row);
                }
            }
            catch
            {
                // ignored
            }
        }

        private void BT_export_Click(object sender, EventArgs e)
        {
            var content = "";
            var saveFileDialog1 =
                new SaveFileDialog
                {
                    Filter = @".txt|*.txt",
                    Title = @"Speichern der Liste"
                };
            saveFileDialog1.ShowDialog();
            StreamWriter writer = null;
            if (saveFileDialog1.FileName != "")
            {
                writer = File.CreateText(saveFileDialog1.FileName);
            }

            for (int k = 0; k < dG_list.Rows.Count - 1; k++)
            {
                content += "{\"art\":\"" + dG_list.Rows[k].Cells[0].Value + "\",";
                content += "\"name\":\"" + dG_list.Rows[k].Cells[1].Value + "\",";
                content += "\"nationId\":\"" + dG_list.Rows[k].Cells[2].Value + "\",";
                content += "\"clubId\":\"" + dG_list.Rows[k].Cells[3].Value + "\",";
                content += "\"position\":\"" + dG_list.Rows[k].Cells[4].Value + "\",";
                content += "\"c_s\":\"" + dG_list.Rows[k].Cells[5].Value + "\",";
                content += "\"buy\":\"" + dG_list.Rows[k].Cells[6].Value + "\",";
                content += "\"sell\":\"" + dG_list.Rows[k].Cells[7].Value + "\",";
                content += "\"bId\":\"" + dG_list.Rows[k].Cells[8].Value + "\",";
                content += "\"rId\":\"" + dG_list.Rows[k].Cells[9].Value + "\"}";

                if (k != dG_list.Rows.Count-2)
                {
                    content += ",";
                }
                
            }
            try
            {
                writer?.WriteLine("[" + content + "]");
            }
            catch
            {
                // ignored
            }
            writer?.Close();
        }

        private void BT_StartAB_Click(object sender, EventArgs e)
        {
            Thread = new Thread(delegate()
            {
                List<string[]> listTp = new List<string[]>();
                for (int z = 0; z < 999999999; z++)
                {
                    Pricecheck();
                    
                    //Spielersuche
                    List<string> message = new List<string>();
                    if (listTp.Count < 28)
                    {
                        for (int k = 0; k < 600 / (dG_list.Rows.Count + 1); k++)
                        {
                            for (int j = 0; j < dG_list.Rows.Count - 1; j++)
                            {
                                try
                                {
                                    message = _eaWebApi.SearchForBuy(
                                        dG_list.Rows[j].Cells[4].Value.ToString(),
                                        dG_list.Rows[j].Cells[5].Value.ToString(),
                                        dG_list.Rows[j].Cells[0].Value.ToString(),
                                        dG_list.Rows[j].Cells[3].Value.ToString(),
                                        dG_list.Rows[j].Cells[6].Value.ToString(),
                                        dG_list.Rows[j].Cells[2].Value.ToString(),
                                        dG_list.Rows[j].Cells[9].Value.ToString(),
                                        dG_list.Rows[j].Cells[7].Value.ToString());
                                }
                                catch (Exception)
                                {
                                    // ignored
                                }
                                Thread.Sleep(500);
                                if (message.Count != 0)
                                {
                                    foreach (string messageElement in message)
                                    {
                                        string pri = _eaWebApi.GetBuyMessage(messageElement);
                                        if (pri != "")
                                        {
                                            int profit = Convert.ToInt32((dG_list.Rows[j].Cells[7].Value.ToString()));
                                            profit = profit * 95 / 100;
                                            profit = profit - Convert.ToInt32(pri);
                                            lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": " + dG_list.Rows[j].Cells[1].Value + " für " + pri + " gekauft (Profit: " + profit + ")");
                                        }
                                        else
                                        {
                                            lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Item verpasst oder zu wenig Coins :(");
                                        }
                                    }
                                }
                            }
                            RemExpiredCoins(listTp);
                        }
                    }
                    else
                    {
                        Thread.Sleep(600000);
                        lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Sicherheitssleep ausgeführt");
                        RemExpiredCoins(listTp);
                    }
                    lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Suche ausgeführt ;)");
                    ReLogin();
                }
            });
            Thread.Start();
        }

        private void BT_StopAB_Click(object sender, EventArgs e)
        {
            Thread.Abort();
        }

        public void Pricecheck()
        {
            EaWebApi eaWebApi = new EaWebApi();

            //Pricecheck
            for (int j = 0; j < dG_list.Rows.Count - 1; j++)
            {
                int price = 0;
                try
                {
                    price = eaWebApi.Pricecheck(
                    dG_list.Rows[j].Cells[4].Value.ToString(),
                    dG_list.Rows[j].Cells[5].Value.ToString(),
                    dG_list.Rows[j].Cells[0].Value.ToString(),
                    dG_list.Rows[j].Cells[3].Value.ToString(),
                    dG_list.Rows[j].Cells[6].Value.ToString(),
                    dG_list.Rows[j].Cells[2].Value.ToString(),
                    dG_list.Rows[j].Cells[9].Value.ToString());
                }
                catch (Exception)
                {
                    lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Error bei Pricecheck bei " + dG_list.Rows[j].Cells[1].Value);
                }
                Thread.Sleep(550);
                EaMath math = new EaMath();
                
                int sellprice = (price * Convert.ToInt32(tb_SellPercent.Text) / 100);
                sellprice = math.RoundPrice(Convert.ToDouble(sellprice));
                dG_list.Rows[j].Cells[7].Value = sellprice.ToString();

                if (chBnetto.Checked == false)
                {
                    int buyprice = (price * Convert.ToInt32(tb_BuyPercent.Text) / 100);
                    buyprice = math.RoundPrice(Convert.ToDouble(buyprice));
                    dG_list.Rows[j].Cells[6].Value = buyprice.ToString();
                }
                else
                {
                    int hilf = Nettogewinn(sellprice, Convert.ToInt32(tB_nettogewinn.Text));
                    dG_list.Rows[j].Cells[6].Value = hilf.ToString();
                }
            }
        }

        public void Login()
        {
            Login login = new Login();
            LoginData.Email = TbMail.Text;
            LoginData.Password = TbPassword.Text;
            LoginData.SecurityHash = TbSecurity.Text;
            LoginData.Platform = "ps3";

            login.StartLogin();

            if (AccountData.WebPhishingToken != "")
            {
                lbStandardLog.Items.Add(DateTime.Now.ToLongTimeString() + ": Erfolgreich angemeldet");
            }
        }

        public void ReLogin()
        {
            LoginData.Email = "";
            LoginData.Password = "";
            LoginData.Platform = "";
            LoginData.SecurityHash = "";
            LoginData.UrLs.Clear();

            AccountData.CookieContainer = null;
            AccountData.Credits = "";
            AccountData.NucId = "";
            AccountData.PersonaId = "";
            AccountData.PersonaName = "";
            AccountData.WebPhishingToken = "";
            AccountData.XutSid = "";
            try
            {
                Login();
            }
            catch (NullReferenceException)
            {
                try
                {
                    Login();
                }
                catch (NullReferenceException)
                {
                    try
                    {
                        Login();
                    }
                    catch (NullReferenceException)
                    {

                    }
                }
            }
        }

        public void RemExpiredCoins(List<string[]> listTp)
        {
            if (listTp == null) throw new ArgumentNullException(nameof(listTp));
            try
            {
                lb_credits.Text = _eaWebApi.Credits();

                listTp = _eaWebApi.GetTradepile();
                lb_TPCount.Text = listTp.Count.ToString();

                var tradeIds = _eaWebApi.ExpiredTradeIDsFromTp();
                foreach (var tradeId in tradeIds)
                {
                    _eaWebApi.RemoveExpiredItems(tradeId);
                }
                _eaWebApi.ResellTp();
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public int Nettogewinn(int input, int win)
        {
            int a = input - win;
            EaMath eaMath = new EaMath();
            int b = eaMath.ValueDown(a);
            return b;
        }
    }
}
