using IslamIsLife.Properties;
using IslamIsLife_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TheArtOfDevHtmlRenderer.Adapters;

namespace IslamIsLife.Users_Forms
{

    public partial class ShowAddEditeUserForm : Form
    {

      

        enum enMode { AddNew =1, Update =2 }
        enMode _Mode = enMode.AddNew;

        private int _Permissions = 0;

        private int _UserID;

        private clsUsers _User;

        public ShowAddEditeUserForm()
        {
            InitializeComponent();

            _Mode = enMode.AddNew;
        }

        public ShowAddEditeUserForm(int UserID)
        {
            InitializeComponent();
            this._UserID = UserID;

            _Mode = enMode.Update;
        }

        private void chkAll_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAll.Checked)
            {
                _Permissions = 31;

                chkPeople.Checked = true;
                chkUsers.Checked = true;
                chkTeachers.Checked = true;
                chkBooks.Checked = true;
                chkTests.Checked = true;

                chkPeople.Enabled = false;
                chkUsers.Enabled = false;
                chkTeachers.Enabled = false;
                chkBooks.Enabled = false;
                chkTests.Enabled = false;

            }
            else
            {
                _Permissions = 31;

                chkPeople.Checked = false;
                chkUsers.Checked = false;
                chkTeachers.Checked = false;
                chkBooks.Checked = false;
                chkTests.Checked = false;

                chkPeople.Enabled = true;
                chkUsers.Enabled = true;
                chkTeachers.Enabled = true;
                chkBooks.Enabled = true;
                chkTests.Enabled = true;
            }
        }

        private void chkPeople_CheckedChanged(object sender, EventArgs e)
        {
            if(chkAll.Checked)
            {
                return;
            }

            if(chkPeople.Checked)
            {
                _Permissions += 1;
            }
            else
            {
                if(_Permissions != 0 || _Permissions != 1)
                   _Permissions -= 1;
            }
        }

        private void chkUsers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                return;
            }

            if (chkUsers.Checked)
            {
                _Permissions += 2;
            }
            else
            {
                if (_Permissions != 0 || _Permissions != 2)
                    _Permissions -= 2;
            }
        }

        private void chkTeachers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                return;
            }

            if (chkTeachers.Checked)
            {
                _Permissions += 4;
            }
            else
            {
                if (_Permissions != 0 || _Permissions != 4)
                    _Permissions -= 4;
            }
        }

        private void chkBooks_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                return;
            }

            if (chkBooks.Checked)
            {
                _Permissions += 8;
            }
            else
            {
                if (_Permissions != 0 || _Permissions != 8)
                    _Permissions -= 8;
            }
        }

        private void chkTests_CheckedChanged(object sender, EventArgs e)
        {
            if (chkAll.Checked)
            {
                return;
            }

            if (chkTests.Checked)
            {
                _Permissions += 16;
            }
            else
            {
                if (_Permissions != 0 || _Permissions != 16)
                    _Permissions -= 16;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnClose2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txtPersonID_Validating(object sender, CancelEventArgs e)
        {

            if (string.IsNullOrEmpty(txtPersonID.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPersonID, "This field is required!");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtPersonID, null);
            }

            if (_Mode == enMode.AddNew)
            {


                if (clsUsers.IsUserExistByPersonID(Convert.ToInt32(txtPersonID.Text.Trim())))
                {
                    MessageBox.Show($"This Person ID {txtPersonID.Text} Already Used By Another User,\n Please Enter Another ID", "Warning",
                                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    errorProvider1.SetError(txtPersonID, "This field is required!");
                }
                else
                {
                    //e.Cancel = false;
                    errorProvider1.SetError(txtPersonID, null);
                }
            }
        }

        private void txtPersonID_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (e.KeyChar == (char)13)
            //{

            //    //btnFind.PerformClick();
            //}

            e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void _ResetDefualtValues()
        {
            //this will initialize the reset the defaule values

            if (_Mode == enMode.AddNew)
            {
                lblTitle.Text = "Add New User";
                _User = new clsUsers();
            }
            else
            {
                lblTitle.Text = "Update User";
            }

            txtPersonID.Clear();
            txtPersonID.Enabled = true;

            txtUserName.Clear();
            txtPassword.Clear();

            rbActive.Checked = true;

            chkAll.Checked = false;

           // txtPersonID.Select();
        }

        private void _LoadData()
        {

            _User = clsUsers.FindUserByID(_UserID);

            if (_User == null)
            {
                MessageBox.Show("No User with ID = " + _UserID, "User Not Found", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                this.Close();
                return;
            }

            lbUserID.Text = _User.UserID.ToString();

            txtPersonID.Text = _User.UserID.ToString();
            txtPersonID.Enabled = false;

            txtUserName.Text = _User.UserName.ToString();
            txtPassword.Text = _User.Password.ToString();

            if(_User.IsActive)
            {
                rbActive.Checked = true;
            }
            else
            {
                rbNotActive.Checked = true;
            }

            // use her the bet wise operators to check every permissions if is there 
            // this solution just for now 
            
                if (_User.Permissions == 31)
                {
                    chkAll.Checked = true;
                    return;
                }

            
        }

        private void ShowAddEditeUserForm_Load(object sender, EventArgs e)
        {
            //chkPeople.Checked = true;
            //rbActive.Checked = true;
            //txtPersonID.Select();

            _ResetDefualtValues();

            if (_Mode == enMode.Update)
                _LoadData();
        }

        private void txtUserName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtUserName.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtUserName, "This field is required!");
                return;
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtUserName, null);
            }

           if(_Mode == enMode.AddNew)
            {
                if (clsUsers.IsUserExistByUserName(txtUserName.Text.Trim()))
                {
                    MessageBox.Show($"This Name : {txtUserName.Text} Already Used By Another User,\n Please Enter Another ID", "Warning",
                                               MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    e.Cancel = true;
                    errorProvider1.SetError(txtUserName, "This field is required!");
                }
                else
                {
                    //e.Cancel = false;
                    errorProvider1.SetError(txtUserName, null);
                }
            }
           
        }

        private void txtPassword_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPassword.Text.Trim()))
            {
                e.Cancel = true;
                errorProvider1.SetError(txtPassword, "This field is required!");
            }
            else
            {
                //e.Cancel = false;
                errorProvider1.SetError(txtPassword, null);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if(!this.ValidateChildren())
            {
                //Here we dont continue becuase the form is not valid
                MessageBox.Show("Some fileds are not valide!, put the mouse over the red icon(s) to see the error", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _User.PersonID = Convert.ToInt32(txtPersonID.Text);
            _User.UserName = txtUserName.Text.Trim();
            _User.Password = txtPassword.Text.Trim();

            _User.IsActive = rbActive.Checked ? true : false;

            _User.Permissions = _Permissions;


            if(_User.Save())
            {
                lbUserID.Text = _User.PersonID.ToString();
                //change form mode to update.
                _Mode = enMode.Update;
                lblTitle.Text = "Update User";

                txtPersonID.Enabled = false;

              

                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
            else
            {
                MessageBox.Show("Error, Data Not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
        }
    }
}
