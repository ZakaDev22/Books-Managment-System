namespace IslamIsLife.People_Forms
{
    partial class ShowManagePeopleForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ctrlPeople1 = new IslamIsLife.MainControls.ctrlPeople();
            this.SuspendLayout();
            // 
            // ctrlPeople1
            // 
            this.ctrlPeople1.BackColor = System.Drawing.Color.White;
            this.ctrlPeople1.Location = new System.Drawing.Point(15, 29);
            this.ctrlPeople1.Name = "ctrlPeople1";
            this.ctrlPeople1.Size = new System.Drawing.Size(873, 464);
            this.ctrlPeople1.TabIndex = 0;
            // 
            // ShowManagePeopleForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 550);
            this.Controls.Add(this.ctrlPeople1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowManagePeopleForm";
            this.Text = "ShowManagePeopleForm";
            this.ResumeLayout(false);

        }

        #endregion

        private MainControls.ctrlPeople ctrlPeople1;
    }
}