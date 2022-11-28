using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymProject.View
{
    public partial class Groupview : Form
    {
        public Groupview()
        {
            InitializeComponent();
        }
        GymdatabaseEntities1 db = new GymdatabaseEntities1();
        private void Groupview_Load(object sender, EventArgs e)
        {
            comboBoxEx1.DataSource = db.Groups.ToArray();
            comboBoxEx1.DisplayMember = "NameGroupe";
            comboBoxEx1.ValueMember = "Id";
            guna2DataGridView1.DataSource = db.Sessions.ToArray();
            guna2DataGridView1.ColumnHeadersHeight = 20;
        }

        private void comboBoxEx1_SelectedIndexChanged(object sender, EventArgs e)
        {
            int t;
            if(int.TryParse(comboBoxEx1.SelectedValue.ToString(),out t))
            label1.Text = comboBoxEx1.SelectedValue.ToString();
        }

        private void comboBoxEx1_Click(object sender, EventArgs e)
        {
            
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Group d = new Group() { NameGroupe = textBoxX1.Text };
            db.Groups.Add(d);
            db.SaveChanges();
            MessageBox.Show(textBoxX1.Text + " has been add to group list");
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            Session d = new Session() { GroupID = (int)comboBoxEx1.SelectedValue, Day = guna2DateTimePicker1.Value }; 
            db.Sessions.Add(d);
            db.SaveChanges();
            MessageBox.Show(d.Day + " has been add to group " + d.Group.NameGroupe);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            int idg = 0;
            if (int.TryParse(textBoxX2.Text,out idg))
            {
                db.Groups.Remove(db.Groups.Where(x => x.Id == idg).FirstOrDefault());
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("error group doesnot exist");
            }
            
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            int idg = 0;
            if (int.TryParse(textBoxX2.Text, out idg))
            {
                db.Groups.Where(x => x.Id == idg).FirstOrDefault().NameGroupe = textBoxX1.Text;
                db.SaveChanges();
            }
            else
            {
                MessageBox.Show("error group doesnot exist");
            }
        }

        private void guna2DataGridView1_RowsRemoved(object sender, DataGridViewRowsRemovedEventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int iddelete = 0;
            if(int.TryParse(guna2DataGridView1.SelectedRows[0].Cells[0].Value.ToString(),out iddelete)&& db.Sessions.Where(x => x.Id == iddelete).Count() >0) { 
            db.Sessions.Remove(db.Sessions.Where(x => x.Id == iddelete).FirstOrDefault());
                db.SaveChanges();
            MessageBox.Show("row deleted");} else
            {
                MessageBox.Show("error");
            }
        }
    }
}
