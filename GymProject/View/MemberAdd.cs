using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GymProject
{
    public partial class MemberAdd : Form
    {
        public MemberAdd()
        {
            InitializeComponent();
        }
        GymdatabaseEntities1 db = new GymdatabaseEntities1();
        private void MemberAdd_Load(object sender, EventArgs e)
        {
            guna2ComboBox1.DataSource = db.Groups.ToArray();
            guna2ComboBox1.DisplayMember = "Name";
            guna2ComboBox1.ValueMember = "Id";
            radioButton1.Checked = true;
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            dbMember Memb = new dbMember();
            string gender = (radioButton1.Checked) ? radioButton1.Text : radioButton2.Text;
            
            Memb.AddMember(guna2TextBox2.Text, guna2TextBox3.Text, guna2TextBox1.Text, guna2DateTimePicker1.Value, gender, guna2TextBox6.Text, guna2TextBox4.Text, guna2TextBox5.Text, path, guna2TextBox7.Text,(int)guna2ComboBox1.SelectedValue);
        }
        string path = "";
        private void buttonX1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                path = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(path);
            }
            
        }
    }
}
