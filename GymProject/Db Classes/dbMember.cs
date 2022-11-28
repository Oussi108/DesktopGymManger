using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.IO;
namespace GymProject
{
    class dbMember
    {
        GymdatabaseEntities1 db = new GymdatabaseEntities1();
        
        public void AddMember(string firstname,string lastname,string cin,DateTime birth,string gender,string adress,string phone,string email,string imgpath,string desc,int grpid )
        {
            try {
                Member nb = new Member();
                if (imgpath == "")
                {
                    using(var d = new MemoryStream())
                    {
                        Properties.Resources.defMember.Save(d, Properties.Resources.defMember.RawFormat);
                        nb.Image = d.ToArray();
                    }
                    
                }
                else {
                Image a = Image.FromFile(imgpath);
                 byte[] b = (byte[])(new ImageConverter()).ConvertTo(a, typeof(byte[]));
                    nb.Image = b;
                }
            nb.FirstName = firstname;
            nb.LastName = lastname;
            nb.CIN = cin;
            nb.Birthday = birth; 
            nb.Gender = gender;
            nb.Adress = adress;
            nb.Email = email;
            nb.Phone = phone;
            
            nb.Description = desc;
            nb.GroupID = grpid;
            db.Members.Add(nb);
            db.SaveChanges();
            }catch(Exception e) { System.Windows.Forms.MessageBox.Show(e.Message); }

        }
        public void EditeMember (int id,string firstname, string lastname, string cin, DateTime birth, string gender, string adress, string phone, string email, string imgpath, string desc, int grpid)
        {
            var req = from a in db.Members
                      where a.Id == id
                      select a;
            foreach (var nb in req)
            {
                Image a = Image.FromFile(imgpath);
                byte[] b = (byte[])(new ImageConverter()).ConvertTo(a, typeof(byte[]));
                
                nb.FirstName = firstname;
                nb.LastName = lastname;
                nb.CIN = cin;
                nb.Birthday = birth;
                nb.Gender = gender;
                nb.Adress = adress;
                nb.Email = email;
                nb.Phone = phone;
                nb.Image = b;
                nb.Description = desc;
                nb.GroupID = grpid;
                
            }
            db.SaveChanges();
        }
        public void EditeMember(Member mm)
        {
            var nb = db.Members.Where(x => x.Id == mm.Id).FirstOrDefault();
            
                nb.FirstName = mm.FirstName;
                nb.LastName = mm.LastName;
                nb.CIN = mm.CIN;
                nb.Birthday = mm.Birthday;
                nb.Gender = mm.Gender;
                nb.Adress = mm.Adress;
                nb.Email = mm.Email;
                nb.Phone = mm.Phone;
                nb.Image = mm.Image;
                nb.Description = mm.Description;
                nb.GroupID = mm.GroupID;
                
            
            db.SaveChanges();

        }
        public void EditeMember(int id, string firstname, string lastname, string cin, DateTime birth, string gender, string adress, string phone, string email, Image img, string desc, int grpid)
        {
            var req = from a in db.Members
                      where a.Id == id
                      select a;
            byte[] im;
            using (var ms = new  MemoryStream())
            {
                img.Save(ms, img.RawFormat);
                im = ms.ToArray();
            }
            foreach (var nb in req)
            {
                

                nb.FirstName = firstname;
                nb.LastName = lastname;
                nb.CIN = cin;
                nb.Birthday = birth;
                nb.Gender = gender;
                nb.Adress = adress;
                nb.Email = email;
                nb.Phone = phone;
                nb.Image = im;
                nb.Description = desc;
                nb.GroupID = grpid;

            }
            db.SaveChanges();
        }
        public void DeleteMember(int id)
        {
            var req = from a in db.Members
                      where a.Id == id
                      select a;
            foreach (var nb in req)
            {
                db.Members.Remove(nb);

            }
            db.SaveChanges();
        }
        List<Member> searchMember(int id)
        {
            var req = from a in db.Members
                      where a.Id == id
                      select a;
            return req.ToList();
        }
        
    }
}
