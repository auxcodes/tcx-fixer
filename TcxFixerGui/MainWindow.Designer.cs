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
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.selectFilesRadio = new System.Windows.Forms.RadioButton();
            this.selectFolderRadio = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.DefaultExt = "tcx";
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Multiselect = true;
            // 
            // selectFileButton
            // 
            this.selectFileButton.AutoSize = true;
            this.selectFileButton.Location = new System.Drawing.Point(13, 39);
            this.selectFileButton.Name = "selectFileButton";
            this.selectFileButton.Size = new System.Drawing.Size(79, 23);
            this.selectFileButton.TabIndex = 3;
            this.selectFileButton.Text = "Select Folder";
            this.selectFileButton.UseVisualStyleBackColor = true;
            this.selectFileButton.Click += new System.EventHandler(this.SelectFileButton_Click);
            // 
            // pathTextBox
            // 
            this.pathTextBox.Location = new System.Drawing.Point(98, 41);
            this.pathTextBox.MaximumSize = new System.Drawing.Size(900, 20);
            this.pathTextBox.MinimumSize = new System.Drawing.Size(250, 20);
            this.pathTextBox.Name = "pathTextBox";
            this.pathTextBox.Size = new System.Drawing.Size(690, 20);
            this.pathTextBox.TabIndex = 3;
            this.pathTextBox.Text = "C:\\Path\\To\\Folder...";
            this.pathTextBox.TextChanged += new System.EventHandler(this.PathTextBox_TextChanged);
            // 
            // startButton
            // 
            this.startButton.Location = new System.Drawing.Point(713, 415);
            this.startButton.Name = "startButton";
            this.startButton.Size = new System.Drawing.Size(75, 23);
            this.startButton.TabIndex = 3;
            this.startButton.Text = "Fix Files";
            this.startButton.UseVisualStyleBackColor = true;
            this.startButton.Click += new System.EventHandler(this.StartButton_Click);
            // 
            // outputTextBox
            // 
            this.outputTextBox.Location = new System.Drawing.Point(13, 67);
            this.outputTextBox.MaximumSize = new System.Drawing.Size(900, 900);
            this.outputTextBox.MinimumSize = new System.Drawing.Size(250, 250);
            this.outputTextBox.Multiline = true;
            this.outputTextBox.Name = "outputTextBox";
            this.outputTextBox.ReadOnly = true;
            this.outputTextBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.outputTextBox.Size = new System.Drawing.Size(775, 343);
            this.outputTextBox.TabIndex = 4;
            this.outputTextBox.Text = "Output...";
            // 
            // folderBrowserDialog1
            // 
            this.folderBrowserDialog1.Description = "Select folder containing .tcx files to be updated.";
            this.folderBrowserDialog1.ShowNewFolderButton = false;
            // 
            // selectFilesRadio
            // 
            this.selectFilesRadio.AutoSize = true;
            this.selectFilesRadio.Location = new System.Drawing.Point(106, 16);
            this.selectFilesRadio.Name = "selectFilesRadio";
            this.selectFilesRadio.Size = new System.Drawing.Size(79, 17);
            this.selectFilesRadio.TabIndex = 1;
            this.selectFilesRadio.Text = "Select Files";
            this.selectFilesRadio.UseVisualStyleBackColor = true;
            this.selectFilesRadio.CheckedChanged += new System.EventHandler(this.SelectFilesRadio_CheckedChanged);
            // 
            // selectFolderRadio
            // 
            this.selectFolderRadio.AutoSize = true;
            this.selectFolderRadio.Checked = true;
            this.selectFolderRadio.Location = new System.Drawing.Point(13, 16);
            this.selectFolderRadio.Name = "selectFolderRadio";
            this.selectFolderRadio.Size = new System.Drawing.Size(87, 17);
            this.selectFolderRadio.TabIndex = 0;
            this.selectFolderRadio.TabStop = true;
            this.selectFolderRadio.Text = "Select Folder";
            this.selectFolderRadio.UseVisualStyleBackColor = true;
            this.selectFolderRadio.CheckedChanged += new System.EventHandler(this.SelectFolderRadio_CheckedChanged);
            // 
            // MainWindow
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.selectFolderRadio);
            this.Controls.Add(this.selectFilesRadio);
            this.Controls.Add(this.outputTextBox);
            this.Controls.Add(this.startButton);
            this.Controls.Add(this.pathTextBox);
            this.Controls.Add(this.selectFileButton);
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
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.RadioButton selectFilesRadio;
        private System.Windows.Forms.RadioButton selectFolderRadio;
    }
}

