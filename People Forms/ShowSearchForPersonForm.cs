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
    public partial class ShowSearchForPersonForm : Form
    {
        public ShowSearchForPersonForm()
        {
            InitializeComponent();
        }

        private void guna2CircleButton1_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void ctrlPersonInfoWithFilter1_OnPersonSelected(object sender, Controls.ctrlPersonInfoWithFilter.PersonSelectedEventArg e)
        {
            if(e.PersonID == null)
            {
                ctrlPersonInfoWithFilter1.SetValueFalse();
            }
        }
    }
}
