using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class Sessions
    {
        public int IDSession { get; set; }
        public string DateSession { get; set; }
        public string TimeStart { get; set; }
        public string TimeEnd { get; set; }
        public int TotalHands { get; set; }
        public double NetWon { get; set; }

        SQL sql = new SQL();        

        internal void Insert()
        {
            sql.Insert("INSERT INTO SESSIONS (DateSession, TimeStart, TimeEnd, TotalHands, NetWon) values ('"+ this.DateSession + "','" + this.TimeStart + "','" + this.TimeEnd + "'," + this.TotalHands + "," + this.NetWon + ")");

            SQLiteDataReader dr = sql.Select("SELECT MAX(IDSession) IDSession FROM SESSIONS");

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    this.IDSession = Convert.ToInt32(dr["IDSession"]);
                }
            }
        }
    }
}
