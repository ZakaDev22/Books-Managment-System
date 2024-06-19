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

namespace IslamIsLife.Users_Forms
{
    public partial class ShowUserInfoForm : Form
    {
        private int _UserID;
        private clsUsers _User;

        public ShowUserInfoForm(int UserID)
        {
            InitializeComponent();
            _UserID = UserID;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ShowUserInfoForm_Load(object sender, EventArgs e)
        {
            _User = clsUsers.FindUserByID(this._UserID);

            if (_User == null)
            {
                MessageBox.Show($"User With ID {this._UserID} Was Not Found\n This Form Will Be Closed", "Warnin",
                           MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
                return;
            }

            ctrlUserCard1.LoadUserInfo(_User.UserID);
        }
    }
}
