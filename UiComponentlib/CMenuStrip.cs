using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Linq;

namespace Rootech.UI.Component
{
    public class CMenuStrip : MenuStrip, IControl
    {
        public enum RenderingLocation
        {
            TitleBar,
            ActionBar,
        }
        private RenderingLocation _renderingLocation = RenderingLocation.TitleBar;

        [Category("Appearance"), DefaultValue(typeof(RenderingLocation), "TitleBar")]
        public RenderingLocation Render
        {
            set
            {
                _renderingLocation = value;
                UpdateBounds();
            }
            get
            {
                return _renderingLocation;
            }
        }

        #region "code related interface properties"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion
        
        // Constructor
        public CMenuStrip()
        {
            Visualization = ComponentVisualization.Instance;
            Renderer = new CMenuStripRender();

            if (DesignMode)
            {
                Dock = DockStyle.None;
                Anchor |= AnchorStyles.Right;
                AutoSize = false;
                this.Padding = new Padding(0);
            }

            Stretch = false;
            
        }

        #region "code related to override mehtods"
        
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int leftMargin = 6 + ComponentVisualization.TITLE_BAR_HEIGHT;

            int w = 0;
            if (Items.Count > 0)
            {
                w = 0;
                foreach (ToolStripItem item in Items)
                {
                    w += item.Width;
                }
            }
            base.SetBoundsCore(leftMargin, 0, w, ComponentVisualization.TITLE_BAR_HEIGHT, specified);
        }
       
        #endregion
    }
}
