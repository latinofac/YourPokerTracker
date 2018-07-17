﻿using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class Cards
    {
        SQL sql = new SQL();

        public double IDHand { get; set; }
        public int IDPlayer { get; set; }
        public string Card { get; set; }
        public int CardOrder { get; set; }

        internal List<Cards> GetCards(double idHand)
        {
            List<Cards> list = new List<Cards>();

            SQLiteDataReader dr = sql.Select("SELECT IDPlayer, Card FROM CARDS WHERE IDHAND=" + idHand);

            if (dr.FieldCount > 0)
            {
                while (dr.Read())
                {
                    Cards newCard = new Cards();
                    newCard.IDHand = idHand;
                    newCard.IDPlayer = Convert.ToInt32(dr["IDPlayer"]);
                    newCard.Card = dr["Card"].ToString();
                    list.Add(newCard);
                }
            }

            return list;
        }

        internal void AddCard(int position, string card, int ordercard)
        {
            this.IDPlayer = position;
            this.Card = card;
            this.CardOrder = ordercard;
            sql.Insert("INSERT INTO CARDS (IDHAND, IDPLAYER, CARD, CARDORDER) VALUES (" + this.IDHand + "," + this.IDPlayer + ",'" + this.Card + "'," + this.CardOrder + ")");
        }

        internal bool AlreadyRecorded(int iDPlayer)
        {
            bool IAmRecorded = false;

            SQLiteDataReader dr = sql.Select("SELECT COUNT(*) TOT FROM CARDS WHERE IDHAND=" + this.IDHand + " AND IDPLAYER=" + iDPlayer);

            while (dr.Read())
            {
                if (Convert.ToInt32(dr["TOT"]) > 0)
                {
                    IAmRecorded = true;
                }                
            }

            return IAmRecorded;
            
        }
    }
}
