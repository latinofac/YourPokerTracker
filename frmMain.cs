using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTracker
{
    public partial class frmMain : Form
    {

        //TODO:não carregar mãos de limit

        SQL sql = new SQL();
        List<PlayerToGrid> list = new List<PlayerToGrid>();
        DataTable dt = new DataTable();
        DataTable dtSession = new DataTable();
        HandLoader hl = new HandLoader();
        private string folderHands = @"C:\Pessoal\Poker\Hands\latinofac\";
        private string backupHands = @"C:\Pessoal\Poker\PokerTracker\PokerTracker\hands\";        

        public struct PlayerToGrid
        {
            public string Nickname;
            public int TotalHands;
            public double NetWon;
            public double VPIP;
            public double PFR;
            public double BB;
        }

        public frmMain()
        {
            this.Icon = new Icon(@"Images\poker.ico");
            InitializeComponent();
            this.btnSessions.Image = new Bitmap(Image.FromFile(@"Images\sessions.png"),126,126);
            this.btnHands.Image = new Bitmap(Image.FromFile(@"Images\hands.jpg"), 126, 126);
            this.btnResults.Image = new Bitmap(Image.FromFile(@"Images\results.jpg"), 126, 126);
            this.btnConfig.Image = new Bitmap(Image.FromFile(@"Images\config2.jpg"), 126, 126);
            this.btnLoad.Image = new Bitmap(Image.FromFile(@"Images\loading.jpg"), 126, 126);
            this.btnClearDB.Image = new Bitmap(Image.FromFile(@"Images\cleardb.jpg"), 126, 126);
            this.btnExit.Image = new Bitmap(Image.FromFile(@"Images\exit.jpg"), 126, 126);
            CheckConfigs();
            LoadPlayersIntoGrid();
            this.prgBar.Visible = false;
        }

        private void CheckConfigs()
        {
            Config config = new Config();

            if (!config.IsDefined())
            {
                MessageBox.Show("Please select the Hand Path where your hands are saved, and the backup path to where they yill be copied.");
                
                frmConfig frmconfig = new frmConfig();
                frmconfig.btnExit.Enabled = false;
                frmconfig.ShowDialog();
            }

            folderHands = config.GetHandPath() + @"\";
            backupHands = config.GetBackupPath() + @"\";
        }

        private void LoadPlayersIntoGrid()
        {
            SQLiteDataReader dr = sql.Select("SELECT P.NICKNAME, P.TOTALHANDS, SUM(HP.NETWON) NETWON, SUM(HP.VPIP)/P.TOTALHANDS VPIP, SUM(HP.PFR)/P.TOTALHANDS PFR, ROUND(SUM(BB)*100/P.TOTALHANDS,2) BB FROM PLAYERS P INNER JOIN HANDPLAYERS HP ON P.IDPLAYER = HP.IDPLAYER GROUP BY P.IDPLAYER ORDER BY P.TOTALHANDS DESC");
            if (dt.Columns.Count == 0)
            {
                dt.Columns.Add("Nickname", typeof(string));
                dt.Columns.Add("TotalHands", typeof(int));
                dt.Columns.Add("NetWon", typeof(double));
                dt.Columns.Add("VPIP", typeof(double));
                dt.Columns.Add("PFR", typeof(double));
                dt.Columns.Add("BB/100", typeof(double));
            }
            else
            {
                dt.Clear();
            }

            while (dr.Read())
            {
                dt.Rows.Add(dr["Nickname"].ToString(), Convert.ToInt32(dr["TotalHands"]), Math.Round(Convert.ToDouble(dr["NetWon"]), 2), Math.Round(Convert.ToDouble(dr["VPIP"]), 2), Math.Round(Convert.ToDouble(dr["PFR"]), 2), Convert.ToDouble(dr["BB"]));
            }

            this.gridPlayers.DataSource = dt;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = string.Format("Nickname like '{0}%'", this.textBox1.Text);
        }


        private void LoadSessions2()
        {
            FormTeste2 frm = new FormTeste2();

            SQLiteDataReader dr = sql.Select("SELECT IDSession, DateSession, TimeStart, TimeEnd, TotalHands, NetWon FROM SESSIONS ORDER BY IDSESSION DESC");

            while (dr.Read())
            {
                /*
                frm.dataGridView1.Rows.Add(Convert.ToInt32(dr["IDSession"]).ToString(),
                                            Convert.ToDateTime(dr["DateSession"].ToString().Substring(2, 2) + "/" + dr["DateSession"].ToString().Substring(0, 2) + "/" + dr["DateSession"].ToString().Substring(4, 4)).ToShortDateString(),
                                            dr["TimeStart"].ToString().Substring(0, dr["TimeStart"].ToString().Length - 4) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 4, 2) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 2, 2),
                                            dr["TimeEnd"].ToString().Substring(0, dr["TimeEnd"].ToString().Length - 4) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 4, 2) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 2, 2),
                                            Convert.ToInt32(dr["TotalHands"]),
                                            Math.Round(Convert.ToDouble(dr["NetWon"]), 2));
                                            */
                frm.dataGridView1.Rows.Add(Convert.ToInt32(dr["IDSession"]).ToString(),
                DateTime.ParseExact(dr["DateSession"].ToString().Substring(2, 2) + "/" + dr["DateSession"].ToString().Substring(0, 2) + "/" + dr["DateSession"].ToString().Substring(4, 4),"MM/dd/yyyy",CultureInfo.InvariantCulture).ToShortDateString(),
                dr["TimeStart"].ToString().Substring(0, dr["TimeStart"].ToString().Length - 4) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 4, 2) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 2, 2),
                dr["TimeEnd"].ToString().Substring(0, dr["TimeEnd"].ToString().Length - 4) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 4, 2) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 2, 2),
                Convert.ToInt32(dr["TotalHands"]),
                Math.Round(Convert.ToDouble(dr["NetWon"]), 2));
            }

            //frm.dataGridView1.Sort(frm.dataGridView1.Columns[2], ListSortDirection.Descending);
            //frm.dataGridView1.Sort(frm.dataGridView1.Columns[1], ListSortDirection.Descending);            
            frm.Show();
        }

        private void LoadSessions()
        {
            frmSessions formSession = new frmSessions();

            SQLiteDataReader dr = sql.Select("SELECT DateSession, TimeStart, TimeEnd, TotalHands, NetWon FROM SESSIONS ORDER BY IDSESSION DESC");
            DataGridView dataGridView1 = new DataGridView();

            dataGridView1.ColumnCount = 5;
            dataGridView1.Columns[0].Name = "Date Session";
            dataGridView1.Columns[1].Name = "Time Start";
            dataGridView1.Columns[2].Name = "Time End";
            dataGridView1.Columns[3].Name = "Total Hands";
            dataGridView1.Columns[4].Name = "Net Won";
            dataGridView1.Name = "MyView";

            while (dr.Read())
            {
                /*
                string[] row = new string[] { Convert.ToDateTime(dr["DateSession"].ToString().Substring(2, 2) + "/" + dr["DateSession"].ToString().Substring(0, 2) + "/" + dr["DateSession"].ToString().Substring(4, 4)).ToShortDateString(),
                                            dr["TimeStart"].ToString().Substring(0, dr["TimeStart"].ToString().Length - 4) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 4, 2) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 2, 2),
                                            dr["TimeEnd"].ToString().Substring(0, dr["TimeEnd"].ToString().Length - 4) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 4, 2) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 2, 2),
                                            Convert.ToInt32(dr["TotalHands"]).ToString(),
                                            Math.Round(Convert.ToDouble(dr["NetWon"]), 2).ToString()};
                */
                dataGridView1.Rows.Add(Convert.ToDateTime(dr["DateSession"].ToString().Substring(2, 2) + "/" + dr["DateSession"].ToString().Substring(0, 2) + "/" + dr["DateSession"].ToString().Substring(4, 4)).ToShortDateString(),
                                            dr["TimeStart"].ToString().Substring(0, dr["TimeStart"].ToString().Length - 4) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 4, 2) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 2, 2),
                                            dr["TimeEnd"].ToString().Substring(0, dr["TimeEnd"].ToString().Length - 4) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 4, 2) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 2, 2),
                                            Convert.ToInt32(dr["TotalHands"]),
                                            Math.Round(Convert.ToDouble(dr["NetWon"]), 2));
            }
                        
            DataGridViewButtonColumn btn = new DataGridViewButtonColumn();
            dataGridView1.Columns.Add(btn);
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Descending);
            btn.HeaderText = "Click Data";
            btn.Text = "Click Here";
            btn.Name = "btn";
            btn.UseColumnTextForButtonValue = true;

            formSession.gridSessions.DataSource = dataGridView1;
            formSession.gridSessions.DataMember = "MyView";
            formSession.Show();
        }

        private void btnSessions_Click(object sender, EventArgs e)
        {
            LoadSessions2();
            //LoadSessions();
            /*
            if (1 == 1)
            {
                frmSessions formSession = new frmSessions();

                SQLiteDataReader dr = sql.Select("SELECT DateSession, TimeStart, TimeEnd, TotalHands, NetWon FROM SESSIONS ORDER BY IDSESSION DESC");

                if (dtSession.Columns.Count == 0)
                {
                    dtSession.Columns.Add("DateSession", typeof(string));
                    dtSession.Columns.Add("TimeStart", typeof(string));
                    dtSession.Columns.Add("TimeEnd", typeof(string));
                    dtSession.Columns.Add("TotalHands", typeof(int));
                    dtSession.Columns.Add("NetWon", typeof(double));
                }

                while (dr.Read())
                {
                    dtSession.Rows.Add(Convert.ToDateTime(dr["DateSession"].ToString().Substring(2, 2) + "/" + dr["DateSession"].ToString().Substring(0, 2) + "/" + dr["DateSession"].ToString().Substring(4, 4)).ToShortDateString(),
                                                          dr["TimeStart"].ToString().Substring(0, dr["TimeStart"].ToString().Length - 4) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 4, 2) + ":" + dr["TimeStart"].ToString().Substring(dr["TimeStart"].ToString().Length - 2, 2),
                                                          dr["TimeEnd"].ToString().Substring(0, dr["TimeEnd"].ToString().Length - 4) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 4, 2) + ":" + dr["TimeEnd"].ToString().Substring(dr["TimeEnd"].ToString().Length - 2, 2),
                                                          Convert.ToInt32(dr["TotalHands"]),
                                                          Math.Round(Convert.ToDouble(dr["NetWon"]), 2));
                }

                //this.gridPlayers.DataSource = dtSession;
                formSession.gridSessions.DataSource = dtSession;

                formSession.Show();
            }
            */
        }

        private void btnTest_Click(object sender, EventArgs e)
        {

            
            /*
            System.Diagnostics.Process[] localAll = System.Diagnostics.Process.GetProcesses();

            foreach (System.Diagnostics.Process proc in localAll)
            {
                //MessageBox.Show(proc.ProcessName);
                if (proc.ProcessName.IndexOf("zim") > -1)
                {
                    MessageBox.Show(proc.ProcessName);
                    MessageBox.Show(proc.Id.ToString());                                        
                }
            }            
            */
            //double idHand = 186686654324;
            //frm6MaxTable frm6maxtable = new frm6MaxTable();
            //frm6maxtable.SetInitialConfig();
            //frm6maxtable.SeatPlayers(idHand);
            //frm6maxtable.SetHoleCards(idHand);

            /*
            Actions teste = new Actions();
            teste.IDHand = 1;
            teste.IDPlayer = 2;
            teste.OrderHand = 1;
            teste.HandMoment = 0;
            teste.Action = -1;
            teste.Value = 0.01;
            frm6maxtable.RunAction(teste);
            */

            //List<Actions> listAction = new List<Actions>();
            //Actions actionGetter = new Actions();
            //listAction = actionGetter.GetActionsByIDHand(idHand);
            //frm6maxtable.listAction = actionGetter.GetActionsByIDHand(idHand);

            //foreach (Actions action in frm6maxtable.listAction)
            //{
            //    if (action.Action == -1)
            //        frm6maxtable.RunAction(action);
            //    else
            //        break;
            //}

            //frm6maxtable.OrderHand = 2;
            //frm6maxtable.Show();
            //frm6maxtable.RunHand(idHand);
        }

        private void btnClearDB_Click(object sender, EventArgs e)
        {
            DialogResult dialog = MessageBox.Show("Are you sure you want to clear your database?", "Warning", MessageBoxButtons.YesNoCancel);

            if (dialog == DialogResult.Yes)
            {
                sql.ClearDB();
                LoadPlayersIntoGrid();
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            DirectoryInfo directory = new DirectoryInfo(folderHands);
            hl.path = folderHands;
            if (directory.GetFiles().Count() > 0)
                sql.BackupDB();
            foreach (FileInfo file in directory.GetFiles())
            {
                hl.file = file.Name;
                //hl.load(this);
                hl.load2(this);
                if (File.Exists(backupHands + file.Name))
                    file.MoveTo(backupHands + file.Name + DateTime.Now.Millisecond.ToString());
                else
                    file.MoveTo(backupHands + file.Name);
            }
            LoadPlayersIntoGrid();
        }

        private void btnHands_Click(object sender, EventArgs e)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();
            List<Players> listPlayers = new List<Players>();
            Players playersGettter = new Players();

            frmHands frmhands = new frmHands();

            int heroIdPlayer = playersGettter.GetHeroID(0);

            listPlayers = playersGettter.GetPlayersSession(0);
            frmhands.cboPlayer.DataSource = listPlayers;
            frmhands.cboPlayer.ValueMember = "IDPlayer";
            frmhands.cboPlayer.DisplayMember = "Nickname";
            frmhands.cboPlayer.SelectedValue = heroIdPlayer;

            listHandView = handViewGetter.GetAllHands(1,heroIdPlayer, "");

            frmhands.dataGridView1.Columns[1].Width = 60;
            frmhands.dataGridView1.Columns[2].Width = 60;
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                frmhands.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                frmhands.dataGridView1.Rows[counter].Height = 80;

                counter++;
                
            }

            frmhands.txtMaxPages.Text = handViewGetter.GetMaxPages(heroIdPlayer).ToString();
            frmhands.txtPage.Text = "1";
            frmhands.btnFirst.Enabled = false;
            frmhands.btnPrevious.Enabled = false;
            frmhands.btnNext.Enabled = (Convert.ToInt32(frmhands.txtMaxPages.Text) > 1);
            frmhands.btnLast.Enabled = frmhands.btnNext.Enabled;
            frmhands.Show();
        }

        private void btnConfig_Click(object sender, EventArgs e)
        {
            Config configGetter = new Config();
            frmConfig frmconfig = new frmConfig();

            frmconfig.txtHandPath.Text = configGetter.GetHandPath();
            frmconfig.txtBackupPath.Text = configGetter.GetBackupPath();
            frmconfig.Show();
        }

        private void btnResults_Click(object sender, EventArgs e)
        {
            frmResults frm = new frmResults();
            int TotHands = 0;
            double NetWon = 0;
            double BB100 = 0;
            List<Players> listPlayers = new List<Players>();
            Players playersGettter = new Players();

            int heroIdPlayer = playersGettter.GetHeroID(0);

            SQLiteDataReader dr = sql.Select("SELECT G.NAME, COUNT(*) TOTHANDS, SUM(HP.NETWON) NETWON, SUM(BB)*100/COUNT(*) BB100 " +
                                             "FROM GAMES G " +
                                             "INNER JOIN HANDGAMES HG ON G.IDGAME = HG.IDGAME " +
                                             "INNER JOIN HANDPLAYERS HP ON HG.IDHAND = HP.IDHAND " + 
                                             "WHERE HP.IDPLAYER = " + heroIdPlayer.ToString() +
                                             " GROUP BY G.NAME");

            if (frm.dgvResults.Columns.Count == 0)
            {
                frm.dgvResults.Columns.Add("NAME", "Name");
                frm.dgvResults.Columns.Add("TOTHANDS", "Total Hands");
                frm.dgvResults.Columns.Add("NETWON", "NetWon");
                frm.dgvResults.Columns.Add("BB100", "BB/100");
            }

            frm.dgvResults.Columns[0].AutoSizeMode = DataGridViewAutoSizeColumnMode.DisplayedCells;

            while (dr.Read())
            {
                frm.dgvResults.Rows.Add(dr["NAME"].ToString(),
                                        Convert.ToInt32(dr["TOTHANDS"]),
                                        Math.Round(Convert.ToDouble(dr["NETWON"]),2),
                                        Math.Round(Convert.ToDouble(dr["BB100"]),2));
                TotHands = TotHands + Convert.ToInt32(dr["TOTHANDS"]);
                NetWon = NetWon + Convert.ToDouble(dr["NETWON"]);
                BB100 = BB100 + Convert.ToDouble(dr["BB100"]);
            }            

            frm.dgvResults.Rows.Add("TOTAL", TotHands, Math.Round(NetWon,2), Math.Round(BB100,2));

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(frm.dgvResults.Font, FontStyle.Bold);
            frm.dgvResults.Rows[frm.dgvResults.Rows.Count -2].DefaultCellStyle = style;            

            listPlayers = playersGettter.GetPlayersSession(0);
            frm.cboPlayer.DataSource = listPlayers;
            frm.cboPlayer.ValueMember = "IDPlayer";
            frm.cboPlayer.DisplayMember = "Nickname";
            frm.cboPlayer.SelectedValue = heroIdPlayer;

            frm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
