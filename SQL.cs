using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace PokerTracker
{
    class SQL
    {
        private static string connection = "Data Source=database.db";        
        private static SQLiteConnection conn = new SQLiteConnection(connection);
        private static SQLiteTransaction transaction;

        public static void connect()
        {
            if (conn.State == ConnectionState.Closed)
            {
                conn.Open();
            }
        }

        public SQLiteDataReader Select(string command)
        { 
            connect();
            //command = command.Replace("'", "''");
            SQLiteCommand cmd = new SQLiteCommand(command, conn);
            SQLiteDataReader dr = cmd.ExecuteReader();
            return dr;
        }

        public void Insert(string command)
        {
            connect();
            SQLiteCommand cmd = new SQLiteCommand(command, conn);
            cmd.ExecuteNonQuery();
        }

        public void BeginTransaction()
        {
            connect();
            transaction = conn.BeginTransaction();
        }

        public void Commit()
        {
            transaction.Commit();
        }

        public void Delete(string command)
        {
            connect();
            SQLiteCommand cmd = new SQLiteCommand(command, conn);
            cmd.ExecuteNonQuery();
        }

        public void Update(string command)
        {
            connect();
            SQLiteCommand cmd = new SQLiteCommand(command, conn);
            cmd.ExecuteNonQuery();
        }

        public void BackupDB()
        {
             //File.Copy("test.db", "test.db.bkp." + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Year + DateTime.Now.Millisecond);
        }

        internal void ClearDB()
        {
            connect();

            SQLiteCommand cmd = new SQLiteCommand("DELETE FROM CARDS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM HANDPLAYERS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM SESSIONS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM HANDS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM PLAYERS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM ACTIONS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM HANDSESSIONS", conn);
            cmd.ExecuteNonQuery();

            cmd = new SQLiteCommand("DELETE FROM HANDGAMES", conn);
            cmd.ExecuteNonQuery();
        }
    }
}
