using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymProject
{
    class typemembershipdb
    {


        static GymdatabaseEntities1 db = new GymdatabaseEntities1();

        public static void add(string name, decimal price, decimal TrainerBonus, string desc)
        {
            MembershipType mt = new MembershipType()
            {
                Name = name,
                Price = price,
                TrainerBonus= TrainerBonus,
                Description = desc
            };
            db.MembershipTypes.Add(mt);
            db.SaveChanges();
        }
        public static void edit(int id, string name, decimal price, decimal TrainerBonus, string desc)
        {
            if (db.MembershipTypes.Where(x=>x.Id==id).Count()>0) { 
            var d = db.MembershipTypes.Where(x => x.Id == id).FirstOrDefault();
            d.Name = name;
            d.Price = price;
            d.TrainerBonus = TrainerBonus;
            d.Description = desc;
            db.SaveChanges();
            }

        }
        public static void delete(int id)
        {
            if (db.MembershipTypes.Where(x => x.Id == id).Count() > 0)
            {
                var d = db.MembershipTypes.Where(x => x.Id == id).FirstOrDefault();
                db.MembershipTypes.Remove(d);
                db.SaveChanges();
            }
        }
    }
}
