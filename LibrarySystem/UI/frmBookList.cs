using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LibrarySystem
{
    public partial class frmBookList : Form
    {
        public DataTable BookDT = null; //For Selected Grid View Row Data

        public frmBookList()
        {
            InitializeComponent();
        }

        private void frmBookList_Load(object sender, EventArgs e)
        {
            PopulateCategory();
            PopulateSearchBook();
            
        }

        //Populate Search BookList
        private void PopulateSearchBook()
        {
            string bookName = txtBookName.Text.Trim();//For Book Name
            string author = txtAuthor.Text.Trim();//For Author Name
            int categoryID = Convert.ToInt32(cboCategory.SelectedValue); //For Category Selected Value ID

            if (cboCategory.Text.Trim() == "")
            {
                categoryID = 0;
            }

            try
            {
                dgvBookList.DataSource = DataAccess.Book_DAO.getSearchBook(bookName, author, categoryID);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PopulateSearchBook();
        }



        // Populate Category Data For Combo Box
        private void PopulateCategory()
        {
            try
            {
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryID";
                cboCategory.DataSource = DataAccess.Category_DAO.getCategory();
                cboCategory.Text = "";
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //For Form Close
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //F4 Key for Search Book Function
        private void frmSearchBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                PopulateSearchBook();
            }else if(e.KeyCode == Keys.F3)
            {
                btnSelect_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }
        }

        private void btnSelect_Click(object sender, EventArgs e)
        {
            try
            {
                BookDT = new DataTable("Book");
                BookDT.Columns.Add("BookID");
                BookDT.Columns.Add("BookName");
                BookDT.Columns.Add("BookCode");
                BookDT.Columns.Add("Author");
                BookDT.Columns.Add("Category");
                BookDT.Columns.Add("ISBN");

                foreach (DataGridViewRow row in dgvBookList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[chkSelect.Name].Value) == true)
                    {
                        DataRow dr = BookDT.NewRow();

                        dr["BookID"] = row.Cells[1].Value.ToString();
                        dr["BookName"] = row.Cells[2].Value.ToString();
                        dr["BookCode"] = row.Cells[3].Value.ToString();
                        dr["Author"] = row.Cells[4].Value.ToString();
                        dr["Category"] = row.Cells[5].Value.ToString();
                        dr["ISBN"] = row.Cells[6].Value.ToString();
                        BookDT.Rows.Add(dr);
                        dr = null;
                    }
                }

                if (BookDT.Rows.Count > 0)
                {
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Please Select Book", "No data fount");
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message,"Error",MessageBoxButtons.OK);
            }
        }
    }
}
