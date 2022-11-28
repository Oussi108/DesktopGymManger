using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class Membershipdb
    {


        GymdatabaseEntities1 db = new GymdatabaseEntities1();

        public void add(int idmember, int idtrainer,int idtypemembership,DateTime start)
        {
            Membership m = new Membership()
            { MemberId = idmember,
                TrainerId = idmember,
                MembershipType = idtypemembership,
                StartDate = start,
                EndDate = start.AddDays(30)

            };
            db.Memberships.Add(m);
            db.SaveChanges();
        }
        public List<Membership> search(int idmember)
        {

            return db.Memberships.Where(x => x.MemberId == idmember).ToList();
        }
        public void cancel(int id)
        {
            var d = db.Memberships.Where(x => x.Id == id).FirstOrDefault();
            db.Memberships.Remove(d);
            db.SaveChanges();
        }
    }
}
