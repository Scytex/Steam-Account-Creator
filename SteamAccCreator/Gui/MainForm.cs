using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.Windows.Forms;

namespace SteamAccCreator.Gui
{
    public partial class MainForm : Form
    {
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

        public void UpdateStatus(int i, string status)
        {
            BeginInvoke(new Action(() =>
            {
                dataAccounts.Rows[i].Cells[3].Value = status;
            }));
        }

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

        private void nmbrAmountAccounts_ValueChanged(object sender, EventArgs e)
        {
            if (nmbrAmountAccounts.Value > 100)
            {
                nmbrAmountAccounts.Value = 100;
            }
        }

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
    }
}
