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

namespace IslamIsLife.Users_Forms.Controls
{
    public partial class ctrlUserCard : UserControl
    {
        public ctrlUserCard()
        {
            InitializeComponent();
        }

        private clsUsers _User;
        private int? _UserID = -1;

        public int? UserID
        {
            get { return _UserID; }
        }

        public void LoadUserInfo(int? UserID)
        {
            _User = clsUsers.FindUserByID(UserID);
            if (_User == null)
            {
                _ResetPersonInfo();
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            _FillUserInfo();
        }

        private void _FillUserInfo()
        {

            ctrlPersonInfo1.LoadPersonInfoByID(_User.PersonID);
            lbUserID.Text = _User.UserID.ToString();
            lbUserName.Text = _User.UserName.ToString();

            if (_User.IsActive)
                lbIsActive.Text = "Yes";
            else
                lbIsActive.Text = "No";

        }

        private void _ResetPersonInfo()
        {

            ctrlPersonInfo1.ResetPersonInfo();
            lbUserID.Text = "[???]";
            lbUserName.Text = "[???]";
            lbIsActive.Text = "[???]";
        }
    }
}
