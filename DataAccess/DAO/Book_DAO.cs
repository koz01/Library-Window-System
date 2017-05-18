using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace DataAccess
{
   public class Book_DAO
    {
       
       //Search For All Book by Filter
       public static List<SearchBook_Result> getSearchBook(string BookName,string Author, int CategoryID)
       {
           List<SearchBook_Result> resultList = new List<SearchBook_Result>();
           BookPOSEntities3 db = new BookPOSEntities3();

           try
           {
               var bookName = new SqlParameter("@BookName", BookName);
               var author = new SqlParameter("@Author", Author);
               var categoryId = new SqlParameter("@Category", CategoryID);

                resultList = db.Database
                .SqlQuery<SearchBook_Result>("SearchBook @BookName,@Author,@Category", bookName,author, categoryId)
                .ToList();
                db.Dispose();
           }catch(Exception ex)
           {
               throw ex;
           }


           return resultList;
       }

       //get Book Rows Count
       public static int getBookCount()
       {
           BookPOSEntities3 db = new BookPOSEntities3();
           int l_count = db.Books.Count();
           db.Dispose();
           return l_count;
       }

       //Insert New Book
       public static int SaveNewBook(DataTable dt)
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
                       var bookObj = db.Set<Book>().Create();

                       bookObj.BookCode = dt.Rows[i]["BookCode"].ToString();
                       bookObj.BookName = dt.Rows[i]["BookName"].ToString();
                       bookObj.CategoryId = Convert.ToInt64(dt.Rows[i]["Category"]);
                       bookObj.Author = dt.Rows[i]["Author"].ToString();
                       bookObj.ISBN = dt.Rows[i]["ISBN"].ToString();

                       db.Books.Add(bookObj);
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
