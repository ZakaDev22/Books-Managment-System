using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife.People_Forms.Controls
{
    public partial class ctrlPersonInfoWithFilter : UserControl
    {
        public ctrlPersonInfoWithFilter()
        {
            InitializeComponent();
        }

        private int _SelectedIndex ;

        private bool _IsFind;

        public int SelectedIndex
        {
            get { return _SelectedIndex ; }

            set { _SelectedIndex = value ; }
              
        }

        public void SetValueFalse()
        {
            ctrlPersonInfo1.SetLikeLabeFalse();
        }

        //public void SetSelendexValue(int Number)
        //{
        //    cbFilterBy.SelectedIndex = Number ;
        //}

        public class PersonSelectedEventArg : EventArgs
        {
            public int? PersonID { get; }
            public string Email { get; }

            public PersonSelectedEventArg(int? PersonID, string Email)
            {
                this.PersonID = PersonID;
                this.Email = Email;
            }
        }

        public event EventHandler<PersonSelectedEventArg> OnPersonSelected;

        public void RiseOnPersonSelected(int? PersonID, string Email)
        {
            RiseOnPersonSelected(new PersonSelectedEventArg(PersonID, Email));
        }

        public void RiseOnPersonSelected(PersonSelectedEventArg e)
        {
            OnPersonSelected?.Invoke(this, e);
        }

        private void btnSearchPerson_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtFilterBy.Text) || string.IsNullOrEmpty(txtFilterBy.Text))
            {
                MessageBox.Show("Please Enter Your ID First Then Click On Search Button.","Warning",
                                                    MessageBoxButtons.OK,MessageBoxIcon.Warning);
                return;
            }

            switch(cbFilterBy.SelectedIndex)
            {
                case 0:
                   _IsFind = ctrlPersonInfo1.LoadPersonInfoByID(int.Parse(txtFilterBy.Text));
                        break;

                case 1:
                    _IsFind = ctrlPersonInfo1.LoadPersonInfoNationalNo(txtFilterBy.Text);
                    break;
            }

            if (OnPersonSelected != null && _IsFind)
                RiseOnPersonSelected(ctrlPersonInfo1._PersonInfo.PersonID, ctrlPersonInfo1._PersonInfo.Email);
        }

        private void txtFilterBy_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(cbFilterBy.SelectedIndex == 0)
               e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ctrlPersonInfoWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Select();
            txtFilterBy.Focus();
        }

        private void txtFilterBy_TextChanged(object sender, EventArgs e)
        {
            if(string.IsNullOrEmpty(txtFilterBy.Text))
            {
                ctrlPersonInfo1.ResetPersonInfo();
            }
        }

        private void btnAddPerson_Click(object sender, EventArgs e)
        {
            ShowAddEditePerson frm = new ShowAddEditePerson();

            frm.DataBack += Frm_DataBack;

            frm.ShowDialog();
        }

        private void Frm_DataBack(object sender, int? PersonID)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Text = PersonID.ToString();

            ctrlPersonInfo1.LoadPersonInfoByID(PersonID);
        }
    }
}