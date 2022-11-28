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
    public partial class MembershiptypeAdd : Form
    {
        public MembershiptypeAdd()
        {
            InitializeComponent();
        }

        private void guna2Button1_Click(object sender, EventArgs e)
        {
            
            MessageBox.Show(guna2NumericUpDown1.Value.ToString());
            typemembershipdb.add(guna2TextBox1.Text, guna2NumericUpDown1.Value, guna2NumericUpDown2.Value, guna2TextBox2.Text);
        }

        private void guna2Button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void MembershiptypeAdd_Load(object sender, EventArgs e)
        {

        }
    }
}
