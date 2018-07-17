using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class HandView
    {

        SQL sql = new SQL();

        public double IDHand { get; set; }
        public string CardOne { get; set; }
        public string CardTwo { get; set; }
        public double NetWon { get; set; }
        public int IDSession { get; set; }

        public List<HandView> GetHandsByIDSession(int idSession, int idPlayer, int page, string filteredHands)
        {            
            List<HandView> listHandView = new List<HandView>();
            /*
            HandView handView = new HandView();
            SQLiteDataReader dr = null;

            if (idSession > 0)
                dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON FROM HANDSESSIONS HS INNER JOIN CARDS C ON HS.IDHAND = C.IDHAND INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND WHERE IDSESSION=" + idSession + " AND P.IDPLAYER = " + idPlayer + " ORDER BY C.IDHAND LIMIT 200  OFFSET " + ((page - 1) * 200).ToString());
            else
                dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON FROM HANDSESSIONS HS INNER JOIN CARDS C ON HS.IDHAND = C.IDHAND INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND WHERE P.IDPLAYER = " + idPlayer + " ORDER BY C.IDHAND LIMIT 200  OFFSET " + ((page - 1) * 200).ToString());  

            while (dr.Read())
            {
                if (handView.CardOne == "" || handView.CardOne == null)
                {                    
                    handView.IDHand = Convert.ToDouble(dr["IDHAND"]);
                    handView.NetWon = Convert.ToDouble(dr["NETWON"]);
                    handView.CardOne = dr["CARD"].ToString();
                }
                else
                {
                    handView.CardTwo = dr["CARD"].ToString();
                    listHandView.Add(handView);
                    handView = new HandView();
                } 
            }
            */
            this.IDSession = idSession;
            listHandView = GetAllHands(page, idPlayer, filteredHands);
            return listHandView;
        }

        public List<HandView> GetAllHands(int page, int idPlayer, string filteredHands)
        {
            List<HandView> listHandView = new List<HandView>();
            HandView handView = new HandView();
            string subquery = "";

            if (filteredHands != "")
            {
                string[] hands = filteredHands.Split(',');
                string suited = "";
                
                foreach (string hand in hands)
                {
                    if (hand.Length > 2 && hand.Substring(2, 1) == "o")
                        suited = "SUBSTR(C1.CARD,2,1) <> SUBSTR(C2.CARD,2,1) AND ";
                    if (hand.Length > 2 && hand.Substring(2, 1) == "s")
                        suited = "SUBSTR(C1.CARD,2,1) = SUBSTR(C2.CARD,2,1) AND ";
                    if (hand.Length == 2)
                        suited = "";

                    if (subquery != "")
                        subquery = subquery + " UNION ";
                    subquery = subquery +
                                "SELECT C.IDHAND " +
                               "FROM CARDS C " +
                               "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER = C.IDPLAYER AND HP.IDHAND = C.IDHAND " +
                               "INNER JOIN HANDSESSIONS HS ON C.IDHAND = HS.IDHAND " +
                               "INNER JOIN CARDS C1 ON C.IDHAND = C1.IDHAND AND C.IDPLAYER = C1.IDPLAYER AND C1.CARDORDER = 1 AND C1.CARD IN ('" + hand.Substring(0,1) + "h', '" + hand.Substring(0, 1) + "d', '" + hand.Substring(0, 1) + "s', '" + hand.Substring(0, 1) + "c') " +
                               "INNER JOIN CARDS C2 ON C.IDHAND = C2.IDHAND AND C.IDPLAYER = C2.IDPLAYER AND C2.CARDORDER = 2 AND C2.CARD IN ('" + hand.Substring(1, 1) + "h', '" + hand.Substring(1, 1) + "d', '" + hand.Substring(1, 1) + "s', '" + hand.Substring(1, 1) + "c') " +
                               "WHERE " + suited + " C.IDPLAYER = " + idPlayer.ToString() + " " +
                               "UNION " +
                               "SELECT C.IDHAND " +
                               "FROM CARDS C " +
                               "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER = C.IDPLAYER AND HP.IDHAND = C.IDHAND " +
                               "INNER JOIN HANDSESSIONS HS ON C.IDHAND = HS.IDHAND " +
                               "INNER JOIN CARDS C1 ON C.IDHAND = C1.IDHAND AND C.IDPLAYER = C1.IDPLAYER AND C1.CARDORDER = 1 AND C1.CARD IN ('" + hand.Substring(1, 1) + "h', '" + hand.Substring(1, 1) + "d', '" + hand.Substring(1, 1) + "s', '" + hand.Substring(1, 1) + "c') " +
                               "INNER JOIN CARDS C2 ON C.IDHAND = C2.IDHAND AND C.IDPLAYER = C2.IDPLAYER AND C2.CARDORDER = 2 AND C2.CARD IN ('" + hand.Substring(0, 1) + "h', '" + hand.Substring(0, 1) + "d', '" + hand.Substring(0, 1) + "s', '" + hand.Substring(0, 1) + "c')" +
                               "WHERE " + suited + " C.IDPLAYER = " + idPlayer + " ";
                }
            }

            SQLiteDataReader dr = null;

            if (this.IDSession > 0)
            {
                if (filteredHands == "")
                    dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON FROM CARDS C INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND INNER JOIN HANDSESSIONS HS ON HP.IDHAND=HS.IDHAND WHERE P.IDPLAYER = " + idPlayer + " AND HS.IDSESSION = " + this.IDSession + " ORDER BY C.IDHAND LIMIT 200 OFFSET " + ((page - 1) * 200).ToString());
                else
                    dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON " +
                                    "FROM CARDS C " + 
                                    "INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER " + 
                                    "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND " + 
                                    "INNER JOIN HANDSESSIONS HS ON HP.IDHAND=HS.IDHAND " + 
                                    "WHERE P.IDPLAYER = " + idPlayer + " AND HS.IDSESSION = " + this.IDSession + " AND C.IDHAND IN (" + 
                                    subquery +
                                    ") ORDER BY C.IDHAND LIMIT 200 OFFSET " + ((page - 1) * 200).ToString());
            }
            else
            {
                if (filteredHands == "")
                    dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON FROM CARDS C INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND WHERE P.IDPLAYER = " + idPlayer + " ORDER BY C.IDHAND LIMIT 200 OFFSET " + ((page - 1) * 200).ToString());
                else
                    dr = sql.Select("SELECT C.IDHAND, C.CARD, HP.NETWON " +
                                    "FROM CARDS C " +
                                    "INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER " +
                                    "INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND " +
                                    "INNER JOIN HANDSESSIONS HS ON HP.IDHAND=HS.IDHAND " +
                                    "WHERE P.IDPLAYER = " + idPlayer + " AND C.IDHAND IN (" +
                                    subquery +
                                    ") ORDER BY C.IDHAND LIMIT 200 OFFSET " + ((page - 1) * 200).ToString());
            }

            while (dr.Read())
            {
                if (handView.CardOne == "" || handView.CardOne == null)
                {
                    handView.IDHand = Convert.ToDouble(dr["IDHAND"]);
                    handView.NetWon = Convert.ToDouble(dr["NETWON"]);
                    handView.CardOne = dr["CARD"].ToString();
                }
                else
                {
                    handView.CardTwo = dr["CARD"].ToString();
                    listHandView.Add(handView);
                    handView = new HandView();
                }
            }

            return listHandView;
        }

        public int GetMaxPages(int idPlayer)
        {
            int NumPages = 0;
            SQLiteDataReader dr = sql.Select("SELECT COUNT(*)/200 MAXPAGES FROM CARDS C INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND WHERE P.IDPLAYER = " + idPlayer);

            while (dr.Read())
            {

                NumPages = Convert.ToInt32(dr["MAXPAGES"]);               
            }

            return NumPages + 1;
        }

        public int GetMaxPagesSession(int idPlayer)
        {
            int NumPages = 0;
            SQLiteDataReader dr = null;

            if (this.IDSession > 0)
                dr = sql.Select("SELECT COUNT(*)/200 MAXPAGES FROM CARDS C INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND WHERE P.IDPLAYER = " + idPlayer + " AND HS.IDSESSION = " + this.IDSession);
            else
                dr = sql.Select("SELECT COUNT(*)/200 MAXPAGES FROM CARDS C INNER JOIN PLAYERS P ON C.IDPLAYER = P.IDPLAYER INNER JOIN HANDPLAYERS HP ON HP.IDPLAYER=P.IDPLAYER AND HP.IDHAND=C.IDHAND INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND WHERE P.IDPLAYER = " + idPlayer);

            while (dr.Read())
            {

                NumPages = Convert.ToInt32(dr["MAXPAGES"]);
            }

            return NumPages + 1;
        }
    }
}
