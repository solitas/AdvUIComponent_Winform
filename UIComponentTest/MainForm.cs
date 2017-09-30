using Rootech.UI.Component;
using System.Windows.Forms;
using System;

namespace UIComponentTest
{
    public partial class MainForm : FormBase
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void cButton1_Click(object sender, EventArgs e)
        {
            MessageBox.Show("button clicked");
        }
    }
}
