using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class histomemberdb
    {
        GymdatabaseEntities1 db = new GymdatabaseEntities1();
        HistoMember hs = new HistoMember();
        public void checkin(int idmember,DateTime checkin)
        {
            hs.MemberId = idmember;
            hs.Enterdate = checkin;
            
        }
        public void checkout(DateTime checkout)
        {
            hs.Leavedate = checkout;

        }
        public void save()
        {
            
            db.HistoMembers.Add(hs);
            db.SaveChanges();
        }
    }
}
