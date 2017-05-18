using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class Rent_DAO
    {
        //Master and Detail INSERT
        public static int SaveDAO(BookRent bookObj, DataTable dt)
        {
            BookPOSEntities3 db = new BookPOSEntities3();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {
                    var entity = db.Set<BookRent>().Create();

                    //mapping the values of your view models to data models
                    entity.MemberId = bookObj.MemberId;
                    entity.StartDate = bookObj.StartDate;
                    entity.IssueDate = bookObj.IssueDate;
                    entity.NumberOfDay = bookObj.NumberOfDay;
                  
                    //INSERT MASTER TABLE
                    db.BookRents.Add(entity);
                    int l_return = db.SaveChanges();
                    long RentID = entity.RentID; //GET MASTER ID

                    if (l_return > 0)
                    {
                        //INSERT DETAIL TABLE
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            var RentDetail = db.Set<RentBookDetail>().Create();
                            RentDetail.RentID = RentID;
                            RentDetail.BookID = Convert.ToInt64(dt.Rows[i]["BookID"].ToString());
                            RentDetail.Status = 1;
                            db.RentBookDetails.Add(RentDetail);
                            l_return = db.SaveChanges();
                            RentDetail = null;

                        }
                    }
                    transaction.Commit();
                    transaction.Dispose();
                    db.Dispose();

                    return l_return;
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    throw ex;
                }
            }
        }

        //Search For Rent Book History
        public static List<SearchRentBookHistory_Result> getSearchRentBookHistory(string BookName, long memberID, int CategoryID,string fDate , string tDate)
        {
            List<SearchRentBookHistory_Result> resultList = new List<SearchRentBookHistory_Result>();
            BookPOSEntities3 db = new BookPOSEntities3();

            try
            {
                var memID = new SqlParameter("@MemberID", memberID);
                var bookName = new SqlParameter("@BookName", BookName);
                var categoryId = new SqlParameter("@Category", CategoryID);
                var startDate = new SqlParameter("@StartDate", fDate);
                var endDate = new SqlParameter("@EndDate", tDate);

                resultList = db.Database
                .SqlQuery<SearchRentBookHistory_Result>("SearchRentBookHistory @MemberID,@BookName,@Category,@StartDate,@EndDate",
                            memID,bookName, categoryId,startDate,endDate).ToList();

                db.Dispose();
            }
            catch (Exception ex)
            {
                throw ex;
            }


            return resultList;
        }


        //Update Restore Book from Rent
        public static int RestoreRentBook(int Id)
        {
            int l_return = 0; // For Return Value for Save or Fail
            BookPOSEntities3 db = new BookPOSEntities3();
            using (var transaction = db.Database.BeginTransaction())
            {
                try
                {

                    var result = db.RentBookDetails.SingleOrDefault(b => b.RentDetailID == Id);
                    if (result != null)
                    {
                        result.Status = 0;
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
