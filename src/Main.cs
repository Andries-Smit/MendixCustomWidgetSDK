using System;
using System.Windows.Forms;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;

namespace MendixWidgets
{

    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    public partial class Main : Form
    { 

        public Main()
        {
            InitializeComponent();
        }

        #region Menu

        public void openBtn()
        {
            if (openFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(openFileDialog.FileName))
                {
                    FrmWidget form = new FrmWidget(openFileDialog.FileName);
                    form.MdiParent = this;
                    form.Show();
                }
            }
        }
        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openBtn();
        }
        private void btnOpen_Click(object sender, EventArgs e)
        {
            openBtn();
        }

        public void newBtn()
        {
            FrmWidget form = new FrmWidget(string.Empty);
            form.MdiParent = this;
            form.Show();
            statusLabel.Text = "New settings completed...";
        }
        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            newBtn();
        }
        private void btnNew_Click(object sender, EventArgs e)
        {
            newBtn();
        }

        public void saveBtn()
        {
            if (this.ActiveMdiChild.GetType() == typeof(FrmWidget))
            {
                FrmWidget child = (FrmWidget)this.ActiveMdiChild;
                if (child != null)
                {
                    child.SaveSettings();
                }
            }
            statusLabel.Text = "Save completed...";
        }
        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveBtn();
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            saveBtn();
        }

        public void saveAsBtn()
        {
            if (this.ActiveMdiChild.GetType() == typeof(FrmWidget)) {
                if (saveFileDialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    if (!string.IsNullOrEmpty(saveFileDialog.FileName))
                    {
                        FrmWidget child = (FrmWidget)this.ActiveMdiChild;
                        if (child != null)
                        {
                            child.settingsFileName = saveFileDialog.FileName;
                            child.SaveSettings();
                        }
                    }
                }
            }
            statusLabel.Text = "Save as completed...";
        }
        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            saveAsBtn();
        }
        private void btnSaveAs_Click(object sender, EventArgs e)
        {
            saveAsBtn();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmWidget child = (FrmWidget)this.ActiveMdiChild;
            if (child != null)
            {
                child.ClearSettings();
            }
        }

        public void closeActive()
        {
            this.ActiveMdiChild.Close();
        }
        private void closeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            closeActive();
        }

        private void DirectoryCopy(string sourceDirName, string destDirName, bool copySubDirs)
        {
            // Get the subdirectories for the specified directory.
            DirectoryInfo dir = new DirectoryInfo(sourceDirName);
            DirectoryInfo[] dirs = dir.GetDirectories();

            if (!dir.Exists)
            {
                throw new DirectoryNotFoundException(
                    "Source directory does not exist or could not be found: "
                    + sourceDirName);
            }

            // If the destination directory doesn't exist, create it. 
            if (!Directory.Exists(destDirName))
            {
                Directory.CreateDirectory(destDirName);
            }

            // Get the files in the directory and copy them to the new location.
            FileInfo[] files = dir.GetFiles();
            foreach (FileInfo file in files)
            {
                string temppath = Path.Combine(destDirName, file.Name);
                file.CopyTo(temppath, false);
            }

            // If copying subdirectories, copy them and their contents to new location. 
            if (copySubDirs)
            {
                foreach (DirectoryInfo subdir in dirs)
                {
                    string temppath = Path.Combine(destDirName, subdir.Name);
                    DirectoryCopy(subdir.FullName, temppath, copySubDirs);
                }
            }
        }
        public void manageWidget()
        {
            newBtn();
        }
        private string rewritePath(string path)
        {
            return path.Replace(@"\\", @"\");
        }
        public void createWidget(string widgetName)
        {
            if (string.IsNullOrEmpty(widgetName))
            {
                MessageBox.Show("You need to type in some kind of name for your widget.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            else
            {
                if (Regex.IsMatch(widgetName, @"^[a-zA-Z]+$"))
                {
                    if (fldBrowseSource.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    {
                        if (Directory.Exists(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName)))
                        {
                            MessageBox.Show("There is already a directory with the widgetName '" + widgetName + "' in this directory.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                        else
                        {
                            // Copy boilerplate files
                            Directory.CreateDirectory(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName));
                            DirectoryCopy(rewritePath(Application.StartupPath + @"\boilerplate\"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName), true);

                            // First move the content to a new directory with the widget name.
                            Directory.Move(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\WidgetName"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName));
                            
                            // Now rename widget source files
                            string packageXml = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\package.xml"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\package.xml"), packageXml.Replace("WidgetName", widgetName));

                            File.Move(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\WidgetName.xml"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\" + widgetName + ".xml"));
                            string contentXml = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\" + widgetName + ".xml"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\" + widgetName + ".xml"), contentXml.Replace("WidgetName", widgetName));
                            
                            File.Move(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\WidgetName.js"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\" + widgetName + @".js"));
                            string contentJs = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\" + widgetName + @".js"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\" + widgetName + @".js"), contentJs.Replace("WidgetName", widgetName));

                            File.Move(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\ui\WidgetName.css"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\ui\" + widgetName + @".css"));
                            string contentCss = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\ui\" + widgetName + @".css"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\ui\" + widgetName + @".css"), contentCss.Replace("WidgetName", widgetName));

                            File.Move(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\templates\WidgetName.html"), rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\templates\" + widgetName + @".html"));
                            string contentHtml = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\templates\" + widgetName + @".html"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\templates\" + widgetName + @".html"), contentHtml.Replace("WidgetName", widgetName));

                            // Alter jQuery
                            string contentJQuery = File.ReadAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\lib\jquery.js"));
                            File.WriteAllText(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\" + widgetName + @"\widget\lib\jquery.js"), contentJQuery.Replace("WidgetName", widgetName));


                            // Creating settings file
                            Directory.CreateDirectory(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\settings"));

                            // Write settings.
                            clsMXWidgetSettings settings = new clsMXWidgetSettings();
                            settings.WidgetName = widgetName;
                            settings.SourcePath = rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\src\");
                            settings.DestinationPath = rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\test\");
                            settings.Monitor = false;
                            XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
                            FileStream writeStream = new FileStream(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\settings\" + widgetName + ".mws"), FileMode.Create);
                            xmlSerializer.Serialize(writeStream, settings);
                            writeStream.Close();

                            // Open a new form
                            FrmWidget form = new FrmWidget(rewritePath(fldBrowseSource.SelectedPath + @"\" + widgetName + @"\settings\" + widgetName + ".mws"));
                            form.MdiParent = this;
                            form.Show();
                        }
                    }
                } else {
                    MessageBox.Show("You can only use a-z A-Z characters in your widgetName.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Quit SDK", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSplash splash = new FrmSplash();
            splash.MdiParent = this;
            splash.Show();
        }


        private void startupScreenToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmMendixWidgetSDK widgetSDK = new FrmMendixWidgetSDK();
            widgetSDK.MdiParent = this;
            widgetSDK.Show();
        }

        #endregion

        public string argument { get; set; }

        private void Main_Load(object sender, EventArgs e)
        {
            FrmMendixWidgetSDK widgetSDKHome = new FrmMendixWidgetSDK();
            widgetSDKHome.MdiParent = this;
            widgetSDKHome.frmMain = this;
            widgetSDKHome.Show();

            if (argument != string.Empty && argument != null)
            {
                FrmWidget form = new FrmWidget(argument);
                form.MdiParent = this;
                form.Show();
            }
        }


    }
}
