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
    public partial class frmHands : Form
    {

        public int IDSession { get; set; }

        public frmHands()
        {
            this.Icon = new Icon(@"Images\poker.ico");
            InitializeComponent();
        }

        private void frmHands_Load(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                DataGridView senderGrid = (DataGridView)sender;
                Games game = new Games();

                double idHand = Convert.ToDouble(senderGrid[0, e.RowIndex].Value);

                if (game.GetGame(idHand) == "6max")
                {
                    frm6MaxTable frm6maxtable = new frm6MaxTable();
                    frm6maxtable.RunHand(idHand);
                }
                else
                {
                    frm9MaxTable frm9maxtable = new frm9MaxTable();
                    frm9maxtable.RunHand(idHand);
                }
            }
        }

        private void btnFirst_Click(object sender, EventArgs e)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();

            handViewGetter.IDSession = this.IDSession;
            listHandView = handViewGetter.GetAllHands(1,Convert.ToInt32(cboPlayer.SelectedValue), this.txtFilteredHands.Text);

            this.dataGridView1.Rows.Clear();

            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                this.dataGridView1.Rows[counter].Height = 80;

                counter++;

            }

            this.txtPage.Text = "1";
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = (Convert.ToInt32(this.txtMaxPages.Text) > 1);
            this.btnLast.Enabled = this.btnNext.Enabled;
            //this.Show();
        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();

            handViewGetter.IDSession = this.IDSession;
            listHandView = handViewGetter.GetAllHands(Convert.ToInt32(this.txtPage.Text) - 1, Convert.ToInt32(cboPlayer.SelectedValue),this.txtFilteredHands.Text);

            this.dataGridView1.Rows.Clear();

            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                this.dataGridView1.Rows[counter].Height = 80;

                counter++;

            }

            this.txtPage.Text = (Convert.ToInt32(this.txtPage.Text) - 1).ToString();
            this.btnFirst.Enabled = (this.txtPage.Text != "1");
            this.btnNext.Enabled = true;
            this.btnPrevious.Enabled = btnFirst.Enabled;
            this.btnLast.Enabled = true;
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();

            handViewGetter.IDSession = this.IDSession;
            listHandView = handViewGetter.GetAllHands(Convert.ToInt32(this.txtPage.Text) + 1, Convert.ToInt32(cboPlayer.SelectedValue),this.txtFilteredHands.Text);

            this.dataGridView1.Rows.Clear();

            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                this.dataGridView1.Rows[counter].Height = 80;

                counter++;

            }

            this.txtPage.Text = (Convert.ToInt32(this.txtPage.Text) + 1).ToString();
            this.btnFirst.Enabled = true;
            this.btnPrevious.Enabled = true;
            this.btnLast.Enabled = (Convert.ToInt32(this.txtPage.Text) != Convert.ToInt32(this.txtMaxPages.Text));
            this.btnNext.Enabled = btnLast.Enabled;
        }

        private void btnLast_Click(object sender, EventArgs e)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();

            handViewGetter.IDSession = this.IDSession;
            listHandView = handViewGetter.GetAllHands(Convert.ToInt32(this.txtMaxPages.Text), Convert.ToInt32(cboPlayer.SelectedValue),this.txtFilteredHands.Text);

            this.dataGridView1.Rows.Clear();

            this.dataGridView1.Columns[1].Width = 60;
            this.dataGridView1.Columns[2].Width = 60;
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                this.dataGridView1.Rows[counter].Height = 80;

                counter++;

            }

            this.txtPage.Text = this.txtPage.Text = this.txtMaxPages.Text;
            this.btnPrevious.Enabled = (Convert.ToInt32(this.txtPage.Text) > 1);
            this.btnFirst.Enabled = true;
            this.btnNext.Enabled = false;
        }

        private void cboPlayer_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cboPlayer.SelectedValue is Int32)
            {
                List<HandView> listHandView = new List<HandView>();
                HandView handViewGetter = new HandView();

                listHandView = handViewGetter.GetHandsByIDSession(this.IDSession, Convert.ToInt32(cboPlayer.SelectedValue),Convert.ToInt32(this.txtPage.Text),this.txtFilteredHands.Text);

                this.dataGridView1.Rows.Clear();
                int counter = 0;
                foreach (HandView handView in listHandView)
                {
                    this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                                new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                                new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                                handView.NetWon);
                    this.dataGridView1.Rows[counter].Height = 80;
                    counter++;
                }

                this.txtMaxPages.Text = handViewGetter.GetMaxPagesSession(Convert.ToInt32(cboPlayer.SelectedValue)).ToString();
                this.txtPage.Text = "1";
                this.btnFirst.Enabled = false;
                this.btnPrevious.Enabled = false;
                this.btnNext.Enabled = (Convert.ToInt32(this.txtMaxPages.Text) > 1);
                this.btnLast.Enabled = this.btnNext.Enabled;
            }
        }

        private void btnFilterHands_Click(object sender, EventArgs e)
        {
            frmFilterHands frmfilterhands = new frmFilterHands();

            frmfilterhands.txtFilterHands.Text = this.txtFilteredHands.Text;
            frmfilterhands.FillButtons();
            frmfilterhands.ShowDialog();
            this.txtFilteredHands.Text = frmfilterhands.txtFilterHands.Text;

            List<HandView> listHandView = new List<HandView>();
            HandView handViewGetter = new HandView();

            listHandView = handViewGetter.GetHandsByIDSession(this.IDSession, Convert.ToInt32(cboPlayer.SelectedValue), Convert.ToInt32(this.txtPage.Text), this.txtFilteredHands.Text);

            this.dataGridView1.Rows.Clear();
            int counter = 0;
            foreach (HandView handView in listHandView)
            {
                this.dataGridView1.Rows.Add(handView.IDHand.ToString(),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardOne + ".jpg"), 60, 80),
                                            new Bitmap(Image.FromFile(@"Images\" + handView.CardTwo + ".jpg"), 60, 80),
                                            handView.NetWon);
                this.dataGridView1.Rows[counter].Height = 80;
                counter++;
            }

            this.txtMaxPages.Text = handViewGetter.GetMaxPagesSession(Convert.ToInt32(cboPlayer.SelectedValue)).ToString();
            this.txtPage.Text = "1";
            this.btnFirst.Enabled = false;
            this.btnPrevious.Enabled = false;
            this.btnNext.Enabled = (Convert.ToInt32(this.txtMaxPages.Text) > 1);
            this.btnLast.Enabled = this.btnNext.Enabled;
        }
    }
}
