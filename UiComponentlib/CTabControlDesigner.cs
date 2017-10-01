using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using static Rootech.UI.Component.Win32Utility;
namespace Rootech.UI.Component
{
    public class CTabControlDesigner : ControlDesigner
    {
        #region "Code related to constants fields"
        private const int WM_LBUTTONDOWN = 0x0201;
        private const int WM_LBUTTONDBCLICK = 0x0203;
        private static int ControlNameNumber = 0;
        #endregion

        private CTabControl _tabControl;
        private DesignerVerbCollection _verbs;
        
        #region "Code related to properties"
        public override DesignerVerbCollection Verbs
        {
            get
            {
                if (_verbs == null)
                {
                    _verbs = new DesignerVerbCollection();
                    _verbs.Add(new DesignerVerb("Add Tab", new EventHandler(AddTab)));
                    _verbs.Add(new DesignerVerb("Remove Tab", new EventHandler(RemoveTab)));
                }
                return _verbs;
            }
        }
        #endregion
        
        // Constructor
        public CTabControlDesigner()
        {

        }
        
        #region "Code related to override methods"
        /// <summary>
        /// Load the control designer with the component to design.
        /// Set up the view on the control using the SetViewFlags method.
        /// </summary>
        /// <param name="component"></param>
        public override void Initialize(IComponent component)
        {
            _tabControl = component as CTabControl;

            if (_tabControl == null)
            {
                DisplayError(new ArgumentException("Tried to use the CTabControl with a class that does not inherit from UserTabControl.", "component"));
            }

            base.Initialize(component);
            // TabPage 추가를 위한 Interface 에 이벤트를 등록한다
            IComponentChangeService compChangeServ = (IComponentChangeService)GetService(typeof(IComponentChangeService));

            if (compChangeServ != null)
            {
                compChangeServ.ComponentRemoved += ComponentRemoved;
            }
        }
        protected override void WndProc(ref Message m)
        {
            try
            {
                int x = 0;
                int y = 0;
                if (_tabControl.Created && m.HWnd == _tabControl.Handle)
                {
                    switch (m.Msg)
                    {
                        case WM_LBUTTONDOWN:
                            x = (m.LParam.ToInt32() << 16) >> 16;
                            y = m.LParam.ToInt32() >> 16;
                            int oi = _tabControl.SelectedIndex;
                            var tabPage = _tabControl.SelectedTab;
                            if (_tabControl.ScrollButtonStyle == CTabScrollButtonStyle.Always && _tabControl.GetLeftScrollButtonRect().Contains(x, y))
                            {
                                _tabControl.ScrollTabs(-10);
                            }
                            else if (_tabControl.ScrollButtonStyle == CTabScrollButtonStyle.Always && _tabControl.GetRightScrollButtonRect().Contains(x, y))
                            {
                                _tabControl.ScrollTabs(10);
                            }
                            else
                            {
                                for (int i = 0; i < _tabControl.Controls.Count; i++)
                                {
                                    Rectangle r = _tabControl.GetTabRect(i);
                                    if (r.Contains(x, y))
                                    {
                                        _tabControl.SelectedIndex = i;
                                        RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedIndex"]);
                                        RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedIndex"], oi, i);
                                        break;
                                    }
                                }
                            }
                            break;
                        case WM_LBUTTONDBCLICK:
                            x = (m.LParam.ToInt32() << 16) >> 16;
                            y = m.LParam.ToInt32() >> 16;
                            if (_tabControl.ScrollButtonStyle == CTabScrollButtonStyle.Always && _tabControl.GetLeftScrollButtonRect().Contains(x, y))
                            {
                                _tabControl.ScrollTabs(-10);
                            }
                            else if (_tabControl.ScrollButtonStyle == CTabScrollButtonStyle.Always && _tabControl.GetRightScrollButtonRect().Contains(x, y))
                            {
                                _tabControl.ScrollTabs(10);
                            }
                            return;
                    }
                }
            }
            finally
            {
                base.WndProc(ref m);
            }
        }
        #endregion
        public override void DoDefaultAction() { }
        /// <summary>
        /// TabRenderer 제거를 처리
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        private void ComponentRemoved(object sender, ComponentEventArgs args)
        {
            if (args.Component == _tabControl.TabRenderer)
            {
                _tabControl.TabRenderer = null;

                RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["TabDrawer"]);
                RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["TabDrawer"], args.Component, null);
            }
        }
        private void AddTab(object sender, EventArgs args)
        {
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (designerHost != null)
            {
                int selectedIndex = _tabControl.SelectedIndex;
                while (true)
                {
                    try
                    {
                       

                        var tabName = GetNewTabName();
                        var tabPage = designerHost.CreateComponent(typeof(CTabPage), tabName) as CTabPage;
                        tabPage.Text = tabName;
                        _tabControl.Controls.Add(tabPage);
                        _tabControl.SelectedTab = tabPage;
                        //designerHost.CreateComponent(typeof(CTabPage), "TabPage");
                        RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedIndex"]);
                        RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedIndex"], selectedIndex, _tabControl.SelectedIndex);

                        break;
                    }
                    catch (Exception)
                    {
                    }
                }
            }
        }
        private void RemoveTab(object sender, EventArgs args)
        {
            IDesignerHost designerHost = (IDesignerHost)GetService(typeof(IDesignerHost));

            if (designerHost != null)
            {
                int index = _tabControl.SelectedIndex;
                if (index > -1)
                {
                   

                    var tabPage = _tabControl.SelectedTab;
                    _tabControl.Controls.Remove(tabPage);
                    designerHost.DestroyComponent(tabPage);
                    RaiseComponentChanging(TypeDescriptor.GetProperties(Control)["SelectedIndex"]);
                    RaiseComponentChanged(TypeDescriptor.GetProperties(Control)["SelectedIndex"], index, 0);
                }
            }
        }
        private string GetNewTabName()
        {
            ControlNameNumber += 1;
            return "tabPage" + ControlNameNumber;
        }

    }
}
