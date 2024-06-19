namespace IslamIsLife.Books_Forms
{
    partial class ShowManageBooksForm
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
            this.ctrlBooks1 = new IslamIsLife.Main_And_Login_Forms.MainControls.ctrlBooks();
            this.SuspendLayout();
            // 
            // ctrlBooks1
            // 
            this.ctrlBooks1.Location = new System.Drawing.Point(15, 28);
            this.ctrlBooks1.Name = "ctrlBooks1";
            this.ctrlBooks1.Size = new System.Drawing.Size(873, 464);
            this.ctrlBooks1.TabIndex = 0;
            // 
            // ShowManageBooksForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 581);
            this.Controls.Add(this.ctrlBooks1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "ShowManageBooksForm";
            this.Text = "ShowManageBooksForm";
            this.ResumeLayout(false);

        }

        #endregion

        private Main_And_Login_Forms.MainControls.ctrlBooks ctrlBooks1;
    }
}