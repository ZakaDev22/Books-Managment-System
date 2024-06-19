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
    public partial class ctrlBooks : UserControl
    {
        private DataTable _dtBooks;

        public ctrlBooks()
        {
            InitializeComponent();
        }

        private void ctrlBooks_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;

            _RefleshBooksPage();
        }

        private void _RefleshBooksPage()
        {
            _dtBooks = clsBooks.GetAllBooks();
            djvBooks.DataSource = _dtBooks;

            lbRecords.Text = djvBooks.RowCount.ToString();

            if (djvBooks.Rows.Count > 0)
            {
                djvBooks.Columns[0].HeaderText = "Book ID";
                djvBooks.Columns[0].Width = 70;

                djvBooks.Columns[1].HeaderText = "Book Title";
                djvBooks.Columns[1].Width = 120;

                djvBooks.Columns[2].HeaderText = "Author";
                djvBooks.Columns[2].Width = 70;

                djvBooks.Columns[3].HeaderText = "ISBN";
                djvBooks.Columns[3].Width = 120;

                djvBooks.Columns[4].HeaderText = "Publication Date";
                djvBooks.Columns[4].Width = 90;

                djvBooks.Columns[5].HeaderText = "Publisher";
                djvBooks.Columns[5].Width = 90;

                djvBooks.Columns[6].HeaderText = "Genre";
                djvBooks.Columns[6].Width = 90;

                djvBooks.Columns[7].HeaderText = "Description";
                djvBooks.Columns[7].Width = 90;

                djvBooks.Columns[8].HeaderText = "Pages";
                djvBooks.Columns[8].Width = 90;

            }

        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";

            switch (cbFilterBy.Text)
            {
                case "BookID":
                    FilterColumn = "BookID";
                    break;
                case "Title":
                    {
                        FilterColumn = "Title";
                        break;
                    };

                case "Genre":
                    FilterColumn = "Genre";
                    break;


                default:
                    FilterColumn = "None";
                    break;
            }


            //Reset the filters in case nothing selected or filter value conains nothing.
            if (txtFilterBy.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtBooks.DefaultView.RowFilter = "";
                lbRecords.Text = djvBooks.Rows.Count.ToString();
                return;
            }


            if (FilterColumn == "BookID")
                //in this case we deal with numbers not string.
                _dtBooks.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterBy.Text.Trim());
            else 
                _dtBooks.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterBy.Text.Trim());

            lbRecords.Text = djvBooks.Rows.Count.ToString();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {

            txtFilterBy.Visible = (cbFilterBy.Text != "None");

            if (cbFilterBy.Text == "None")
            {
                txtFilterBy.Enabled = false;
                

            }
            else
            {
                txtFilterBy.Enabled = true;

                txtFilterBy.Text = "";
                txtFilterBy.Focus();
            }

        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "BookID")
            {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
            }
        }
    }
}
