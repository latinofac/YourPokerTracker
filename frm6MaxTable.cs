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
    public partial class frm6MaxTable : Form
    {
        private List<HandPlayers> listHP = new List<HandPlayers>();
        private List<Players> listPlayers = new List<Players>();
        internal List<Actions> listAction = new List<Actions>();
        internal int OrderHand = 0;
        internal int HandMoment = 0;
        internal int MaxMoment = 0;
        List<Cards> listCards = new List<Cards>();

        List<Dictionary<int,bool>> listBorders = new List<Dictionary<int,bool>>();
        List<Dictionary<int,double>> listStacks = new List<Dictionary<int,double>>();
        List<Dictionary<int,double>> listBets = new List<Dictionary<int,double>>();
        Dictionary<int, double> listPot = new Dictionary<int, double>();
        List<Dictionary<int, string>> listFirstCards = new List<Dictionary<int, string>>();
        List<Dictionary<int, string>> listSecondCards = new List<Dictionary<int, string>>();
        List<string> listNicknames = new List<string>();

        public frm6MaxTable()
        {
            this.Icon = new Icon(@"Images\poker.ico");
            InitializeComponent();
        }

        internal void SetInitialConfig()
        {            
            c1.ImageLocation = @"Images\greenCard.jpg";
            c2.ImageLocation = @"Images\greenCard.jpg";
            c3.ImageLocation = @"Images\greenCard.jpg";
            c4.ImageLocation = @"Images\greenCard.jpg";
            c5.ImageLocation = @"Images\greenCard.jpg";

            bet1.Visible = false;
            bet2.Visible = false;
            bet3.Visible = false;
            bet4.Visible = false;
            bet5.Visible = false;
            bet6.Visible = false;

            bet1.Text = "";
            bet2.Text = "";
            bet3.Text = "";
            bet4.Text = "";
            bet5.Text = "";
            bet6.Text = "";

            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
            btn6.Visible = false;

            pot.Text = "";
            pot.Visible = false;

            border1.Visible = false;
            border2.Visible = false;
            border3.Visible = false;
            border4.Visible = false;
            border5.Visible = false;
            border6.Visible = false;
            /*
            p1.Text = "latinofac - $ 2.00";
            p2.Text = "Padilha SP - $ 2.10";
            p3.Text = "Aakari - $ 3.20";
            p4.Text = "Pessagno - $ 1.90";
            p5.Text = "IneedMassari - $ 1.85";
            p6.Text = "joaoMathias - $ 2.33";

            bet1.Text = "$0.01";
            bet2.Text = "$0.02";
            bet3.Text = "$0.06";
            bet4.Text = "$0.18";
            bet5.Text = "$0.54";
            bet6.Text = "$2.32";

            pot.Text = "POT - $ 15.21";
            
            p1c1.ImageLocation = @"Images\Ac.jpg";
            p1c2.ImageLocation = @"Images\2c.jpg";
            p2c1.ImageLocation = @"Images\3c.jpg";
            p2c2.ImageLocation = @"Images\4c.jpg";
            p3c1.ImageLocation = @"Images\5c.jpg";
            p3c2.ImageLocation = @"Images\6c.jpg";
            p4c1.ImageLocation = @"Images\7c.jpg";
            p4c2.ImageLocation = @"Images\8c.jpg";
            p5c1.ImageLocation = @"Images\9c.jpg";
            p5c2.ImageLocation = @"Images\Tc.jpg";
            p6c1.ImageLocation = @"Images\Jc.jpg";
            p6c2.ImageLocation = @"Images\Qc.jpg";
            */
        }

        internal void SeatPlayers(double idHand)        
        {
            HandPlayers hpGetter = new HandPlayers();
            listHP = new List<HandPlayers>();
            listPlayers = new List<Players>();
            Players player = new Players();
            bool showButton = false;

            listHP = hpGetter.getList(idHand);

            foreach (HandPlayers hp in listHP)
            {
                player = new Players();
                player.FindById(hp.IDPlayer);
                listPlayers.Add(player);
                if (hp.Position == 6)
                    showButton = true;
                switch (hp.Seat)
                {
                    case 1:
                        btn1.Visible = showButton;
                        p1.Text = player.Nickname + " - " + hp.InitialStack;
                        //border1.Visible = hp.Position == 3;
                        break;
                    case 2:
                        btn2.Visible = showButton;
                        p2.Text = player.Nickname + " - " + hp.InitialStack;
                        //border2.Visible = hp.Position == 3;
                        break;
                    case 3:
                        btn3.Visible = showButton;
                        p3.Text = player.Nickname + " - " + hp.InitialStack;
                        //border3.Visible = hp.Position == 3;
                        break;
                    case 4:
                        btn4.Visible = showButton;
                        p4.Text = player.Nickname + " - " + hp.InitialStack;
                        //border4.Visible = hp.Position == 3;
                        break;
                    case 5:
                        btn5.Visible = showButton;
                        p5.Text = player.Nickname + " - " + hp.InitialStack;
                        //border5.Visible = hp.Position == 3;
                        break;
                    case 6:
                        btn6.Visible = showButton;
                        p6.Text = player.Nickname + " - " + hp.InitialStack;
                        //border6.Visible = hp.Position == 3;
                        break;
                    default:                        
                        break;
                }
                showButton = false;
            }
        }

        internal void SetHoleCards(double idHand)
        {
            Cards cardsGetter = new Cards();
            listCards = new List<Cards>();

            MaxMoment = 0;

            listCards = cardsGetter.GetCards(idHand);

            MaxMoment = listCards.Min(x => x.IDPlayer);
            if (MaxMoment == -5)
                MaxMoment = 3;
            else if (MaxMoment == -4)
                MaxMoment = 2;
            else if (MaxMoment == -3)
                MaxMoment = 1;
            else
                MaxMoment = 0;

            listFirstCards[0].Add(1, "redCard");
            listFirstCards[0].Add(2, "redCard");
            listFirstCards[0].Add(3, "redCard");
            listFirstCards[0].Add(4, "redCard");
            listFirstCards[0].Add(5, "redCard");
            listFirstCards[0].Add(6, "redCard");

            listSecondCards[0].Add(1, "redCard");
            listSecondCards[0].Add(2, "redCard");
            listSecondCards[0].Add(3, "redCard");
            listSecondCards[0].Add(4, "redCard");
            listSecondCards[0].Add(5, "redCard");
            listSecondCards[0].Add(6, "redCard");

            foreach (Cards card in listCards)
            {
                if (card.IDPlayer > 0)
                {
                    switch (listHP.Find(x => x.IDPlayer == card.IDPlayer).Seat)
                    {
                        case 1:
                            if (p1c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p1c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][1] = card.Card;
                            }
                            else
                            {
                                p1c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][1] = card.Card;
                            }
                            break;
                        case 2:
                            if (p2c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p2c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][2] = card.Card;
                            }
                            else
                            {
                                p2c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][2] = card.Card;
                            }
                            break;
                        case 3:
                            if (p3c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p3c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][3] = card.Card;
                            }
                            else
                            {
                                p3c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][3] = card.Card;
                            }
                            break;
                        case 4:
                            if (p4c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p4c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][4] = card.Card;
                            }
                            else
                            {
                                p4c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][4] = card.Card;
                            }
                            break;
                        case 5:
                            if (p5c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p5c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][5] = card.Card;
                            }
                            else
                            {
                                p5c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][5] = card.Card;
                            }
                            break;
                        case 6:
                            if (p6c1.ImageLocation == @"Images\redCard.jpg")
                            {
                                p6c1.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listFirstCards[0][6] = card.Card;
                            }
                            else
                            {
                                p6c2.ImageLocation = @"Images\" + card.Card + ".jpg";
                                listSecondCards[0][6] = card.Card;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal void RunAction(Actions action)
        {
            int OrderToConsider = 0;
            HandPlayers hp = new HandPlayers();
            hp = listHP.Find(x => x.IDPlayer == action.IDPlayer);
            hp.InitialStack = hp.InitialStack - action.Value;            

            Players player = new Players();
            player = listPlayers.Find(y => y.IDPlayer == action.IDPlayer);

            if (OrderHand + 1 < listAction.Count())
            {
                if (OrderHand == 0)
                    OrderToConsider = 2;
                else
                    OrderToConsider = OrderHand + 1;
                border1.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 1;
                border2.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 2;
                border3.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 3;
                border4.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 4;
                border5.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 5;
                border6.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 6;
            }

            if ((action.Action == -1) || (action.Action == 3) || (action.Action == 2))
            {

                pot.Visible = true;
                if (pot.Text != "")
                    pot.Text = "$ " + (Convert.ToDouble(pot.Text.Substring(2, pot.Text.Length - 2)) + action.Value).ToString();
                else
                    pot.Text = "$ " + action.Value.ToString();

                switch (hp.Seat)
                {
                    case 1:
                        if (bet1.Text != "")
                            bet1.Text = "$ " + (Convert.ToDouble(bet1.Text.Substring(2, bet1.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet1.Text = "$ " + action.Value.ToString();
                        bet1.Visible = true;
                        p1.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 2:
                        if (bet2.Text != "")
                            bet2.Text = "$ " + (Convert.ToDouble(bet2.Text.Substring(2, bet2.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet2.Text = "$ " + action.Value.ToString();
                        bet2.Visible = true;
                        p2.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 3:
                        if (bet3.Text != "")
                            bet3.Text = "$ " + (Convert.ToDouble(bet3.Text.Substring(2, bet3.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet3.Text = "$ " + action.Value.ToString();
                        bet3.Visible = true;
                        p3.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 4:
                        if (bet4.Text != "")
                            bet4.Text = "$ " + (Convert.ToDouble(bet4.Text.Substring(2, bet4.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet4.Text = "$ " + action.Value.ToString();
                        bet4.Visible = true;
                        p4.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 5:
                        if (bet5.Text != "")
                            bet5.Text = "$ " + (Convert.ToDouble(bet5.Text.Substring(2, bet5.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet5.Text = "$ " + action.Value.ToString();
                        bet5.Visible = true;
                        p5.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 6:
                        if (bet6.Text != "")
                            bet6.Text = "$ " + (Convert.ToDouble(bet6.Text.Substring(2, bet6.Text.Length - 2)) + action.Value).ToString();
                        else
                            bet6.Text = "$ " + action.Value.ToString();
                        bet6.Visible = true;
                        p6.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    default:
                        break;
                }
            }

            if (action.Action == 1)
            {
                switch (hp.Seat)
                {
                    case 1:
                        p1c1.ImageLocation = @"Images\whiteCard.jpg";
                        p1c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    case 2:
                        p2c1.ImageLocation = @"Images\whiteCard.jpg";
                        p2c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    case 3:
                        p3c1.ImageLocation = @"Images\whiteCard.jpg";
                        p3c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    case 4:
                        p4c1.ImageLocation = @"Images\whiteCard.jpg";
                        p4c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    case 5:
                        p5c1.ImageLocation = @"Images\whiteCard.jpg";
                        p5c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    case 6:
                        p6c1.ImageLocation = @"Images\whiteCard.jpg";
                        p6c2.ImageLocation = @"Images\whiteCard.jpg";
                        break;
                    default:
                        break;
                }
            }
            
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            if (OrderHand == listAction.Count() - 1)
            {
                RunCards(true);
                HandMoment++;
            }
            else
            {
                if (listAction[OrderHand + 1].HandMoment != HandMoment)
                {
                    ChangeMoment(listAction[OrderHand].IDHand, listAction[OrderHand + 1].HandMoment);
                    HandMoment = listAction[OrderHand + 1].HandMoment;
                }
                else
                {
                    OrderHand++;
                    RunAction2(OrderHand);
                }
            }

            this.btnNext.Enabled = (OrderHand != listAction.Count() - 1) || HandMoment < MaxMoment;
            this.btnPrev.Enabled = (OrderHand > 1);
            
        }

        private void RunCards(bool forward)
        {
            if (forward)
            {
                switch (HandMoment)
                {
                    case 0:
                        c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                        c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                        c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                        c4.ImageLocation = @"Images\greenCard.jpg";
                        c5.ImageLocation = @"Images\greenCard.jpg";
                        break;
                    case 1:
                        c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                        c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                        c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                        c4.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -4).Card + ".jpg";
                        c5.ImageLocation = @"Images\greenCard.jpg";
                        break;
                    case 2:
                        c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                        c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                        c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                        c4.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -4).Card + ".jpg";
                        c5.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -5).Card + ".jpg";
                        break;
                    default:
                        break;
                }
            }
            else
            {
                switch (HandMoment)
                {
                    case 0:
                        c1.ImageLocation = @"Images\greenCard.jpg";
                        c2.ImageLocation = @"Images\greenCard.jpg";
                        c3.ImageLocation = @"Images\greenCard.jpg";
                        c4.ImageLocation = @"Images\greenCard.jpg";
                        c5.ImageLocation = @"Images\greenCard.jpg";
                        break;
                    case 1:
                        c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                        c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                        c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                        c4.ImageLocation = @"Images\greenCard.jpg";
                        c5.ImageLocation = @"Images\greenCard.jpg";
                        break;
                    case 2:
                        c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                        c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                        c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                        c4.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -4).Card + ".jpg";
                        c5.ImageLocation = @"Images\greenCard.jpg";
                        break;
                    default:
                        break;
                }
            }

            // if (forward)
              //  HandMoment++;
            //else
              //  HandMoment--;

            // if (HandMoment == 3)
              //  HandMoment = 2;
            //if (HandMoment == -1)
              //  HandMoment = 0;
        }

        private void ChangeMoment(double idHand, int handMoment)
        {            
            Cards cardsGetter = new Cards();

            listCards = cardsGetter.GetCards(idHand);

            bet1.Visible = false;
            bet2.Visible = false;
            bet3.Visible = false;
            bet4.Visible = false;
            bet5.Visible = false;
            bet6.Visible = false;

            bet1.Text = "";
            bet2.Text = "";
            bet3.Text = "";
            bet4.Text = "";
            bet5.Text = "";
            bet6.Text = "";

            switch (handMoment)
            {
                case 0:
                    c1.ImageLocation = @"Images\greenCard.jpg";
                    c2.ImageLocation = @"Images\greenCard.jpg";
                    c3.ImageLocation = @"Images\greenCard.jpg";
                    c4.ImageLocation = @"Images\greenCard.jpg";
                    c5.ImageLocation = @"Images\greenCard.jpg";
                    break;
                case 1:
                    c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                    c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                    c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                    c4.ImageLocation = @"Images\greenCard.jpg";
                    c5.ImageLocation = @"Images\greenCard.jpg";
                    break;
                case 2:
                    c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                    c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                    c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                    c4.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -4).Card + ".jpg";
                    c5.ImageLocation = @"Images\greenCard.jpg";
                    break;
                case 3:
                    c1.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -1).Card + ".jpg";
                    c2.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -2).Card + ".jpg";
                    c3.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -3).Card + ".jpg";
                    c4.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -4).Card + ".jpg";
                    c5.ImageLocation = @"Images\" + listCards.Find(x => x.IDPlayer == -5).Card + ".jpg";
                    break;
                default:
                    break;
            }

            HandPlayers nextHandPlayer = new HandPlayers();
            nextHandPlayer = listHP.Find(x => x.IDPlayer == listAction[OrderHand + 1].IDPlayer);

            border1.Visible = nextHandPlayer.Seat == 1;
            border2.Visible = nextHandPlayer.Seat == 2;
            border3.Visible = nextHandPlayer.Seat == 3;
            border4.Visible = nextHandPlayer.Seat == 4;
            border5.Visible = nextHandPlayer.Seat == 5;
            border6.Visible = nextHandPlayer.Seat == 6;

        }

        private void btnPrev_Click(object sender, EventArgs e)
        {
            if (listAction[OrderHand - 1].HandMoment != HandMoment)
            {
                HandMoment = listAction[OrderHand - 1].HandMoment;
                RunCards(false);
                OrderHand--;
                RunAction2(OrderHand);
            }
            else
            {
                OrderHand--;
                RunAction2(OrderHand);
            }
            
            this.btnPrev.Enabled = (OrderHand > 1);
            this.btnNext.Enabled = true;
        }

        internal void ReverseAction(Actions action)
        {
            int OrderToConsider = 0;
            HandPlayers hp = new HandPlayers();
            double previousBet = 0;
            hp = listHP.Find(x => x.IDPlayer == action.IDPlayer);
            hp.InitialStack = hp.InitialStack + action.Value;

            Players player = new Players();
            player = listPlayers.Find(y => y.IDPlayer == action.IDPlayer);

            if (OrderHand + 1 < listAction.Count())
            {
                OrderToConsider = OrderHand;
                border1.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 1;
                border2.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 2;
                border3.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 3;
                border4.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 4;
                border5.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 5;
                border6.Visible = listHP.Find(y => y.IDPlayer == listAction[OrderToConsider].IDPlayer).Seat == 6;
            }

            if (action.Action == 1)
            {
                switch (hp.Seat)
                {
                    case 1:
                        p1c1.ImageLocation = @"Images\redCard.jpg";
                        p1c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    case 2:
                        p2c1.ImageLocation = @"Images\redCard.jpg";
                        p2c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    case 3:
                        p3c1.ImageLocation = @"Images\redCard.jpg";
                        p3c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    case 4:
                        p4c1.ImageLocation = @"Images\redCard.jpg";
                        p4c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    case 5:
                        p5c1.ImageLocation = @"Images\redCard.jpg";
                        p5c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    case 6:
                        p6c1.ImageLocation = @"Images\redCard.jpg";
                        p6c2.ImageLocation = @"Images\redCard.jpg";
                        break;
                    default:
                        break;
                }
            }

            if (action.Action == 3 || action.Action == 2)
            {

                pot.Text = "$ " + (Convert.ToDouble(pot.Text.Substring(2, pot.Text.Length - 2)) - action.Value).ToString();                
                pot.Visible = Convert.ToDouble(pot.Text.Substring(2, pot.Text.Length - 2)) - action.Value != 0;

                switch (hp.Seat)
                {
                    case 1:
                        previousBet = Convert.ToDouble(bet1.Text.Substring(2, bet1.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet1.Visible = false;
                            bet1.Text = "";
                        }
                        else
                        {
                            bet1.Text = "$ " + (previousBet - action.Value).ToString();
                            bet1.Visible = true;
                        }
                        p1.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 2:
                        previousBet = Convert.ToDouble(bet2.Text.Substring(2, bet2.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet2.Visible = false;
                            bet2.Text = "";
                        }
                        else
                        {
                            bet2.Text = "$ " + (previousBet - action.Value).ToString();
                            bet2.Visible = true;
                        }
                        p2.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 3:
                        previousBet = Convert.ToDouble(bet3.Text.Substring(2, bet3.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet3.Visible = false;
                            bet3.Text = "";
                        }
                        else
                        {
                            bet3.Text = "$ " + (previousBet - action.Value).ToString();
                            bet3.Visible = true;
                        }
                        p3.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 4:
                        previousBet = Convert.ToDouble(bet4.Text.Substring(2, bet4.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet4.Visible = false;
                            bet4.Text = "";
                        }
                        else
                        {
                            bet4.Text = "$ " + (previousBet - action.Value).ToString();
                            bet4.Visible = true;
                        }
                        p4.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 5:
                        previousBet = Convert.ToDouble(bet5.Text.Substring(2, bet5.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet5.Visible = false;
                            bet5.Text = "";
                        }
                        else
                        {
                            bet5.Text = "$ " + (previousBet - action.Value).ToString();
                            bet5.Visible = true;
                        }
                        p5.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    case 6:
                        previousBet = Convert.ToDouble(bet6.Text.Substring(2, bet6.Text.Length - 2));
                        if (previousBet == action.Value)
                        {
                            bet6.Visible = false;
                            bet6.Text = "";
                        }
                        else
                        {
                            bet6.Text = "$ " + (previousBet - action.Value).ToString();
                            bet6.Visible = true;
                        }
                        p6.Text = player.Nickname + " - " + hp.InitialStack;
                        break;
                    default:
                        break;
                }
            }

        }

        internal void RebuildBets(int handmoment)
        {
            List<Actions> listActionsMoment = listAction.Where(x => x.HandMoment == handmoment).ToList();
            bet1.Text = "";
            bet2.Text = "";
            bet3.Text = "";
            bet4.Text = "";
            bet5.Text = "";
            bet6.Text = "";
            int seat = 0;
            
            foreach (Actions action in listActionsMoment)
            {
                if (action.Value > 0)
                {
                    seat = listHP.Find(x => x.IDPlayer == action.IDPlayer).Seat;                    
                    switch (seat)
                    {
                        case 1:
                            if (bet1.Text == "")
                                bet1.Text = "$ " + action.Value.ToString();
                            else
                                bet1.Text = "$ " + (Convert.ToDouble(bet1.Text.Substring(2, bet1.Text.Length - 2)) + action.Value).ToString();
                            bet1.Visible = true;
                            //p1.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        case 2:
                            if (bet2.Text == "")
                                bet2.Text = "$ " + action.Value.ToString();
                            else
                                bet2.Text = "$ " + (Convert.ToDouble(bet2.Text.Substring(2, bet2.Text.Length - 2)) + action.Value).ToString();
                            bet2.Visible = true;
                            //p2.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        case 3:
                            if (bet3.Text == "")
                                bet3.Text = "$ " + action.Value.ToString();
                            else
                                bet3.Text = "$ " + (Convert.ToDouble(bet3.Text.Substring(2, bet3.Text.Length - 2)) + action.Value).ToString();
                            bet3.Visible = true;
                            //p3.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        case 4:
                            if (bet4.Text == "")
                                bet4.Text = "$ " + action.Value.ToString();
                            else
                                bet4.Text = "$ " + (Convert.ToDouble(bet4.Text.Substring(2, bet4.Text.Length - 2)) + action.Value).ToString();
                            bet4.Visible = true;
                            //p4.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        case 5:
                            if (bet5.Text == "")
                                bet5.Text = "$ " + action.Value.ToString();
                            else
                                bet5.Text = "$ " + (Convert.ToDouble(bet5.Text.Substring(2, bet5.Text.Length - 2)) + action.Value).ToString();
                            bet5.Visible = true;
                            //p5.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        case 6:
                            if (bet6.Text == "")
                                bet6.Text = "$ " + action.Value.ToString();
                            else
                                bet6.Text = "$ " + (Convert.ToDouble(bet6.Text.Substring(2, bet6.Text.Length - 2)) + action.Value).ToString();
                            bet6.Visible = true;
                            //p6.Text = player.Nickname + " - " + hp.InitialStack;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        internal void SetInitialConfig2()
        {
            listCards = new List<Cards>();

            listHP = new List<HandPlayers>();
            listAction = new List<Actions>();

            Dictionary<int, bool> listBorders = new Dictionary<int, bool>();
            Dictionary<int, double> listStacks = new Dictionary<int, double>();
            Dictionary<int, double> listBets = new Dictionary<int, double>();
            Dictionary<int, double> listPot = new Dictionary<int, double>();
            Dictionary<int, string> listFirstCards = new Dictionary<int, string>();
            Dictionary<int, string> listSecondCards = new Dictionary<int, string>();

            c1.ImageLocation = @"Images\greenCard.jpg";
            c2.ImageLocation = @"Images\greenCard.jpg";
            c3.ImageLocation = @"Images\greenCard.jpg";
            c4.ImageLocation = @"Images\greenCard.jpg";
            c5.ImageLocation = @"Images\greenCard.jpg";

            p1c1.ImageLocation = @"Images\redCard.jpg";
            p1c2.ImageLocation = @"Images\redCard.jpg";
            p2c1.ImageLocation = @"Images\redCard.jpg";
            p2c2.ImageLocation = @"Images\redCard.jpg";
            p3c1.ImageLocation = @"Images\redCard.jpg";
            p3c2.ImageLocation = @"Images\redCard.jpg";
            p4c1.ImageLocation = @"Images\redCard.jpg";
            p4c2.ImageLocation = @"Images\redCard.jpg";
            p5c1.ImageLocation = @"Images\redCard.jpg";
            p5c2.ImageLocation = @"Images\redCard.jpg";
            p6c1.ImageLocation = @"Images\redCard.jpg";
            p6c2.ImageLocation = @"Images\redCard.jpg";

            bet1.Text = "";
            bet2.Text = "";
            bet3.Text = "";
            bet4.Text = "";
            bet5.Text = "";
            bet6.Text = "";

            bet1.Visible = false;
            bet2.Visible = false;
            bet3.Visible = false;
            bet4.Visible = false;
            bet5.Visible = false;
            bet6.Visible = false;

            btn1.Visible = false;
            btn2.Visible = false;
            btn3.Visible = false;
            btn4.Visible = false;
            btn5.Visible = false;
            btn6.Visible = false;

            pot.Text = "";
            pot.Visible = false;

            border1.Visible = false;
            border2.Visible = false;
            border3.Visible = false;
            border4.Visible = false;
            border5.Visible = false;
            border6.Visible = false;
        }

        internal void RunHand(double idHand)
        {
            SetInitialConfig2();            

            HandPlayers hpGetter = new HandPlayers();
            Actions actionGetter = new Actions();

            listHP = hpGetter.getList(idHand);
            listAction = actionGetter.GetActionsByIDHand(idHand);

            InitiateLists();

            SetHoleCards(idHand);
            SetPlayers(idHand);
            SetButton();

            RunAction2(0);
            RunAction2(1);
            OrderHand = 1;

            this.Show();
        }

        private void SetPlayers(double idHand)
        {
            Players player = new Players();

            foreach (HandPlayers handPlayer in listHP)
            {
                player = new Players();
                player.FindById(handPlayer.IDPlayer);

                switch (handPlayer.Seat)
                {
                    case 1:
                        p1.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][1] = handPlayer.InitialStack;
                        listNicknames[0] = player.Nickname;
                        break;
                    case 2:
                        p2.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][2] = handPlayer.InitialStack;
                        listNicknames[1] = player.Nickname;
                        break;
                    case 3:
                        p3.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][3] = handPlayer.InitialStack;
                        listNicknames[2] = player.Nickname;
                        break;
                    case 4:
                        p4.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][4] = handPlayer.InitialStack;
                        listNicknames[3] = player.Nickname;
                        break;
                    case 5:
                        p5.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][5] = handPlayer.InitialStack;
                        listNicknames[4] = player.Nickname;
                        break;
                    case 6:
                        p6.Text = player.Nickname + " - " + handPlayer.InitialStack;
                        listStacks[0][6] = handPlayer.InitialStack;
                        listNicknames[5] = player.Nickname;
                        break;
                    default:
                        break;
                }
            }

            if (p1.Text == "Player 1")
            {
                p1.Visible = false;
                p1c1.Visible = false;
                p1c2.Visible = false;            
            }
            if (p2.Text == "Player 1")
            {
                p2.Visible = false;
                p2c1.Visible = false;
                p2c2.Visible = false;
            }
            if (p3.Text == "Player 1")
            {
                p3.Visible = false;
                p3c1.Visible = false;
                p3c2.Visible = false;
            }
            if (p4.Text == "Player 1")
            {
                p4.Visible = false;
                p4c1.Visible = false;
                p4c2.Visible = false;
            }
            if (p5.Text == "Player 1")
            {
                p5.Visible = false;
                p5c1.Visible = false;
                p5c2.Visible = false;
            }
            if (p6.Text == "Player 1")
            {
                p6.Visible = false;
                p6c1.Visible = false;
                p6c2.Visible = false;
            }
        }

        private void InitiateLists()
        {
            for (int i = 0; i < listAction.Count(); i++)
            {
                listBorders.Add(new Dictionary<int, bool>());
                listStacks.Add(new Dictionary<int, double>());
                listBets.Add(new Dictionary<int, double>());
                listFirstCards.Add(new Dictionary<int, string>());
                listSecondCards.Add(new Dictionary<int, string>());
                listNicknames.Add("");
            }
        }

        private void SetButton()
        {
            foreach (HandPlayers hp in listHP)
            {
                if (hp.Position == 6)
                {
                    switch (hp.Seat)
                    {
                        case 1:
                            btn1.Visible = true;
                            break;
                        case 2:
                            btn2.Visible = true;
                            break;
                        case 3:
                            btn3.Visible = true;
                            break;
                        case 4:
                            btn4.Visible = true;
                            break;
                        case 5:
                            btn5.Visible = true;
                            break;
                        case 6:
                            btn6.Visible = true;
                            break;
                        default:
                            break;
                    }
                }
            }
        }

        private void RunAction2(int orderHand)
        {
            if (listBets[orderHand].Count() < 1)
                SetStates(orderHand);            

            if (orderHand != listAction.Count - 1)
            {
                if (listAction[orderHand + 1].HandMoment != HandMoment)
                {
                    border1.Visible = false;
                    border2.Visible = false;
                    border3.Visible = false;
                    border4.Visible = false;
                    border5.Visible = false;
                    border6.Visible = false;
                }
                else
                {
                    border1.Visible = listBorders[orderHand][1];
                    border2.Visible = listBorders[orderHand][2];
                    border3.Visible = listBorders[orderHand][3];
                    border4.Visible = listBorders[orderHand][4];
                    border5.Visible = listBorders[orderHand][5];
                    border6.Visible = listBorders[orderHand][6];
                }
            }
            else
            {
                border1.Visible = false;
                border2.Visible = false;
                border3.Visible = false;
                border4.Visible = false;
                border5.Visible = false;
                border6.Visible = false;
            }

            if (listStacks[orderHand].Count > 0)
                p1.Text = listNicknames[0] + " - " + listStacks[orderHand][1].ToString();
            if (listStacks[orderHand].Count > 1)
                p2.Text = listNicknames[1] + " - " + listStacks[orderHand][2].ToString();
            if (listStacks[orderHand].Count > 2)
                p3.Text = listNicknames[2] + " - " + listStacks[orderHand][3].ToString();
            if (listStacks[orderHand].Count > 3)
                p4.Text = listNicknames[3] + " - " + listStacks[orderHand][4].ToString();
            if (listStacks[orderHand].Count > 4)
                p5.Text = listNicknames[4] + " - " + listStacks[orderHand][5].ToString();
            if (listStacks[orderHand].Count > 5)
                p6.Text = listNicknames[5] + " - " + listStacks[orderHand][6].ToString();

            bet1.Text = "$ " + listBets[orderHand][1].ToString();
            bet2.Text = "$ " + listBets[orderHand][2].ToString();
            bet3.Text = "$ " + listBets[orderHand][3].ToString();
            bet4.Text = "$ " + listBets[orderHand][4].ToString();
            bet5.Text = "$ " + listBets[orderHand][5].ToString();
            bet6.Text = "$ " + listBets[orderHand][6].ToString();

            pot.Text = "$ " + listPot[orderHand].ToString();

            p1c1.ImageLocation = @"Images\" + listFirstCards[orderHand][1] + ".jpg";
            p2c1.ImageLocation = @"Images\" + listFirstCards[orderHand][2] + ".jpg";
            p3c1.ImageLocation = @"Images\" + listFirstCards[orderHand][3] + ".jpg";
            p4c1.ImageLocation = @"Images\" + listFirstCards[orderHand][4] + ".jpg";
            p5c1.ImageLocation = @"Images\" + listFirstCards[orderHand][5] + ".jpg";
            p6c1.ImageLocation = @"Images\" + listFirstCards[orderHand][6] + ".jpg";

            p1c2.ImageLocation = @"Images\" + listSecondCards[orderHand][1] + ".jpg";
            p2c2.ImageLocation = @"Images\" + listSecondCards[orderHand][2] + ".jpg";
            p3c2.ImageLocation = @"Images\" + listSecondCards[orderHand][3] + ".jpg";
            p4c2.ImageLocation = @"Images\" + listSecondCards[orderHand][4] + ".jpg";
            p5c2.ImageLocation = @"Images\" + listSecondCards[orderHand][5] + ".jpg";
            p6c2.ImageLocation = @"Images\" + listSecondCards[orderHand][6] + ".jpg";

            bet1.Visible = (bet1.Text != "$ 0");
            bet2.Visible = (bet2.Text != "$ 0");
            bet3.Visible = (bet3.Text != "$ 0");
            bet4.Visible = (bet4.Text != "$ 0");
            bet5.Visible = (bet5.Text != "$ 0");
            bet6.Visible = (bet6.Text != "$ 0");

            pot.Visible = (pot.Text != "$ 0");
        }

        private void SetStates(int orderHand)
        {
            Actions action = listAction[orderHand];
            HandPlayers hp = new HandPlayers();
            HandPlayers nextHP = new HandPlayers();
            hp = listHP.Find(x => x.IDPlayer == action.IDPlayer);            

            if (orderHand > 0)
            {
                listFirstCards[orderHand].Add(1, listFirstCards[orderHand - 1][1]);
                listFirstCards[orderHand].Add(2, listFirstCards[orderHand - 1][2]);
                listFirstCards[orderHand].Add(3, listFirstCards[orderHand - 1][3]);
                listFirstCards[orderHand].Add(4, listFirstCards[orderHand - 1][4]);
                listFirstCards[orderHand].Add(5, listFirstCards[orderHand - 1][5]);
                listFirstCards[orderHand].Add(6, listFirstCards[orderHand - 1][6]);

                listSecondCards[orderHand].Add(1, listSecondCards[orderHand - 1][1]);
                listSecondCards[orderHand].Add(2, listSecondCards[orderHand - 1][2]);
                listSecondCards[orderHand].Add(3, listSecondCards[orderHand - 1][3]);
                listSecondCards[orderHand].Add(4, listSecondCards[orderHand - 1][4]);
                listSecondCards[orderHand].Add(5, listSecondCards[orderHand - 1][5]);
                listSecondCards[orderHand].Add(6, listSecondCards[orderHand - 1][6]);

                if (listStacks[orderHand - 1].Count > 0)
                    listStacks[orderHand].Add(1, listStacks[orderHand - 1][1]);
                if (listStacks[orderHand - 1].Count > 1)
                    listStacks[orderHand].Add(2, listStacks[orderHand - 1][2]);
                if (listStacks[orderHand - 1].Count > 2)
                    listStacks[orderHand].Add(3, listStacks[orderHand - 1][3]);
                if (listStacks[orderHand - 1].Count > 3)
                    listStacks[orderHand].Add(4, listStacks[orderHand - 1][4]);
                if (listStacks[orderHand - 1].Count > 4)
                    listStacks[orderHand].Add(5, listStacks[orderHand - 1][5]);
                if (listStacks[orderHand - 1].Count > 5)
                    listStacks[orderHand].Add(6, listStacks[orderHand - 1][6]);
            }

            if (pot.Text == "")
                listPot[orderHand] = 0;
            else
                listPot[orderHand] = Convert.ToDouble(pot.Text.Substring(2, pot.Text.Length - 2));
            
            if (bet1.Text == "")
                listBets[orderHand].Add(1, 0);
            else
                listBets[orderHand].Add(1, Convert.ToDouble(bet1.Text.Substring(2, bet1.Text.Length - 2)));
            if (bet2.Text == "")
                listBets[orderHand].Add(2, 0);
            else
                listBets[orderHand].Add(2, Convert.ToDouble(bet2.Text.Substring(2, bet2.Text.Length - 2)));
            if (bet3.Text == "")
                listBets[orderHand].Add(3, 0);
            else
                listBets[orderHand].Add(3, Convert.ToDouble(bet3.Text.Substring(2, bet3.Text.Length - 2)));
            if (bet4.Text == "")
                listBets[orderHand].Add(4, 0);
            else
                listBets[orderHand].Add(4, Convert.ToDouble(bet4.Text.Substring(2, bet4.Text.Length - 2)));
            if (bet5.Text == "")
                listBets[orderHand].Add(5, 0);
            else
                listBets[orderHand].Add(5, Convert.ToDouble(bet5.Text.Substring(2, bet5.Text.Length - 2)));
            if (bet6.Text == "")
                listBets[orderHand].Add(6, 0);
            else
                listBets[orderHand].Add(6, Convert.ToDouble(bet6.Text.Substring(2, bet6.Text.Length - 2)));

            if (orderHand != listAction.Count() - 1)
            {
                nextHP = listHP.Find(x => x.IDPlayer == listAction[orderHand + 1].IDPlayer);
                listBorders[orderHand].Add(1, nextHP.Seat == 1);
                listBorders[orderHand].Add(2, nextHP.Seat == 2);
                listBorders[orderHand].Add(3, nextHP.Seat == 3);
                listBorders[orderHand].Add(4, nextHP.Seat == 4);
                listBorders[orderHand].Add(5, nextHP.Seat == 5);
                listBorders[orderHand].Add(6, nextHP.Seat == 6);
            }
            switch (action.Action)
            {
                case -1:
                case 3:
                case 2:                    
                    listPot[orderHand] = listPot[orderHand] + action.Value;
                    switch (hp.Seat)
                    {
                        case 1:
                            listBets[orderHand][1] = listBets[orderHand][1] + action.Value;
                            listStacks[orderHand][1] = listStacks[orderHand][1] - action.Value;
                            break;
                        case 2:
                            listBets[orderHand][2] = listBets[orderHand][2] + action.Value;
                            listStacks[orderHand][2] = listStacks[orderHand][2] - action.Value;
                            break;
                        case 3:
                            listBets[orderHand][3] = listBets[orderHand][3] + action.Value;
                            listStacks[orderHand][3] = listStacks[orderHand][3] - action.Value;
                            break;
                        case 4:
                            listBets[orderHand][4] = listBets[orderHand][4] + action.Value;
                            listStacks[orderHand][4] = listStacks[orderHand][4] - action.Value;
                            break;
                        case 5:
                            listBets[orderHand][5] = listBets[orderHand][5] + action.Value;
                            listStacks[orderHand][5] = listStacks[orderHand][5] - action.Value;
                            break;
                        case 6:
                            listBets[orderHand][6] = listBets[orderHand][6] + action.Value;
                            listStacks[orderHand][6] = listStacks[orderHand][6] - action.Value;
                            break;
                        default:
                            break;
                    }
                    break;
                case 1:                    
                    switch (hp.Seat)
                    {
                        case 1:
                            listFirstCards[orderHand][1] = "whiteCard";
                            listSecondCards[orderHand][1] = "whiteCard";
                            break;
                        case 2:
                            listFirstCards[orderHand][2] = "whiteCard";
                            listSecondCards[orderHand][2] = "whiteCard";
                            break;
                        case 3:
                            listFirstCards[orderHand][3] = "whiteCard";
                            listSecondCards[orderHand][3] = "whiteCard";
                            break;
                        case 4:
                            listFirstCards[orderHand][4] = "whiteCard";
                            listSecondCards[orderHand][4] = "whiteCard";
                            break;
                        case 5:
                            listFirstCards[orderHand][5] = "whiteCard";
                            listSecondCards[orderHand][5] = "whiteCard";
                            break;
                        case 6:
                            listFirstCards[orderHand][6] = "whiteCard";
                            listSecondCards[orderHand][6] = "whiteCard";
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }

            if (listStacks[orderHand].Count > 0)
                listStacks[orderHand][1] = Math.Round(listStacks[orderHand][1], 2);
            if (listStacks[orderHand].Count > 1)
                listStacks[orderHand][2] = Math.Round(listStacks[orderHand][2], 2);
            if (listStacks[orderHand].Count > 2)
                listStacks[orderHand][3] = Math.Round(listStacks[orderHand][3], 2);
            if (listStacks[orderHand].Count > 3)
                listStacks[orderHand][4] = Math.Round(listStacks[orderHand][4], 2);
            if (listStacks[orderHand].Count > 4)
                listStacks[orderHand][5] = Math.Round(listStacks[orderHand][5], 2);
            if (listStacks[orderHand].Count > 5)
                listStacks[orderHand][6] = Math.Round(listStacks[orderHand][6], 2);            
        }
    }
}
