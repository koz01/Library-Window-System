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
    public partial class frmAddNewMember : Form
    {
        DataTable memberDT = null;

        public frmAddNewMember()
        {
            InitializeComponent();
        }

        private void frmRent_Load(object sender, EventArgs e)
        {
            txtMemberCode.ReadOnly = true;
            txtMemberCode.BackColor = Color.White;
            GenerateMemberCode();
        }

        //Add Text Box Data to Grid View
        private void btnAddBook_Click(object sender, EventArgs e)
        {
            try
            {
                if(txtMemberName.Text.Trim() =="")
                {
                    MessageBox.Show("Incomplete Member Name data.", "Warning");
                    return;
                }else if(txtEmail.Text.Trim() == "")
                {
                    MessageBox.Show("Incomplete Email address.", "Warning");
                    return;
                }else if(txtPhone.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter phone number", "Incomplete Phone");
                    return;
                }
                else if (txtCity.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter city name", "Incomplete City");
                    return;
                }
                else if (txtAddress.Text.Trim() == "")
                {
                    MessageBox.Show("Please enter Address", "Incomplete Address");
                    return;
                }
                else
                {

                    string code = txtMemberCode.Text.Trim();
                    string name = txtMemberName.Text.Trim();
                    string email = txtEmail.Text.Trim();
                    string city = txtCity.Text.Trim();
                    string ph = txtPhone.Text.Trim();
                    string add = txtAddress.Text.Trim();
                    string[] row = { "false", code, name, email, city, ph, add };
                    dgvMemberList.Rows.Add(row);

                    Clear();
                    GenerateMemberCode();
                }

                txtMemberName.Focus();
            }catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (DataGridViewRow row in dgvMemberList.Rows)
            {
                if (Convert.ToBoolean(row.Cells[ChkSelect.Name].Value) == true)
                {
                    dgvMemberList.Rows.Remove(row);
                }
            }

            GenerateMemberCode();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (dgvMemberList.Rows.Count > 0)
                {

                    memberDT = new DataTable();
                    memberDT.Columns.Add("MemberName");
                    memberDT.Columns.Add("MemberCode");
                    memberDT.Columns.Add("Email");
                    memberDT.Columns.Add("City");
                    memberDT.Columns.Add("Phone");
                    memberDT.Columns.Add("Address");

                    foreach (DataGridViewRow row in dgvMemberList.Rows)
                    {
                        DataRow dr = memberDT.NewRow();
                        dr["MemberName"] = row.Cells[1].Value.ToString();
                        dr["MemberCode"] = row.Cells[2].Value.ToString();
                        dr["Email"] = row.Cells[3].Value.ToString();
                        dr["City"] = row.Cells[4].Value.ToString();
                        dr["Phone"] = row.Cells[5].Value.ToString();

                        dr["Address"] = row.Cells[6].Value.ToString();                      
                        memberDT.Rows.Add(dr);
                        dr = null;
                    }

                    if(memberDT.Rows.Count > 0)
                    {
                        if (Member_DAO.SaveNewMember(memberDT) > 0)
                        {
                            MessageBox.Show("Save Successfully.", "Save");
                            dgvMemberList.DataSource = null;
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

        
        //Generate Book Code
        private void GenerateMemberCode()
        {
            int Code = Member_DAO.getMemberCount() + dgvMemberList.Rows.Count;
            txtMemberCode.Text = string.Format("C-" + "{0:00000}", ++Code);
        }

       

        //Clear Data
        private void Clear()
        {
            txtMemberCode.Text = "";
            txtMemberName.Text = "";
            txtEmail.Text = "";
            txtPhone.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
        }

        private void cboCategory_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }

        private void btnAddBook_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtMemberName.Focus();
            }
        }

        private void txtMemberName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                txtEmail.Focus();
            }
        }

        private void txtEmail_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtPhone.Focus();
            }
        }

        private void txtPhone_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtCity.Focus();
            }
        }

        private void txtCity_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                txtAddress.Focus();
            }
        }

        private void txtAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnAdd.Focus();
            }
        }
      
    }
}
