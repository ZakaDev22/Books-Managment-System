using IslamIsLife.People_Forms;
using IslamIsLife.Users_Forms;
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

namespace IslamIsLife.Main_And_Login_Forms.MainControls
{
    public partial class ctrlTeachers : UserControl
    {
        private DataTable _dtTeachers;
        public ctrlTeachers()
        {
            InitializeComponent();
        }

        private void _RefleshTeachersPage()
        {
            _dtTeachers = clsTeachers.GetAllTeachers();
            djvTeachers.DataSource = _dtTeachers;

            lbRecords.Text = djvTeachers.RowCount.ToString();

            if (djvTeachers.Rows.Count > 0)
            {
                djvTeachers.Columns[0].HeaderText = "Teacher ID";
                djvTeachers.Columns[0].Width = 70;

                djvTeachers.Columns[1].HeaderText = "Person ID";
                djvTeachers.Columns[1].Width = 70;

                djvTeachers.Columns[2].HeaderText = "Teacher Name";
                djvTeachers.Columns[2].Width = 120;

                djvTeachers.Columns[3].HeaderText = "User ID";
                djvTeachers.Columns[3].Width = 80;

                djvTeachers.Columns[4].HeaderText = "T.User Name";
                djvTeachers.Columns[4].Width = 120;

                djvTeachers.Columns[5].HeaderText = "Recitations No";
                djvTeachers.Columns[5].Width = 90;

                djvTeachers.Columns[6].HeaderText = "Is Active";
                djvTeachers.Columns[6].Width = 90;

                djvTeachers.Columns[7].HeaderText = "Permissions";
                djvTeachers.Columns[7].Width = 90;


            }
        }

        private void ctrlTeachers_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _RefleshTeachersPage();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Is Active")
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
                _dtTeachers.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string. Note : iChange Numbers To String
                _dtTeachers.DefaultView.RowFilter = $"{FilterColumn} = {FilterValue} ";

            //_dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}'", FilterColumn, FilterValue);

            lbRecords.Text = djvTeachers.Rows.Count.ToString();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "Teacher ID":
                    FilterColumn = "TeacherID";
                    break;
                case "Person ID":
                    {
                        FilterColumn = "PersonID";
                        break;
                    };

                case "User ID":
                    FilterColumn = "UserID";
                    break;

                case "Full Name":
                    FilterColumn = "TeacheFullName";
                    break;

                case "User Name":
                    FilterColumn = "UserName";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtTeachers.DefaultView.RowFilter = "";
                lbRecords.Text = djvTeachers.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "TeacherID" || FilterColumn == "PersonID" || FilterColumn == "UserID")
                //in this case we deal with numbers not string.
                _dtTeachers.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else
                _dtTeachers.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            lbRecords.Text = djvTeachers.Rows.Count.ToString();
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Teacher ID" || cbFilterBy.Text == "Person ID" || cbFilterBy.Text == "User ID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void sHowPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int? UserID = (int)djvTeachers.CurrentRow.Cells[3].Value;

            int? PersonID = clsUsers.FindUserByID(UserID).PersonID;

            ShowPersonInfo frm = new ShowPersonInfo(PersonID);
            frm.ShowDialog();
        }

        private void showUserDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int UserID = (int)djvTeachers.CurrentRow.Cells[3].Value;

            ShowUserInfoForm frm = new ShowUserInfoForm(UserID);
            frm.ShowDialog();
        }

        private void deleteTeacherToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int TeacherID = (int)djvTeachers.CurrentRow.Cells[0].Value;

            if(MessageBox.Show($"Are You Sure You Want To Delete Teacher With ID {TeacherID}","Warning",
                                MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if(clsTeachers.DeleteTeacher(TeacherID))
                {
                    MessageBox.Show($"Success, Deleting Teacher With ID {TeacherID} Was Done Successfully.", "Success",
                                MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    _RefleshTeachersPage();
                }
                else
                {
                    MessageBox.Show($"Deleting Teacher With ID {TeacherID} Was Failed.", "Failed",
                               MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show($"Deleting Teacher With ID {TeacherID} Was Canceled.", "Warnin",
                              MessageBoxButtons.OK, MessageBoxIcon.Exclamation, MessageBoxDefaultButton.Button1);
            }
        }
    }

}
