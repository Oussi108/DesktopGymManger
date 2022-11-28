using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Sessiondb
    {
        GymdatabaseEntities1 db = new GymdatabaseEntities1();

        void add(int idgroup, DateTime day)
        {
            Session s = new Session()
            {
                GroupID = idgroup,
                Day = day,
               
            };
            db.Sessions.Add(s);
            db.SaveChanges();
        }
        void edit(int id, int idgroup, DateTime day)
        {
            var d = db.Sessions.Where(x => x.Id == id).FirstOrDefault();
            d.GroupID = idgroup;
            d.Day = day;
            db.SaveChanges();

        }
        void delete(int id)
        {
            var d = db.Sessions.Where(x => x.Id == id).FirstOrDefault();
            db.Sessions.Remove(d);
            db.SaveChanges();
        }
    }
}
