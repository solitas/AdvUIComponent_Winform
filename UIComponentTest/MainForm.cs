using Rootech.UI.Component;
using System.Windows.Forms;
using System;

namespace UIComponentTest
{
    public partial class MainForm : FormBase
    {
        CMenu menu = new CMenu();
        public MainForm()
        {
            Visualization = new ComponentVisualization();

            InitializeComponent();
            Controls.Add(menu);
        }
    }

    public class CMenu : MenuStrip
    {
        public CMenu()
        {
            //this.LayoutStyle = ToolStripLayoutStyle.Flow;
            //this.SetBoundsCore(10, 30, Size.Width - 10, Size.Height, BoundsSpecified.All);
            //this.UpdateBounds();

        }

        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            base.SetBoundsCore(10, 30, Size.Width - 10, Size.Height, BoundsSpecified.All);
        }
        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            var c = Location;
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }
    }
}
