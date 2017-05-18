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
    public partial class frmRent : Form
    {
        Member Memberobj = null;
        DataTable DetailBookDT = null;

        public frmRent()
        {
            InitializeComponent();
        }

        private void frmRent_Load(object sender, EventArgs e)
        {
            txtMemberName.ReadOnly = true;
            txtMemberName.BackColor = Color.White;

        }

        private void txtMemberCode_Leave(object sender, EventArgs e)
        {
            string code = txtMemberCode.Text.Trim() ; //For Member COde
            try
            {
                if(code != "" && code != null)
                {
                    if(!code.StartsWith("C"))
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

            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void txtMemberCode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                dtpFromDate.Focus();
            }
        }

        //Day Calculate
        private void TotalDayCount()
        {
            int StartDay = dtpFromDate.Value.Day;
            int EndDay = dtpToDate.Value.Day;
            if (EndDay > StartDay)
            {
                txtDays.Text = (EndDay - StartDay).ToString();
            }
        }

        private void dtpToDate_Leave(object sender, EventArgs e)
        {
            TotalDayCount();
        }

        private void dtpFromDate_Leave(object sender, EventArgs e)
        {
            TotalDayCount();
        }

        private void btnAddBook_Click(object sender, EventArgs e)
        {

            frmBookList frmbookList = new frmBookList();
            frmbookList.ShowDialog();
            try
            {
                if (frmbookList.BookDT !=null)
                {
                    dgvBookList.DataSource = frmbookList.BookDT;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //FOR Key Event
        private void frmRent_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode==Keys.F2)
            {
                btnAddBook_Click(sender, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                btnDelete_Click(sender, e);
            }else if(e.KeyCode == Keys.F5)
            {
                btnSave_Click(sender, e);
            }else if(e.KeyCode == Keys.F10)
            {
                btnClose_Click(sender, e);
            }
           
        }

        //For Delete Row Event
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (dgvBookList.SelectedRows.Count > 0)
            {
                DialogResult msg = MessageBox.Show("Are your sure want to delete", "Confirmation", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (msg == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in dgvBookList.SelectedRows)
                    {
                        dgvBookList.Rows.Remove(row);
                    }
                }
            }
            else
            {
                MessageBox.Show( "No records found to delete","No Data found");
            }
        }

        //Save Event
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (txtMemberCode.Text.Trim() == "" || txtDays.Text.Trim() == "" || dgvBookList.Rows.Count == 0)
                {
                    MessageBox.Show("Please fill complete records", "Incomplete Records");
                }
                else
                {
                    DetailBookDT = new DataTable();
                    DetailBookDT.Columns.Add("BookID");

                    for (int i = 0; i < dgvBookList.Rows.Count; i++)
                    {
                        if (dgvBookList.Rows[i].Cells[1].Value == null)
                        {
                            break;
                        }
                        DataRow dr = DetailBookDT.NewRow();
                        dr["BookID"] = dgvBookList.Rows[i].Cells[0].Value.ToString();
                        DetailBookDT.Rows.Add(dr);

                    }

                    if (btnSave.Text.Contains("Save"))
                    {
                        BookRent rentObj = new BookRent();
                        rentObj.MemberId = Memberobj.MemberID;
                        rentObj.StartDate = dtpFromDate.Value;
                        rentObj.IssueDate = dtpToDate.Value;
                        rentObj.NumberOfDay = Convert.ToInt32(txtDays.Text.Trim());
                        
                        int l_return = Rent_DAO.SaveDAO(rentObj, DetailBookDT);
                        if (l_return > 0)
                        {
                            MessageBox.Show("Save Successfully", "Save");
                            Clear();
                        }
                    }
                }
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
            DetailBookDT = null;
            txtDays.Text = "";
            txtMemberCode.Text = "";
            txtMemberName.Text = "";
            dtpFromDate.Value = DateTime.Now;
            dtpToDate.Value = DateTime.Now;
            dgvBookList.DataSource = null;

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            Clear();
            this.Close();
        }

        private void dtpToDate_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode== Keys.Enter)
            {
                txtDays.Focus();
            }
        }
      
    }
}
