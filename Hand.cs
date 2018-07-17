using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class Hand
    {

        SQL sql = new SQL();

        public double IDHand { get; set; }
        public string Stakes { get; set; }
        public string DateHand { get; set; }
        public string TimeHand { get; set; }
        public int NumPlayers { get; set; }

        public void Insert()
        {
            sql.Insert("INSERT INTO HANDS (Stakes,DateHand,TimeHand,NumPlayers) values ('" + this.Stakes + "','" + this.DateHand + "','" + this.TimeHand + "'," + this.NumPlayers + ")");

            SQLiteDataReader dr = sql.Select("SELECT * FROM HANDS ORDER BY IDHand DESC LIMIT 1");

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    this.IDHand = Convert.ToDouble(dr["IDHand"]);                    
                }
            }
        }

        public int GetMaxIDHand()
        {
            int MaxIDHand = 0;

            SQLiteDataReader dr = sql.Select("SELECT MAX(IDHAND) IDHand FROM HANDS");
            
            while (dr.Read())
            {
                if (dr["IDHand"].ToString() != "")
                    MaxIDHand = Convert.ToInt32(dr["IDHand"]);
            }
            return MaxIDHand;
        }

        public void Insert2()
        {
            sql.Insert("INSERT INTO HANDS (IDHand, Stakes,DateHand,TimeHand,NumPlayers) values (" + this.IDHand + ",'" + this.Stakes + "','" + this.DateHand + "','" + this.TimeHand + "'," + this.NumPlayers + ")");            
        }

        internal bool AmIANewHand()
        {
            bool IAmANewHand = false;

            SQLiteDataReader dr = sql.Select("SELECT COUNT(*) TOT FROM HANDS WHERE IDHAND=" + this.IDHand);

            while (dr.Read())
            {
                if (Convert.ToInt32(dr["TOT"]) == 0)
                    IAmANewHand = true;
            }

            return IAmANewHand;
        }
    }   
}
