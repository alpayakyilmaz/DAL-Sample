using BL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DALSample
{
    public partial class UpdateCategory : Form
    {
        private Category _category;
        public UpdateCategory(Category category )
        {
            InitializeComponent();
            _category = category;
            _category.CategoryChanged += new Category.CategoryEventhandler(_category_CategoryChanged); 

        }

        void _category_CategoryChanged(object sender, CategoryEventsArgs e)
        {
            if (e.Column == "CategoryName")
            {
                MessageBox.Show("Kategori Adı:" + e.Category.CategoryName + "olarak değitirildi");
            }
        }

        private void txtAciklama_TextChanged(object sender, EventArgs e)
        {

        }

        private void UpdateCategory_Load(object sender, EventArgs e)
        {
            GetEntity();
        }

        private void GetEntity()
        {
            txtKategoriAdi.Text = _category.CategoryName;
            txtAciklama.Text = _category.Description;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            this._category.CategoryName = txtKategoriAdi.Text;
            this._category.Description = txtAciklama.Text;
            try
            {
                CategoryProvider.UpdateCategory(_category);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            try
            {
                CategoryProvider.DeleteCategory(_category);
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message);
            }
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close(); 
        }
    }
}
