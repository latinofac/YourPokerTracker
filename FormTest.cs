using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SQLite;
using System.Diagnostics;

namespace PokerTracker
{
    public partial class FormTest : Form
    {

        private SQL sql = new SQL();
        
        public FormTest()
        {
            InitializeComponent();
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SQLiteDataReader dr = sql.Select("SELECT * FROM STUDENT");
            List<Student> list = new List<Student>();

            while (dr.Read())
            {
                list.Add(new Student { ID = Convert.ToInt32(dr["ID"]), Name = dr["Name"].ToString(), Grade = Convert.ToInt32(dr["Grade"]) });

            }
            
            this.dataGridView1.DataSource = list;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            sql.Insert("INSERT INTO STUDENT (ID, Name, Grade) values (3,'" + txtNome.Text + "', " + txtNota.Text + ")");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            sql.Delete("DELETE FROM STUDENT WHERE ID = " + txtID.Text);
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //MessageBox.Show("Current BindingManager Position: " + dataGridView1.CurrentCell.Value);
            //MessageBox.Show("Current BindingManager Position: " + dataGridView1.CurrentRow.Cells["ID"].Value);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow dataGridViewRow in dataGridView1.Rows)
            {
                Student student = new Student();
                student.ID = Convert.ToInt32(dataGridViewRow.Cells["ID"].Value);
                student.Name = Convert.ToString(dataGridViewRow.Cells["Name"].Value);
                student.Grade = Convert.ToInt32(dataGridViewRow.Cells["Grade"].Value);
                sql.Update("UPDATE STUDENT SET NAME='" + student.Name + "', GRADE=" + student.Grade.ToString() + " WHERE ID=" + student.ID.ToString());

            }
            
        }

        private void button5_Click(object sender, EventArgs e)
        {
            HandLoader hl = new HandLoader();
            //hl.load(this);            
            MessageBox.Show("Finished!");
        }
    }
}
