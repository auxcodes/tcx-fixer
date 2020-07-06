namespace TcxFixerGui
{
    partial class MainWindow
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
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.selectFileButton = new System.Windows.Forms.Button();
            this.pathTextBox = new System.Windows.Forms.TextBox();
            this.startButton = new System.Windows.Forms.Button();
            this.outputTextBox = new System.Windows.Forms.TextBox();
            this.selectFolderButton = new System.Windows.Forms.Button();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "tcx";
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // selectFileButton
            // 
            this.selectFileButton.AutoSize = true;
            this.selectFileButton.Location = new System.Drawing.Point(18, 20);
            this.selectFileButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(112, 35);
            this.selectFileButton.TabIndex = 0;
            this.selectFileButton.Text = "Select File";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.selectFileButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(18, 65);
            this.pathTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(1162, 26);
            this.pathTextBox.TabIndex = 1;
            this.pathTextBox.Text = "File/Folder...";
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(1068, 101);
            this.startButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(112, 35);
            this.startButton.TabIndex = 2;
            this.startButton.Text = "Start";
            this.startButton.UseVisualStyleBackColor = true;
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(20, 146);
            this.outputTextBox.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.Size = new System.Drawing.Size(1160, 525);
            this.outputTextBox.TabIndex = 3;
            this.outputTextBox.Text = "Output...";
            // 
            // selectFolderButton
            // 
            this.selectFolderButton.AutoSize = true;
            this.selectFolderButton.Location = new System.Drawing.Point(138, 20);
            this.selectFolderButton.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.selectFolderButton.Name = "selectFolderButton";
            this.selectFolderButton.Size = new System.Drawing.Size(113, 35);
            this.selectFolderButton.TabIndex = 4;
            this.selectFolderButton.Text = "Select Folder";
            this.selectFolderButton.UseVisualStyleBackColor = true;
            this.selectFolderButton.Click += new System.EventHandler(this.selectFolderButton_Click);
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select folder containing .tcx files to be updated.";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1200, 692);
            this.Controls.Add(this.selectFolderButton);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.selectFileButton);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "MainWindow";
            this.Text = "Tcx Fixer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button selectFileButton;
        private System.Windows.Forms.TextBox pathTextBox;
        private System.Windows.Forms.Button startButton;
        private System.Windows.Forms.TextBox outputTextBox;
        private System.Windows.Forms.Button selectFolderButton;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}

