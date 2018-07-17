using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PokerTracker
{
    public partial class FormTeste2 : Form
    {
        public FormTeste2()
        {
            this.Icon = new Icon(@"Images\poker.ico");
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView senderGrid = (DataGridView)sender;
                List<HandView> listHandView = new List<HandView>();
                HandView handViewGetter = new HandView();
                List<Players> listPlayers = new List<Players>();
                Players playersGettter = new Players();                

                frmHands frmhands = new frmHands();

                int idSession = Convert.ToInt32(senderGrid[0, e.RowIndex].Value);
                int heroIdPlayer = playersGettter.GetHeroID(idSession);
                
                listPlayers = playersGettter.GetPlayersSession(idSession);
                frmhands.cboPlayer.DataSource = listPlayers;
                frmhands.cboPlayer.ValueMember = "IDPlayer";
                frmhands.cboPlayer.DisplayMember = "Nickname";
                frmhands.cboPlayer.SelectedValue = heroIdPlayer;

                listHandView = handViewGetter.GetHandsByIDSession(idSession,heroIdPlayer,1,"");

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

                handViewGetter.IDSession = idSession;
                frmhands.IDSession = idSession;
                frmhands.txtMaxPages.Text = handViewGetter.GetMaxPagesSession(heroIdPlayer).ToString();
                frmhands.txtPage.Text = "1";
                frmhands.btnFirst.Enabled = false;
                frmhands.btnPrevious.Enabled = false;
                frmhands.btnNext.Enabled = (Convert.ToInt32(frmhands.txtMaxPages.Text) > 1);
                frmhands.btnLast.Enabled = frmhands.btnNext.Enabled;

                frmhands.Show();
            }
        }
    }
}
