using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ClosedXML.Excel;
using GymProject.View;

namespace GymProject
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        GymdatabaseEntities1 db;
        bool lol = true;
        private void ribbonBar1_ItemClick(object sender, EventArgs e)
        {

        }

        private void ribbonBar3_ItemClick(object sender, EventArgs e)
        {

        }

        private void buttonItem14_Click(object sender, EventArgs e)
        {

        }

        private void ribbonBar10_ItemClick(object sender, EventArgs e)
        {

        }

        private void ribbonTabItem1_Click(object sender, EventArgs e)
        {
            
            panelEx1.BringToFront();
        }
        void hideALl()
        {
            panelEx2.Hide();
            panelEx1.Hide();
            panelEx3.Hide();
            panelEx4.Hide();
            panelEx5.Hide();
        }

        private void Members_Click(object sender, EventArgs e)
        {
            panelEx2.BringToFront();
        }

        private void ribbonTabItem2_Click(object sender, EventArgs e)
        {
            panelEx7.BringToFront();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            panelEx1.BringToFront();
            db= new GymdatabaseEntities1();
            foreach (var item in db.MembershipTypes.ToArray())
            {
                comboBoxItem1.Items.Add(item.Name);
            }
            foreach (var item in db.Trainers.ToArray())
            {
                comboBoxItem2.Items.Add(item.Id);
                
            }
            object[] d =  { "CIN", "Phone", "Adress", "Email" };
            comboBoxItem3.Items.AddRange(d);
            //memberpage
            comboBoxEx2.DataSource = db.Groups.ToArray();
            comboBoxEx2.DisplayMember = "NameGroupe";
            comboBoxEx2.ValueMember = "Id";
            ////
            dataGridViewX4.DataSource = db.MembershipTypes.ToArray();
            comboBoxEx3.DataSource = db.MembershipTypes.ToArray();
            comboBoxEx3.DisplayMember = "Name";
            comboBoxEx3.ValueMember = "Id";

        }
        void loadmember(int id)
        {
            warningBox1.Visible = false;
            warningBox1.Text = "<b>Expired Membership</b>";
            dataGridViewX2.DataSource = db.Memberships.Where(x => x.MemberId == id).ToArray();
            foreach (var item in db.Memberships.Where(x => x.MemberId == id))
            {
                if (item.EndDate.Value < DateTime.Now.Date)
                {
                    warningBox1.Text += "<i>Membership Number : "+item.Id+" Ended in :"+item.EndDate.ToString()+" </i> " +
                        "\n";
                    warningBox1.Visible = true;
                }
            }
        }

        private void Membership_Click(object sender, EventArgs e)
        {
            panelEx4.BringToFront();
        }

        private void ribbonTabItem3_Click(object sender, EventArgs e)
        {
            panelEx3.BringToFront();
        }

        private void buttonItem14_Click_1(object sender, EventArgs e)
        {
            dataGridViewX1.DataSource = db.Members.ToArray();
        }

        private void Form1_Resize(object sender, EventArgs e)
        {
            Size d = new Size(this.Width,this.Height-156);
            panelEx5.Size = d;
            Point h = new Point(0, 300);
            dataGridViewX2.Location = h;
            Point h1 = new Point(0, 120);
            dataGridViewX6.Location = h1;
            

        }

        private void buttonItem16_Click(object sender, EventArgs e)
        {
            MessageBox.Show(comboBoxItem3.Text);
            switch (comboBoxItem3.Text)
            {
                case "CIN":
                    var res = db.Members.Where(x => x.CIN == textBoxItem4.Text);
                    dataGridViewX1.DataSource = res.ToArray();
                    break;
                case "Phone":
                    var del = db.Members.Where(x => x.Phone == textBoxItem4.Text);
                    dataGridViewX1.DataSource = del.ToArray();
                    break;
                case "Email":
                    var res1 = db.Members.Where(x => x.Email == textBoxItem4.Text);
                    dataGridViewX1.DataSource = res1.ToArray();
                    break;
                case "Adress":
                    var res2 = db.Members.Where(x => x.Adress == textBoxItem4.Text);
                    dataGridViewX1.DataSource = res2.ToArray();
                    break;
                default:
                    MessageBox.Show("Please select search methode");
                    break;

            }
        }

        private void textBoxItem1_TextChanged(object sender, EventArgs e)
        {
            var res = db.Members.Where(x => x.FirstName.Contains(textBoxItem1.Text));
            dataGridViewX1.DataSource = res.ToArray();
        }

        private void textBoxItem2_TextChanged(object sender, EventArgs e)
        {
            var res = db.Members.Where(x => x.LastName.Contains(textBoxItem2.Text));
            dataGridViewX1.DataSource = res.ToArray();
        }

        private void comboBoxItem1_TextChanged(object sender, EventArgs e)
        {
            var id = db.MembershipTypes.Where(i => i.Name == comboBoxItem1.Text).FirstOrDefault().Id;
            var w = db.Members.Where(x => x.Memberships.Any(r => r.MemberId == id));

            
                dataGridViewX1.DataSource = w.ToArray();
        }

        private void comboBoxItem2_TextChanged(object sender, EventArgs e)
        {
            var d = db.Memberships.Where(x => x.Trainer.FirstName == comboBoxItem1.Text);
            List<Member> res = new List<Member>();
            foreach (var item in d)
            {
                res.Add(item.Member);
            }
            dataGridViewX1.DataSource = res;
        }

        private void buttonItem18_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Members");
            string[] op =  { "id", "First name", "Last Name", "CIN", "Birthday" };
            
            foreach (var item in op)
            {
                dt.Columns.Add(item);
            }
            for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
            {
                dt.Rows.Add(dataGridViewX1.Rows[i].Cells["Id"].Value,
                    dataGridViewX1.Rows[i].Cells["FirstName"].Value,
                    dataGridViewX1.Rows[i].Cells["LastName"].Value,
                    dataGridViewX1.Rows[i].Cells["CIN"].Value,
                    dataGridViewX1.Rows[i].Cells["Birthday"].Value);
            }
            saveFileDialog1.Filter = "XML|*.xml";
            if(saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                dt.WriteXml(saveFileDialog1.FileName);

            }
            
            

        }

        private void buttonItem17_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Members");
            string[] op = { "id", "First name", "Last Name", "CIN", "Birthday" };

            foreach (var item in op)
            {
                dt.Columns.Add(item);
            }
            for (int i = 0; i < dataGridViewX1.Rows.Count; i++)
            {
                dt.Rows.Add(dataGridViewX1.Rows[i].Cells["Id"].Value,
                    dataGridViewX1.Rows[i].Cells["FirstName"].Value,
                    dataGridViewX1.Rows[i].Cells["LastName"].Value,
                    dataGridViewX1.Rows[i].Cells["CIN"].Value,
                    dataGridViewX1.Rows[i].Cells["Birthday"].Value);
            }
            saveFileDialog1.Filter = "Excel|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using(XLWorkbook wk = new XLWorkbook())
                {
                    wk.Worksheets.Add(dt);
                    wk.SaveAs(saveFileDialog1.FileName);
                }

            }

        }

        private void buttonItem19_Click(object sender, EventArgs e)
        {
            MemberAdd f = new MemberAdd();
            f.ShowDialog();
        }

        private void buttonItem20_Click(object sender, EventArgs e)
        {
            if (lol) {
                textBox1.ReadOnly = false;
                textBox2.ReadOnly = false;
                textBox3.ReadOnly = false;
                textBox4.ReadOnly = false;
                textBox5.ReadOnly = false;
                textBox6.ReadOnly = false;
                textBox7.ReadOnly = false;
                textBoxX1.Enabled = true;
                dateTimeInput1.Enabled = true;
                comboBoxEx2.Enabled = true;
                buttonX4.Visible = true;
                lol = false;
            }
            else
            {
                textBox1.ReadOnly = true;
                textBox2.ReadOnly = true;
                textBox3.ReadOnly = true;
                textBox4.ReadOnly = true;
                textBox5.ReadOnly = true;
                textBox6.ReadOnly = true;
                textBox7.ReadOnly = true;
                textBoxX1.Enabled = false;
                dateTimeInput1.Enabled = false;
                comboBoxEx2.Enabled = false;
                buttonX4.Visible = false;
                lol = true;
            }
           
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string path = openFileDialog1.FileName;
                pictureBox1.Image = Image.FromFile(path);
            }
        }
        Member mmber=new Member();
        private void buttonItem23_Click(object sender, EventArgs e)
        {
            
            dbMember m = new dbMember();
            if (!mmber.Id.Equals(0) )
            { 
                m.EditeMember(mmber.Id,textBox1.Text,textBox2.Text,textBox3.Text,dateTimeInput1.Value,(radioButton1.Checked)?"Male":"Female",textBox4.Text,textBox5.Text,textBox6.Text,pictureBox1.Image,textBoxX1.Text,int.Parse(comboBoxEx2.SelectedValue.ToString()));
            }
            
        }

        private void buttonItem24_Click(object sender, EventArgs e)
        {
            try
            {
               
            
                if (db.Members.Where(x => x.CIN == textBoxItem3.Text).Count()>0) {
            mmber = db.Members.Where(x => x.CIN == textBoxItem3.Text).FirstOrDefault();
            labelX48.Text = mmber.Id.ToString();
            memberid = mmber.Id;
            textBox1.Text = mmber.FirstName;
            textBox2.Text = mmber.LastName;
            textBox3.Text = mmber.CIN;
            textBox4.Text = mmber.Adress;
            textBox5.Text = mmber.Phone;
            textBox6.Text = mmber.Email;
            textBoxX1.Text = mmber.Description;
            radioButton1.Checked = (mmber.Gender.ToUpper() == "MALE") ? true : false ;
            radioButton2.Checked = (mmber.Gender.ToUpper() == "FEMALE") ? true : false;
            dateTimeInput1.Value = mmber.Birthday.Value;
            comboBoxEx2.SelectedValue = mmber.GroupID;
                    loadmember(mmber.Id);
            Membershipdb ms = new Membershipdb();
            dataGridViewX2.DataSource = ms.search(mmber.Id);


                    if (mmber.Image is null)
                    {

                        pictureBox1.Image = Properties.Resources.defMember;
                    }
                    else
                        using (MemoryStream memstr = new MemoryStream(mmber.Image))
                        {
                            Image img = Image.FromStream(memstr);
                            pictureBox1.Image = img;
                        }
                }
                else { MessageBox.Show("This member doesnot exist"); }
            }catch(Exception ex) { MessageBox.Show(ex.Message); }
            
            
        }

        private void buttonItem22_Click(object sender, EventArgs e)
        {
            dbMember m = new dbMember();
            if (!mmber.Equals(null))
            {
                m.DeleteMember(mmber.Id);
            }
        }
        int index = 0;
        private void buttonItem26_Click(object sender, EventArgs e)
        {
            List<int> d = new List<int>();
            foreach (var item in db.Members)
            {
                d.Add(item.Id);
            }
            if (index > 0)
            {
                index--;
                int b = d[index];
                mmber = db.Members.Where(x => x.Id == b).FirstOrDefault() ;
                fill();

            }else if(index == 0) {
                int b = d[index];
                mmber = db.Members.Where(x => x.Id == b).FirstOrDefault();
                fill();
            }
        }

        private void buttonItem27_Click(object sender, EventArgs e)
        {
            List<int> d = new List<int>();
            foreach (var item in db.Members)
            {
                d.Add(item.Id);
            }
            if (index < d.Count-1)
            {
                index++;
                int b = d[index];
                mmber = db.Members.Where(x => x.Id == b ).FirstOrDefault();
                fill();

            }
        }
        void fill()
        {
            try
            {
                labelX48.Text = mmber.Id.ToString();
                memberid = mmber.Id;
                textBox1.Text = mmber.FirstName;
                textBox2.Text = mmber.LastName;
                textBox3.Text = mmber.CIN;
                textBox4.Text = mmber.Adress;
                textBox5.Text = mmber.Phone;
                textBox6.Text = mmber.Email;
                textBoxX1.Text = mmber.Description;
                radioButton1.Checked = (mmber.Gender.ToUpper() == "MALE") ? true : false ;
                radioButton2.Checked = (mmber.Gender.ToUpper() == "FEMALE") ? true : false;
                dateTimeInput1.Value = mmber.Birthday.Value;
                comboBoxEx2.SelectedValue = mmber.GroupID;
                loadmember(mmber.Id);
                Membershipdb ms = new Membershipdb();
                dataGridViewX2.DataSource = ms.search(mmber.Id);
                if (mmber.Image is null) {

                    pictureBox1.Image = Properties.Resources.defMember;
                }
                else
                using (MemoryStream memstr = new MemoryStream(mmber.Image))
                {
                    Image img = Image.FromStream(memstr);
                    pictureBox1.Image = img;
                }
            }
            catch (Exception ex) { MessageBox.Show(ex.Message); }
        }
        int memberid;
        private void buttonX1_Click(object sender, EventArgs e)
        {
            if(db.Members.Where(x=>x.Id == memberid).Count() > 0) 
            {
             membershipform f = new membershipform(memberid);
                f.ShowDialog();
            }
        }
        bool edit = true;
        private void buttonItem32_Click(object sender, EventArgs e)
        {
            if (edit) {
            textBoxX3.ReadOnly = false;
            guna2NumericUpDown1.Enabled = true;
                guna2NumericUpDown2.Enabled = true;
                textBoxX5.ReadOnly = false;
                edit = false;
            }
            else
            {
                textBoxX3.ReadOnly = true;
                guna2NumericUpDown1.Enabled = false;
                guna2NumericUpDown2.Enabled = false;
                textBoxX5.ReadOnly = true;
                edit = true;
            }
           
        }

        private void buttonItem29_Click(object sender, EventArgs e)
        {
            MembershiptypeAdd d = new MembershiptypeAdd();
            d.ShowDialog();
        }

        private void dataGridViewX4_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
           textBoxX2.Text = dataGridViewX4.CurrentRow.Cells[0].Value.ToString();
            guna2NumericUpDown1.Value = (decimal)dataGridViewX4.CurrentRow.Cells[2].Value;
            guna2NumericUpDown2.Value = (decimal)dataGridViewX4.CurrentRow.Cells[4].Value;
            textBoxX3.Text = dataGridViewX4.CurrentRow.Cells[1].Value.ToString();
            textBoxX5.Text = dataGridViewX4.CurrentRow.Cells[3].Value.ToString();
        }

        private void buttonItem34_Click(object sender, EventArgs e)
        {
            
            if (!string.IsNullOrEmpty(textBoxX2.Text)) {
                typemembershipdb.edit(int.Parse(textBoxX2.Text), textBoxX3.Text, guna2NumericUpDown1.Value, guna2NumericUpDown2.Value, textBoxX5.Text);
                /*int a = int.Parse(textBoxX2.Text);
                var d = db.MembershipTypes.Where(x => x.Id == a).FirstOrDefault();
                d.Name = textBoxX3.Text;
                d.Price = guna2NumericUpDown1.Value;
                d.TrainerBonus = guna2NumericUpDown2.Value;
                d.Description = textBoxX5.Text;
                db.SaveChanges();*/
            }
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {
            Membershipdb mm = new Membershipdb();
            if(dataGridViewX2.CurrentRow.Selected)
            mm.cancel((int)dataGridViewX2.CurrentRow.Cells[0].Value);
            
        }

        private void buttonItem33_Click(object sender, EventArgs e)
        {

            typemembershipdb.delete(int.Parse(textBoxX2.Text));
        }

        private void ribbonTabItem4_Click(object sender, EventArgs e)
        {
            panelEx6.BringToFront();
        }

        private void buttonItem35_Click(object sender, EventArgs e)
        {
            if (db.Members.Where(X => X.CIN == textBoxItem5.Text).Count() > 0)
            {
                var mseach = db.Members.Where(X => X.CIN == textBoxItem5.Text).FirstOrDefault();
                textBox9.Text = mseach.CIN;
                textBox7.Text = mseach.FirstName;
                textBox8.Text = mseach.LastName;

                if (mseach.Image is null)
                {

                    pictureBox2.Image = Properties.Resources.defMember;
                }
                else
                using (MemoryStream memstr = new MemoryStream(mseach.Image))
                {
                    Image img = Image.FromStream(memstr);
                    pictureBox2.Image = img;
                }
                
                dataGridViewX5.DataSource = mseach.HistoMembers.ToArray();
            }
        }

        private void buttonX5_Click(object sender, EventArgs e)
        {
            if (db.Members.Where(X => X.CIN == textBoxItem5.Text).Count() > 0)
            {
                var mseach = db.Members.Where(X => X.CIN == textBoxItem5.Text).FirstOrDefault();
                HistoMember h = new HistoMember();
                h.MemberId = mseach.Id;
                h.Enterdate = DateTime.Now;
                h.Leavedate = DateTime.Now.AddHours(3);
                db.HistoMembers.Add(h);
                db.SaveChanges();

            }
        }

        private void btnViewTrainers_Click(object sender, EventArgs e)
        {
            advTree1.Nodes.Clear();
            foreach (var T in db.Trainers)
            {
                DevComponents.AdvTree.Node n = new DevComponents.AdvTree.Node(T.FirstName+" "+ T.LastName);
                n.Tag = T.Id;
                advTree1.Nodes.Add(n);
            }
            
        }

        private void ribbonBar12_ItemClick(object sender, EventArgs e)
        {

        }

        private void advTree1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            int trainer =(int)advTree1.SelectedNode.Tag;
           
            var x = db.Memberships.Where(c => c.TrainerId == trainer).Distinct();
            var join = x.Join(db.Members, ms => ms.MemberId, d => d.Id, (ms, d) => new { ms.MembershipType1.Name, FullName = d.FirstName + " " + d.LastName,Date= ms.StartDate, ms.MembershipType1.TrainerBonus });
            dataGridViewX6.DataSource = join.ToArray();
            textBoxX6.Text = db.Memberships.Where(O =>  O.TrainerId == (int)advTree1.SelectedNode.Tag).Sum(P=>P.MembershipType1.TrainerBonus).ToString();
            textBoxX7.Text = db.Memberships.Where(O => O.TrainerId == (int)advTree1.SelectedNode.Tag && O.EndDate.Value.Month == DateTime.Now.Month).Sum(P => P.MembershipType1.TrainerBonus).ToString();
            textBoxX4.Text = db.Memberships.Where(O => O.TrainerId == (int)advTree1.SelectedNode.Tag).Select(h => new { h.MemberId }).Distinct().Count().ToString();
         }

        private void buttonItem1_Click(object sender, EventArgs e)
        {
            AddTrainer ad = new AddTrainer();
            ad.ShowDialog();
        }

        private void buttonItem25_Click(object sender, EventArgs e)
        {
            
            db.Trainers.Remove(db.Trainers.Where(x => x.Id == (int)advTree1.SelectedNode.Tag).FirstOrDefault());
            db.SaveChanges();
        }

        private void buttonItem21_Click(object sender, EventArgs e)
        {
            if(  advTree1.SelectedNode.Tag is null)
            {
                advTree1.SelectedNode.Tag = 0;
            }
            int idt = (int)advTree1.SelectedNode.Tag;
            if (true) { 
            AddTrainer ad = new AddTrainer(db.Trainers.Where(x=>x.Id==idt).FirstOrDefault());
            ad.ShowDialog();
            }
        }

        private void buttonItem15_Click(object sender, EventArgs e)
        {
            textBoxItem1.Text = "";
            textBoxItem2.Text = "";
            dataGridViewX1.DataSource = db.Members.ToArray();
        }

        private void buttonItem28_Click(object sender, EventArgs e)
        {
            dataGridViewX2.DataSource = mmber.Memberships.ToArray();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            db.Dispose();
            
        }

        private void Btnitem1_Click(object sender, EventArgs e)
        {
            this.Form1_Load(sender,e);
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked) {
                DateTime dt = dateTimeInput2.Value;

                labelX28.Text = db.Members.Count().ToString();
                labelX26.Text = db.Members.Where(x => x.Gender.ToUpper() == "FEMALE").Count().ToString();
                labelX27.Text = db.Members.Where(x => x.Gender.ToUpper() == "MALE").Count().ToString();
                labelX25.Text = db.Members.Where(x => x.Memberships.Any(f => f.StartDate.Month == dt.Month)).Count().ToString();
                ////membership
                ///
                if (!(comboBoxEx3.SelectedValue is null))
                {
                    int msi = (int)comboBoxEx3.SelectedValue;
                    if (msi > 0)
                    {
                        labelX31.Text = db.Memberships.Where(x => x.MembershipType == msi && x.StartDate.Month == dt.Month).Count().ToString();
                        labelX30.Text = db.Members.Where(x => x.Memberships.Any(f => f.MembershipType == msi && f.StartDate.Month == dt.Month) && x.Gender.ToUpper() == "MALE" ).Count().ToString();
                        labelX29.Text = db.Members.Where(x => x.Memberships.Any(f => f.MembershipType == msi && f.StartDate.Month == dt.Month) && x.Gender.ToUpper() == "FEMALE").Count().ToString();
                        var pl = db.Memberships.Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, x.MembershipType, f.Price,x.StartDate });
                        labelX32.Text = pl.Where(x => x.MembershipType == msi && x.StartDate.Month == dt.Month).Sum(x => x.Price).ToString();
                    }
                }
                /////
                labelX35.Text = db.Memberships.Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price,x.StartDate }).Where(x=>x.StartDate.Month == dt.Month).Sum(x => x.Price).ToString();
                labelX34.Text = db.Memberships.Where(d => d.Member.Gender.ToUpper() == "MALE").Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price, x.StartDate }).Where(x => x.StartDate.Month == dt.Month).Sum(x => x.Price).ToString();
                labelX33.Text = db.Memberships.Where(d => d.Member.Gender.ToUpper() == "FEMALE").Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price, x.StartDate }).Where(x=> x.StartDate.Month == dt.Month).Sum(x => x.Price).ToString();
            }
            else
            {
                labelX28.Text = db.Members.Count().ToString();
                labelX26.Text = db.Members.Where(x => x.Gender.ToUpper() == "FEMALE").Count().ToString();
                labelX27.Text = db.Members.Where(x => x.Gender.ToUpper() == "MALE").Count().ToString();
                labelX25.Text = db.Members.Where(x => x.Memberships.Any(f => f.EndDate > DateTime.Now)).Count().ToString();
                ////membership
                ///
                if (!(comboBoxEx3.SelectedValue is null))
                {
                    int msi = (int)comboBoxEx3.SelectedValue;
                    if (msi > 0)
                    {
                        labelX31.Text = db.Memberships.Where(x => x.MembershipType == msi).Count().ToString();
                        labelX30.Text = db.Members.Where(x => x.Memberships.Any(f => f.MembershipType == msi) && x.Gender.ToUpper() == "MALE").Count().ToString();
                        labelX29.Text = db.Members.Where(x => x.Memberships.Any(f => f.MembershipType == msi) && x.Gender.ToUpper() == "FEMALE").Count().ToString();
                        var pl = db.Memberships.Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, x.MembershipType, f.Price });
                        labelX32.Text = pl.Where(x => x.MembershipType == msi).Sum(x => x.Price).ToString();
                    }
                }
                /////
                labelX35.Text = db.Memberships.Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price }).Sum(x => x.Price).ToString();
                labelX34.Text = db.Memberships.Where(d => d.Member.Gender.ToUpper() == "MALE").Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price }).Sum(x => x.Price).ToString();
                labelX33.Text = db.Memberships.Where(d => d.Member.Gender.ToUpper() == "FEMALE").Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { x.Id, f.Price }).Sum(x => x.Price).ToString();
            }
            dataGridViewX3.DataSource = db.Memberships.Join(db.MembershipTypes, x => x.MembershipType, f => f.Id, (x, f) => new { f.Name, f.Price }).GroupBy(x => x.Name).Select(grop => new {PlanName = grop.Key, total =grop.Sum(x => x.Price) }).ToArray();
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                dateTimeInput2.Enabled = true;
            }else dateTimeInput2.Enabled = false;
        }

        private void buttonItem38_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable("Report");
            string[] op = { "Properties","Values" };

            foreach (var item in op)
            {
                dt.Columns.Add(item);
            }
            
                
            dt.Rows.Add(labelX13.Text, labelX27.Text);
            dt.Rows.Add(labelX14.Text, labelX26.Text);
            dt.Rows.Add(labelX15.Text, labelX25.Text);
            dt.Rows.Add(labelX16.Text, labelX28.Text);
            dt.Rows.Add(labelX17.Text, labelX31.Text);
            dt.Rows.Add(labelX18.Text, labelX30.Text);
            dt.Rows.Add(labelX19.Text, labelX29.Text);
            dt.Rows.Add(labelX38.Text, labelX32.Text);
            dt.Rows.Add(labelX23.Text, labelX35.Text);
            dt.Rows.Add(labelX21.Text, labelX34.Text);
            dt.Rows.Add(labelX22.Text, labelX33.Text);
            saveFileDialog1.Filter = "Excel|*.xlsx";
            if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            {
                using (XLWorkbook wk = new XLWorkbook())
                {
                    wk.Worksheets.Add(dt);
                    wk.SaveAs(saveFileDialog1.FileName);
                }

            }

        }

        private void buttonItem36_Click(object sender, EventArgs e)
        {
            pictureBox2.Image = null;
            textBox9.Text = "";
            textBox7.Text = "";
            textBox8.Text = "";

        }

        private void buttonItem39_Click(object sender, EventArgs e)
        {
            Groupview f = new Groupview();
            f.ShowDialog();
        }

        private void ribbonControl1_Click(object sender, EventArgs e)
        {

        }
    }
}
