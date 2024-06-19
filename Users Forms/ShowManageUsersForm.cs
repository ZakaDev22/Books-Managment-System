using IslamIsLife.Global_Classes;
using IslamIsLife.People_Forms;
using IslamIsLife_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static IslamIsLife.People_Forms.ShowAddEditePerson;

namespace IslamIsLife.Users_Forms
{
    public partial class ShowManageUsersForm : Form
    {
        private  DataTable _dtUsers;

        public ShowManageUsersForm()
        {
            InitializeComponent();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

      

        private void _RefleshUsersPage()
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;

            _dtUsers = clsUsers.GetAllUsers();
            djvUsers.DataSource = _dtUsers;
            lbRecords.Text = djvUsers.RowCount.ToString();

            if (djvUsers.Rows.Count > 0)
            {
               djvUsers.Columns[0].HeaderText = "User ID";
               djvUsers.Columns[0].Width = 70;

               djvUsers.Columns[1].HeaderText = "User Name";
               djvUsers.Columns[1].Width = 70;

                djvUsers.Columns[2].HeaderText = "Person ID";
                djvUsers.Columns[2].Width = 70;

                djvUsers.Columns[3].HeaderText = "Full Name";
                djvUsers.Columns[3].Width = 100;             

               djvUsers.Columns[4].HeaderText = "Country Name";
               djvUsers.Columns[4].Width = 100;

          
               djvUsers.Columns[5].HeaderText = "Is Active";
               djvUsers.Columns[5].Width = 60;

               djvUsers.Columns[6].HeaderText = "Permissions";
               djvUsers.Columns[6].Width = 90;
            }

        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (cbFilterBy.Text == "IsActive")
            {
                txtFilterBy.Visible = false;
                cbIsActive.Visible = true;
                cbIsActive.Focus();
                cbIsActive.SelectedIndex = 0;
            }

            else

            {

                txtFilterBy.Visible = (cbFilterBy.Text != "None");
                cbIsActive.Visible = false;

                if (cbFilterBy.Text == "None")
                {
                    txtFilterBy.Enabled = false;
                    //_dtDetainedLicenses.DefaultView.RowFilter = "";
                    //lblTotalRecords.Text = dgvDetainedLicenses.Rows.Count.ToString();

                }
                else
                    txtFilterBy.Enabled = true;

                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }
        }

        private void cbIsActive_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "IsActive";
            string FilterValue = cbIsActive.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Yes":
                    FilterValue = "1";
                    break;
                case "No":
                    FilterValue = "0";
                    break;
            }


            if (FilterValue == "All")
                _dtUsers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string. Note : iChange Numbers To String
                _dtUsers.DefaultView.RowFilter = $"{FilterColumn} = {FilterValue}";

            //_dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}'", FilterColumn, FilterValue);

            lbRecords.Text = djvUsers.Rows.Count.ToString();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 


            switch (cbFilterBy.Text)
            {
                case "None":
                    FilterColumn = "None";
                    break;

                case "UserID":
                    FilterColumn = "UserID";
                    break;

                case "UserName":
                    FilterColumn = "UserName";
                    break;

                case "PersonID":
                    FilterColumn = "PersonID";
                    break;

                case "FullName":
                    {
                        FilterColumn = "UserFullName";
                        break;
                    };

                case "Permissions":
                    FilterColumn = "Permissions";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtUsers.DefaultView.RowFilter = "";
                lbRecords.Text = djvUsers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "UserID" || FilterColumn == "PersonID" || FilterColumn == "Permissions")
                //in this case we deal with numbers not string.
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else 
                _dtUsers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            lbRecords.Text = _dtUsers.Rows.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex == 3 || cbFilterBy.SelectedIndex == 5)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void ShowManageUsersForm_Load(object sender, EventArgs e)
        {
            _RefleshUsersPage();
        }

        private void btnAddUser_Click(object sender, EventArgs e)
        {
            ShowAddEditeUserForm frm = new ShowAddEditeUserForm();
            frm.ShowDialog();

            _RefleshUsersPage();
        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)djvUsers.CurrentRow.Cells[2].Value;

            if (PersonID == 1 && clsGlobal.CurrentUser.UserID != 1)
            {
                MessageBox.Show(@"Access Denied, You Can't Show Admin Information.
                                Plese Contact Your Admin.","Warning",MessageBoxButtons.OK, MessageBoxIcon.Warning );

                return;
            }

            ShowPersonInfo frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)djvUsers.CurrentRow.Cells[0].Value;

            if (UserID == 1 && clsGlobal.CurrentUser.UserID != 1)
            {
                MessageBox.Show(@"Access Denied, You Can't Show Admin Information.
                                Plese Contact Your Admin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            // ShowUserInfo

            ShowUserInfoForm frm = new ShowUserInfoForm(UserID);

            frm.ShowDialog();
        }

        private void editeUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)djvUsers.CurrentRow.Cells[0].Value;

            if (UserID == 1 && clsGlobal.CurrentUser.UserID != 1)
            {
                MessageBox.Show(@"Access Denied, You Can't Edite Admin Information.
                                Plese Contact Your Admin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            ShowAddEditeUserForm frm = new ShowAddEditeUserForm(UserID);
            frm.ShowDialog();

            _RefleshUsersPage();
        }

        private void deleteUserToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)djvUsers.CurrentRow.Cells[0].Value;

            if (UserID == 1 && clsGlobal.CurrentUser.UserID != 1)
            {
                MessageBox.Show(@"Access Denied, You Can't Delete The Admin.
                                Plese Contact Your Admin.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                return;
            }

            if (MessageBox.Show($"Are You Sure You Want To Delete This User With ID {UserID} ❓", "Warning", MessageBoxButtons.YesNo,
                                MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if(clsUsers.DeleteUser(UserID))
                {
                    MessageBox.Show($"Success. Deleting User With ID {UserID} Done Successfully.", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }
                else
                {
                    MessageBox.Show($"Error. Deleting User With ID {UserID} Was Failed.", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);

                }
            }
            else
            {
                MessageBox.Show($"Deleting User With ID {UserID} Was Canceled.", "Canceled", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);

            }
        }

        private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This Future Not Complete Yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show($"This Future Not Complete Yet", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }


        private void toolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ChangeUserPasswordForm frm = new ChangeUserPasswordForm((int)djvUsers.CurrentRow.Cells[0].Value);

            frm.ShowDialog();
        }
    }
}
