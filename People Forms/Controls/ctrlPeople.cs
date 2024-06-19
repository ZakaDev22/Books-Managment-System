using IslamIsLife.People_Forms;
using IslamIsLife_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife.MainControls
{
    public partial class ctrlPeople : UserControl
    {
        public ctrlPeople()
        {
            InitializeComponent();
        }

        private DataTable _dtPeople;

        private void ctrlPeople_Load(object sender, EventArgs e)
        {


            _RefleshPeoplePage();


        }

        private void _RefleshPeoplePage()
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Visible = false;

            _dtPeople = clsPeople.GetAllPeople();
            djvPeople.DataSource = _dtPeople;
            lbRecords.Text = djvPeople.RowCount.ToString();

            if (djvPeople.Rows.Count > 0)
            {
                djvPeople.Columns[0].HeaderText = "Person ID";
                djvPeople.Columns[0].Width = 70;

                djvPeople.Columns[1].HeaderText = "Full Name";
                djvPeople.Columns[1].Width = 120;

                djvPeople.Columns[2].HeaderText = "Gendor";
                djvPeople.Columns[2].Width = 70;

                djvPeople.Columns[3].HeaderText = "Date Of Birth";
                djvPeople.Columns[3].Width = 120;

                djvPeople.Columns[4].HeaderText = "Country Name";
                djvPeople.Columns[4].Width = 90;

                djvPeople.Columns[5].HeaderText = "City Name";
                djvPeople.Columns[5].Width = 90;

                djvPeople.Columns[6].HeaderText = "Email";
                djvPeople.Columns[6].Width = 150;

                djvPeople.Columns[7].HeaderText = "Phone";
                djvPeople.Columns[7].Width = 110;
            }

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            //Map Selected Filter to real Column name 


            switch (cbFilterBy.Text)
            {
                case "PersonID":
                    FilterColumn = "PersonID";
                    break;
                case "FullName":
                    {
                        FilterColumn = "FullName";
                        break;
                    };

                case "Gendor":
                    FilterColumn = "Gendor";
                    break;

               
                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lbRecords.Text = djvPeople.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "PersonID")
                //in this case we deal with numbers not string.
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else if (FilterColumn != "PersonID" && FilterColumn != "Gendor")
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            lbRecords.Text = _dtPeople.Rows.Count.ToString();

        }

        private void cbGendor_SelectedIndexChanged(object sender, EventArgs e)
        {
            string FilterColumn = "Gendor";
            string FilterValue = cbGendor.Text;

            switch (FilterValue)
            {
                case "All":
                    break;
                case "Male":
                    FilterValue = "Male";
                    break;
                case "Female":
                    FilterValue = "Female";
                    break;
            }


            if (FilterValue == "All")
                _dtPeople.DefaultView.RowFilter = "";
            else
                //in this case we deal with numbers not string. Note : iChange Numbers To String
                _dtPeople.DefaultView.RowFilter = $"{FilterColumn} like '{FilterValue}'";

               //_dtPeople.DefaultView.RowFilter = string.Format("[{0}] like '{1}'", FilterColumn, FilterValue);

            lbRecords.Text = djvPeople.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbFilterBy.Text == "Gendor")
            {
                txtFilterBy.Visible = false;
                cbGendor.Visible = true;
                cbGendor.Focus();
                cbGendor.SelectedIndex = 0;
            }

            else

            {

                txtFilterBy.Visible = (cbFilterBy.Text != "None");
                cbGendor.Visible = false;

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

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.Text== "PersonID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }

        private void djvPeople_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void showPersonDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if(djvPeople.RowCount == 0)
            {
                MessageBox.Show("Error DJV People Is Empty.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ShowPersonInfo frm = new ShowPersonInfo((int)djvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefleshPeoplePage();
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            ShowAddEditePerson frm = new ShowAddEditePerson();
            frm.ShowDialog();
            _RefleshPeoplePage();
        }

        private void updatePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {

            ShowAddEditePerson frm = new ShowAddEditePerson((int)djvPeople.CurrentRow.Cells[0].Value);
            frm.ShowDialog();
            _RefleshPeoplePage();
        }

        private void deletePersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int PersonID = (int)djvPeople.CurrentRow.Cells[0].Value;

            if(MessageBox.Show("Are You Sure You Want To Delete This Person","Warning",MessageBoxButtons.YesNo
                        ,MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2) == DialogResult.Yes)
            {
                if(clsPeople.DeletePerson(PersonID))
                {
                    MessageBox.Show($"Person With ID {PersonID} Was Deleted Successfully", "Info", MessageBoxButtons.OK
                        , MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
                    _RefleshPeoplePage();
                }
                else
                {
                    MessageBox.Show($"Error, Person With ID {PersonID} Was Not Deleted ", "Error", MessageBoxButtons.OK
                       , MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
            }
            else
            {
                MessageBox.Show($"Deleting Person With ID {PersonID} Was Canceled ", "Warning", MessageBoxButtons.OK
                      , MessageBoxIcon.Warning, MessageBoxDefaultButton.Button1);
            }
        }

        private void sendSMSToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
