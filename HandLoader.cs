using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class HandLoader
    {

        private SQL sql = new SQL();

        private Dictionary<string, int> handmoment = new Dictionary<string, int>();
        private Dictionary<string, int> dicAction = new Dictionary<string, int>();
        private List<Players> listPlayers = new List<Players>();
        private List<Players> listPlayersHand = new List<Players>();
        private List<Hand> listHands = new List<Hand>();
        private List<Actions> listActions = new List<Actions>();
        private List<HandPlayers> listHandPlayers = new List<HandPlayers>();
        private List<HandPlayers> listTotalHandPlayers = new List<HandPlayers>();
        private int MaxIDHand = 0;
        private int SBSeat = 0;
        private int numPlayersNow = 0;
        private double netWon = 0;
        public string path = @"C:\Pessoal\Poker\PokerTracker\PokerTracker\hands\";
        public string file = "HH20180502 Halley - $0.01-$0.02 - USD No Limit Hold'em.txt";
        public double bb = 0;
        public List<double> listInitialStacks = new List<double>();
        int PreFlop = 0;
        int Flop = 1;
        int Turn = 2;
        int River = 3;
        int Fold = 1;
        int Call = 2;
        int Raise = 3;
        int Check = 4;

        private void loadDictionaries()
        {
            if (handmoment.Count() == 0)
            {
                handmoment.Add("PreFlop", 0);
                handmoment.Add("Flop", 1);
                handmoment.Add("Turn", 2);
                handmoment.Add("River", 3);
            }

            if (dicAction.Count == 0)
            {
                dicAction.Add("Fold", 1);
                dicAction.Add("Call", 2);
                dicAction.Add("Raise", 3);
                dicAction.Add("Check", 4);
            }
        }

        public void load(frmMain sender)
        {

            loadDictionaries();

            Cards cards = new Cards();
            string stakes = "";
            string datehand = "";
            string timehand = "";
            int numplayers = 0;
            int moment = -1;
            int order = 0;
            double bet = 0;
            Hand hand = new Hand();
            MaxIDHand = hand.GetMaxIDHand();
            bb = 0;
            Sessions session = new Sessions();

            //string[] lines = System.IO.File.ReadAllLines(@"C:\Pessoal\Poker\PokerTracker\PokerTracker\hands\HH20180502 Halley - $0.01-$0.02 - USD No Limit Hold'em.txt");
            string[] lines = System.IO.File.ReadAllLines(path + file);

            sender.prgBar.Value = 0;
            sender.prgBar.Maximum = lines.Count();
            listActions = new List<Actions>();
            listHands = new List<Hand>();
            listTotalHandPlayers = new List<HandPlayers>();

            sql.BeginTransaction();
            foreach (string line in lines)
            {                
                sender.prgBar.Value++;

                if (line.Length > 8 && line.Substring(0,9) == "Dealt to ")
                {
                    cards.IDHand = hand.IDHand;
                    cards.AddCard(listPlayersHand.Find(x => x.Nickname == line.Substring(line.IndexOf("Dealt to ") + 9,line.Length - 17)).IDPlayer, line.Substring(line.IndexOf("[") + 1, 2),1);
                    cards.AddCard(listPlayersHand.Find(x => x.Nickname == line.Substring(line.IndexOf("Dealt to ") + 9, line.Length - 17)).IDPlayer, line.Substring(line.IndexOf("[") + 4, 2),2);
                }
                if (line.Length > 8 && line.Substring(0, 8) == "*** FLOP")
                {
                    bet = 0;
                    cards.IDHand = hand.IDHand;
                    moment = handmoment["Flop"];
                    cards.AddCard(-1, line.Substring(14, 2),0);
                    cards.AddCard(-2, line.Substring(17, 2),0);
                    cards.AddCard(-3, line.Substring(20, 2),0);
                }
                else if (line.Length > 8 && line.Substring(0, 8) == "*** TURN")
                {
                    bet = 0;
                    moment = handmoment["Turn"];
                    cards.AddCard(-4, line.Substring(25, 2),0);
                }
                else if (line.Length > 8 && line.Substring(0, 9) == "*** RIVER")
                {
                    bet = 0;
                    moment = handmoment["River"];
                    cards.AddCard(-5, line.Substring(29, 2),0);
                }

                if (line.Length > 10 && line.Substring(0, 10).ToUpper() == "POKERSTARS")
                {
                    cards = new Cards();

                    if (listHandPlayers.Count() > 0)
                    {
                        foreach (HandPlayers handplayer in listHandPlayers)
                        {
                            handplayer.Insert();
                        }
                    }
                    listHandPlayers = new List<HandPlayers>();
                    listPlayersHand = new List<Players>();
                    listInitialStacks = new List<double>();
                    numplayers = 0;
                    numPlayersNow = 0;
                    stakes = line.Substring(line.IndexOf("(") + 1, 11);
                    datehand = line.Substring(line.IndexOf(")") + 12, 2) + line.Substring(line.IndexOf(")") + 9, 2) + line.Substring(line.IndexOf(")") + 4, 4);
                    timehand = line.Substring(line.IndexOf(")") + 15, 2) + line.Substring(line.IndexOf(")") + 18, 2) + line.Substring(line.IndexOf(")") + 21, 2);
                    if (timehand[1] == ':')
                        timehand = line.Substring(line.IndexOf(")") + 15, 1) + line.Substring(line.IndexOf(")") + 17, 2) + line.Substring(line.IndexOf(")") + 20, 2);
                    order = 0;
                    if (session.DateSession == "" || session.DateSession == null)
                    {
                        session.DateSession = datehand;
                        session.TimeStart = timehand;
                    }
                    session.TotalHands++;
                }

                if (line.Length > 4 && line.Substring(0, 5).ToUpper() == "SEAT " && numplayers >= 0)
                {
                    numplayers++;
                    numPlayersNow++;
                    Players player = new Players();
                    player.Nickname = line.Substring(line.IndexOf(": ") + 2, line.IndexOf("($") - line.IndexOf(": ") - 3);
                    player.FindByNick(player.Nickname);

                    if (player.IDPlayer == 0) player.Insert();
                    else player.UpdateTotalHands();

                    listPlayers.Add(player);
                    listPlayersHand.Add(player);

                    listInitialStacks.Add(Convert.ToDouble(line.Substring(line.IndexOf(" ($") + 3,line.Length - line.IndexOf(" ($") - 3 - 11)));
                }
                if (line.Length > 4 && line.Substring(0, 5).ToUpper() != "SEAT " && numplayers > 0)
                {

                    moment = handmoment["PreFlop"];

                    hand = new Hand();
                    hand.Stakes = stakes;
                    hand.DateHand = datehand;
                    hand.TimeHand = timehand;
                    hand.NumPlayers = numplayers;
                    MaxIDHand++;
                    hand.IDHand = MaxIDHand;
                    listHands.Add(hand);
                    
                    numplayers = -1;                    
                    
                }

                if (moment >= 0 && (line.Length > 4 && line.Substring(0, 5).ToUpper() != "SEAT "))
                {
                    Actions action = new Actions();                    

                    if (line.IndexOf(": posts") > 0)
                    {
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": posts"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = -1;
                        action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" blind $") + 8, line.Length - line.IndexOf(" blind $") - 8));
                        bb = action.Value;
                        bet = bb;
                    }
                    if (line.IndexOf(": folds") > 0)
                    {
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": folds"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = dicAction["Fold"];
                        action.Value = 0;
                    }
                    if (line.IndexOf(": calls") > 0)
                    {
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": calls"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = dicAction["Call"];
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": calls") + 9, line.Length - line.IndexOf(": calls") - 9 - 14));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": calls") + 9, line.Length - line.IndexOf(": calls") - 9));
                        if (moment == 0)
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).VPIP = 1;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB - (action.Value / bb);
                    }
                    if (line.IndexOf(": bets") > 0)
                    {                        
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": bets"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = dicAction["Raise"];
                        if (moment == 0)
                        {
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).VPIP = 1;
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).PFR = 1;
                        }
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": bets") + 8, line.IndexOf(" and is all-in") - line.IndexOf(": bets") - 8));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": bets") + 8, line.Length - line.IndexOf(": bets") - 8));

                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB - (action.Value / bb);
                        bet = action.Value;
                    }
                    if (line.IndexOf(": raises") > 0)
                    {
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": raises"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = dicAction["Raise"];
                        if (moment == 0)
                        {
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).VPIP = 1;
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).PFR = 1;
                        }
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" to $") + 5, line.Length - line.IndexOf(" to $") - 19));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" to $") + 5, line.Length - line.IndexOf(" to $") - 5));
                        
                        if (moment == 0 && listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).Position == 1)
                            action.Value = action.Value - (bet/2);
                        else
                            action.Value = action.Value - bet;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).BB - (action.Value / bb);
                    }
                    if (line.IndexOf(": checks") > 0)
                    {
                        order++;
                        action.IDHand = hand.IDHand;
                        action.OrderHand = order;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": checks"))).IDPlayer;
                        action.HandMoment = moment;
                        action.Action = dicAction["Check"];
                        action.Value = 0;
                    }

                    if (action.IDHand != 0) listActions.Add(action);
                }

                if (line.Length > 10 && line.IndexOf("posts small blind") > 0)
                {
                    SBSeat = 0;

                    HandPlayers handPlayers = new HandPlayers();
                    handPlayers.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": posts"))).IDPlayer;
                    handPlayers.IDHand = MaxIDHand;
                    handPlayers.Position = 1;
                    handPlayers.NetWon = 0 - Convert.ToDouble(line.Substring(line.IndexOf(" blind $") + 8, line.Length - line.IndexOf(" blind $") - 8));
                    handPlayers.VPIP = 0;
                    handPlayers.PFR = 0;
                    handPlayers.BB = 0 - 0.5;
                    handPlayers.Seat = listPlayersHand.FindIndex(x => x.IDPlayer == handPlayers.IDPlayer) + 1;
                    handPlayers.InitialStack = listInitialStacks[handPlayers.Seat - 1];

                    listHandPlayers.Add(handPlayers);
                    listTotalHandPlayers.Add(handPlayers);

                    SBSeat = listPlayersHand.FindIndex(x => x.IDPlayer == handPlayers.IDPlayer) + 1;

                    for (int seats = 1; seats <= numPlayersNow; seats++)
                    {
                        if (SBSeat != seats)
                        {
                            Players playerToAddHand = listPlayersHand[seats - 1];
                            HandPlayers handPlayersToAdd = new HandPlayers();
                            handPlayersToAdd.IDPlayer = playerToAddHand.IDPlayer;
                            handPlayersToAdd.IDHand = MaxIDHand;

                            if (SBSeat > seats)
                                handPlayersToAdd.Position = listPlayersHand.Count() - SBSeat + seats + 1;
                            else
                                handPlayersToAdd.Position = seats + 1 - SBSeat;

                            if ((SBSeat == numPlayersNow && seats == 1) || (seats == SBSeat + 1))
                            {
                                handPlayersToAdd.NetWon = 0 - 2 * Convert.ToDouble(line.Substring(line.IndexOf(" blind $") + 8, line.Length - line.IndexOf(" blind $") - 8));
                                handPlayersToAdd.BB = 0-1;
                            } else
                            {
                                handPlayersToAdd.NetWon = 0;
                                handPlayersToAdd.BB = 0;
                            }
                            //handPlayersToAdd.NetWon = 0;
                            handPlayersToAdd.VPIP = 0;
                            handPlayersToAdd.PFR = 0;
                            handPlayersToAdd.Seat = listPlayersHand.FindIndex(x => x.IDPlayer == handPlayersToAdd.IDPlayer) + 1;
                            handPlayersToAdd.InitialStack = listInitialStacks[handPlayersToAdd.Seat - 1];

                            listHandPlayers.Add(handPlayersToAdd);
                            listTotalHandPlayers.Add(handPlayersToAdd);
                        }                        
                    }
                    
                }

                if (line.Length > 5 && line.IndexOf(") returned to ") > 0)
                {
                    Players playerWhoWon = listPlayersHand.Find(y => y.Nickname == line.Substring(line.IndexOf(") returned to ") + 14, line.Length - line.IndexOf(") returned to ") - 14));
                    netWon = Convert.ToDouble(line.Substring(line.IndexOf("($") + 2, line.IndexOf(") returned to ") - line.IndexOf("($") - 2));
                    listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).NetWon + netWon;
                    listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).BB = listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).BB + (netWon / bb);
                    netWon = 0;
                }

                if (line.Length > 5 && line.IndexOf(" collected $") > 0)
                {
                    Players playerWhoWon = listPlayersHand.Find(y => y.Nickname == line.Substring(0,line.IndexOf(" collected $")));
                    if (line.IndexOf(" from side pot") > 0 || line.IndexOf(" from main pot") > 0)
                        netWon = Convert.ToDouble(line.Substring(line.IndexOf(" collected $") + 12, line.Length - line.IndexOf(" collected $") - 26));
                    else
                        netWon = Convert.ToDouble(line.Substring(line.IndexOf(" collected $") + 12, line.Length - line.IndexOf(" collected $") - 21));

                    listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).NetWon + netWon;
                    listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).BB = listHandPlayers.Find(x => x.IDHand == MaxIDHand && x.IDPlayer == playerWhoWon.IDPlayer).BB + (netWon / bb);
                    netWon = 0;
                }

            }
            
            foreach (Actions action in listActions)
            {
                action.Insert();
            }
            foreach (Hand handToInsert in listHands)
            {
                handToInsert.Insert();                
                session.TimeEnd = handToInsert.TimeHand;
            }

            var idPlayer = listActions.GroupBy(x => x.IDPlayer).OrderByDescending(y => y.Count()).First();
            foreach (HandPlayers handplayer in listTotalHandPlayers.Where(x => x.IDPlayer == idPlayer.Key))
            {
                session.NetWon = session.NetWon + Math.Round(handplayer.NetWon,2);
            }
                        
            session.Insert();

            sql.Commit();

            sender.prgBar.Visible = false;

        }

        public void load2(frmMain sender)
        {
            int handMoment = 0;
            string[] lines = System.IO.File.ReadAllLines(path + file);
            Hand hand = new Hand();
            Players player = new Players();
            List<Players> listPlayers = new List<Players>();
            int orderHand = 0;
            Actions action = new Actions();
            List<Actions> listActions = new List<Actions>();
            HandPlayers handPlayer = new HandPlayers();
            List<HandPlayers> listHandPlayers = new List<HandPlayers>();
            Cards card = new Cards();
            double BigBlind = 0;
            int idPlayerHero = 0;
            Sessions session = new Sessions();
            List<HandSessions> listHandSessions = new List<HandSessions>();
            HandSessions handSession = new HandSessions();
            bool canILoadThisHand = true;
            bool canILoadThisFile = true;
            bool isZoom = false;
            Games game = new Games();

            sender.prgBar.Value = 0;
            sender.prgBar.Maximum = lines.Count();
            sender.prgBar.Visible = true;

            sql.BeginTransaction();
            foreach (string line in lines)
            {
                sender.prgBar.Value++;
                
                //******** POKERSTARS *********
                if (line.Length > 9 && line.Substring(0, 10).ToUpper() == "POKERSTARS")
                {
                    if (line.IndexOf("Tournament") > 0 || line.IndexOf("Omaha") > 0 || line.IndexOf("8-Game") > 0)
                    {
                        canILoadThisFile = false;
                        break;
                    }

                    if (line.Substring(11, 4) == "Zoom")
                        isZoom = true;

                    handSession = new HandSessions();                    
                    card = new Cards();
                    listHandPlayers = new List<HandPlayers>();
                    listActions = new List<Actions>();
                    orderHand = 0;
                    listPlayers = new List<Players>();
                    handMoment = -1;
                    hand = new Hand();
                    hand.IDHand = Convert.ToDouble(line.Substring(line.IndexOf("#") + 1, line.Length - (line.Length - line.IndexOf(":") - 1) - line.IndexOf("#") - 2));

                    canILoadThisHand = hand.AmIANewHand();

                    if (canILoadThisHand)
                    {
                        hand.Stakes = line.Substring(line.IndexOf("(") + 1, 11);
                        hand.DateHand = line.Substring(line.IndexOf(")") + 12, 2) + line.Substring(line.IndexOf(")") + 9, 2) + line.Substring(line.IndexOf(")") + 4, 4);
                        hand.TimeHand = line.Substring(line.IndexOf(")") + 15, 2) + line.Substring(line.IndexOf(")") + 18, 2) + line.Substring(line.IndexOf(")") + 21, 2);
                        if (hand.TimeHand[1] == ':')
                            hand.TimeHand = line.Substring(line.IndexOf(")") + 15, 1) + line.Substring(line.IndexOf(")") + 17, 2) + line.Substring(line.IndexOf(")") + 20, 2);

                        hand.NumPlayers = 0;

                        handSession.IDHand = hand.IDHand;
                        listHandSessions.Add(handSession);

                        if (session.DateSession == "" || session.DateSession == null)
                        {
                            session.DateSession = hand.DateHand;
                            session.TimeStart = hand.TimeHand;
                        }
                        session.TotalHands++;
                    }
                }

                if (canILoadThisHand)
                {
                    //******** SELECTING GAME ***
                    if (line.Length > 6 && line.Substring(0, 7).ToUpper() == "TABLE '")
                    {
                        game = new Games();
                        if (line.IndexOf("6-max") > -1 && isZoom)
                            game.IDGame = 3;
                        if (line.IndexOf("6-max") > -1 && !isZoom)
                            game.IDGame = 1;
                        if (line.IndexOf("9-max") > -1 && isZoom)
                            game.IDGame = 4;
                        if (line.IndexOf("9-max") > -1 && !isZoom)
                            game.IDGame = 2;

                        game.InsertHand(hand.IDHand);
                    }

                    //******** SEATINGS *********
                    if (line.Length > 4 && line.Substring(0, 5).ToUpper() == "SEAT " && handMoment == -1)
                    {
                        hand.NumPlayers++;
                        player = new Players();
                        player.Nickname = line.Substring(line.IndexOf(": ") + 2, line.IndexOf("($") - line.IndexOf(": ") - 3);
                        player.FindByNick(player.Nickname);

                        if (player.IDPlayer == 0) player.Insert();
                        else player.UpdateTotalHands();

                        listPlayers.Add(player);

                        handPlayer = new HandPlayers();
                        handPlayer.IDHand = hand.IDHand;
                        handPlayer.IDPlayer = player.IDPlayer;
                        handPlayer.Seat = hand.NumPlayers;
                        handPlayer.InitialStack = Convert.ToDouble(line.Substring(line.IndexOf(" ($") + 3, line.Length - line.IndexOf(" ($") - 3 - 11));
                        handPlayer.PreviousBet = 0;
                        handPlayer.BB = 0;
                        handPlayer.VPIP = 0;
                        handPlayer.PFR = 0;
                        handPlayer.Position = 0;

                        listHandPlayers.Add(handPlayer);

                    }

                    //******** HOLE CARDS *********
                    if (line.Length > 8 && line.Substring(0, 9) == "Dealt to ")
                    {
                        card.IDHand = hand.IDHand;
                        card.AddCard(listPlayers.Find(x => x.Nickname == line.Substring(line.IndexOf("Dealt to ") + 9, line.Length - 17)).IDPlayer, line.Substring(line.IndexOf("[") + 1, 2),1);
                        card.AddCard(listPlayers.Find(x => x.Nickname == line.Substring(line.IndexOf("Dealt to ") + 9, line.Length - 17)).IDPlayer, line.Substring(line.IndexOf("[") + 4, 2),2);
                        idPlayerHero = listPlayers.Find(x => x.Nickname == line.Substring(line.IndexOf("Dealt to ") + 9, line.Length - 17)).IDPlayer;
                    }

                    //******** BLINDS *********
                    if (line.Length > 14 && line.IndexOf(": posts ") > 0)
                    {
                        handMoment = PreFlop;

                        orderHand++;
                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": posts"))).IDPlayer;
                        action.HandMoment = PreFlop;
                        action.Action = -1;

                        if (line.IndexOf("posts small & big blinds") > 0)
                        {
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" blinds $") + 9, line.Length - line.IndexOf(" blinds $") - 9));
                        }
                        else if (line.IndexOf("posts the ante") > -1)
                        {
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" the ante $") + 11, line.Length - line.IndexOf(" the ante $") - 11));
                        }
                        else
                        {
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" blind $") + 8, line.Length - line.IndexOf(" blind $") - 8));
                        }

                        listActions.Add(action);

                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon - action.Value;

                        if (orderHand == 2)
                        {
                            BigBlind = action.Value;
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB = -1;
                            listHandPlayers.Find(x => x.IDPlayer == listActions[0].IDPlayer).BB = -(listActions[0].Value / BigBlind);
                        }

                        if (line.IndexOf("posts the ante") == -1)
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PreviousBet = action.Value;
                    }

                    //******** FLOP *********
                    if (line.Length > 11 && line.Substring(0, 12).ToUpper() == "*** FLOP ***")
                    {
                        handMoment = Flop;
                        card.IDHand = hand.IDHand;
                        card.AddCard(-1, line.Substring(14, 2),0);
                        card.AddCard(-2, line.Substring(17, 2),0);
                        card.AddCard(-3, line.Substring(20, 2),0);
                        foreach (HandPlayers handPlayersToResetBet in listHandPlayers)
                        {
                            handPlayersToResetBet.PreviousBet = 0;
                        }
                    }

                    //******** TURN *********
                    if (line.Length > 11 && line.Substring(0, 12).ToUpper() == "*** TURN ***")
                    {
                        handMoment = Turn;
                        card.AddCard(-4, line.Substring(25, 2),0);
                        foreach (HandPlayers handPlayersToResetBet in listHandPlayers)
                        {
                            handPlayersToResetBet.PreviousBet = 0;
                        }
                    }

                    //******** RIVER *********
                    if (line.Length > 12 && line.Substring(0, 13).ToUpper() == "*** RIVER ***")
                    {
                        handMoment = River;
                        card.AddCard(-5, line.Substring(29, 2),0);
                        foreach (HandPlayers handPlayersToResetBet in listHandPlayers)
                        {
                            handPlayersToResetBet.PreviousBet = 0;
                        }
                    }

                    //******** FOLD *********
                    if (line.Length > 6 && line.IndexOf(": folds") > 0)
                    {
                        orderHand++;

                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": folds"))).IDPlayer;
                        action.HandMoment = handMoment;
                        action.Action = Fold;
                        action.Value = 0;

                        listActions.Add(action);
                    }

                    //******** CALL *********
                    if (line.Length > 7 && line.IndexOf(": calls ") > 0)
                    {
                        orderHand++;

                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": calls"))).IDPlayer;
                        action.HandMoment = handMoment;
                        action.Action = Call;
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": calls") + 9, line.Length - line.IndexOf(": calls") - 9 - 14));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": calls") + 9, line.Length - line.IndexOf(": calls") - 9));

                        listActions.Add(action);

                        if (handMoment == PreFlop)
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).VPIP = 1;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB - (action.Value / BigBlind);
                    }

                    //******** BET *********
                    if (line.Length > 6 && line.IndexOf(": bets ") > 0)
                    {
                        orderHand++;

                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": bets"))).IDPlayer;
                        action.HandMoment = handMoment;
                        action.Action = Raise;
                        if (handMoment == 0)
                        {
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).VPIP = 1;
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer && x.IDHand == action.IDHand).PFR = 1;
                        }
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": bets") + 8, line.IndexOf(" and is all-in") - line.IndexOf(": bets") - 8));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(": bets") + 8, line.Length - line.IndexOf(": bets") - 8));

                        listActions.Add(action);

                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB - (action.Value / BigBlind);
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PreviousBet = action.Value;
                    }

                    //******** RAISE *********
                    if (line.Length > 8 && line.IndexOf(": raises ") > 0)
                    {
                        orderHand++;

                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": raises"))).IDPlayer;
                        action.HandMoment = handMoment;
                        action.Action = Raise;
                        if (handMoment == 0)
                        {
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).VPIP = 1;
                            listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PFR = 1;
                        }
                        if (line.IndexOf(" and is all-in") != -1)
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" to $") + 5, line.Length - line.IndexOf(" to $") - 19));
                        else
                            action.Value = Convert.ToDouble(line.Substring(line.IndexOf(" to $") + 5, line.Length - line.IndexOf(" to $") - 5));

                        action.Value = action.Value - listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PreviousBet;

                        listActions.Add(action);

                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PreviousBet = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).PreviousBet + action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).NetWon - action.Value;
                        listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB = listHandPlayers.Find(x => x.IDPlayer == action.IDPlayer).BB - (action.Value / BigBlind);
                    }

                    //******** CHECKS *********
                    if (line.Length > 7 && line.IndexOf(": checks") > 0)
                    {
                        orderHand++;

                        action = new Actions();
                        action.IDHand = hand.IDHand;
                        action.OrderHand = orderHand;
                        action.IDPlayer = listPlayers.FirstOrDefault(x => x.Nickname == line.Substring(0, line.IndexOf(": checks"))).IDPlayer;
                        action.HandMoment = handMoment;
                        action.Action = Check;
                        action.Value = 0;

                        listActions.Add(action);
                    }

                    //******** COLECTING MONEY *********
                    if (line.Length > 13 && line.IndexOf(") returned to ") > 0)
                    {
                        Players playerWhoWon = listPlayers.Find(y => y.Nickname == line.Substring(line.IndexOf(") returned to ") + 14, line.Length - line.IndexOf(") returned to ") - 14));
                        netWon = Convert.ToDouble(line.Substring(line.IndexOf("($") + 2, line.IndexOf(") returned to ") - line.IndexOf("($") - 2));
                        listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).NetWon + netWon;
                        listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).BB = listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).BB + (netWon / BigBlind);
                    }
                    else if (line.Length > 11 && line.IndexOf(" collected $") > 0)
                    {
                        Players playerWhoWon = listPlayers.Find(y => y.Nickname == line.Substring(0, line.IndexOf(" collected $")));
                        if (line.IndexOf(" from side pot") > 0 || line.IndexOf(" from main pot") > 0)
                            netWon = Convert.ToDouble(line.Substring(line.IndexOf(" collected $") + 12, line.Length - line.IndexOf(" collected $") - 26));
                        else
                            netWon = Convert.ToDouble(line.Substring(line.IndexOf(" collected $") + 12, line.Length - line.IndexOf(" collected $") - 21));

                        listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).NetWon = listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).NetWon + netWon;
                        listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).BB = listHandPlayers.Find(x => x.IDPlayer == playerWhoWon.IDPlayer).BB + (netWon / BigBlind);
                    }

                    //******** SHOW DOWN *********
                    if (line.Length > 8 && line.IndexOf(": shows [") > 0)
                    {
                        if (listPlayers.Find(y => y.Nickname == line.Substring(0, line.IndexOf(": shows ["))).IDPlayer != idPlayerHero)
                        {
                            card.AddCard(listPlayers.Find(y => y.Nickname == line.Substring(0, line.IndexOf(": shows ["))).IDPlayer, line.Substring(line.IndexOf(": shows [") + 9, 2),1);
                            if (line.Substring(line.IndexOf(": shows [") + 9, 2) != line.Substring(line.IndexOf("] (") - 2, 2))
                            {
                                card.AddCard(listPlayers.Find(y => y.Nickname == line.Substring(0, line.IndexOf(": shows ["))).IDPlayer, line.Substring(line.IndexOf("] (") - 2, 2),2);
                            }
                        }
                    }

                    //******** MUCKED HANDS *********
                    if (line.Length > 6 && line.IndexOf(" mucked [") > 0)
                    {
                        Players playerWhoMucked = new Players();
                        if (line.IndexOf(" (") > -1)
                            playerWhoMucked.FindByNick(line.Substring(8, line.IndexOf(" (") - 8));
                        else
                            playerWhoMucked.FindByNick(line.Substring(8, line.Length - 23));

                        if (!card.AlreadyRecorded(playerWhoMucked.IDPlayer))
                        {
                            card.AddCard(playerWhoMucked.IDPlayer, line.Substring(line.IndexOf(" mucked [") + 9, 2),1);
                            card.AddCard(playerWhoMucked.IDPlayer, line.Substring(line.IndexOf(" mucked [") + 12, 2),2);
                        }
                    }


                    //******** SUMMARY *********
                    if (line.Length > 14 && line.Substring(0, 15).ToUpper() == "*** SUMMARY ***")
                    {
                        hand.Insert2();

                        foreach (Actions actionToInsert in listActions)
                        {
                            actionToInsert.Insert();
                        }

                        foreach (HandPlayers handPlayerToInsert in listHandPlayers)
                        {
                            handPlayerToInsert.Insert();
                            if (handPlayerToInsert.IDPlayer == idPlayerHero)
                                session.NetWon = session.NetWon + handPlayerToInsert.NetWon;
                        }
                    }
                }
            }

            if (canILoadThisFile && session.TotalHands > 0)
            {
                session.TimeEnd = hand.TimeHand;
                session.Insert();

                foreach (HandSessions handSessionsToInsert in listHandSessions)
                {
                    handSessionsToInsert.IDSession = session.IDSession;
                    handSessionsToInsert.Insert();
                }
            }
            sql.Commit();
            sender.prgBar.Visible = false;
        }
            
    }
}
