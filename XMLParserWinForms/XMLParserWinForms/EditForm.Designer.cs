namespace XMLParserWinForms
{
    partial class EditForm
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
            this.NameLabel = new System.Windows.Forms.Label();
            this.NameTextBox = new System.Windows.Forms.TextBox();
            this.ParamsTextBox = new System.Windows.Forms.TextBox();
            this.ParamsLabel = new System.Windows.Forms.Label();
            this.PackageTextBox = new System.Windows.Forms.TextBox();
            this.PackageLabel = new System.Windows.Forms.Label();
            this.TimeTextBox = new System.Windows.Forms.TextBox();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.MsLabel = new System.Windows.Forms.Label();
            this.OkButton = new System.Windows.Forms.Button();
            this.CancelButton = new System.Windows.Forms.Button();
            this.ResetButton = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // NameLabel
            // 
            this.NameLabel.AutoSize = true;
            this.NameLabel.Location = new System.Drawing.Point(12, 15);
            this.NameLabel.Name = "NameLabel";
            this.NameLabel.Size = new System.Drawing.Size(75, 13);
            this.NameLabel.TabIndex = 0;
            this.NameLabel.Text = "Method name:";
            // 
            // NameTextBox
            // 
            this.NameTextBox.Location = new System.Drawing.Point(93, 12);
            this.NameTextBox.Name = "NameTextBox";
            this.NameTextBox.Size = new System.Drawing.Size(179, 20);
            this.NameTextBox.TabIndex = 1;
            // 
            // ParamsTextBox
            // 
            this.ParamsTextBox.Location = new System.Drawing.Point(93, 54);
            this.ParamsTextBox.Name = "ParamsTextBox";
            this.ParamsTextBox.Size = new System.Drawing.Size(179, 20);
            this.ParamsTextBox.TabIndex = 3;
            // 
            // ParamsLabel
            // 
            this.ParamsLabel.AutoSize = true;
            this.ParamsLabel.Location = new System.Drawing.Point(12, 57);
            this.ParamsLabel.Name = "ParamsLabel";
            this.ParamsLabel.Size = new System.Drawing.Size(75, 13);
            this.ParamsLabel.TabIndex = 2;
            this.ParamsLabel.Text = "Params count:";
            // 
            // PackageTextBox
            // 
            this.PackageTextBox.Location = new System.Drawing.Point(93, 80);
            this.PackageTextBox.Name = "PackageTextBox";
            this.PackageTextBox.Size = new System.Drawing.Size(179, 20);
            this.PackageTextBox.TabIndex = 5;
            // 
            // PackageLabel
            // 
            this.PackageLabel.AutoSize = true;
            this.PackageLabel.Location = new System.Drawing.Point(12, 83);
            this.PackageLabel.Name = "PackageLabel";
            this.PackageLabel.Size = new System.Drawing.Size(53, 13);
            this.PackageLabel.TabIndex = 4;
            this.PackageLabel.Text = "Package:";
            // 
            // TimeTextBox
            // 
            this.TimeTextBox.Location = new System.Drawing.Point(93, 107);
            this.TimeTextBox.Name = "TimeTextBox";
            this.TimeTextBox.Size = new System.Drawing.Size(131, 20);
            this.TimeTextBox.TabIndex = 7;
            this.TimeTextBox.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // TimeLabel
            // 
            this.TimeLabel.AutoSize = true;
            this.TimeLabel.Location = new System.Drawing.Point(12, 110);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(33, 13);
            this.TimeLabel.TabIndex = 6;
            this.TimeLabel.Text = "Time:";
            // 
            // MsLabel
            // 
            this.MsLabel.AutoSize = true;
            this.MsLabel.Location = new System.Drawing.Point(230, 110);
            this.MsLabel.Name = "MsLabel";
            this.MsLabel.Size = new System.Drawing.Size(20, 13);
            this.MsLabel.TabIndex = 8;
            this.MsLabel.Text = "ms";
            // 
            // OkButton
            // 
            this.OkButton.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.OkButton.Location = new System.Drawing.Point(15, 157);
            this.OkButton.Name = "OkButton";
            this.OkButton.Size = new System.Drawing.Size(75, 23);
            this.OkButton.TabIndex = 9;
            this.OkButton.Text = "OK";
            this.OkButton.UseVisualStyleBackColor = true;
            // 
            // CancelButton
            // 
            this.CancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.CancelButton.Location = new System.Drawing.Point(96, 157);
            this.CancelButton.Name = "CancelButton";
            this.CancelButton.Size = new System.Drawing.Size(75, 23);
            this.CancelButton.TabIndex = 10;
            this.CancelButton.Text = "Cancel";
            this.CancelButton.UseVisualStyleBackColor = true;
            // 
            // ResetButton
            // 
            this.ResetButton.Location = new System.Drawing.Point(197, 157);
            this.ResetButton.Name = "ResetButton";
            this.ResetButton.Size = new System.Drawing.Size(75, 23);
            this.ResetButton.TabIndex = 11;
            this.ResetButton.Text = "Reset";
            this.ResetButton.UseVisualStyleBackColor = true;
            this.ResetButton.Click += new System.EventHandler(this.ResetButton_Click);
            // 
            // EditForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(284, 195);
            this.ControlBox = false;
            this.Controls.Add(this.ResetButton);
            this.Controls.Add(this.CancelButton);
            this.Controls.Add(this.OkButton);
            this.Controls.Add(this.MsLabel);
            this.Controls.Add(this.TimeTextBox);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.PackageTextBox);
            this.Controls.Add(this.PackageLabel);
            this.Controls.Add(this.ParamsTextBox);
            this.Controls.Add(this.ParamsLabel);
            this.Controls.Add(this.NameTextBox);
            this.Controls.Add(this.NameLabel);
            this.Name = "EditForm";
            this.Text = "Edit method";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label NameLabel;
        private System.Windows.Forms.TextBox NameTextBox;
        private System.Windows.Forms.TextBox ParamsTextBox;
        private System.Windows.Forms.Label ParamsLabel;
        private System.Windows.Forms.TextBox PackageTextBox;
        private System.Windows.Forms.Label PackageLabel;
        private System.Windows.Forms.TextBox TimeTextBox;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Label MsLabel;
        private System.Windows.Forms.Button OkButton;
        private System.Windows.Forms.Button CancelButton;
        private System.Windows.Forms.Button ResetButton;

    }
}