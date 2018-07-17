using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SQLite;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTracker
{
    public partial class frmResults : Form
    {
        SQL sql = new SQL();

        public frmResults()
        {
            this.Icon = new Icon(@"Images\poker.ico");
            InitializeComponent();
        }

        private void FilterResults()
        {
            int TotHands = 0;
            double NetWon = 0;
            double BB100 = 0;
            List<Players> listPlayers = new List<Players>();
            Players playersGettter = new Players();
            string subquery = "";
            int idPlayer = Convert.ToInt32(this.cboPlayer.SelectedValue);

            if (this.txtFilteredHands.Text != "")
            {
                string[] hands = this.txtFilteredHands.Text.Split(',');

                subquery = " AND HG.IDHAND IN (";

                string suited = "";

                foreach (string hand in hands)
                {
                    if (hand.Length > 2 && hand.Substring(2, 1) == "o")
                        suited = "SUBSTR(C1.CARD,2,1) <> SUBSTR(C2.CARD,2,1) AND ";
                    if (hand.Length > 2 && hand.Substring(2, 1) == "s")
                        suited = "SUBSTR(C1.CARD,2,1) = SUBSTR(C2.CARD,2,1) AND ";
                    if (hand.Length == 2)
                        suited = "";

                    if (subquery != " AND HG.IDHAND IN (")
                        subquery = subquery + " UNION ";
                    subquery = subquery +
                                "SELECT C.IDHAND " +
                               "FROM CARDS C " +
                               "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER = C.IDPLAYER AND HP.IDHAND = C.IDHAND " +
                               "INNER JOIN CARDS C1 ON C.IDHAND = C1.IDHAND AND C.IDPLAYER = C1.IDPLAYER AND C1.CARDORDER = 1 AND C1.CARD IN ('" + hand.Substring(0, 1) + "h', '" + hand.Substring(0, 1) + "d', '" + hand.Substring(0, 1) + "s', '" + hand.Substring(0, 1) + "c') " +
                               "INNER JOIN CARDS C2 ON C.IDHAND = C2.IDHAND AND C.IDPLAYER = C2.IDPLAYER AND C2.CARDORDER = 2 AND C2.CARD IN ('" + hand.Substring(1, 1) + "h', '" + hand.Substring(1, 1) + "d', '" + hand.Substring(1, 1) + "s', '" + hand.Substring(1, 1) + "c') " +
                               "WHERE " + suited + " C.IDPLAYER = " + idPlayer.ToString() + " " +
                               "UNION " +
                               "SELECT C.IDHAND " +
                               "FROM CARDS C " +
                               "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER = C.IDPLAYER AND HP.IDHAND = C.IDHAND " +
                               "INNER JOIN CARDS C1 ON C.IDHAND = C1.IDHAND AND C.IDPLAYER = C1.IDPLAYER AND C1.CARDORDER = 1 AND C1.CARD IN ('" + hand.Substring(1, 1) + "h', '" + hand.Substring(1, 1) + "d', '" + hand.Substring(1, 1) + "s', '" + hand.Substring(1, 1) + "c') " +
                               "INNER JOIN CARDS C2 ON C.IDHAND = C2.IDHAND AND C.IDPLAYER = C2.IDPLAYER AND C2.CARDORDER = 2 AND C2.CARD IN ('" + hand.Substring(0, 1) + "h', '" + hand.Substring(0, 1) + "d', '" + hand.Substring(0, 1) + "s', '" + hand.Substring(0, 1) + "c')" +
                               "WHERE " + suited + " C.IDPLAYER = " + idPlayer + " ";
                }

                subquery = subquery + ")";
            }

            SQLiteDataReader dr = sql.Select("SELECT G.NAME, COUNT(*) TOTHANDS, SUM(HP.NETWON) NETWON, SUM(BB)*100/COUNT(*) BB100 " +
                                             "FROM GAMES G " +
                                             "INNER JOIN HANDGAMES HG ON G.IDGAME = HG.IDGAME " +
                                             "INNER JOIN HANDPLAYERS HP ON HG.IDHAND = HP.IDHAND " +
                                             "WHERE HP.IDPLAYER = " + this.cboPlayer.SelectedValue.ToString() +
                                             subquery + 
                                             " GROUP BY G.NAME");

            this.dgvResults.Rows.Clear();

            while (dr.Read())
            {
                this.dgvResults.Rows.Add(dr["NAME"].ToString(),
                                        Convert.ToInt32(dr["TOTHANDS"]),
                                        Math.Round(Convert.ToDouble(dr["NETWON"]), 2),
                                        Math.Round(Convert.ToDouble(dr["BB100"]), 2));
                TotHands = TotHands + Convert.ToInt32(dr["TOTHANDS"]);
                NetWon = NetWon + Convert.ToDouble(dr["NETWON"]);
                BB100 = BB100 + Convert.ToDouble(dr["BB100"]);
            }

            this.dgvResults.Rows.Add("TOTAL", TotHands, Math.Round(NetWon, 2), Math.Round(BB100, 2));

            DataGridViewCellStyle style = new DataGridViewCellStyle();
            style.Font = new Font(this.dgvResults.Font, FontStyle.Bold);
            this.dgvResults.Rows[this.dgvResults.Rows.Count - 2].DefaultCellStyle = style;
            
        }

        private void cboPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPlayer.SelectedValue is Int32)
                FilterResults();
        }

        private void btnFilterHands_Click(object sender, EventArgs e)
        {
            frmFilterHands frmfilterhands = new frmFilterHands();

            frmfilterhands.txtFilterHands.Text = this.txtFilteredHands.Text;
            frmfilterhands.FillButtons();
            frmfilterhands.ShowDialog();
            this.txtFilteredHands.Text = frmfilterhands.txtFilterHands.Text;

            FilterResults();
        }
    }
}
