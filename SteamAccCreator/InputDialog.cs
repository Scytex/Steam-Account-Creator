using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SteamAccCreator
{
    public partial class InputDialog : Form
    {
        public InputDialog(string error)
        {
            InitializeComponent();
            lblError.Text = error;
        }

        private void InputDialog_Load(object sender, EventArgs e)
        {

        }

        private void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
