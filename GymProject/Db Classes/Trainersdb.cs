using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject.Db_Classes
{
    public class Trainersdb
    {
        GymdatabaseEntities1 db = new GymdatabaseEntities1();

        public void Add(string firstname, string lastname, DateTime birth, string gender, string phone, string email, string desc)
        {

            Trainer nb = new Trainer();
            nb.FirstName = firstname;
            nb.LastName = lastname;

            nb.Birthday = birth;
            nb.Gender = gender;

            nb.Email = email;
            nb.Phone = phone;

            nb.Description = desc;

            db.Trainers.Add(nb);
            db.SaveChanges();


        }
        public void editeMember(int id, string firstname, string lastname, DateTime birth, string gender, string phone, string email, string desc)
        {


            var nb = db.Trainers.Where(x => x.Id == id).FirstOrDefault();
                nb.FirstName = firstname;
                nb.LastName = lastname;
                nb.Birthday = birth;
                nb.Gender = gender;
                nb.Email = email;
                nb.Phone = phone;
                nb.Description = desc;


            
            db.SaveChanges();
        }
        public void DeleteTrainer(int id)
        {
            var req = from a in db.Trainers
                      where a.Id == id
                      select a;
            foreach (var nb in req)
            {
                db.Trainers.Remove(nb);

            }

        }
    }
}
