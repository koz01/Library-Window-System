using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace DataAccess
{
    public class Member_DAO
    {

        //Search Member Name By Code
        public static Member getMemberByCode(string memberCode)
        {
            Member memberObj = new Member();
            BookPOSEntities3 db = new BookPOSEntities3();
            try
            {

                var memQuery = from mem in db.Members
                               where mem.MemberCode == memberCode
                               select mem;
                if (memQuery.SingleOrDefault() != null) //must be use first or default (can error when return multiple value)
                {
                    memberObj = memQuery.Single();
                }
                else
                {
                    memberObj = null;
                }
                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return memberObj;
        }

        //get Member Rows Count
        public static int getMemberCount()
        {
            BookPOSEntities3 db = new BookPOSEntities3();
            int l_count = db.Members.Count();
            db.Dispose();
            return l_count;
        }


        //Insert New Member
        public static int SaveNewMember(DataTable dt)
        {
            BookPOSEntities3 db = new BookPOSEntities3();
            int l_return = 0;
            using (var transaction = db.Database.BeginTransaction())
            {

                try
                {
                    //INSERT DETAIL TABLE
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        var Obj = db.Set<Member>().Create();

                        Obj.MemberCode = dt.Rows[i]["MemberCode"].ToString();
                        Obj.MemberName = dt.Rows[i]["MemberName"].ToString();
                        Obj.email = dt.Rows[i]["Email"].ToString();
                        Obj.City = dt.Rows[i]["City"].ToString();
                        Obj.Phone = dt.Rows[i]["Phone"].ToString();
                        Obj.Active = 1;
                        Obj.Address = dt.Rows[i]["Address"].ToString();

                        db.Members.Add(Obj);
                        l_return = db.SaveChanges();
                    }

                    transaction.Commit();
                    transaction.Dispose();
                    db.Dispose();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
            return l_return;
        }

    }
}
