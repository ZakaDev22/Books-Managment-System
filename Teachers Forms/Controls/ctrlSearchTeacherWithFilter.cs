using IslamIsLife.People_Forms;
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

namespace IslamIsLife.Teachers_Forms.Controls
{
    public partial class ctrlSearchTeacherWithFilter : UserControl
    {
       

        public ctrlSearchTeacherWithFilter()
        {
            InitializeComponent();
        }

        private int _SelectedIndex;

        private bool _IsFind;

       public  clsTeachers _Taecher;


        public int SelectedIndex
        {
            get { return _SelectedIndex; }

            set { _SelectedIndex = value; }

        }

        public void SetValueFalse()
        {
            ctrlPersonInfo1.SetLikeLabeFalse();
        }


        public class TeacherSelectedEventArg : EventArgs
        {
            public int? TeacherID { get; }

            public int? UserID { get; }

            public int? PersonID { get; }

            public TeacherSelectedEventArg(int? TeacherID,int? PersonID, int? UserID)
            {
                this.TeacherID = TeacherID;
                this.PersonID = PersonID;
                this.UserID = UserID;
            }
        }

        public event EventHandler<TeacherSelectedEventArg> OnTeacherSelected;

        public void RiseOnTeacherSelected(int? TeacherID,int? PersonID, int? UserID)
        {
            RiseOnTeacherSelected(new TeacherSelectedEventArg(TeacherID,PersonID, UserID));
        }

        public void RiseOnTeacherSelected(TeacherSelectedEventArg e)
        {
            OnTeacherSelected?.Invoke(this, e);
        }


    
        //refresh this Function To Give You back Teacher ID
        private void Frm_DataBack(object sender, int? TeacherID)
        {
            cbFilterBy.SelectedIndex = 0;

            clsTeachers Teacher = clsTeachers.FindTeacherByID(TeacherID);

            txtFilterBy.Text = Teacher.TeacherID.ToString();

            ctrlPersonInfo1.LoadPersonInfoByID(Teacher.PersonID);

            lbTeacherID.Text = Teacher.TeacherID.ToString();

            lbTeacherName.Text = Teacher._UserInfo.UserName;

            lbIsActive.Text = Teacher.IsActive ? "Yes" : "No";
        }

        // in Case Current User Is Searching By Teacher ID
        public bool LoadTeacherDataByTeacherID(int? TeacherID)
        {
            bool IsFound = false;

            _Taecher = clsTeachers.FindTeacherByID(TeacherID);

            if(_Taecher == null)
            {
                MessageBox.Show($"Error, Teacher With ID {TeacherID} Was Not Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IsFound;
            }

            IsFound = true;

            ctrlPersonInfo1.LoadPersonInfoByID(_Taecher.PersonID);

            lbTeacherID.Text = _Taecher.TeacherID.ToString();
            lbTeacherName.Text = _Taecher._UserInfo.UserName;

            lbIsActive.Text = _Taecher.IsActive ? "Yes" : "No";

            return IsFound;
        }

        // in Case Current User Is Searching By User ID
        public bool LoadTeacherDataByUserID(int? UserID)
        {
            bool IsFound = false;

            _Taecher = clsTeachers.FindTeacherByUserID(UserID);

            if (_Taecher == null)
            {
                MessageBox.Show($"Error, Teacher With User ID {UserID} Was Not Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IsFound;
            }

            IsFound = true;

            ctrlPersonInfo1.LoadPersonInfoByID(_Taecher.PersonID);

            lbTeacherID.Text = _Taecher.TeacherID.ToString();
            lbTeacherName.Text = _Taecher._UserInfo.UserName;

            lbIsActive.Text = _Taecher.IsActive ? "Yes" : "No";

            return IsFound;
        }

        // in Case Current User Is Searching By Person ID
        public bool LoadTeacherDataByPersonID(int? PersonID)
        {
            bool IsFound = false;

            _Taecher = clsTeachers.FindTeacherByPersonID(PersonID);

            if (_Taecher == null)
            {
                MessageBox.Show($"Error, Teacher With Person ID {PersonID} Was Not Found", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return IsFound;
            }

            IsFound = true;

            ctrlPersonInfo1.LoadPersonInfoByID(_Taecher.PersonID);

            lbTeacherID.Text = _Taecher.TeacherID.ToString();
            lbTeacherName.Text = _Taecher._UserInfo.UserName;

            lbIsActive.Text = _Taecher.IsActive ? "Yes" : "No";

            return IsFound;
        }

        private void btnSearchPerson_Click_1(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFilterBy.Text) || string.IsNullOrEmpty(txtFilterBy.Text))
            {
                MessageBox.Show("Please Enter Your ID First Then Click On Search Button.", "Warning",
                                                    MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            switch (cbFilterBy.SelectedIndex)
            {
                case 0:
                    _IsFind = LoadTeacherDataByTeacherID(int.Parse(txtFilterBy.Text));
                    break;

                case 1:
                    _IsFind = LoadTeacherDataByPersonID(int.Parse(txtFilterBy.Text));
                    break;


                case 2:
                    _IsFind = LoadTeacherDataByUserID(int.Parse(txtFilterBy.Text));
                    break;
            }

            if (OnTeacherSelected != null && _IsFind)
                RiseOnTeacherSelected(_Taecher.TeacherID,_Taecher.PersonID,_Taecher.UserID);
        }

        private void btnAddTeacher_Click(object sender, EventArgs e)
        {
         //   ShowAddEditeTeacher frm = new ShowAddEditeTeacher();

         ////   frm.DataBack += Frm_DataBack;

         //   frm.ShowDialog();
        }

        private void txtFilterBy_TextChanged_1(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtFilterBy.Text))
            {
                ctrlPersonInfo1.ResetPersonInfo();
                lbTeacherID.Text = "[???]";
                lbTeacherName.Text = "[???]";
                lbIsActive.Text = "[???]";
            }
        }

        private void txtFilterBy_KeyPress_1(object sender, KeyPressEventArgs e)
        {
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }

        private void ctrlSearchTeacherWithFilter_Load(object sender, EventArgs e)
        {
            cbFilterBy.SelectedIndex = 0;
            txtFilterBy.Select();
            txtFilterBy.Focus();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterBy.Clear();
            txtFilterBy.Focus();
        }

        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ctrlSearchTeacherWithFilter));
            this.ctrlPersonInfo1 = new IslamIsLife.People_Forms.Controls.ctrlPersonInfo();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.lbIsActive = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lbTeacherName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lbTeacherID = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.gbFilter = new Guna.UI2.WinForms.Guna2GroupBox();
            this.btnAddTeacher = new Guna.UI2.WinForms.Guna2CircleButton();
            this.btnSearchTeacher = new Guna.UI2.WinForms.Guna2CircleButton();
            this.txtFilterBy = new Guna.UI2.WinForms.Guna2TextBox();
            this.cbFilterBy = new Guna.UI2.WinForms.Guna2ComboBox();
            this.groupBox1.SuspendLayout();
            this.gbFilter.SuspendLayout();
            this.SuspendLayout();
            // 
            // ctrlPersonInfo1
            // 
            this.ctrlPersonInfo1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ctrlPersonInfo1.Location = new System.Drawing.Point(101, 133);
            this.ctrlPersonInfo1.Margin = new System.Windows.Forms.Padding(4);
            this.ctrlPersonInfo1.Name = "ctrlPersonInfo1";
            this.ctrlPersonInfo1.Size = new System.Drawing.Size(547, 270);
            this.ctrlPersonInfo1.TabIndex = 0;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.lbIsActive);
            this.groupBox1.Controls.Add(this.label7);
            this.groupBox1.Controls.Add(this.lbTeacherName);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label8);
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.lbTeacherID);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.groupBox1.Location = new System.Drawing.Point(18, 411);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(740, 67);
            this.groupBox1.TabIndex = 2;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Info";
            // 
            // lbIsActive
            // 
            this.lbIsActive.AutoSize = true;
            this.lbIsActive.ForeColor = System.Drawing.Color.Red;
            this.lbIsActive.Location = new System.Drawing.Point(673, 31);
            this.lbIsActive.Name = "lbIsActive";
            this.lbIsActive.Size = new System.Drawing.Size(41, 16);
            this.lbIsActive.TabIndex = 6;
            this.lbIsActive.Text = "[???]";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(578, 31);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(78, 16);
            this.label7.TabIndex = 5;
            this.label7.Text = "Is Active ?";
            // 
            // lbTeacherName
            // 
            this.lbTeacherName.AutoSize = true;
            this.lbTeacherName.ForeColor = System.Drawing.Color.Red;
            this.lbTeacherName.Location = new System.Drawing.Point(413, 31);
            this.lbTeacherName.Name = "lbTeacherName";
            this.lbTeacherName.Size = new System.Drawing.Size(41, 16);
            this.lbTeacherName.TabIndex = 4;
            this.lbTeacherName.Text = "[???]";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(238, 31);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(155, 16);
            this.label5.TabIndex = 3;
            this.label5.Text = "Teacher User Name :";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(210, 18);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(11, 32);
            this.label8.TabIndex = 2;
            this.label8.Text = "|\r\n|\r\n";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(537, 18);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(11, 32);
            this.label3.TabIndex = 2;
            this.label3.Text = "|\r\n|\r\n";
            // 
            // lbTeacherID
            // 
            this.lbTeacherID.AutoSize = true;
            this.lbTeacherID.ForeColor = System.Drawing.Color.Red;
            this.lbTeacherID.Location = new System.Drawing.Point(141, 31);
            this.lbTeacherID.Name = "lbTeacherID";
            this.lbTeacherID.Size = new System.Drawing.Size(41, 16);
            this.lbTeacherID.TabIndex = 1;
            this.lbTeacherID.Text = "[???]";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 16);
            this.label1.TabIndex = 0;
            this.label1.Text = "Teacher ID :";
            // 
            // gbFilter
            // 
            this.gbFilter.BorderColor = System.Drawing.Color.Lime;
            this.gbFilter.BorderRadius = 10;
            this.gbFilter.Controls.Add(this.btnAddTeacher);
            this.gbFilter.Controls.Add(this.btnSearchTeacher);
            this.gbFilter.Controls.Add(this.txtFilterBy);
            this.gbFilter.Controls.Add(this.cbFilterBy);
            this.gbFilter.CustomBorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(192)))));
            this.gbFilter.Font = new System.Drawing.Font("Segoe UI Historic", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.gbFilter.ForeColor = System.Drawing.Color.Black;
            this.gbFilter.Location = new System.Drawing.Point(101, 3);
            this.gbFilter.Name = "gbFilter";
            this.gbFilter.Size = new System.Drawing.Size(547, 123);
            this.gbFilter.TabIndex = 3;
            this.gbFilter.Text = "Filter By :";
            // 
            // btnAddTeacher
            // 
            this.btnAddTeacher.Animated = true;
            this.btnAddTeacher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.btnAddTeacher.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnAddTeacher.BackgroundImage")));
            this.btnAddTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnAddTeacher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnAddTeacher.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnAddTeacher.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnAddTeacher.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnAddTeacher.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnAddTeacher.FillColor = System.Drawing.Color.Transparent;
            this.btnAddTeacher.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnAddTeacher.ForeColor = System.Drawing.Color.White;
            this.btnAddTeacher.IndicateFocus = true;
            this.btnAddTeacher.Location = new System.Drawing.Point(424, 49);
            this.btnAddTeacher.Name = "btnAddTeacher";
            this.btnAddTeacher.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnAddTeacher.Size = new System.Drawing.Size(48, 46);
            this.btnAddTeacher.TabIndex = 3;
            this.btnAddTeacher.Click += new System.EventHandler(this.btnAddTeacher_Click);
            // 
            // btnSearchTeacher
            // 
            this.btnSearchTeacher.Animated = true;
            this.btnSearchTeacher.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.btnSearchTeacher.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("btnSearchTeacher.BackgroundImage")));
            this.btnSearchTeacher.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnSearchTeacher.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnSearchTeacher.DisabledState.BorderColor = System.Drawing.Color.DarkGray;
            this.btnSearchTeacher.DisabledState.CustomBorderColor = System.Drawing.Color.DarkGray;
            this.btnSearchTeacher.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(169)))), ((int)(((byte)(169)))), ((int)(((byte)(169)))));
            this.btnSearchTeacher.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(141)))), ((int)(((byte)(141)))), ((int)(((byte)(141)))));
            this.btnSearchTeacher.FillColor = System.Drawing.Color.Transparent;
            this.btnSearchTeacher.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.btnSearchTeacher.ForeColor = System.Drawing.Color.White;
            this.btnSearchTeacher.IndicateFocus = true;
            this.btnSearchTeacher.Location = new System.Drawing.Point(357, 49);
            this.btnSearchTeacher.Name = "btnSearchTeacher";
            this.btnSearchTeacher.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            this.btnSearchTeacher.Size = new System.Drawing.Size(48, 46);
            this.btnSearchTeacher.TabIndex = 2;
            this.btnSearchTeacher.Click += new System.EventHandler(this.btnSearchPerson_Click_1);
            // 
            // txtFilterBy
            // 
            this.txtFilterBy.BorderColor = System.Drawing.Color.Red;
            this.txtFilterBy.BorderRadius = 5;
            this.txtFilterBy.BorderThickness = 2;
            this.txtFilterBy.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFilterBy.DefaultText = "";
            this.txtFilterBy.DisabledState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(208)))), ((int)(((byte)(208)))), ((int)(((byte)(208)))));
            this.txtFilterBy.DisabledState.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(226)))), ((int)(((byte)(226)))), ((int)(((byte)(226)))));
            this.txtFilterBy.DisabledState.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.DisabledState.PlaceholderForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(138)))), ((int)(((byte)(138)))), ((int)(((byte)(138)))));
            this.txtFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.txtFilterBy.ForeColor = System.Drawing.Color.Black;
            this.txtFilterBy.HoverState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.txtFilterBy.Location = new System.Drawing.Point(172, 59);
            this.txtFilterBy.Name = "txtFilterBy";
            this.txtFilterBy.PasswordChar = '\0';
            this.txtFilterBy.PlaceholderText = "Enter The ID ";
            this.txtFilterBy.SelectedText = "";
            this.txtFilterBy.Size = new System.Drawing.Size(158, 36);
            this.txtFilterBy.TabIndex = 1;
            this.txtFilterBy.TextChanged += new System.EventHandler(this.txtFilterBy_TextChanged_1);
            this.txtFilterBy.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.txtFilterBy_KeyPress_1);
            // 
            // cbFilterBy
            // 
            this.cbFilterBy.BackColor = System.Drawing.Color.Transparent;
            this.cbFilterBy.BorderRadius = 5;
            this.cbFilterBy.BorderThickness = 3;
            this.cbFilterBy.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cbFilterBy.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbFilterBy.FocusedColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.FocusedState.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(94)))), ((int)(((byte)(148)))), ((int)(((byte)(255)))));
            this.cbFilterBy.Font = new System.Drawing.Font("Segoe UI Symbol", 9.75F, System.Drawing.FontStyle.Bold);
            this.cbFilterBy.ForeColor = System.Drawing.Color.Black;
            this.cbFilterBy.ItemHeight = 30;
            this.cbFilterBy.Items.AddRange(new object[] {
            "Teacher ID",
            "Person ID",
            "User ID"});
            this.cbFilterBy.Location = new System.Drawing.Point(15, 59);
            this.cbFilterBy.Name = "cbFilterBy";
            this.cbFilterBy.Size = new System.Drawing.Size(151, 36);
            this.cbFilterBy.TabIndex = 0;
            this.cbFilterBy.SelectedIndexChanged += new System.EventHandler(this.cbFilterBy_SelectedIndexChanged);
            // 
            // ctrlSearchTeacherWithFilter
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.gbFilter);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.ctrlPersonInfo1);
            this.Name = "ctrlSearchTeacherWithFilter";
            this.Size = new System.Drawing.Size(777, 481);
            this.Load += new System.EventHandler(this.ctrlSearchTeacherWithFilter_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.gbFilter.ResumeLayout(false);
            this.ResumeLayout(false);

        }
    }
}
