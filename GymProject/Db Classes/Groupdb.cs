using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Groupdb
    {
        GymdatabaseEntities1 db = new GymdatabaseEntities1();

        void add(int idmember,string namegro)
        {
            Group g = new Group() {  NameGroupe = namegro };
            db.Groups.Add(g);
            db.SaveChanges();
        }
        void edit(int id , int idmember, string namegro)
        {
            var d = db.Groups.Where(x => x.Id == id).FirstOrDefault();
            
            d.NameGroupe = namegro;
            db.SaveChanges();

        }
        void delete(int id)
        {
            var d = db.Groups.Where(x => x.Id == id).FirstOrDefault();
            db.Groups.Remove(d);
            db.SaveChanges();
        }
    }
}
