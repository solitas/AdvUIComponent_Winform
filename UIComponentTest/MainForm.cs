using Rootech.UI.Component;
using System.Windows.Forms;
using System;

namespace UIComponentTest
{
    public partial class MainForm : FormBase
    {
        public MainForm()
        {
            Visualization = ComponentVisualization.Instance;
            InitializeComponent();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }
    }
}
