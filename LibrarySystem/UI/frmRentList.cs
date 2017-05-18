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
    public partial class frmRentList : Form
    {
        Member Memberobj = null;
      
        public frmRentList()
        {
            InitializeComponent();
        }

        private void frmRent_Load(object sender, EventArgs e)
        {
            PopulateCategory();
            Clear();
        }

        //Member Code TextBox Leave
        private void txtMemberCode_Leave(object sender, EventArgs e)
        {
            string code = txtMemberCode.Text.Trim(); //For Member COde
            try
            {
                if (code != "" && code != null)
                {
                    if (!code.StartsWith("C"))
                    {
                        code = "C" + code;
                        txtMemberCode.Text = code;
                    }

                    Memberobj = new Member();
                    Memberobj = Member_DAO.getMemberByCode(code);

                    if (Memberobj != null)
                    {
                        txtMemberName.Text = Memberobj.MemberName.ToString();
                    }
                    else
                    {
                        MessageBox.Show("Invalid Member Code");
                        txtMemberCode.Text = "";
                    }
                }
                else
                {
                    txtMemberName.Text = "";
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
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

        //Clear Control
        private void Clear()
        {
            Memberobj = null;
            txtBookName.Text = "";
            txtMemberCode.Text = "";
            txtMemberName.Text = "";
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            dgvBookList.DataSource = null;
            cboCategory.Text = "";

        }

       private void txtMemberCode_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMemberCode_Leave(sender, e);
                txtBookName.Focus();
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            long memberID = 0;
            int category = 0;
            try
            {
                if (Memberobj != null)
                {
                    memberID = Memberobj.MemberID;
                }
                string bookName = txtBookName.Text.Trim();

                if (cboCategory.Text != "")
                {
                    category = Convert.ToInt32(cboCategory.SelectedValue);
                }
                else
                {
                    category = 0;
                }
                string startDate = dtpFromDate.Value.ToShortDateString();
                string toDate = dtpToDate.Value.ToShortDateString();

                dgvBookList.DataSource = Rent_DAO.getSearchRentBookHistory(bookName, memberID, category, startDate, toDate);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnRestore_Click(object sender, EventArgs e)
        {
            try
            {
                foreach (DataGridViewRow row in dgvBookList.Rows)
                {
                    if (Convert.ToBoolean(row.Cells[ChkSelect.Name].Value) == true)
                    {
                        int Id = Convert.ToInt32(row.Cells[1].Value);
                        
                        Rent_DAO.RestoreRentBook(Id);
                    }
                }

                MessageBox.Show("Update Successfully");
                btnSearch_Click(sender, e);
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void frmRentList_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F4)
            {
                btnSearch_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F6)
            {
                btnRestore_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }
        }


    }
}
