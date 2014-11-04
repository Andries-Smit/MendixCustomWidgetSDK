namespace MendixWidgets
{
    partial class FrmWidget
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmWidget));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSource = new System.Windows.Forms.TextBox();
            this.txtDestination = new System.Windows.Forms.TextBox();
            this.btnSetSource = new System.Windows.Forms.Button();
            this.btnSetDestination = new System.Windows.Forms.Button();
            this.fldBrowseSource = new System.Windows.Forms.FolderBrowserDialog();
            this.fldBrowseDestination = new System.Windows.Forms.FolderBrowserDialog();
            this.label3 = new System.Windows.Forms.Label();
            this.flsWatchDirectory = new System.IO.FileSystemWatcher();
            this.txtWidgetName = new System.Windows.Forms.TextBox();
            this.chkWatch = new System.Windows.Forms.CheckBox();
            this.label4 = new System.Windows.Forms.Label();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.chkIgnoreDirectories = new System.Windows.Forms.CheckBox();
            this.txtIgnoreDirectories = new System.Windows.Forms.TextBox();
            this.lblWidgetName = new System.Windows.Forms.Label();
            this.flsWatchDestDirectory = new System.IO.FileSystemWatcher();
            this.txtIgnoreFiles = new System.Windows.Forms.TextBox();
            this.chkIgnoreFiles = new System.Windows.Forms.CheckBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabPage3 = new System.Windows.Forms.TabPage();
            this.txtChanges = new System.Windows.Forms.TextBox();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.txtWidgetJS = new System.Windows.Forms.TextBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.txtWidgetCSS = new System.Windows.Forms.TextBox();
            this.btnCreateWidget2 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.flsWatchDirectory)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.flsWatchDestDirectory)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabPage3.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(17, 131);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Source folder:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 188);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Destination folder *:";
            // 
            // txtSource
            // 
            this.txtSource.Location = new System.Drawing.Point(19, 160);
            this.txtSource.Margin = new System.Windows.Forms.Padding(2);
            this.txtSource.Name = "txtSource";
            this.txtSource.Size = new System.Drawing.Size(230, 20);
            this.txtSource.TabIndex = 4;
            this.txtSource.TextChanged += new System.EventHandler(this.txtSource_TextChanged);
            this.txtSource.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtSource_KeyUp);
            // 
            // txtDestination
            // 
            this.txtDestination.Location = new System.Drawing.Point(19, 213);
            this.txtDestination.Margin = new System.Windows.Forms.Padding(2);
            this.txtDestination.Name = "txtDestination";
            this.txtDestination.Size = new System.Drawing.Size(229, 20);
            this.txtDestination.TabIndex = 5;
            this.txtDestination.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDestination_KeyUp);
            // 
            // btnSetSource
            // 
            this.btnSetSource.Location = new System.Drawing.Point(252, 160);
            this.btnSetSource.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetSource.Name = "btnSetSource";
            this.btnSetSource.Size = new System.Drawing.Size(94, 20);
            this.btnSetSource.TabIndex = 7;
            this.btnSetSource.Text = "browse";
            this.btnSetSource.UseVisualStyleBackColor = true;
            this.btnSetSource.Click += new System.EventHandler(this.btnSetSource_Click);
            // 
            // btnSetDestination
            // 
            this.btnSetDestination.Location = new System.Drawing.Point(252, 213);
            this.btnSetDestination.Margin = new System.Windows.Forms.Padding(2);
            this.btnSetDestination.Name = "btnSetDestination";
            this.btnSetDestination.Size = new System.Drawing.Size(94, 20);
            this.btnSetDestination.TabIndex = 8;
            this.btnSetDestination.Text = "browse";
            this.btnSetDestination.UseVisualStyleBackColor = true;
            this.btnSetDestination.Click += new System.EventHandler(this.btnSetDestination_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 73);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(73, 13);
            this.label3.TabIndex = 9;
            this.label3.Text = "Widget name:";
            // 
            // flsWatchDirectory
            // 
            this.flsWatchDirectory.EnableRaisingEvents = true;
            this.flsWatchDirectory.SynchronizingObject = this;
            // 
            // txtWidgetName
            // 
            this.txtWidgetName.Location = new System.Drawing.Point(19, 102);
            this.txtWidgetName.Margin = new System.Windows.Forms.Padding(2);
            this.txtWidgetName.Name = "txtWidgetName";
            this.txtWidgetName.Size = new System.Drawing.Size(228, 20);
            this.txtWidgetName.TabIndex = 0;
            this.txtWidgetName.TextChanged += new System.EventHandler(this.txtWidgetName_TextChanged);
            this.txtWidgetName.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtWidgetName_KeyUp);
            // 
            // chkWatch
            // 
            this.chkWatch.AutoSize = true;
            this.chkWatch.Location = new System.Drawing.Point(17, 280);
            this.chkWatch.Margin = new System.Windows.Forms.Padding(2);
            this.chkWatch.Name = "chkWatch";
            this.chkWatch.Size = new System.Drawing.Size(324, 17);
            this.chkWatch.TabIndex = 11;
            this.chkWatch.Text = "Watch the source directory and create a widget on file change.";
            this.chkWatch.UseVisualStyleBackColor = true;
            this.chkWatch.CheckedChanged += new System.EventHandler(this.chkWatch_CheckedChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 7.8F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.DimGray;
            this.label4.Location = new System.Drawing.Point(15, 241);
            this.label4.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(152, 13);
            this.label4.TabIndex = 13;
            this.label4.Text = "* just select the project folder...";
            // 
            // openFileDialog
            // 
            this.openFileDialog.FileName = "*.mws";
            this.openFileDialog.Filter = "*.mws|";
            this.openFileDialog.InitialDirectory = "%documents%";
            this.openFileDialog.RestoreDirectory = true;
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "*.mws|";
            this.saveFileDialog.InitialDirectory = "%documents%";
            this.saveFileDialog.RestoreDirectory = true;
            // 
            // chkIgnoreDirectories
            // 
            this.chkIgnoreDirectories.AutoSize = true;
            this.chkIgnoreDirectories.Checked = true;
            this.chkIgnoreDirectories.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkIgnoreDirectories.Location = new System.Drawing.Point(17, 309);
            this.chkIgnoreDirectories.Margin = new System.Windows.Forms.Padding(2);
            this.chkIgnoreDirectories.Name = "chkIgnoreDirectories";
            this.chkIgnoreDirectories.Size = new System.Drawing.Size(110, 17);
            this.chkIgnoreDirectories.TabIndex = 18;
            this.chkIgnoreDirectories.Text = "Ignore directories:";
            this.chkIgnoreDirectories.UseVisualStyleBackColor = true;
            // 
            // txtIgnoreDirectories
            // 
            this.txtIgnoreDirectories.Location = new System.Drawing.Point(147, 307);
            this.txtIgnoreDirectories.Margin = new System.Windows.Forms.Padding(2);
            this.txtIgnoreDirectories.Name = "txtIgnoreDirectories";
            this.txtIgnoreDirectories.Size = new System.Drawing.Size(200, 20);
            this.txtIgnoreDirectories.TabIndex = 19;
            this.txtIgnoreDirectories.Text = ".git,.idea,.svn";
            // 
            // lblWidgetName
            // 
            this.lblWidgetName.AutoSize = true;
            this.lblWidgetName.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.2F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWidgetName.Location = new System.Drawing.Point(13, 22);
            this.lblWidgetName.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblWidgetName.Name = "lblWidgetName";
            this.lblWidgetName.Size = new System.Drawing.Size(30, 26);
            this.lblWidgetName.TabIndex = 20;
            this.lblWidgetName.Text = "...";
            // 
            // flsWatchDestDirectory
            // 
            this.flsWatchDestDirectory.EnableRaisingEvents = true;
            this.flsWatchDestDirectory.SynchronizingObject = this;
            // 
            // txtIgnoreFiles
            // 
            this.txtIgnoreFiles.Location = new System.Drawing.Point(147, 337);
            this.txtIgnoreFiles.Margin = new System.Windows.Forms.Padding(2);
            this.txtIgnoreFiles.Name = "txtIgnoreFiles";
            this.txtIgnoreFiles.Size = new System.Drawing.Size(200, 20);
            this.txtIgnoreFiles.TabIndex = 22;
            this.txtIgnoreFiles.Text = ".js___jb_bak___,.js___jb_old___";
            // 
            // chkIgnoreFiles
            // 
            this.chkIgnoreFiles.AutoSize = true;
            this.chkIgnoreFiles.Location = new System.Drawing.Point(17, 339);
            this.chkIgnoreFiles.Margin = new System.Windows.Forms.Padding(2);
            this.chkIgnoreFiles.Name = "chkIgnoreFiles";
            this.chkIgnoreFiles.Size = new System.Drawing.Size(126, 17);
            this.chkIgnoreFiles.TabIndex = 21;
            this.chkIgnoreFiles.Text = "Ignore file extentions:";
            this.chkIgnoreFiles.UseVisualStyleBackColor = true;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.tabControl1);
            this.groupBox2.Location = new System.Drawing.Point(353, 73);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(390, 329);
            this.groupBox2.TabIndex = 23;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Compiled widget.js and widget.css";
            this.groupBox2.Visible = false;
            // 
            // tabControl1
            // 
            this.tabControl1.Controls.Add(this.tabPage3);
            this.tabControl1.Controls.Add(this.tabPage1);
            this.tabControl1.Controls.Add(this.tabPage2);
            this.tabControl1.Location = new System.Drawing.Point(7, 20);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(377, 303);
            this.tabControl1.TabIndex = 0;
            // 
            // tabPage3
            // 
            this.tabPage3.Controls.Add(this.txtChanges);
            this.tabPage3.Location = new System.Drawing.Point(4, 22);
            this.tabPage3.Name = "tabPage3";
            this.tabPage3.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage3.Size = new System.Drawing.Size(369, 277);
            this.tabPage3.TabIndex = 2;
            this.tabPage3.Text = "changes made";
            this.tabPage3.UseVisualStyleBackColor = true;
            // 
            // txtChanges
            // 
            this.txtChanges.BackColor = System.Drawing.Color.Gainsboro;
            this.txtChanges.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtChanges.Font = new System.Drawing.Font("Verdana", 7.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtChanges.Location = new System.Drawing.Point(3, 3);
            this.txtChanges.Margin = new System.Windows.Forms.Padding(2);
            this.txtChanges.Multiline = true;
            this.txtChanges.Name = "txtChanges";
            this.txtChanges.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtChanges.Size = new System.Drawing.Size(363, 271);
            this.txtChanges.TabIndex = 1;
            this.txtChanges.WordWrap = false;
            // 
            // tabPage1
            // 
            this.tabPage1.Controls.Add(this.txtWidgetJS);
            this.tabPage1.Location = new System.Drawing.Point(4, 22);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(369, 277);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "widget.js";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // txtWidgetJS
            // 
            this.txtWidgetJS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWidgetJS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidgetJS.Location = new System.Drawing.Point(3, 3);
            this.txtWidgetJS.Multiline = true;
            this.txtWidgetJS.Name = "txtWidgetJS";
            this.txtWidgetJS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWidgetJS.Size = new System.Drawing.Size(363, 271);
            this.txtWidgetJS.TabIndex = 0;
            this.txtWidgetJS.WordWrap = false;
            // 
            // tabPage2
            // 
            this.tabPage2.Controls.Add(this.txtWidgetCSS);
            this.tabPage2.Location = new System.Drawing.Point(4, 22);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(369, 277);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "widget.css";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // txtWidgetCSS
            // 
            this.txtWidgetCSS.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtWidgetCSS.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtWidgetCSS.Location = new System.Drawing.Point(3, 3);
            this.txtWidgetCSS.Multiline = true;
            this.txtWidgetCSS.Name = "txtWidgetCSS";
            this.txtWidgetCSS.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.txtWidgetCSS.Size = new System.Drawing.Size(363, 271);
            this.txtWidgetCSS.TabIndex = 1;
            this.txtWidgetCSS.WordWrap = false;
            // 
            // btnCreateWidget2
            // 
            this.btnCreateWidget2.BackColor = System.Drawing.Color.Transparent;
            this.btnCreateWidget2.BackgroundImage = global::MendixWidgets.Properties.Resources.btnCreateWidget;
            this.btnCreateWidget2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btnCreateWidget2.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCreateWidget2.Location = new System.Drawing.Point(567, 408);
            this.btnCreateWidget2.Name = "btnCreateWidget2";
            this.btnCreateWidget2.Size = new System.Drawing.Size(176, 55);
            this.btnCreateWidget2.TabIndex = 24;
            this.btnCreateWidget2.Click += new System.EventHandler(this.btnCreateWidget2_Click);
            // 
            // FrmWidget
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::MendixWidgets.Properties.Resources.widget;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(755, 466);
            this.Controls.Add(this.btnCreateWidget2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.txtIgnoreFiles);
            this.Controls.Add(this.chkIgnoreFiles);
            this.Controls.Add(this.lblWidgetName);
            this.Controls.Add(this.txtIgnoreDirectories);
            this.Controls.Add(this.chkIgnoreDirectories);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.chkWatch);
            this.Controls.Add(this.txtWidgetName);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnSetDestination);
            this.Controls.Add(this.btnSetSource);
            this.Controls.Add(this.txtDestination);
            this.Controls.Add(this.txtSource);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(2);
            this.MaximumSize = new System.Drawing.Size(771, 505);
            this.MinimumSize = new System.Drawing.Size(771, 505);
            this.Name = "FrmWidget";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "...";
            this.Load += new System.EventHandler(this.Main_Load);
            ((System.ComponentModel.ISupportInitialize)(this.flsWatchDirectory)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.flsWatchDestDirectory)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.tabControl1.ResumeLayout(false);
            this.tabPage3.ResumeLayout(false);
            this.tabPage3.PerformLayout();
            this.tabPage1.ResumeLayout(false);
            this.tabPage1.PerformLayout();
            this.tabPage2.ResumeLayout(false);
            this.tabPage2.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSource;
        private System.Windows.Forms.TextBox txtDestination;
        private System.Windows.Forms.Button btnSetSource;
        private System.Windows.Forms.Button btnSetDestination;
        private System.Windows.Forms.FolderBrowserDialog fldBrowseSource;
        private System.Windows.Forms.FolderBrowserDialog fldBrowseDestination;
        private System.Windows.Forms.Label label3;
        private System.IO.FileSystemWatcher flsWatchDirectory;
        private System.Windows.Forms.TextBox txtWidgetName;
        private System.Windows.Forms.CheckBox chkWatch;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.TextBox txtIgnoreDirectories;
        private System.Windows.Forms.CheckBox chkIgnoreDirectories;
        private System.Windows.Forms.Label lblWidgetName;
        private System.IO.FileSystemWatcher flsWatchDestDirectory;
        private System.Windows.Forms.TextBox txtIgnoreFiles;
        private System.Windows.Forms.CheckBox chkIgnoreFiles;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TextBox txtWidgetJS;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.TextBox txtWidgetCSS;
        private System.Windows.Forms.TabPage tabPage3;
        private System.Windows.Forms.TextBox txtChanges;
        private System.Windows.Forms.Panel btnCreateWidget2;
    }
}