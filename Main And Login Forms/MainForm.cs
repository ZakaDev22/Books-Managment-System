using IslamIsLife.Books_Forms;
using IslamIsLife.Global_Classes;
using IslamIsLife.Main_And_Login_Forms;
using IslamIsLife.People_Forms;
using IslamIsLife.Teachers_Forms;
using IslamIsLife.Tests_Forms;
using IslamIsLife.Users_Forms;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Security;
using System.Windows.Forms;

namespace IslamIsLife
{
    public partial class MainForm : Form
    {
        LoginForm _loginForm;

        public MainForm(LoginForm Form)
        {
            InitializeComponent();
            _loginForm = Form;
        }

        //
        // Make All The Rest Of Controls A Reflesh Method To Reflesh The Data Evry Time You Changet
        //

        private void btnLogout_Click(object sender, EventArgs e)
        {
            _loginForm.Show();

            this.Close();
           
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btnHome.Top;

            ctrlHome1._RefreshHomePage();

        }

        private void btnPeople_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btnPeople.Top;


           ShowManagePeopleForm frm = new ShowManagePeopleForm();
            frm.ShowDialog();

            btnHome.PerformClick();
        }

        private void btnUserse_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btnUserse.Top;

            ShowManageUsersForm frm = new ShowManageUsersForm();
            frm.ShowDialog();

            btnHome.PerformClick();
        }

        private void btntests_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btntests.Top;

            ShowManageTestsForm frm = new ShowManageTestsForm();
            frm.ShowDialog();

            btnHome.PerformClick();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _loginForm.Close();
            Application.Exit();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            lbTitle.Text += " " + clsGlobal.CurrentUser.UserName + " 👌";
        }

        private void btnBooks_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btnBooks.Top;

            ShowManageBooksForm frm = new ShowManageBooksForm();
            frm.ShowDialog();

            btnHome.PerformClick();
        }

      

        private void btnTeachers_Click(object sender, EventArgs e)
        {
            pnOnbtnClick.Top = btnTeachers.Top;

            ShowManageTeachersForm frm = new ShowManageTeachersForm();
            frm.ShowDialog();

            btnHome.PerformClick();

        }

        private void button5_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This Future Not Complete Yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
    }

       
 }

