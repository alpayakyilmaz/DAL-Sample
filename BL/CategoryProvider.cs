using DAL;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CategoryProvider
    {

        public static Category GetCategoryByCategoryId(int categoryId)
        {
            Category newCategory = new Category();
            DatabaseProvider myDatabase = new DatabaseProvider();
            myDatabase.AddinParameters("@CategoryID", DbType.Int32, categoryId);
            IDataReader myReader = myDatabase.ExecuteReader("SELECT * FROM Categories WHERE CategoryID = @CategoryID", CommandType.Text); 
            while(myReader.Read()){
                newCategory.CategoryId = myReader["CategoryID"] is DBNull ? 0 : Convert.ToInt32(myReader["CategoryID"]);
                newCategory.CategoryName = myReader["CategoryName"] is DBNull ? string.Empty : myReader["CategoryName"].ToString();
                newCategory.Description = myReader["Description"] is DBNull ? string.Empty : myReader["Description"].ToString();
            }

            return newCategory;
        }

        public static CategoryCollection GetAllCategories()
        {
            CategoryCollection newCollection = new CategoryCollection();
            DatabaseProvider myDatabase = new DatabaseProvider();
            IDataReader myReader = myDatabase.ExecuteReader("GetAllCategories", CommandType.StoredProcedure);                                                             
            while (myReader.Read())
            {
                Category mycategory = new Category();
                mycategory.CategoryId = myReader["CategoryID"] is DBNull ? 0 : Convert.ToInt32(myReader["CategoryID"]);
                mycategory.CategoryName = myReader["CategoryName"] is DBNull ? string.Empty : myReader["CategoryName"].ToString();
                mycategory.Description = myReader["Description"] is DBNull ? string.Empty : myReader["Description"].ToString();
                newCollection.Add(mycategory);
            }

            return newCollection;
        }

        public static bool DeleteCategory(int categoryID)
        {
            bool returnValue;
            DatabaseProvider myDatabase = new DatabaseProvider();
            myDatabase.AddinParameters("@CategoryID",DbType.Int32,categoryID);
            returnValue = Convert.ToBoolean(myDatabase.ExecuteNonQuery("DELETE FROM Categories WHERE CategoryID = @CategoryID", CommandType.Text));
            return returnValue;
        }

        public static bool DeleteCategory(Category category)
        {
            return DeleteCategory(category.CategoryId);
        }

        public static bool UpdateCategory(int categoryID, string categoryName, string description)
        {

            bool returnValue;
            DatabaseProvider myDataBase = new DatabaseProvider();
            myDataBase.AddinParameters("@CategoryID", DbType.Int32,categoryID);
            myDataBase.AddinParameters("@CategoryName", DbType.String, categoryName);
            myDataBase.AddinParameters("@Description", DbType.String, description);
            returnValue = Convert.ToBoolean(myDataBase.ExecuteNonQuery("Update Categories SET CategoryName = @CategoryName, Description = @Description WHERE CategoryID = @CategoryID", CommandType.Text));
            return returnValue;
        }

        public static bool UpdateCategory(Category category)
        {
            return UpdateCategory(category.CategoryId, category.CategoryName, category.Description);
        }
    }
}
