using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CategoryEventsArgs : EventArgs
    {

        private Category _category;
        private string _column;

        public CategoryEventsArgs(Category category, string column)
        {
            _column = column;
            _category = category;
             
        }

        public Category Category
        {
            get
            {
                return _category;
            }
        }

        public string Column
        {
            get
            {
                return _column;
            }
        }
    }
}
