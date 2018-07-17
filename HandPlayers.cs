using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class HandPlayers
    {

        SQL sql = new SQL();

        public double IDHand { get; set; }
        public int IDPlayer { get; set; }
        public int Position { get; set; }
        public double NetWon { get; set; }
        public double VPIP { get; set; }
        public double PFR { get; set; }
        public double BB { get; set; }
        public int Seat { get; set; }
        public double InitialStack { get; set; }
        public double PreviousBet { get; set; }

        public void Insert()
        {
            sql.Insert("INSERT INTO HANDPLAYERS (IDHand, IDPlayer, Position, NetWon, VPIP, PFR, BB, Seat, InitialStack) values (" + this.IDHand + "," + this.IDPlayer + "," + this.Position + "," + this.NetWon + "," + this.VPIP + "," + this.PFR + "," + this.BB + "," + this.Seat + "," + this.InitialStack + ")");
        }

        internal List<HandPlayers> getList(double idHand)
        {
            SQLiteDataReader dr = sql.Select("SELECT * FROM HANDPLAYERS WHERE IDHAND=" + idHand.ToString());
            List<HandPlayers> list = new List<HandPlayers>();
            HandPlayers hp = new HandPlayers();

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    hp = new HandPlayers();
                    hp.IDHand = idHand;
                    hp.IDPlayer = Convert.ToInt32(dr["IDPlayer"]);
                    hp.Position = Convert.ToInt32(dr["Position"]);
                    hp.NetWon = Convert.ToDouble(dr["NetWon"]);
                    hp.VPIP = Convert.ToDouble(dr["VPIP"]);
                    hp.PFR = Convert.ToDouble(dr["PFR"]);
                    hp.BB = Convert.ToDouble(dr["BB"]);
                    hp.Seat = Convert.ToInt32(dr["Seat"]);
                    hp.InitialStack = Convert.ToDouble(dr["InitialStack"]);
                    list.Add(hp);
                }
            }

            return list;
        }
    }
}
