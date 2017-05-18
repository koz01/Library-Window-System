using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataAccess;

namespace LibrarySystem
{
    public partial class frmAddNewBook : Form
    {
        DataTable BookDT = null;

        public frmAddNewBook()
        {
            InitializeComponent();
        }

        private void frmRent_Load(object sender, EventArgs e)
        {
            txtBookCode.ReadOnly = true;
            txtBookCode.BackColor = Color.White;
            GenerateBookCode();
            PopulateCategory();
        }

        //Add Text Box Data to Grid View
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtBookName.Text.Trim() =="")
                {
                    MessageBox.Show("Incomplete Book Name data.", "Warning");
                    return;
                }else if(txtAuthor.Text.Trim() == "")
                {
                    MessageBox.Show("Incomplete Author Name data.", "Warning");
                    return;
                }
                else if (cboCategory.Text.Trim() == "")
                {
                    MessageBox.Show("Please select Category", "Incomplete Category");
                    return;
                }
                else
                {
                    string code = txtBookCode.Text.Trim();
                    string name = txtBookName.Text.Trim();
                    string Author = txtAuthor.Text.Trim();
                    string ISBN = txtisbn.Text.Trim();
                    string category = cboCategory.Text;
                    string[] row = { "false",name,code,Author, category,ISBN};
                    dgvBookList.Rows.Add(row);
                    Clear();
                    GenerateBookCode();
                    
                }

                txtBookName.Focus();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvBookList.Rows)
            {
                if (Convert.ToBoolean(row.Cells[ChkSelect.Name].Value) == true)
                {
                    dgvBookList.Rows.Remove(row);
                }
            }
            GenerateBookCode();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvBookList.Rows.Count > 0)
                {

                    BookDT = new DataTable();
                    BookDT.Columns.Add("BookCode");
                    BookDT.Columns.Add("BookName");
                    BookDT.Columns.Add("Author");
                    BookDT.Columns.Add("ISBN");
                    BookDT.Columns.Add("Category");

                    foreach (DataGridViewRow row in dgvBookList.Rows)
                    {
                        DataRow dr = BookDT.NewRow();
                        dr["BookCode"] = row.Cells[1].Value.ToString();
                        dr["BookName"] = row.Cells[2].Value.ToString();
                        dr["Author"] = row.Cells[3].Value.ToString();
                        dr["ISBN"] = row.Cells[4].Value.ToString();

                        string category = row.Cells[5].Value.ToString();
                        dr["Category"] = Category_DAO.getCategoryID(category);
                       
                        BookDT.Rows.Add(dr);
                        dr = null;
                    }

                    if(BookDT.Rows.Count > 0)
                    {
                        if (Book_DAO.SaveNewBook(BookDT) > 0)
                        {
                            MessageBox.Show("Save Successfully.", "Save");
                            dgvBookList.DataSource = null;
                        }
                    }
                }
                else
                {
                    MessageBox.Show("No Data found to Save.");
                }
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAddNewBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F2)
            {
                btnAddBook_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDelete_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                btnSave_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }
        }

        private void txtBookCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtBookName.Focus();
            }
        }

        private void txtBookName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.Enter)
            {
                txtAuthor.Focus();
            }
        }

        private void txtAuthor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtisbn.Focus();
            }
        }

        private void txtisbn_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                cboCategory.Focus();
            }
        }

        //Generate Book Code
        private void GenerateBookCode()
        {
            int Code = Book_DAO.getBookCount() + dgvBookList.Rows.Count;
            txtBookCode.Text = string.Format("BK-" + "{0:00000}", ++Code);
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

        //Clear Data
        private void Clear()
        {
            txtBookCode.Text = "";
            txtBookName.Text = "";
            txtAuthor.Text = "";
            txtisbn.Text = "";
            cboCategory.Text = "";
        }

        private void cboCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAddBook.Focus();
            }
        }

        private void btnAddBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtBookName.Focus();
            }
        }
      
    }
}
