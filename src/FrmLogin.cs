using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MendixWidgets
{
    public partial class FrmLogin : Form
    {

        public string path { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public string domain { get; set; }

        public FrmLogin()
        {
            InitializeComponent();
        }

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(path))
            {
                txtPath.Text = path;
            }
        }

        private void chkShow_CheckedChanged(object sender, EventArgs e)
        {
            if (chkShow.Checked)
            {
                txtPassword.PasswordChar = Convert.ToChar(" ");
            }
            else
            {
                txtPassword.PasswordChar = '*';
            }
        }

        private void txtPath_TextChanged(object sender, EventArgs e)
        {
            path = txtPath.Text;
        }

        private void txtUsername_TextChanged(object sender, EventArgs e)
        {
            username = txtUsername.Text;
        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {
            password = txtPassword.Text;
        }

        private void txtDomain_TextChanged(object sender, EventArgs e)
        {
            domain = txtDomain.Text;
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            
        }

        private void FrmLogin_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (string.IsNullOrEmpty(txtPath.Text) || string.IsNullOrEmpty(txtDomain.Text) || string.IsNullOrEmpty(txtUsername.Text) || string.IsNullOrEmpty(txtPassword.Text))
            {
                if (string.IsNullOrEmpty(txtPath.Text))
                {
                    MessageBox.Show("Path should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (string.IsNullOrEmpty(txtDomain.Text))
                {
                    MessageBox.Show("Domain should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (string.IsNullOrEmpty(txtUsername.Text))
                {
                    MessageBox.Show("Username should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (string.IsNullOrEmpty(txtPassword.Text))
                {
                    MessageBox.Show("Password should not be empty", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                
                e.Cancel = true;
            }
        }
    }
}
