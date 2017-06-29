using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Category
    {
        public delegate void CategoryEventhandler(object sender, CategoryEventsArgs e);

        public event CategoryEventhandler CategoryChanged;

        private int _categoryId;

        public int CategoryId
        {
            get { return _categoryId; }
            set { _categoryId = value; }
        }
        private string _categoryName;

        public string CategoryName
        {
            get
            { 
                return _categoryName;
            }
            set 
            {
                if (_categoryName == value)
                {
                    return;
                }
                _categoryName = value;

                if (CategoryChanged != null) 
                {
                    CategoryChanged(this, new CategoryEventsArgs(this, "CategoryName"));
                }
            }
        }
        private string _description;

        public string Description
        {
            get 
            {

                return _description; 
            }
            set 
            {
                if (_description == value)
                {
                    return;
                }
                _description = value;

                if (CategoryChanged != null)
                {
                    CategoryChanged(this, new CategoryEventsArgs(this, "Description"));
                }

            }
        }

        internal Category(string CategoryName, string Description) 
        {
            this.CategoryName = CategoryName;
            this.Description = Description;
        }

        internal Category() 
        { }

    }
}
