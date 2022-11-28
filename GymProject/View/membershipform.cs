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
    public partial class membershipform : Form
    {
        int memberid;
        public membershipform(int a)
        {
            memberid = a;
            InitializeComponent();
        }
        GymdatabaseEntities1 db = new GymdatabaseEntities1();
        private void membershipform_Load(object sender, EventArgs e)
        {
            //  member
            guna2ComboBox1.DataSource = db.Members.ToArray();
            guna2ComboBox1.DisplayMember = "CIN";
            guna2ComboBox1.ValueMember = "Id";
            guna2ComboBox1.SelectedValue=memberid;
            //  trainer
            var dc = new Dictionary<int, string>();
            foreach (var a in db.Trainers)
            {
                dc.Add(a.Id, a.FirstName + " " + a.LastName);

            }
            
            guna2ComboBox2.DataSource = dc.ToArray();
            guna2ComboBox2.DisplayMember = "Value";
            guna2ComboBox2.ValueMember = "Key";
            //  type
            guna2ComboBox3.DataSource = db.MembershipTypes.ToArray();
            guna2ComboBox3.DisplayMember = "Name";
            guna2ComboBox3.ValueMember = "Id";
           
        }

        private void guna2DateTimePicker1_ValueChanged(object sender, EventArgs e)
        {
            labelX6.Text = guna2DateTimePicker1.Value.AddDays(30).ToShortDateString();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            Membership ms = new Membership();
            ms.MemberId = memberid;
            ms.StartDate = guna2DateTimePicker1.Value;
            ms.EndDate = guna2DateTimePicker1.Value.AddDays(30);
            ms.TrainerId = (int)guna2ComboBox2.SelectedValue;
            ms.MembershipType = (int)guna2ComboBox3.SelectedValue;

            db.Memberships.Add(ms);
            db.SaveChanges();
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
