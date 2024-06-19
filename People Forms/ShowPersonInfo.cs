using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IslamIsLife.People_Forms
{
    public partial class ShowPersonInfo : Form
    {
        private int? _PersonID = null;
        public ShowPersonInfo(int? personID)
        {
            InitializeComponent();
            _PersonID = personID;
        }

        private void ShowPersonInfo_Load(object sender, EventArgs e)
        {
            ctrlPersonInfo1.LoadPersonInfoByID(_PersonID);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
