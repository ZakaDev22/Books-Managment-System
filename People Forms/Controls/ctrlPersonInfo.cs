using IslamIsLife.Global_Classes;
using IslamIsLife.Properties;
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

namespace IslamIsLife.People_Forms.Controls
{
    public partial class ctrlPersonInfo : UserControl
    {

        private int? _PersonID = null;

        public ctrlPersonInfo()
        {
            InitializeComponent();
        }

       public clsPeople _PersonInfo;

        public void SetLikeLabeFalse()
        {
            linkEditePerson.Visible = false;
        }

        public bool LoadPersonInfoByID(int? PersonID)
        {
            bool IsFound = false;

            clsPeople _Person = clsPeople.FindPersonWithID(PersonID);

            if (_Person == null)
            {
                MessageBox.Show($"Error. Person With ID {PersonID} Was Not Found","Error",
                                                        MessageBoxButtons.OK,MessageBoxIcon.Error);

                return IsFound;
            }

            IsFound = true;

            linkEditePerson.Enabled = true;

            _PersonInfo = _Person;

            _PersonID = _Person.PersonID;

            lbPersonID.Text = _Person.PersonID.ToString();
            lbNationalNo.Text = _Person.NationalNo ?? "Unknown";
            lbFullName.Text = _Person.FullName();
            lbGendor.Text = _Person.GendorName(_Person.Gendor);

            // replace The Null Values With "Unknown" String Just In Case 

            lbCountryName.Text = _Person.CountryName ?? "Unknown";
            lbCityName.Text = _Person.CityName ?? "Unknown";

            lbDateOfBirth.Text = _Person.DateOfBirth.ToString();

            lbPhone.Text = _Person.Phone ?? "Unknown";

            lbEmail.Text = _Person.Email ?? "Unknown";

            if(_Person.ImagePath != null)
            {
                pcPersonImage.ImageLocation = _Person.ImagePath;
            }

            return IsFound;
        }

        public bool LoadPersonInfoNationalNo(string NationalNo)
        {
            bool IsFound = false;

            clsPeople _Person = clsPeople.FindPersonByNationalNo(NationalNo);

            if (_Person == null)
            {
                MessageBox.Show($"Error. Person With National No {NationalNo} Was Not Found", "Error",
                                                        MessageBoxButtons.OK, MessageBoxIcon.Error);

                return IsFound;
            }

            IsFound = true;

            linkEditePerson.Enabled = true;

            _PersonInfo = _Person;

            _PersonID = _Person.PersonID;

            lbPersonID.Text = _Person.PersonID.ToString();
            lbNationalNo.Text = _Person.NationalNo ?? "Unknown";
            lbFullName.Text = _Person.FullName();
            lbGendor.Text = _Person.GendorName(_Person.Gendor);

            // replace The Null Values With "Unknown" String Just In Case 

            lbCountryName.Text = _Person.CountryName ?? "Unknown";
            lbCityName.Text = _Person.CityName ?? "Unknown";

            lbDateOfBirth.Text = _Person.DateOfBirth.ToString();

            lbPhone.Text = _Person.Phone ?? "Unknown";

            lbEmail.Text = _Person.Email ?? "Unknown";

            if (_Person.ImagePath != null)
            {
                pcPersonImage.ImageLocation = _Person.ImagePath;
            }

            return IsFound;
        }

        public void ResetPersonInfo()
        {
            _PersonID = null;

            linkEditePerson.Enabled = false;
            lbPersonID.Text = "[????]";
            lbNationalNo.Text = "[????]";
            lbFullName.Text = "[????]";
            pcPersonImage.Image = Resources.Male_512;
            lbGendor.Text = "[????]";
            lbEmail.Text = "[????]";
            lbPhone.Text = "[????]";
            lbDateOfBirth.Text = "[????]";
            lbCountryName.Text = "[????]";
            lbCityName.Text = "[????]";

        }

        private void linkEditePerson_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            ShowAddEditePerson frm = new ShowAddEditePerson(_PersonID);
            frm.ShowDialog();

            // reflesh The data
            LoadPersonInfoByID(_PersonID);
        }

        private void ctrlPersonInfo_Load(object sender, EventArgs e)
        {
            linkEditePerson.Enabled = false;
        }
    }
}
