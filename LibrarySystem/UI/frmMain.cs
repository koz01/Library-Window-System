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
    public partial class frmMain : Form
    {

        public frmMain()
        {
            InitializeComponent();
        }

        private void ShowNewForm(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

            frmSearchBook frmSearchBook = new frmSearchBook();
            frmSearchBook.MdiParent = frmMain.ActiveForm;
            frmSearchBook.Show();


        }

        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

            frmRent frmRent = new frmRent();
            frmRent.MdiParent = frmMain.ActiveForm;
            frmRent.Show();

        }

        private void frmMain_Load(object sender, EventArgs e)
        {

        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

            frmRentList frmRentList = new frmRentList();
            frmRentList.MdiParent = frmMain.ActiveForm;
            frmRentList.Show();
        }

        private void printToolStripButton_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

            frmAddNewBook frmNewBook = new frmAddNewBook();
            frmNewBook.MdiParent = frmMain.ActiveForm;
            frmNewBook.Show();
        }

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            foreach (Form childForm in MdiChildren)
            {
                childForm.Close();
            }

            frmAddNewMember frmNewMember = new frmAddNewMember();
            frmNewMember.MdiParent = frmMain.ActiveForm;
            frmNewMember.Show();

        }

        private void frmMain_FormClosed(object sender, FormClosedEventArgs e)
        {
            Application.Exit();
        }


    }
}
