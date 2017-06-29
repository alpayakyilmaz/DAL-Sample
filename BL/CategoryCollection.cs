using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class CategoryCollection : CollectionBase
    {
        internal CategoryCollection()
        {

        }

        public int Add(Category category) 
        {
            return this.List.Add(category);
        }

        public Category Add(string categoryName, string description)
        {
            Category myCategory = new Category(categoryName, description);
            this.Add(myCategory);                        
            return myCategory;

        }

        public void Remove(Category category)
        {
            this.List.Remove(category);
        }

        public Category this[int index]
        {
            get
            {
                return (Category)this.List[index];
            }
            set 
            {
                this.List[index] = value;
            }
        }
    }
}
