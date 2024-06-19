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
    public partial class ctrlTests : UserControl
    {
        private DataTable _dtTests;

        public ctrlTests()
        {
            InitializeComponent();
        }

        private void ctrlTests_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            _RefleshTestsPage();
        }


        // reflesh the bugs you have here.
        private void _RefleshTestsPage()
        {
            _dtTests = clsTests.GetAllTests();
            djvTests.DataSource = _dtTests;

            lbRecords.Text = djvTests.RowCount.ToString();

            if (djvTests.Rows.Count > 0)
            {
                djvTests.Columns[0].HeaderText = "Tests ID";
                djvTests.Columns[0].Width = 70;

                djvTests.Columns[1].HeaderText = "Student ID";
                djvTests.Columns[1].Width = 70;

                djvTests.Columns[2].HeaderText = "Student Name";
                djvTests.Columns[2].Width = 100;

                djvTests.Columns[3].HeaderText = "Teacher ID";
                djvTests.Columns[3].Width = 70;

                djvTests.Columns[4].HeaderText = "Book ID";
                djvTests.Columns[4].Width = 70;

                djvTests.Columns[5].HeaderText = "Book Title";
                djvTests.Columns[5].Width = 100;

                djvTests.Columns[6].HeaderText = "Surah No";
                djvTests.Columns[6].Width = 50;

                djvTests.Columns[7].HeaderText = "From Verse";
                djvTests.Columns[7].Width = 50;

                djvTests.Columns[8].HeaderText = "To Verse";
                djvTests.Columns[8].Width = 50;

                djvTests.Columns[9].HeaderText = "Tests Date";
                djvTests.Columns[9].Width = 100;

                djvTests.Columns[10].HeaderText = "Test Result";
                djvTests.Columns[10].Width = 70;


            }
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {

            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {

                case "Test ID":
                    FilterColumn = "TestID";
                    break;

               
                case "Teacher ID":
                    FilterColumn = "TeacherID";
                    break;

                case "Student ID":
                    {
                        FilterColumn = "StudentID";
                        break;
                    };

                case "Book ID":
                    FilterColumn = "BookID";
                    break;

                case "Surah No":
                    FilterColumn = "SurahNo";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtTests.DefaultView.RowFilter = "";
                lbRecords.Text = djvTests.Rows.Count.ToString();
                return;
            }
            
            _dtTests.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
           
            lbRecords.Text = djvTests.Rows.Count.ToString();
        }

    
        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
           

                txtFilterBy.Visible = (cbFilterBy.Text != "None");

                if (cbFilterBy.Text == "None")
                {
                    txtFilterBy.Enabled = false;
                  
                }
                else
                    txtFilterBy.Enabled = true;

                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.SelectedIndex == 1 || cbFilterBy.SelectedIndex ==2|| cbFilterBy.SelectedIndex ==3||
                            cbFilterBy.SelectedIndex ==4 || cbFilterBy.SelectedIndex == 5)
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
