using System;
using System.Windows.Forms;
using System.Security.Permissions;

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

        private void openToolStripMenuItem_Click_1(object sender, EventArgs e)
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

        private void newToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmWidget form = new FrmWidget(string.Empty);
            form.MdiParent = this;
            form.Show();
        }

        private void saveToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FrmWidget child = (FrmWidget)this.ActiveMdiChild;
            if (child != null)
            {
                child.SaveSettings();
            }
        }

        private void saveAsToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
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

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Are you sure you want to exit?", "Quit SDK", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.OK)
            {
                Application.Exit();
            }
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmWidget child = (FrmWidget)this.ActiveMdiChild;
            if (child != null)
            {
                child.ClearSettings();
            }
        }

        private void closeToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            this.ActiveMdiChild.Close();
        }

        private void aboutUsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmSplash splash = new FrmSplash();
            splash.MdiParent = this;
            splash.Show();
        }

        #endregion

        public string argument { get; set; }

        private void Main_Load(object sender, EventArgs e)
        {
            if (argument != string.Empty && argument != null)
            {
                FrmWidget form = new FrmWidget(argument);
                form.MdiParent = this;
                form.Show();
            }
        }


    }
}
