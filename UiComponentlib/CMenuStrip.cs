using System;
using System.ComponentModel;
using System.Windows.Forms;

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
        }
        #region "code related interface properties"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion
        public CMenuStrip()
        {
            if (DesignMode)
            {
                Dock = DockStyle.None;
                Anchor |= AnchorStyles.Right;
                AutoSize = false;
            }
            Stretch = false;
        }
        protected override void OnParentChanged(EventArgs e)
        {
            base.OnParentChanged(e);
            CMenuStripRender renderer = new CMenuStripRender();
            renderer.Visualization = Visualization;
            Renderer = renderer;
        }
        #region "code related to override mehtods"
        protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
        {
            int w = 0;

            int leftMargin = 6 + ComponentVisualization.TITLE_BAR_HEIGHT;
            x = leftMargin;

            if (Items.Count == 0)
            {
                if (_renderingLocation == RenderingLocation.TitleBar)
                {
                    y = 0;
                    width = width - (leftMargin + y) - (ComponentVisualization.PADDING + ComponentVisualization.TITLE_BAR_HEIGHT * 3);
                }
                else
                {
                    y = ComponentVisualization.TITLE_BAR_HEIGHT + 1;
                }
            }
            else
            {
                
                foreach (ToolStripItem item in Items)
                {
                    w += item.Width;
                }
                width = w;
                y = 0;
            }

            base.SetBoundsCore(x, y, width, height, specified);
        }


        protected override void OnMouseDown(MouseEventArgs mea)
        {
            base.OnMouseDown(mea);
            if (Parent is FormBase)
            {
                //((FormBase)Parent).
            }
        }
        #endregion
    }
}
