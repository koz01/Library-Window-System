using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess
{
  public  class Category_DAO
    {

        //get Category
        public static List<Category> getCategory()
        {
            List<Category> categoryList = null;
            try
            {
                BookPOSEntities3 db = new BookPOSEntities3();
                categoryList = db.Categories.ToList();
                db.Dispose();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return categoryList;
        }

        public static int getCategoryID(string catName)
        {
            int category_Id = 0;
            try
            {
                BookPOSEntities3 db = new BookPOSEntities3();

                category_Id = Convert.ToInt32((from cat in db.Categories
                                                   where cat.CategoryName.Equals(catName)
                                                   select cat.CategoryId).SingleOrDefault());

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return category_Id;
        }
    }
}
