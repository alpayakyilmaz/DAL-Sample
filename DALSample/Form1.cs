using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using BL;

namespace DALSample
{
    public partial class Form1 : Form
    {

        CategoryCollection _categories;
        public Form1()
        {
            InitializeComponent();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            this.GetAllCategories();
        }

        private void GetAllCategories()
        {
            try
            {
                this._categories = CategoryProvider.GetAllCategories();
                bindingSource1.DataSource = this._categories;
                bindingNavigator1.BindingSource = bindingSource1;
                dataGridView1.DataSource = bindingSource1;
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void bindingNavigatorDeleteItem_Click(object sender, EventArgs e)
        {
            int categoryId = this._categories[dataGridView1.CurrentCell.RowIndex].CategoryId;
            CategoryProvider.DeleteCategory(categoryId);
            GetAllCategories();
        }

        private void tsbCategoryName_Click(object sender, EventArgs e)
        {
           // MessageBox.Show(this._categories[dataGridView1.CurrentCell.RowIndex].CategoryId.ToString());
        }

        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            UpdateCategory updateCategory = new UpdateCategory(this._categories[e.RowIndex]);
            updateCategory.ShowDialog();
            GetAllCategories();            
        }
    }
}
