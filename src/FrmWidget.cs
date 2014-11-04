using System;
using System.IO;
using System.Windows.Forms;
using System.Xml.Serialization;
using System.Security.Permissions;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace MendixWidgets
{
    public partial class FrmWidget : Form
    {
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, 
                         int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint NetUseAdd(
             string UncServerName,
             UInt32 Level,
             ref USE_INFO_2 Buf,
             out UInt32 ParmError
        );

        [DllImport("NetApi32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern uint NetUseDel(
            string  UncServerName,
            string  UseName,
            UInt32 ForceCond
        );

        [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Unicode)]
        public struct USE_INFO_2
        {
            public string ui2_local;
            public string ui2_remote;
            public string ui2_password;
            public UInt32 ui2_status;
            public UInt32 ui2_asg_type;
            public UInt32 ui2_refcount;
            public UInt32 ui2_usecount;
            public string ui2_username;
            public string ui2_domainname;
        }

        private clsMXWidgetSettings settings;
        public string settingsFileName { get; set; }
        private Timer timer = new Timer();

        public FrmWidget(string path)
        {
            InitializeComponent();

            string documentDir = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + @"\MendixCustomWidgetSDK";
            if (!Directory.Exists(documentDir))
            {
                Directory.CreateDirectory(documentDir);
            }

            settingsFileName = (path != string.Empty) ? path : documentDir + @"\settings.mws";
            settings = new clsMXWidgetSettings();

            OpenSettings();
        }

        #region Input actions

        private void txtWidgetName_TextChanged(object sender, EventArgs e)
        {
            lblWidgetName.Text = txtWidgetName.Text;
            this.Text = "Widget: " + txtWidgetName.Text;
        }

        private void connectToNetwork(string path, string domain, string username, string password)
        {
            FrmLogin login = new FrmLogin();

            if (!string.IsNullOrEmpty(path))
            {
                login.path = path;
            }
            if (!string.IsNullOrEmpty(domain))
            {
                login.domain = domain;
            }
            if (!string.IsNullOrEmpty(username))
            {
                login.username = username;
            }
            if (!string.IsNullOrEmpty(password))
            {
                login.password = password;
            }

            if (login.ShowDialog() == DialogResult.OK)
            {

                try
                {
                    USE_INFO_2 info = new USE_INFO_2();
                    info.ui2_local = null;
                    info.ui2_asg_type = 0xFFFFFFFF;
                    info.ui2_remote = txtSource.Text;
                    info.ui2_username = login.username;
                    info.ui2_password = login.password;
                    info.ui2_domainname = login.domain;

                    uint paramErrorIndex;
                    uint returnCode = NetUseAdd(null, 2, ref info, out paramErrorIndex);

                    if (returnCode != 0)
                    {
                        throw new Win32Exception((int)returnCode);
                    }
                }
                catch (Win32Exception err)
                {
                    MessageBox.Show(err.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    connectToNetwork(login.path,login.domain,login.username,login.password);
                }
            }
        }

        private void btnSetSource_Click(object sender, EventArgs e)
        {
            if (fldBrowseSource.ShowDialog() == DialogResult.OK)
            {
                txtSource.Text = fldBrowseSource.SelectedPath;
                SaveSettings();
                if (txtSource.Text.StartsWith("\\"))
                {
                    connectToNetwork(txtSource.Text, string.Empty, string.Empty, string.Empty);
                }
            }
        }

        private void btnSetDestination_Click(object sender, EventArgs e)
        {
            if (fldBrowseDestination.ShowDialog() == DialogResult.OK)
            {
                txtDestination.Text = fldBrowseDestination.SelectedPath;
                SaveSettings();
            }
            btnCreateWidget2.Focus();
        }

        private void btnCreateWidget_Click(object sender, EventArgs e)
        {

        }

        private void CreateWidgetJS()
        {
            if(File.Exists(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.js")){
                File.Delete(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.js");
            }
            DirectoryInfo[] subDirs = new DirectoryInfo(this.txtDestination.Text + "\\deployment\\web\\widgets\\").GetDirectories();
            string js = string.Empty;
            foreach (DirectoryInfo dirInfo in subDirs)
            {
                if (!isDirectoryIgnored(dirInfo.FullName))
                {
                    if (Directory.Exists(this.txtDestination.Text + "\\deployment\\web\\widgets\\" + dirInfo.Name + "\\widget\\"))
                    {

                        FileInfo[] files = new DirectoryInfo(this.txtDestination.Text + "\\deployment\\web\\widgets\\" + dirInfo.Name + "\\widget\\").GetFiles();

                        foreach (FileInfo fi in files)
                        {
                            if (fi.FullName.Contains(".js") && !isFileIgnored(fi.FullName))
                            {
                                js += "dojo.registerModulePath(\"" + dirInfo.Name + "\", \"../../widgets/" + dirInfo.Name + "\");\r\n";
                                js += File.ReadAllText(fi.FullName).Replace("\n", "\r\n").Replace("\r\r\n", "\r\n");
                            }
                        }

                    }

                    this.txtWidgetJS.Text = js;

                    File.WriteAllText(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.js", js);
                }
            }
        }

        
        private void CreateWidgetCSS()
        {
            string css = string.Empty;
            this.txtWidgetCSS.Text = string.Empty;
            if(File.Exists(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.css")){
                File.Delete(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.css");
            }
            css = CheckForCSS(string.Empty, new DirectoryInfo(this.txtDestination.Text + "\\deployment\\web\\widgets\\"));
            this.txtWidgetCSS.Text = css;
            File.WriteAllText(this.txtDestination.Text + "\\deployment\\web\\widgets\\widgets.css", css);
        }

        private string CheckForCSS(string css, DirectoryInfo root)
        {
            DirectoryInfo[] subDirs = root.GetDirectories();

            FileInfo[] files = new DirectoryInfo(root.FullName).GetFiles();

            foreach (FileInfo fi in files)
            {
                if (fi.FullName.Contains(".css") && !isFileIgnored(fi.FullName))
                {
                    css += File.ReadAllText(fi.FullName);
                }
            }

            foreach (DirectoryInfo dir in subDirs)
            {
                if (!isDirectoryIgnored(dir.FullName))
                {
                    css = CheckForCSS(css, dir);
                }
            }

            return css;
        }

        private void myProcess_ErrorDataReceived(object sender, DataReceivedEventArgs e)
        {
            ReformatZip();
        }

        private void myProcess_Disposed(object sender, EventArgs e)
        {
            ReformatZip();
        }

        private void myProcess_Exited(object sender, EventArgs e)
        {
            ReformatZip();
        }

        private void txtWidgetName_KeyUp(object sender, KeyEventArgs e)
        {
            SaveSettings();
        }

        private void txtSource_KeyUp(object sender, KeyEventArgs e)
        {
            SaveSettings();
        }

        private void txtDestination_KeyUp(object sender, KeyEventArgs e)
        {
            SaveSettings();
        }

        private void chkWatch_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWatch.Checked)
            {
                SaveSettings();
            }
            if (txtSource.Text != string.Empty && txtDestination.Text != string.Empty)
            {
                StartMonitoring();
            }
        }

        private void txtChanges_TextChanged(object sender, EventArgs e)
        {
            txtChanges.SelectionStart = txtChanges.TextLength;
            txtChanges.ScrollToCaret();
        }

        #endregion

        #region Eventhandlers

        private bool isDirectoryIgnored(string path)
        {
            if(chkIgnoreDirectories.Checked == false){
                return false;
            }

            char[] chars = { ',' };
            string[] dirList = txtIgnoreDirectories.Text.Split(chars);

            foreach (string dir in dirList)
            {
                if (path.Contains(dir))
                {
                    return true;
                }
            }

            return false;
        }

        private bool isFileIgnored(string path)
        {
            if (chkIgnoreFiles.Checked == false)
            {
                return false;
            }

            char[] chars = { ',' };
            string[] fileList = txtIgnoreFiles.Text.Split(chars);

            foreach (string file in fileList)
            {
                if (path.Contains(file))
                {
                    return true;
                }
            }

            return false;
        }

        private void checkSizeOfChanges()
        {
            if (txtChanges.Text.Length > 5000)
            {
                txtChanges.Text = string.Empty;
            }
        }

        private void flsWatchDestDirectory_Created(object sender, FileSystemEventArgs e)
        {
            checkSizeOfChanges();
        }

        private void flsWatchDirectory_Renamed(object sender, RenamedEventArgs e)
        {
            if (chkWatch.Checked && isDirectoryIgnored(e.OldName) == false && isFileIgnored(e.OldName) == false)
            {
                txtChanges.Text += "[SOURCE]: File " + e.OldName + " has been renamed to " + e.Name + ".\r\n";
                string sourceFilename = txtDestination.Text + "\\deployment\\web\\widgets\\" + e.OldName;
                string destinationFilename = txtDestination.Text + "\\deployment\\web\\widgets\\" + e.Name;
                CreateMPK();
                if (Directory.Exists(txtDestination.Text + "\\deployment\\web\\widgets\\" + txtWidgetName.Text))
                {
                    RenameSourceDest(sourceFilename, destinationFilename);
                }
                if (Directory.Exists(this.txtDestination.Text + "\\deployment"))
                {
                    txtChanges.Text += "[DESTINATION]: destination has changed recreating widget.js and widget.css\r\n";
                    CreateWidgetJS();
                    CreateWidgetCSS();
                }
                checkSizeOfChanges();
            }
        }

        private void flsWatchDirectory_Deleted(object sender, FileSystemEventArgs e)
        {
            if (chkWatch.Checked && isDirectoryIgnored(e.Name) == false && isFileIgnored(e.Name) == false)
            {
                txtChanges.Text += "[SOURCE]: File " + e.Name + " has been deleted.\r\n";
                string destinationFilename = txtDestination.Text + "\\deployment\\web\\widgets\\" + e.Name;
                CreateMPK();
                if (Directory.Exists(txtDestination.Text + "\\deployment\\web\\widgets\\" + txtWidgetName.Text))
                {
                    if (Directory.Exists(destinationFilename))
                    {
                        Directory.Delete(destinationFilename, true);
                        txtChanges.Text += "[DESTINATION]: Deleted directory: " + destinationFilename + ".\r\n";
                    }
                    else
                    {
                        File.Delete(destinationFilename);
                        txtChanges.Text += "[DESTINATION]: Deleted file: " + destinationFilename + ".\r\n";
                    }
                }
                checkSizeOfChanges();
            }
        }

        private void flsWatchDirectory_Changed(object sender, FileSystemEventArgs e)
        {
            if (chkWatch.Checked && isDirectoryIgnored(e.Name) == false && isFileIgnored(e.Name) == false)
            {
                txtChanges.Text += "[SOURCE]: File " + e.Name + " has been changed / modified.\r\n";
                CreateMPK();

                // When the directory "...\deployment\web\widgets\[widgetname]" exists alter files on deployment.
                string sourceFilename = e.FullPath;
                string destinationFilename = txtDestination.Text + "\\deployment\\web\\widgets\\" + e.Name;
                if (Directory.Exists(txtDestination.Text + "\\deployment\\web\\widgets\\" + txtWidgetName.Text))
                {
                    CopySourceDest(sourceFilename, destinationFilename);
                }
                checkSizeOfChanges();
            }
        }

        #endregion

        #region Actions

        private void StartMonitoring()
        {
            if (chkWatch.Checked)
            {
                if (Directory.Exists(txtSource.Text))
                {
                    if (Directory.Exists(txtDestination.Text + "\\deployment\\web\\widgets"))
                    {
                        flsWatchDirectory.Path = txtSource.Text;
                        flsWatchDirectory.IncludeSubdirectories = true;
                        flsWatchDirectory.InternalBufferSize = 4 * 4096; 
                        flsWatchDirectory.NotifyFilter = NotifyFilters.Attributes |
                        NotifyFilters.CreationTime |
                        NotifyFilters.DirectoryName |
                        NotifyFilters.FileName |
                        NotifyFilters.LastAccess |
                        NotifyFilters.LastWrite |
                        NotifyFilters.Security |
                        NotifyFilters.Size;
                        flsWatchDirectory.Filter = "*.*";
                        flsWatchDirectory.Renamed += new RenamedEventHandler(flsWatchDirectory_Renamed);
                        flsWatchDirectory.Changed += new FileSystemEventHandler(flsWatchDirectory_Changed);
                        flsWatchDirectory.Created += new FileSystemEventHandler(flsWatchDirectory_Changed);
                        flsWatchDirectory.Deleted += new FileSystemEventHandler(flsWatchDirectory_Deleted);
                        flsWatchDirectory.EnableRaisingEvents = true;
                        txtChanges.Text += "Started watching: " + txtSource.Text + ".\r\n";
                    }
                }
                if (Directory.Exists(txtSource.Text))
                {
                    if (Directory.Exists(txtDestination.Text + "\\deployment\\web\\widgets")) { 
                        flsWatchDestDirectory.Path = txtDestination.Text + "\\deployment\\web\\widgets";
                        flsWatchDestDirectory.IncludeSubdirectories = true;
                        flsWatchDestDirectory.InternalBufferSize = 4 * 4096; 
                        flsWatchDestDirectory.NotifyFilter = NotifyFilters.Attributes |
                        NotifyFilters.CreationTime |
                        NotifyFilters.DirectoryName |
                        NotifyFilters.FileName |
                        NotifyFilters.LastAccess |
                        NotifyFilters.LastWrite |
                        NotifyFilters.Security |
                        NotifyFilters.Size;
                        flsWatchDestDirectory.Filter = "*.*";
                        flsWatchDestDirectory.Changed += new FileSystemEventHandler(flsWatchDestDirectory_Created);
                        flsWatchDestDirectory.Created += new FileSystemEventHandler(flsWatchDestDirectory_Created);
                        flsWatchDestDirectory.Deleted += new FileSystemEventHandler(flsWatchDestDirectory_Created);
                        flsWatchDestDirectory.EnableRaisingEvents = true;
                        txtChanges.Text += "Started watching: " + txtDestination.Text + "\\deployment\\web\\widgets .\r\n";
                    }
                    else
                    {
                        chkWatch.Checked = false;
                        txtChanges.Text += "[ERROR: Directory not exists " + txtDestination.Text + "\\deployment\\web\\widgets.\r\n";
                    }
                }
            }
            else
            {
                flsWatchDirectory.EnableRaisingEvents = false;
                txtChanges.Text += "Stopped watching: " + txtSource.Text + ".\r\n";
            }
        }

        public void ClearSettings()
        {
            // Set the values
            txtWidgetName.Text = string.Empty;
            txtSource.Text = string.Empty;
            txtDestination.Text = string.Empty;
            chkWatch.Checked = false;
            // Delete file
            if (File.Exists(settingsFileName))
            {
                File.Delete(settingsFileName);
            }
            // Write settings.
            settings.WidgetName = txtWidgetName.Text;
            settings.SourcePath = txtSource.Text;
            settings.DestinationPath = txtDestination.Text;
            settings.Monitor = chkWatch.Checked;
            XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
            FileStream writeStream = new FileStream(settingsFileName, FileMode.Create);
            xmlSerializer.Serialize(writeStream, settings);
            writeStream.Close();
        }

        public void OpenSettings()
        {
            if (File.Exists(settingsFileName))
            {
                string allText = File.ReadAllText(settingsFileName);
                if (allText.StartsWith("<?xml"))
                {
                    try
                    {
                        // Read settings.
                        XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
                        FileStream readStream = new FileStream(settingsFileName, FileMode.Open);
                        settings = (clsMXWidgetSettings)xmlSerializer.Deserialize(readStream);
                        readStream.Close();

                        // Set the values
                        txtWidgetName.Text = settings.WidgetName;
                        txtSource.Text = settings.SourcePath;
                        txtDestination.Text = settings.DestinationPath;
                        chkWatch.Checked = settings.Monitor;
                    }
                    catch
                    {
                        MessageBox.Show("You did not provide a proper Mendix Custom Widget SDK file.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                
            }
        }

        public void SaveSettings()
        {
            if (File.Exists(settingsFileName))
            {
                File.Delete(settingsFileName);
            }
            // Write settings.
            settings.WidgetName = txtWidgetName.Text;
            settings.SourcePath = txtSource.Text;
            settings.DestinationPath = txtDestination.Text;
            settings.Monitor = chkWatch.Checked;
            XmlSerializer xmlSerializer = new XmlSerializer(settings.GetType());
            FileStream writeStream = new FileStream(settingsFileName, FileMode.Create);
            xmlSerializer.Serialize(writeStream, settings);
            writeStream.Close();
        }

        private void ReformatZip()
        {
            if (File.Exists(txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".zip"))
            {
                File.Copy(txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".zip", txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".mpk", true);
                File.Delete(txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".zip");
            }
        }

        private void CreateMPK()
        {
            if (txtSource.Text != string.Empty && txtDestination.Text != string.Empty)
            {
                if (File.Exists(txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".mpk"))
                {
                    File.Delete(txtDestination.Text + "\\widgets\\" + txtWidgetName.Text + ".mpk");
                }

                Process myProcess = new Process();

                try
                {
                    myProcess.StartInfo.UseShellExecute = false;
                    myProcess.Exited += new EventHandler(myProcess_Exited);
                    myProcess.Disposed += new EventHandler(myProcess_Disposed);
                    myProcess.ErrorDataReceived += new DataReceivedEventHandler(myProcess_ErrorDataReceived);
                    // You can start any process, HelloWorld is a do-nothing example.
                    string sevenZip = (File.Exists(@"C:\Program Files (x86)\7-Zip\7z.exe")) ? @"C:\Program Files (x86)\7-Zip\7z.exe" : (File.Exists(@"C:\Program Files\7-Zip\7z.exe")) ? @"C:\Program Files\7-Zip\7z.exe" : string.Empty;
                    myProcess.StartInfo.FileName = sevenZip;
                    myProcess.StartInfo.Arguments = "a" + @" """ + txtDestination.Text + @"\widgets\" + txtWidgetName.Text + @".zip" + @""" """ + txtSource.Text + @"\*""";
                    myProcess.StartInfo.CreateNoWindow = true;
                    myProcess.Start();

                    while (!myProcess.HasExited)
                    {
                        // Event handlers sometimes do not fire!
                    }
                    ReformatZip();
                    MessageBox.Show("The widget with name '" + txtWidgetName.Text + "' is created.", "Widget created", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                catch (Exception e)
                {
                    txtChanges.Text += e.Message + "\r\n";
                    MessageBox.Show("The widget with name '" + txtWidgetName.Text + "' is not created.\r\n" + e.Message, "Widget not created", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                txtChanges.Text += "Widget is created succesfully.\r\n";

            }
            else
            {
                MessageBox.Show("The widget source directory is not defined we cannot create the widget.", "Widget not created", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void RenameSourceDest(string source, string dest)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    Directory.Move(source, dest);
                    txtChanges.Text += "[DESTINATION]: Renamed directory to: " + dest + ".\r\n";
                }
                else
                {
                    File.Copy(source, dest, true);
                    File.Delete(source);
                    txtChanges.Text += "[DESTINATION]: Renamed file: " + source + " to " + dest + ".\r\n";
                }
            }
            catch (Exception err)
            {
                txtChanges.Text += "[DESTINATION]: Error: " + err.Message + "\r\n";
            }
        }

        private void CopySourceDest(string source, string dest)
        {
            try
            {
                if (Directory.Exists(source))
                {
                    Directory.CreateDirectory(dest);
                    txtChanges.Text += "[DESTINATION]: Created directory: " + dest + ".\r\n";
                }
                else
                {
                    File.Copy(source, dest, true);
                    txtChanges.Text += "[DESTINATION]: File altered: " + dest + ".\r\n";
                }
            }
            catch (Exception err)
            {
                txtChanges.Text += "[DESTINATION]: Error: " + err.Message + "\r\n";
            }
        }

        private void txtSource_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnCreateWidget2_Click(object sender, EventArgs e)
        {
            CreateMPK();
            if (Directory.Exists(this.txtDestination.Text + "\\deployment"))
            {
                CreateWidgetJS();
                CreateWidgetCSS();
            }
            
        }

        #endregion

        #region Main Load
        
        private void Main_Load(object sender, EventArgs e)
        {

        }

        #endregion

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }





    }
}
