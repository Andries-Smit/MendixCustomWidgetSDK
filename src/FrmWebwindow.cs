using System;
using System.Windows.Forms;
using System.Security.Permissions;

namespace MendixWidgets
{
    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmWebwindow : Form
    {
        public FrmWebwindow()
        {
            InitializeComponent();
        }

        private void FrmWebwindow_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this;
            // Uncomment the following line when you are finished debugging.
            //webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.Navigate(new System.Uri("file://" + Application.StartupPath.Replace("\\","/") + "/www/index.html"));
        }

        public void Test(String message)
        {
            MessageBox.Show(message, "client code");
        }

        public void executeJavascript()
        {
            webBrowser1.Document.InvokeScript("test",
                new String[] { "called from client code" });
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {

        }
    }
}
