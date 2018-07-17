using System;
using System.Data.SQLite;

namespace PokerTracker
{
    internal class Games
    {
        public int IDGame { get; set; }
        public string Name { get; set; }

        private SQL sql = new SQL();

        internal void InsertHand(double iDHand)
        {
            sql.Insert("INSERT INTO HANDGAMES (IDGAME, IDHAND) VALUES (" + this.IDGame + "," + iDHand + ")");
        }

        internal string GetGame(double idHand)
        {            
            SQLiteDataReader dr = sql.Select("SELECT IDGame FROM HANDGAMES WHERE IDHAND=" + idHand);

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    this.IDGame = Convert.ToInt32(dr["IDGame"]);                    
                }
            }

            if (this.IDGame == 1 || this.IDGame == 3)
                this.Name = "6max";
            else
                this.Name = "9max";

            return this.Name;

        }
    }
}