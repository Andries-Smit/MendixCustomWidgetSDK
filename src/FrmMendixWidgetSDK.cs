using System;
using System.Windows.Forms;
using System.Security.Permissions;

namespace MendixWidgets
{

    [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
    [System.Runtime.InteropServices.ComVisibleAttribute(true)]
    public partial class FrmMendixWidgetSDK : Form
    {

        public Main frmMain { get; set; }

        public FrmMendixWidgetSDK()
        {
            InitializeComponent();
        }

        public void OpenWidget()
        {
            frmMain.openBtn();
        }
        public void CreateWidget(string widgetName)
        {
            frmMain.createWidget(widgetName);
        }
        public void ManageWidget()
        {
            frmMain.manageWidget();
        }

        private void FrmMendixWidgetSDK_Load(object sender, EventArgs e)
        {
            webBrowser1.AllowWebBrowserDrop = false;
            webBrowser1.IsWebBrowserContextMenuEnabled = false;
            webBrowser1.WebBrowserShortcutsEnabled = false;
            webBrowser1.ObjectForScripting = this;
            webBrowser1.Navigate(Application.StartupPath + @"\www\index.html?d=" + new DateTime().ToString());
        }
    }
}
