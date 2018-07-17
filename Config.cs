using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokerTracker
{
    class Config
    {
        public int IDConfig { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }

        private SQL sql = new SQL();

        internal void Update()
        {
            SQLiteDataReader dr = sql.Select("SELECT COUNT(*) TOT FROM CONFIG WHERE IDCONFIG = " + this.IDConfig);

            while (dr.Read())
            {
                if (Convert.ToInt32(dr["TOT"]) == 1)
                    sql.Update("UPDATE CONFIG SET VALUE = '" + this.Value + "' WHERE IDCONFIG=" + this.IDConfig);
                else
                    sql.Insert("INSERT INTO CONFIG (IDCONFIG, NAME, VALUE) VALUES (" + this.IDConfig + ", '" + this.Name + "','" + this.Value + "')");
            }
            
        }

        internal bool IsDefined()
        {
            bool IAmDefined = false;

            SQLiteDataReader dr = sql.Select("SELECT COUNT(*) TOT FROM CONFIG WHERE IDCONFIG IN (1,2)");

            while (dr.Read())
            {
                if (Convert.ToInt32(dr["TOT"]) == 2)
                    IAmDefined = true;
            }

            return IAmDefined;
        }

        internal string GetBackupPath()
        {
            string backupPath = "";

            SQLiteDataReader dr = sql.Select("SELECT VALUE FROM CONFIG WHERE IDCONFIG = 2");

            while (dr.Read())
            {
                backupPath = dr["VALUE"].ToString();                    
            }

            return backupPath;
        }

        internal string GetHandPath()
        {
            string handPath = "";

            SQLiteDataReader dr = sql.Select("SELECT VALUE FROM CONFIG WHERE IDCONFIG = 1");

            while (dr.Read())
            {
                handPath = dr["VALUE"].ToString();
            }

            return handPath;
        }
    }
}
