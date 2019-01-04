using System;
using System.Drawing;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;

namespace SteamAccCreator.Gui
{
    public partial class MainForm : Form
    {
        #region Proxy / Beginning Shit
        public bool RandomMail { get; private set; }
        public bool RandomAlias { get; private set; }
        public bool RandomPass { get; private set; }
        public bool WriteIntoFile { get; private set; }
        public bool UseProxy { get; private set; }
        public bool AutoMailVerify { get; private set; }
        public bool UseCaptchaService { get; private set; }

        private int _index = 0;

        public MainForm()
        {
            InitializeComponent();
        }

        public string proxyval = null;
        public int proxyport = 0;
        public bool proxy = false;
        #endregion
        #region Creation Button

        // Starting the creation
        // checks if we are using a custom
        // file to edit to and if we are using
        // a proxy.
        public void BtnCreateAccount_Click(object sender, EventArgs e)
        {
            if (nmbrAmountAccounts.Value > 100)
            {
                nmbrAmountAccounts.Value = 100;
            }

            if (checkBox1.Checked == true)
            {
                proxyval = textBox1.Text;
                proxyport = Convert.ToInt16(textBox2.Text);
                proxy = true;
            } else
            {
                proxy = false;
            }

            if (checkBox4.Checked == true)
            {
                if (file != null)
                {
                    for (var i = 0; i < nmbrAmountAccounts.Value; i++)
                    {
                        var accCreator = new AccountCreator(this, txtEmail.Text, txtAlias.Text, txtPass.Text, _index);
                        var thread = new Thread(accCreator.Run);
                        thread.Start();
                        _index++;
                    }
                } else
                {
                    MessageBox.Show("Please Select a File to Edit. :)");
                }
            } else
            {
                for (var i = 0; i < nmbrAmountAccounts.Value; i++)
                {
                    var accCreator = new AccountCreator(this, txtEmail.Text, txtAlias.Text, txtPass.Text, _index);
                    var thread = new Thread(accCreator.Run);
                    thread.Start();
                    _index++;
                }
            }
        }
        #endregion
        #region DataTable
        // Function that runs as many times as
        // the number of accs inputted supplied with
        // the shit in the (). Prob could be optimised
        // but that's for a l8r time. :) <3 SpookedOnion
        public void AddToTable(string mail, string alias, string pass)
        {
            BeginInvoke(new Action(() =>
            {
                dataAccounts.Rows.Add(new DataGridViewRow
                {
                    Cells =
                    {
                        new DataGridViewTextBoxCell {Value = mail},
                        new DataGridViewTextBoxCell {Value = alias},
                        new DataGridViewTextBoxCell {Value = pass},
                        new DataGridViewTextBoxCell {Value = "Ready"}
                    }
                });
            }));
        }

        // Setting the status row to the current
        // step that the generator is on.
        // .Bug Had to remove a lot of the update checks along
        // the way as it was causing a few errors and such.
        // <3 SpookedOnion
        public void UpdateStatus(int i, string status)
        {
            BeginInvoke(new Action(() =>
            {
                dataAccounts.Rows[i].Cells[3].Value = status;
            }));
        }
        #endregion
        #region Check Boxs / Gay Function
        private void ChkAutoVerifyMail_CheckedChanged(object sender, EventArgs e)
        {
            AutoMailVerify = chkAutoVerifyMail.Checked;
        }

        private void ChkWriteIntoFile_CheckedChanged(object sender, EventArgs e)
        {
            WriteIntoFile = chkWriteIntoFile.Checked;
        }

        private void ChkRandomMail_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomMail.Checked;
            chkAutoVerifyMail.Enabled = state;
            chkAutoVerifyMail.Checked = state;
            RandomMail = state;
            txtEmail.Enabled = !state;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomPass_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomPass.Checked;
            txtPass.Enabled = !state;
            RandomPass = state;
            ToggleForceWriteIntoFile();
        }

        private void ChkRandomAlias_CheckedChanged(object sender, EventArgs e)
        {
            var state = chkRandomAlias.Checked;
            txtAlias.Enabled = !state;
            RandomAlias = state;
            ToggleForceWriteIntoFile();
        }

        private void ToggleForceWriteIntoFile()
        {
            var shouldForce = chkRandomMail.Checked || chkRandomPass.Checked || chkRandomAlias.Checked;
            chkWriteIntoFile.Checked = shouldForce;
            chkWriteIntoFile.Enabled = !shouldForce;
        }
        #endregion
        #region Various Checks
        // Some checks and stuff for the custom
        // file path. <3 SpookedOnion
        public bool istrue = false;
        public string Path = @"accounts.txt";

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox4.Checked == true)
            {
                button1.Enabled = true;
                Path = file;
            } else
            {
                button1.Enabled = false;
                Path = @"accounts.txt";
            }
        }

        public string file = null;

        private void button1_Click(object sender, EventArgs e)
        {
            openFileDialog1.Filter = "Text File|*.txt";
            openFileDialog1.Title = "Save Files To";

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                file = openFileDialog1.FileName;
            }
        }

        // Check for if the email should be written
        // <3 SpookedOnion
        private void checkBox7_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox7.Checked == true)
            {
                istrue = true;
            } else
            {
                istrue = false;
            }
        }

        private void openFileDialog1_FileOk(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Path = openFileDialog1.FileName;
            MessageBox.Show(Path);
        }

        // Limits # of accounts to 100 to avoid
        // some errors. <3 SpookedOnion
        private void nmbrAmountAccounts_ValueChanged(object sender, EventArgs e)
        {
            if (nmbrAmountAccounts.Value > 100)
            {
                nmbrAmountAccounts.Value = 100;
            }
        }

        // Formatting check for the file manager
        // <3 SpookedOnion
        private void comboBox1_TextChanged(object sender, EventArgs e)
        {
            if (comboBox1.Text != "User:Pass Formatting" && comboBox1.Text != "Original Formatting")
            {
                comboBox1.Text = "User:Pass Formatting";
            }

            if (comboBox1.Text == "User:Pass Formatting")
            {
                original = true;
            }
            else if (comboBox1.Text == "Original Formatting")
            {
                original = false;
            }
        }

        public bool original = true;

        // Activates/Deactivates proxy text boxs
        // based on if the checkbox is checked or
        // not. <3 SpookedOnion
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
            } else
            {
                textBox1.Enabled = false;
                textBox2.Enabled = false;
            }
        }
        #endregion
        #region Proxy Check
        // Not much to explain here
        // opens a socket ¯\_(ツ)_/¯
        // checks if the proxy works
        // From agentsix1
        public static bool SocketConnect(string host, int port)
        {
            var is_success = false;
            try
            {
                var connsock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                connsock.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.SendTimeout, 200);
                Thread.Sleep(500);
                var hip = IPAddress.Parse(host);
                var ipep = new IPEndPoint(hip, port);
                connsock.Connect(ipep);
                if (connsock.Connected)
                {
                    is_success = true;
                }
                connsock.Close();
            }
            catch (Exception)
            {
                is_success = false;
            }
            return is_success;
        }

        // Checks if the proxy is working
        // by calling the bool func and getting
        // if it's true or false. <3 SpookedOnion
        private void button2_Click(object sender, EventArgs e)
        {
            if (checkBox1.Checked == true)
            {
                if (textBox1.Text != "" && textBox2.Text != "")
                {
                    if (SocketConnect(textBox1.Text, Convert.ToInt16(textBox2.Text)) == true)
                    {
                        label1.Text = "Working: True";
                    }
                    else
                    {
                        label1.Text = "Working: False";
                    }
                }
            }
        }
        #endregion
        #region Form Drag
        // Drag form code I have had for a while
        // probably pasted this from stackoverflow
        // bc I was too lasy anyways credits to that guy
        // <3 SpookedOnion
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;
        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public static void Drag_Form(IntPtr Handle, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Drag_Form(Handle, e);
        }

        private void panel2_MouseMove(object sender, MouseEventArgs e)
        {
            Drag_Form(Handle, e);
        }
        #endregion
        #region RGB
        // Rotating RGB
        // Really gay way of doing this but idc so
        // come at me nerd. :) <3 SpookedOnion

        int rgb = 0;
        int r = 254;
        int g = 0;
        int b = 0;
        bool start = true;

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (rgb == 0)
            {
                if (r <= 254)
                {
                    if (start == false)
                    {
                        r = r + 1;
                        b = b - 1;
                    }
                    else
                    {
                        r = r + 1;
                    }
                }
                else
                {
                    if (start == true)
                    {
                        start = false;
                    }
                    rgb = 1;
                }
            }
            else if (rgb == 1)
            {
                if (g <= 254)
                {
                    g = g + 1;
                    r = r - 1;
                }
                else
                {
                    rgb = 2;
                }
            }
            else if (rgb == 2)
            {
                if (b <= 254)
                {
                    b = b + 1;
                    g = g - 1;
                }
                else
                {
                    rgb = 0;
                }
            }

            panel1.BackColor = Color.FromArgb(r, g, b);
        }
        #endregion
        #region TopBar Buttons
        // Exit Button
        private void button3_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // Minimize Button
        private void button4_Click(object sender, EventArgs e)
        {
            if (WindowState != FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Minimized;
            }
        }
        #endregion
    }
}
