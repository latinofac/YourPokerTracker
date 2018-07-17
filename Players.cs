using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace PokerTracker
{
    class Players
    {
        SQL sql = new SQL();

        public int IDPlayer { get; set; }
        public string Nickname { get; set; }
        public int TotalHands { get; set; }
        public double NetWon { get; set; }

        public void FindByNick(string nickname)
        {
            SQLiteDataReader dr =  sql.Select("SELECT * FROM PLAYERS WHERE NICKNAME='" + nickname.Replace("'","''") + "'");

            if (dr.FieldCount > 0)
            {                
                while (dr.Read())
                {
                    this.IDPlayer = Convert.ToInt32(dr["IDPlayer"]);
                    this.Nickname = nickname;
                    this.TotalHands = Convert.ToInt32(dr["TotalHands"]);
                    this.NetWon = Convert.ToDouble(dr["NetWon"]);
                }
            }
            
        }

        public void Insert()
        {
            sql.Insert("INSERT INTO PLAYERS (Nickname, TotalHands, NetWon) values ('" + this.Nickname.Replace("'","''") + "',1,0)");

            SQLiteDataReader dr = sql.Select("SELECT * FROM PLAYERS WHERE NICKNAME='" + this.Nickname.Replace("'","''") + "'");

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    this.IDPlayer = Convert.ToInt32(dr["IDPlayer"]);                    
                }
            }
        }

        internal int GetHeroID(int idSession)
        {
            int idHero = 0;
            SQLiteDataReader dr = null;

            if (idSession > 0)
                dr = sql.Select("SELECT IDPLAYER, COUNT(*) TOT FROM HANDPLAYERS HP INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND WHERE IDSESSION=" + idSession + " GROUP BY IDPLAYER ORDER BY TOT DESC LIMIT 1");
            else
                dr = sql.Select("SELECT IDPLAYER, COUNT(*) TOT FROM HANDPLAYERS HP INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND GROUP BY IDPLAYER ORDER BY TOT DESC LIMIT 1");

            while (dr.Read())
            {
                idHero = Convert.ToInt32(dr["IDPlayer"]);                
            }

            return idHero;
        }

        internal List<Players> GetPlayersSession(int idSession)
        {
            List<Players> listPlayers = new List<Players>();
            SQLiteDataReader dr = null;

            if (idSession > 0)
                dr = sql.Select("SELECT DISTINCT P.IDPLAYER, P.NICKNAME FROM PLAYERS P INNER JOIN HANDPLAYERS HP ON P.IDPLAYER = HP.IDPLAYER INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND INNER JOIN CARDS C ON HS.IDHAND=C.IDHAND AND P.IDPLAYER=C.IDPLAYER WHERE IDSESSION=" + idSession);
            else
                dr = sql.Select("SELECT DISTINCT P.IDPLAYER, P.NICKNAME FROM PLAYERS P INNER JOIN HANDPLAYERS HP ON P.IDPLAYER = HP.IDPLAYER INNER JOIN HANDSESSIONS HS ON HP.IDHAND = HS.IDHAND INNER JOIN CARDS C ON HS.IDHAND=C.IDHAND AND P.IDPLAYER=C.IDPLAYER");

            while (dr.Read())
            {
                Players player = new Players();
                player.IDPlayer = Convert.ToInt32(dr["IDPlayer"]);
                player.Nickname = dr["Nickname"].ToString();
                listPlayers.Add(player);
            }

            return listPlayers;
        }

        public int GetMaxIDPlayer()
        {
            int MaxIDPlayer = 0;

            SQLiteDataReader dr = sql.Select("SELECT MAX(IDPLAYER) IDPlayer FROM PLAYERS");

            while (dr.Read())
            {
                if (dr["IDPlayer"].ToString() != "")
                    MaxIDPlayer = Convert.ToInt32(dr["IDPlayer"]);
            }
            return MaxIDPlayer;
        }

        public void UpdateTotalHands()
        {
            sql.Update("UPDATE PLAYERS SET TotalHands = TotalHands + 1 WHERE IDPlayer = " + this.IDPlayer);
        }

        internal void FindById(int iDPlayer)
        {
            SQLiteDataReader dr = sql.Select("SELECT * FROM PLAYERS WHERE IDPLAYER = " + iDPlayer);

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    this.IDPlayer = iDPlayer;
                    this.Nickname = dr["Nickname"].ToString();
                    this.TotalHands = Convert.ToInt32(dr["TotalHands"]);                    
                }                 
            }
        }
        
    }
}
