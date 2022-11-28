using GymProject.Db_Classes;
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
    public partial class AddTrainer : Form
    {
        public AddTrainer()
        {
            InitializeComponent();
        }
        Trainer tr;
        bool sw = false;
        public AddTrainer(Trainer d)
        {
            sw = true;
            tr = d;
            InitializeComponent();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            Trainersdb tar = new Trainersdb();
            if (sw)
            {
                tar.editeMember(tr.Id, tr.FirstName, tr.LastName, tr.Birthday.Value, tr.Gender, tr.Phone, tr.Email, tr.Description);   
            }

         else { 
            
            tar.Add(guna2TextBox1.Text, guna2TextBox2.Text, guna2DateTimePicker1.Value, (radioButton1.Checked) ? "Male":"Famale",guna2TextBox3.Text,guna2TextBox4.Text,guna2TextBox5.Text) ;
            }   
            
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void AddTrainer_Load(object sender, EventArgs e)
        {
            if(sw)
            {
                guna2TextBox1.Text = tr.FirstName;
                guna2TextBox2.Text = tr.LastName;
                radioButton1.Checked = (tr.Gender.ToUpper() == "MALE");
                radioButton2.Checked = (tr.Gender.ToUpper() == "FEMALE");
                guna2TextBox3.Text = tr.Phone;
                guna2TextBox4.Text = tr.Email;
                guna2TextBox5.Text = tr.Description;
                guna2DateTimePicker1.Value = tr.Birthday.Value;
                buttonX1.Text = "Edite";
                this.Text = "EditeTrainer";
            }
        }
    }
}
