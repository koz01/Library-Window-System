using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using LibrarySystem.report;
using Microsoft.Reporting.WinForms;

namespace LibrarySystem
{
    public partial class frmSearchBook : Form
    {

        public frmSearchBook()
        {
            InitializeComponent();
        }

        private void frmSearchBook_Load(object sender, EventArgs e)
        {
            PopulateCategory();
            
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
            }
            else if (e.KeyCode == Keys.F7)
            {
                btnPrint_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }

        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable bookDT = new DataTable();
                bookDT.Columns.Add("BookName");
                bookDT.Columns.Add("BookCode");
                bookDT.Columns.Add("Author");
                bookDT.Columns.Add("Category");
                bookDT.Columns.Add("ISBN");

                foreach (DataGridViewRow row in dgvBookList.Rows)
                {

                    DataRow dr = bookDT.NewRow();

                    string str = row.Cells[1].Value.ToString();

                    dr["BookName"] = row.Cells[1].Value.ToString();
                    dr["BookCode"] = row.Cells[2].Value.ToString();
                    dr["Author"] = row.Cells[3].Value.ToString();
                    dr["Category"] = row.Cells[4].Value.ToString();
                    dr["ISBN"] = row.Cells[5].Value.ToString();
                    bookDT.Rows.Add(dr.ItemArray);
                    dr = null;
                }


                frmReportViewer frmReport = new frmReportViewer();
                frmReport.MdiParent = frmMain.ActiveForm;
                frmReport.Show();
                ReportViewer v = frmReport.Controls.Find("reportViewer1", true).FirstOrDefault() as ReportViewer;
                ReportDataSource dataset = new ReportDataSource("DataSet1", bookDT);
                v.LocalReport.ReportEmbeddedResource = "LibrarySystem.report.Report1.rdlc";
                v.LocalReport.DataSources.Clear();
                v.LocalReport.DataSources.Add(dataset);
                v.LocalReport.Refresh();
                dataset.Value = bookDT;
                v.RefreshReport();
                v.LocalReport.Refresh();

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
