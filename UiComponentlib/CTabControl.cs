﻿using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace Rootech.UI.Component
{
    public delegate void TabChangingEventHandler(object sender, TabChangingEventArgs tcea);

    [Designer(typeof(CTabControlDesigner))]
    public class CTabControl : TabControl, IControl
    {
        #region "code related interface properties"
        public int Depth { set; get; }

        public MouseButtonState MouseButtonState { set; get; }

        public ComponentVisualization Visualization { set; get; }
        #endregion

        #region "Code related to event"
        /// <summary>
        /// Occurs when the selected tab is about to change.
        /// </summary>
        public event TabChangingEventHandler TabChanging;

        /// <summary>
        /// Occurs after the selected tab has changed.
        /// </summary>
        public event EventHandler TabChanged;

        /// <summary>
        /// Occurs after the border color has changed
        /// </summary>
        public event EventHandler BorderColorChanged;

        /// <summary>
        /// Occurs after the active color has changed
        /// </summary>
        public event EventHandler ActiveColorChanged;

        /// <summary>
        /// Occurs after the inactive color has changed
        /// </summary>
        public event EventHandler InactiveColorChanged;

        /// <summary>
        /// Occurs after the margin for the control has changed.
        /// </summary>
        public event EventHandler TabMarginChanged;

        /// <summary>
        /// Occurs after the <see cref="TabDock"/> property
        /// has changed.
        /// </summary>
        public event EventHandler TabDockChanged;

        /// <summary>
        /// Occurs after the <see cref="TabRenderer"/> property
        /// has changed.
        /// </summary>
        public event EventHandler TabDrawerChanged;

        /// <summary>
        /// Occurs after the <see cref="TabFont"/> property
        /// has changed.
        /// </summary>
        public event EventHandler TabFontChanged;

        /// <summary>
        /// Occurs after the <see cref="ScrollButtonStyle"/>
        /// property has changed.
        /// </summary>
        public event EventHandler ScrollButtonStyleChanged;
        /// <summary>
        /// Used to monitor the text changing of a <see cref="CTabPage" />.
        /// </summary>
        private EventHandler ChildTextChangeEventHandler;
        #endregion

        #region "Code related to fields"
        /// <summary>
        /// Monitors when child <see cref="CTabPage"/>s have their
        /// <see cref="CTabPage.Text"/> property changed.
        /// </summary>
        /// <param name="sender">A <see cref="CTabPage"/>.</param>
        /// <param name="ea">Some <see cref="EventArgs"/>.</param>
        private void ChildTabTextChanged(object sender, EventArgs ea)
        {
            CalculateTabLengths();
            InU();
        }

        /// <summary>
        /// The index to use as the default image for the tabs.
        /// </summary>
        private int defaultImageIndex;

        /// <summary>
        /// The <see cref="ImageList"/> used to draw the images in
        /// the tabs.
        /// </summary>
        private ImageList images;

        /// <summary>
        /// A flag to indicate if the tabs can scroll left.
        /// </summary>
        private bool canScrollLeft;

        /// <summary>
        /// A flag to indicate if the tabs can scroll right.
        /// </summary>
        private bool canScrollRight;

        /// <summary>
        /// A flag to indicate if scroll buttons should get drawn.
        /// </summary>
        private CTabScrollButtonStyle showScrollButton;

        /// <summary>
        /// The array of floats whose each entry measures a tab's width.
        /// </summary>
        private ArrayList tabLengths;

        /// <summary>
        /// The sum of the lengths of all the tabs.
        /// </summary>
        private float totalTabSpan;

        /// <summary>
        /// The margin around the visible <see cref="CTabPage"/>.
        /// </summary>
        private int userTabMargin;

        /// <summary>
        /// The span of the tabs. Used as the height/width of the
        /// tabs, depending on the orientation.
        /// </summary>
        private float tabSpan;

        /// <summary>
        /// The amount that the tabs have been scrolled to the left.
        /// </summary>
        private float tabLeftDif;

        /// <summary>
        /// The <see cref="Point"/>s that define the left scroll arrow.
        /// </summary>
        private Point[] leftArrow;

        /// <summary>
        /// The <see cref="Point"/>s that define the right scroll arrow.
        /// </summary>
        private Point[] rightArrow;

        /// <summary>
        /// The index of the last visible tab.
        /// </summary>
        private int lastVisibleTabIndex;

        /// <summary>
        /// The length from the left of the tab control
        /// to the left of the last visible tab.
        /// </summary>
        private float lastVisibleTabLeft;

        /// <summary>
        /// The brush used to draw the strings in the tabs.
        /// </summary>
        private Brush foreBrush;
        private Brush inactiveForeBrush;
        private Color inactiveForeColor;
        /// <summary>
        /// The color of the active tab and area.
        /// </summary>
        private Color activeColor;

        /// <summary>
        /// The brush used to color the active-colored area.
        /// </summary>
        private Brush activeBrush;

        /// <summary>
        /// The color of the inactive areas.
        /// </summary>
        private Color inActiveColor;

        /// <summary>
        /// The brush used to color the inactive-colored area.
        /// </summary>
        private Brush inActiveBrush;

        /// <summary>
        /// The pen used to draw the highlight lines.
        /// </summary>
        private Pen highlightPen;

        /// <summary>
        /// The pen used to draw the shadow lines.
        /// </summary>
        private Pen shadowPen;

        /// <summary>
        /// The pen used to draw the border.
        /// </summary>
        private Pen borderPen;

        /// <summary>
        /// The index of the selected tab.
        /// </summary>
        private int selectedIndex;

        /// <summary>
        /// The currently selected tab.
        /// </summary>
        private CTabPage selectedTab;

        /// <summary>
        /// The side on which the tabs get docked.
        /// </summary>
        private DockStyle userTabDock;

        /// <summary>
        /// The rectangle in which the tabs get drawn.
        /// </summary>
        private Rectangle tabsRectangle;

        /// <summary>
        /// The rectangle in which the client gets drawn.
        /// </summary>
        private Rectangle clientRectangle;

        /// <summary>
        /// The rectangle in which the currently selected
        /// <see cref="CTabPage"/> gets drawn oriented as
        /// if the tabs were docked to the top of the control.
        /// </summary>
        private Rectangle displayRectangle;

        /// <summary>
        /// The rectangle transformed for the <see cref="DisplayRectangle"/>
        /// property to return.
        /// </summary>
        private Rectangle transformedDisplayRectangle;

        /// <summary>
        /// The height used to calculate the rectangles.
        /// </summary>
        private int calcHeight;

        /// <summary>
        /// The width used to calculate the rectangles.
        /// </summary>
        private int calcWidth;

        /// <summary>
        /// The regular font used to draw the strings in the tabs.
        /// </summary>
        private Font userTabFont;

        /// <summary>
        /// The bold font used to draw the strings in the active tab.
        /// </summary>
        private Font boldTabFont;

        /// <summary>
        /// The <see cref="UserTabRenderer"/> used to draw the
        /// tabs.
        /// </summary>
        private CTabRenderer tabRenderer;



        /// <summary>
        /// Used to monitor if a person has elected to scroll the tabs.
        /// </summary>
        private bool keepScrolling;
        #endregion

        #region "Code related to properties"
        /// <summary>
        /// Gets and sets the value of the index of the image in
        /// <see cref="ImageList"/> to use to draw the default image on tabs
        /// that do not specify an image to use.
        /// </summary>
        /// <value>
        /// The zero-based index to the image in the <see cref="CTabControl.ImageList"/>
        /// that appears on the tab. The default is -1, which signifies no image.
        /// </value>
        /// <exception cref="ArgumentException">
        /// The value of <see cref="ImageIndex"/> is less than -1.
        /// </exception>
        public virtual int ImageIndex
        {
            get
            {
                return defaultImageIndex;
            }
            set
            {
                defaultImageIndex = value;
                CalculateTabLengths();
                InU();
            }
        }

        /// <summary>
        /// Gets and sets the <see cref="ImageList"/> used by this
        /// <see cref="CTabControl"/>.
        /// </summary>
        /// <remarks>
        /// To display an image on a tab, set the <see cref="ImageIndex"/> property
        /// of that <see cref="CTabPage"/>. The <see cref="ImageIndex"/> acts as the
        /// index into the <see cref="ImageList"/>.
        /// </remarks>
        public new virtual ImageList ImageList
        {
            get
            {
                return images;
            }
            set
            {
                images = value;
                CalculateTabLengths();
                InU();
            }
        }

        /// <summary>
        /// Gets and sets the <see cref="UserTabRenderer"/> used
        /// to draw the tabs for the <see cref="CTabControl"/>.
        /// </summary>
        /// <remarks>
        /// <para>The default value of this property is <b>null</b>.</para>
        /// <para>When this property is <b>null</b>, no tabs get drawn.</para>
        /// </remarks>
        public virtual CTabRenderer TabRenderer
        {
            get
            {
                return tabRenderer;
            }
            set
            {
                tabRenderer = value;
                InU();
                OnTabDrawerChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets and sets the docking side of the tabs.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if the property tries to get set to
        /// <see cref="DockStyle.Fill"/> of <see cref="DockStyle.None"/>.
        /// </exception>
        /// <remarks>
        /// The default value of this property is <see cref="DockStyle.Top"/>.
        /// </remarks>
        public virtual DockStyle TabDock
        {
            get
            {
                return userTabDock;
            }
            set
            {
                if (DockStyle.Fill == value || DockStyle.None == value)
                {
                    throw new ArgumentException("Tried to set the TabDock property to an invlaid value of Fill or None.");
                }
                if (tabRenderer == null || tabRenderer.SupportsTabDockStyle(value))
                {
                    userTabDock = value;
                }
                else
                {
                    throw new ArgumentException("Tried to set the TabDock property to a value not supported by the current tab drawer.");
                }
                CalculateRectangles();
                PerformLayout();
                InU();
                OnTabDockChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets and sets the <see cref="Font"/> used to draw the strings in
        /// the tabs.
        /// </summary>
        public virtual Font TabFont
        {
            get
            {
                return userTabFont;
            }
            set
            {
                if (value != null)
                {
                    userTabFont = value;
                    boldTabFont = new Font(value.FontFamily.Name, value.Size, FontStyle.Bold);
                    CalculateTabSpan();
                    CalculateTabLengths();
                    CalculateRectangles();
                    PerformLayout();
                    InU();
                }
                else
                {
                    userTabFont = Font;
                }
                OnTabFontChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets and sets the number of pixels to use as the margin.
        /// </summary>
        /// <remarks>
        /// This property puts a margin between the edge of the control
        /// and the tabs, between the tabs and the active tab page, and
        /// the active tab page and the edges of the control. The default
        /// value is 3.
        /// </remarks>
        /// <exception cref="ArgumentException">
        /// Thrown if this property get set to a value less than 0.
        /// </exception>
        public virtual int TabMargin
        {
            get
            {
                return userTabMargin;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Tried to set the property Margin to a negative number.");
                }
                userTabMargin = value;
                CalculateRectangles();
                CalculateTabLengths();
                CalculateTabSpan();
                CalculateLastVisibleTabIndex();
                PerformLayout();
                InU();
                OnTabMarginChanged(new EventArgs());
            }
        }

        /// <summary>
        /// The <see cref="Color"/> of the active tab's
        /// background and the margins around the visible
        /// <see cref="CTabPage"/>.
        /// </summary>
        /// <remarks>
        /// The default value for this is
        /// <see cref="SystemColors.Control"/>.
        /// </remarks>
        public virtual Color ActiveColor
        {
            get
            {
                return activeColor;
            }
            set
            {
                activeColor = value;
                activeBrush.Dispose();
                activeBrush = new SolidBrush(value);
                shadowPen.Color = ControlPaint.DarkDark(value);
                OnActiveColorChanged(new EventArgs());
                InU();
            }
        }

        /// <summary>
        /// The <see cref="Color"/> of the inactive tabs'
        /// background.
        /// </summary>
        /// <remarks>
        /// The default value for this property is <see cref="Color.GhostWhite"/>.
        /// </remarks>
        public virtual Color InactiveColor
        {
            get
            {
                return inActiveColor;
            }
            set
            {
                inActiveColor = value;
                inActiveBrush.Dispose();
                inActiveBrush = new SolidBrush(value);
                highlightPen.Color = ControlPaint.LightLight(value);
                OnInactiveColorChanged(new EventArgs());
                InU();
            }
        }

        /// <summary>
        /// The <see cref="Color"/> of the border drawn
        /// around the control.
        /// </summary>
        /// <remarks>
        /// The default value for this property is <see cref="Color.DarkGray"/>.
        /// </remarks>
        public virtual Color BorderColor
        {
            get
            {
                return borderPen.Color;
            }
            set
            {
                borderPen.Color = value;
                OnBorderColorChanged(new EventArgs());
                InU();
            }
        }

        public virtual int OverIndex { get; set; }

        /// <summary>
        /// Gets and sets the zero-based index of the selected
        /// <see cref="CTabPage"/>.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if this property gets set to a value less than 0 when
        /// <see cref="CTabPage"/>s exist in the control collection.
        /// </exception>
        /// <exception cref="IndexOutOfRangeException">
        /// Thrown if this property gets set to a value greater than
        /// <see cref="Control.ControlCollection.Count"/>.
        /// </exception>
        public new virtual int SelectedIndex
        {
            get
            {
                return selectedIndex;
            }
            set
            {
                if (value < 0 && Controls.Count > 0)
                {
                    throw new ArgumentException("Tried to set the property SelectedIndex to a negative number.");
                }
                else if (value >= Controls.Count)
                {
                    throw new IndexOutOfRangeException("Tried to set the property of the SelectedIndex to a value greater than the number of controls.");
                }
                TabChangingEventArgs tcea = new TabChangingEventArgs(selectedIndex, value);
                OnTabChanging(tcea);
                if (tcea.Cancel)
                {
                    return;
                }
                selectedIndex = value;
                if (Controls.Count > 0)
                {
                    foreach (Control ctrl in this.Controls)
                    {
                        ctrl.Visible = false;
                    }
                    selectedTab = (CTabPage)Controls[value];
                    selectedTab.Visible = true;
                    PerformLayout();
                    InU();
                }
                OnTabChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets and sets how the scroll buttons should get
        /// shown when drawing the tabs in the tab area.
        /// </summary>
        /// <remarks>
        /// The default value for this is <see cref="UserTabScrollButtonStyle.Always"/>.
        /// </remarks>
        public virtual CTabScrollButtonStyle ScrollButtonStyle
        {
            get
            {
                return showScrollButton;
            }
            set
            {
                showScrollButton = value;
                InU();
                OnScrollButtonStyleChanged(new EventArgs());
            }
        }

        /// <summary>
        /// Gets and sets the currently selected tab.
        /// </summary>
        /// <exception cref="ArgumentException">
        /// Thrown if this property gets set to a <see cref="CTabPage"/>
        /// that has not been added to the <see cref="CTabControl"/>.
        /// </exception>
        /// <exception cref="ArgumentNullException">
        /// Thrown if this property gets set to a <b>null</b> value when
        /// <see cref="CTabPage"/>s exist in the control.
        /// </exception>
        public new virtual CTabPage SelectedTab
        {
            get
            {
                return selectedTab;
            }
            set
            {
                if (value == null && Controls.Count > 0)
                {
                    throw new ArgumentNullException("value", "Tried to set the SelectedTab property to a null value.");
                }
                else if (value != null && !Controls.Contains(value))
                {
                    throw new ArgumentException("Tried to set the SelectedTab property to a CTabPage that has not been added to this CTabControl.");
                }
                if (Controls.Count > 0)
                {
                    int newIndex;
                    for (newIndex = 0; newIndex < Controls.Count; newIndex++)
                    {
                        if (value == Controls[newIndex])
                        {
                            break;
                        }
                    }
                    TabChangingEventArgs tcea = new TabChangingEventArgs(selectedIndex, newIndex);
                    OnTabChanging(tcea);
                    if (tcea.Cancel)
                    {
                        return;
                    }
                    selectedIndex = newIndex;
                    selectedTab.Visible = false;
                    selectedTab = value;
                    selectedTab.Visible = true;
                    PerformLayout();
                    InU();
                    OnTabChanged(new EventArgs());
                }
            }
        }

        /// <summary>
        /// Inherited from <see cref="Control"/>.
        /// </summary>
        /// <remarks>See <see cref="Control.ForeColor"/>.
        /// </remarks>
        public override Color ForeColor
        {
            get
            {
                return base.ForeColor;
            }
            set
            {
                base.ForeColor = value;
                foreBrush = new SolidBrush(value);
            }
        }

        public Color InActiveForeColor
        {
            get
            {
                return inactiveForeColor;
            }
            set
            {
                inactiveForeBrush = new SolidBrush(value);
            }
        }

        /// <summary>
        /// Inherited from <see cref="Control"/>.
        /// </summary>
        public override Rectangle DisplayRectangle
        {
            get
            {
                return transformedDisplayRectangle;
            }
        }
#endregion

        public CTabControl()
        {
            SetStyle(ControlStyles.UserPaint | ControlStyles.AllPaintingInWmPaint | ControlStyles.DoubleBuffer, true);

            defaultImageIndex = -1;
            tabLengths = new ArrayList(5);
            leftArrow = new Point[3];
            rightArrow = new Point[3];
            for (int i = 0; i < 3; i++)
            {
                leftArrow[i] = new Point(0, 0);
                rightArrow[i] = new Point(0, 0);
            }
            userTabFont = Font;
            boldTabFont = new Font(Font.FontFamily.Name, Font.Size, FontStyle.Bold);
            userTabMargin = 3;
            userTabDock = DockStyle.Top;
            selectedIndex = -1;
            foreBrush = new SolidBrush(ForeColor);
            inactiveForeColor = ForeColor;
            inactiveForeBrush = new SolidBrush(ForeColor);

            activeBrush = (Brush)SystemBrushes.Control.Clone();
            activeColor = SystemColors.Control;
            inActiveBrush = (Brush)SystemBrushes.Window.Clone();
            inActiveColor = SystemColors.Window;
            borderPen = (Pen)Pens.DarkGray.Clone();
            shadowPen = (Pen)SystemPens.ControlDark.Clone();
            highlightPen = (Pen)SystemPens.ControlLight.Clone();
            displayRectangle = Rectangle.Empty;
            tabsRectangle = Rectangle.Empty;
            clientRectangle = Rectangle.Empty;
            transformedDisplayRectangle = Rectangle.Empty;
            Height = Width = 300;
            BackColor = SystemColors.Control;
            CalculateTabSpan();
            CalculateTabLengths();
            CalculateLastVisibleTabIndex();
            ChildTextChangeEventHandler = new EventHandler(TabPage_TextChanged);
            OverIndex = -1;
        }
        
        #region "Code related to Bounds"
        /// <summary>
        /// Returns the bounding rectangle for a specified tab in this tab control.
        /// </summary>
        /// <param name="index">The 0-based index of the tab you want.</param>
        /// <returns>A <see cref="Rectangle"/> that represents the bounds of the specified tab.</returns>
        /// <exception cref="ArgumentOutOfRangeException">
        /// The index is less than zero.<br />-or-<br />The index is greater than or equal to <see cref="Control.ControlCollection.Count" />.
        /// </exception>
        public new virtual Rectangle GetTabRect(int index)
        {
            if (index < 0 || index >= Controls.Count)
            {
                throw new ArgumentOutOfRangeException("index", index, "The value of index passed to GetTabRect fell outside the valid range.");
            }
            float l = 0.0f;
            if (userTabDock == DockStyle.Left)
            {
                l = Height - Convert.ToSingle(tabLengths[0]) + tabLeftDif;
                for (int i = 0; i < index; i++)
                {
                    l -= Convert.ToSingle(tabLengths[i + 1]);
                }
            }
            else
            {
                l = 2.0f * Convert.ToSingle(userTabMargin) - tabLeftDif;
                for (int i = 0; i < index; i++)
                {
                    l += Convert.ToSingle(tabLengths[i]);
                }
            }
            switch (userTabDock)
            {
                case DockStyle.Bottom:
                    return new Rectangle(Convert.ToInt32(l), 3 * userTabMargin + clientRectangle.Height, Convert.ToInt32(tabLengths[index]), Convert.ToInt32(tabSpan) + userTabMargin);
                case DockStyle.Right:
                    return new Rectangle(clientRectangle.Height, Convert.ToInt32(l), Convert.ToInt32(tabSpan) + userTabMargin, Convert.ToInt32(tabLengths[index]));
                case DockStyle.Left:
                    return new Rectangle(userTabMargin, Convert.ToInt32(l), Convert.ToInt32(tabSpan) + userTabMargin, Convert.ToInt32(tabLengths[index]));
            }
            return new Rectangle(Convert.ToInt32(l), 2 * userTabMargin, Convert.ToInt32(tabLengths[index]), Convert.ToInt32(tabSpan) + userTabMargin);
        }

        /// <summary>
        /// Gets the <see cref="Rectangle"/> that contains the left
        /// scroll button.
        /// </summary>
        /// <returns>
        /// A <see cref="Rectangle"/>.
        /// </returns>
        public virtual Rectangle GetLeftScrollButtonRect()
        {
            Rectangle r = Rectangle.Empty;
            if (showScrollButton == CTabScrollButtonStyle.Always)
            {
                int tabSpan = Convert.ToInt32(this.tabSpan);
                switch (userTabDock)
                {
                    case DockStyle.Top:
                        r = new Rectangle(Width - 2 * tabsRectangle.Height, 0, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Bottom:
                        r = new Rectangle(Width - 2 * tabsRectangle.Height, clientRectangle.Height, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Left:
                        r = new Rectangle(0, tabsRectangle.Height, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Right:
                        r = new Rectangle(Width - tabsRectangle.Height, Height - 2 * tabsRectangle.Height, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                }
            }
            return r;
        }

        /// <summary>
        /// Gets the <see cref="Rectangle"/> that contains the left
        /// scroll button.
        /// </summary>
        /// <returns>
        /// A <see cref="Rectangle"/>.
        /// </returns>
        public virtual Rectangle GetRightScrollButtonRect()
        {
            Rectangle r = Rectangle.Empty;
            if (showScrollButton == CTabScrollButtonStyle.Always)
            {
                int tabSpan = Convert.ToInt32(this.tabSpan);
                switch (userTabDock)
                {
                    case DockStyle.Top:
                        r = new Rectangle(Width - tabsRectangle.Height, 0, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Bottom:
                        r = new Rectangle(Width - tabsRectangle.Height, clientRectangle.Height, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Left:
                        r = new Rectangle(0, 0, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                    case DockStyle.Right:
                        r = new Rectangle(Width - tabsRectangle.Height, Height - tabsRectangle.Height, tabsRectangle.Height, tabsRectangle.Height);
                        break;
                }
            }
            return r;
        }
        /// <summary>
        /// Scrolls the tabs by the specified <i>amount</i>.
        /// </summary>
        /// <param name="amount">
        /// The number of pixels to scroll the tabs.
        /// </param>
        /// <remarks>
        /// Positive amounts will scroll the tabs to the left. Negative
        /// amounts will scroll the tabs to the right.
        /// </remarks>
        public virtual void ScrollTabs(int amount)
        {
            tabLeftDif = Math.Max(0, tabLeftDif - amount);
            if (tabLeftDif <= 0 || tabLeftDif >= totalTabSpan - Convert.ToSingle(tabLengths[tabLengths.Count - 1]))
            {
                lock (this)
                {
                    keepScrolling = false;
                }
            }
            if (tabLeftDif >= totalTabSpan - Convert.ToSingle(tabLengths[tabLengths.Count - 1]))
            {
                canScrollLeft = false;
            }
            if (tabLeftDif <= 0)
            {
                canScrollRight = false;
            }
            CalculateLastVisibleTabIndex();
            InU();
        }

        #endregion

        #region "Code related to event handler"
        /// <summary>
        /// Fires the <see cref="ScrollButtonStyleChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnScrollButtonStyleChanged(EventArgs ea)
        {
            ScrollButtonStyleChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="TabFontChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnTabFontChanged(EventArgs ea)
        {
            TabFontChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="TabDrawerChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnTabDrawerChanged(EventArgs ea)
        {
            TabDrawerChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="TabDockChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnTabDockChanged(EventArgs ea)
        {
            TabDockChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="TabMarginChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnTabMarginChanged(EventArgs ea)
        {
            TabMarginChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="InactiveColorChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnInactiveColorChanged(EventArgs ea)
        {
            InactiveColorChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="ActiveColorChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnActiveColorChanged(EventArgs ea)
        {
            ActiveColorChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="BorderColorChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/>.
        /// </param>
        protected virtual void OnBorderColorChanged(EventArgs ea)
        {
            BorderColorChanged?.Invoke(this, ea);
        }

        /// <summary>
        /// Fires the <see cref="TabChanging"/> event.
        /// </summary>
        /// <param name="tcea">
        /// Some <see cref="TabChangingEventArgs"/> for the event.
        /// </param>
        protected virtual void OnTabChanging(TabChangingEventArgs tcea)
        {
            TabChanging?.Invoke(this, tcea);
        }

        /// <summary>
        /// Fires the <see cref="TabChanged"/> event.
        /// </summary>
        /// <param name="ea">
        /// Some <see cref="EventArgs"/> for the event.
        /// </param>
        protected virtual void OnTabChanged(EventArgs ea)
        {
            TabChanged?.Invoke(this, ea);
        }

        private void TabPage_TextChanged(object sender, EventArgs e)
        {
            CalculateTabLengths();
            CalculateLastVisibleTabIndex();
        }
        #endregion

        #region "Code related to override methods"
        protected override void OnMouseLeave(EventArgs mea)
        {
            OverIndex = -1;
            Invalidate();
        }
        protected override void OnMouseMove(MouseEventArgs mea)
        {
            base.OnMouseMove(mea);
            int t = -Convert.ToInt32(tabLeftDif);
            Point p = new Point(mea.X - 2 * userTabMargin, mea.Y);
            switch (userTabDock)
            {
                case DockStyle.Bottom:
                    p.Y -= clientRectangle.Height;
                    break;
                case DockStyle.Left:
                    p.Y = mea.X;
                    p.X = Height - mea.Y;
                    break;
                case DockStyle.Right:
                    p.Y = Width - mea.X;
                    p.X = mea.Y;
                    break;
            }
            if (p.Y > userTabMargin && p.Y < Convert.ToInt32(tabSpan + 3.0f * userTabMargin))
            {
                int runningTotal = t;
                for (int i = 0; i <= lastVisibleTabIndex; i++)
                {
                    if (p.X >= runningTotal && p.X < runningTotal + Convert.ToInt32(tabLengths[i]))
                    {
                        bool changed = OverIndex != i;
                        if (changed)
                        {
                            OverIndex = i;
                            Invalidate();
                        }
                        break;
                    }
                    runningTotal += Convert.ToInt32(tabLengths[i]);
                }
            }
            else
            {
                OverIndex = -1;
                Invalidate();
            }
        }
        protected override void OnMouseDown(MouseEventArgs mea)
        {
            // TabScroll button을 눌렀을 때 처리
            base.OnMouseDown(mea);
            Point p = new Point(mea.X - 2 * userTabMargin, mea.Y);
            switch (userTabDock)
            {
                case DockStyle.Bottom:
                    p.Y -= clientRectangle.Height;
                    break;
                case DockStyle.Left:
                    p.Y = mea.X;
                    p.X = Height - mea.Y;
                    break;
                case DockStyle.Right:
                    p.Y = Width - mea.X;
                    p.X = mea.Y;
                    break;
            }
            if (p.Y > userTabMargin && p.Y < Convert.ToInt32(tabSpan + 3.0f * userTabMargin))
            {
                if ((showScrollButton == CTabScrollButtonStyle.Always || (showScrollButton == CTabScrollButtonStyle.Auto && totalTabSpan > calcWidth)) && p.X >= rightArrow[0].X - 3 * userTabMargin)
                {
                    if (canScrollRight)
                    {
                        keepScrolling = true;
                        ScrollerThread st = new ScrollerThread(2, this);
                        Thread t = new Thread(new ThreadStart(st.ScrollIt));
                        t.Start();
                    }
                }
                else if ((showScrollButton == CTabScrollButtonStyle.Always || (showScrollButton == CTabScrollButtonStyle.Auto && totalTabSpan > calcWidth)) && p.X >= leftArrow[2].X - 3 * userTabMargin)
                {
                    if (canScrollLeft)
                    {
                        keepScrolling = true;
                        ScrollerThread st = new ScrollerThread(-2, this);
                        Thread t = new Thread(new ThreadStart(st.ScrollIt));
                        t.Start();
                    }
                }
                else
                {
                    int t = -Convert.ToInt32(tabLeftDif);
                    for (int i = 0; i <= lastVisibleTabIndex; i++)
                    {
                        if (p.X >= t && p.X < t + Convert.ToInt32(tabLengths[i]))
                        {
                            SelectedIndex = i;
                            break;
                        }
                        t += Convert.ToInt32(tabLengths[i]);
                    }
                }
            }
        }
        protected override void OnMouseUp(MouseEventArgs mea)
        {
            lock (this)
            {
                keepScrolling = false;
            }
            base.OnMouseUp(mea);
        }
        protected override void OnControlAdded(ControlEventArgs cea)
        {
            base.OnControlAdded(cea);
            cea.Control.Visible = false;
            if (selectedIndex == -1)
            {
                selectedIndex = 0;
                selectedTab = (CTabPage)cea.Control;
                selectedTab.Visible = true;
            }
            cea.Control.TextChanged += ChildTextChangeEventHandler;
            CalculateTabLengths();
            CalculateLastVisibleTabIndex();
            InU();
        }
        protected override void OnControlRemoved(ControlEventArgs cea)
        {
            cea.Control.TextChanged -= ChildTextChangeEventHandler;
            base.OnControlRemoved(cea);
            if (Controls.Count > 0)
            {
                selectedIndex = 0;
                selectedTab.Visible = false;
                selectedTab = (CTabPage)Controls[0];
                selectedTab.Visible = true;
            }
            else
            {
                selectedIndex = -1;
                selectedTab = null;
            }
            CalculateTabLengths();
            CalculateLastVisibleTabIndex();
            InU();
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.Dispose(disposing);
                inActiveBrush.Dispose();
                activeBrush.Dispose();
                foreBrush.Dispose();
                inactiveForeBrush.Dispose();
                highlightPen.Dispose();
                shadowPen.Dispose();
                borderPen.Dispose();
                if (tabRenderer != null)
                {
                    tabRenderer.Dispose();
                }
                foreach (Control c in Controls)
                {
                    c.Dispose();
                }
            }
        }
        protected override void OnPaint(PaintEventArgs pea)
        {
            if (Controls.Count > 0)
            {
                CalculateTabLengths();

                bool invert = false;
                // Create a transformation given the orientation of the tabs.
                switch (userTabDock)
                {
                    case DockStyle.Bottom:
                        invert = true;
                        pea.Graphics.TranslateTransform(Convert.ToSingle(calcWidth), Convert.ToSingle(calcHeight));
                        pea.Graphics.RotateTransform(180.0f);
                        break;
                    case DockStyle.Left:
                        pea.Graphics.TranslateTransform(0, Convert.ToSingle(Height));
                        pea.Graphics.RotateTransform(-90.0f);
                        break;
                    case DockStyle.Right:
                        pea.Graphics.TranslateTransform(Convert.ToSingle(Width), 0);
                        pea.Graphics.RotateTransform(90.0f);
                        break;
                }

                // Paint the areas.
                pea.Graphics.FillRectangle(inActiveBrush, tabsRectangle);
                pea.Graphics.FillRectangle(activeBrush, clientRectangle);

                // Draws the highlight/shadow line, if applicable.
                Pen p = borderPen;
                if (tabRenderer != null && tabRenderer.UsesHighlghts)
                {
                    if (DockStyle.Right == userTabDock || DockStyle.Bottom == userTabDock)
                    {
                        p = shadowPen;
                    }
                    else
                    {
                        p = highlightPen;
                    }
                }
                pea.Graphics.DrawLine(p, 0, clientRectangle.Y, calcWidth, clientRectangle.Y);

                // Save the current transform so that we can go back to it
                // after printing the tabs.
                Matrix m = pea.Graphics.Transform;
                SizeF s = new SizeF(0, tabSpan + 2.0f * userTabMargin + 1.0f);

                // If a tab drawer exists, use it.
                if (tabRenderer != null)
                {
                    if (!invert)
                    {
                        pea.Graphics.TranslateTransform(2.0f * Convert.ToSingle(userTabMargin) - tabLeftDif, userTabMargin + 1.0f);
                    }
                    else
                    {
                        pea.Graphics.TranslateTransform(Convert.ToSingle(Width) - 2.0f * userTabMargin - Convert.ToSingle(tabLengths[0]) + tabLeftDif, userTabMargin + 1.0f);
                    }
                    // The transform to the selected tab.
                    Matrix selTransform = null;

                    // Draw the tabs from left to right skipping over the
                    // selected tab.
                    for (int i = 0; i <= lastVisibleTabIndex; i++)
                    {
                        s.Width = Convert.ToSingle(tabLengths[i]);
                        if (i != selectedIndex)
                        {
                            tabRenderer.DrawTab(activeColor, inActiveColor, highlightPen.Color, shadowPen.Color, borderPen.Color, false, i == OverIndex, userTabDock, pea.Graphics, s);
                        }
                        else
                        {
                            selTransform = pea.Graphics.Transform;
                        }
                        if (invert)
                        {
                            if (i + 1 < tabLengths.Count)
                            {
                                pea.Graphics.TranslateTransform(-Convert.ToSingle(tabLengths[i + 1]), 0.0f);
                            }
                        }
                        else
                        {
                            pea.Graphics.TranslateTransform(s.Width, 0.0f);
                        }
                    }

                    // Now, draw the selected tab.
                    if (selTransform != null)
                    {
                        pea.Graphics.Transform = selTransform;
                        s.Width = Convert.ToSingle(tabLengths[selectedIndex]);
                        tabRenderer.DrawTab(activeColor, inActiveColor, highlightPen.Color, shadowPen.Color, borderPen.Color, true, SelectedIndex == OverIndex, userTabDock, pea.Graphics, s);
                    }
                }

                // Draw the strings. If the tabs are docked on the bottom, change
                // the tranformation to draw the string right-side up.
                if (!invert)
                {
                    pea.Graphics.Transform = m;
                    pea.Graphics.TranslateTransform(2.0f * userTabMargin - tabLeftDif, userTabMargin);
                }
                else
                {
                    pea.Graphics.ResetTransform();
                    pea.Graphics.TranslateTransform(2.0f * userTabMargin - tabLeftDif, clientRectangle.Height);
                }
                CTabPage userTabPage = null;
                for (int i = 0; i <= lastVisibleTabIndex; i++)
                {
                    s.Width = Convert.ToSingle(tabLengths[i]);
                    userTabPage = Controls[i] as CTabPage;
                    if (userTabPage != null && images != null && userTabPage.ImageIndex > -1 && userTabPage.ImageIndex < images.Images.Count && images.Images[userTabPage.ImageIndex] != null)
                    {
                        pea.Graphics.DrawImage(images.Images[userTabPage.ImageIndex], 1.5f * userTabMargin, userTabMargin);
                        if (i != selectedIndex)
                        {
                            pea.Graphics.DrawString(Controls[i].Text, userTabFont, inactiveForeBrush, 2.5f * userTabMargin + images.Images[userTabPage.ImageIndex].Width, 1.5f * userTabMargin);
                        }
                        else
                        {
                            pea.Graphics.DrawString(Controls[i].Text, boldTabFont, foreBrush, 2.5f * userTabMargin + images.Images[userTabPage.ImageIndex].Width, 1.5f * userTabMargin);
                        }
                    }
                    else if (userTabPage != null && images != null && defaultImageIndex > -1 && defaultImageIndex < images.Images.Count && images.Images[defaultImageIndex] != null)
                    {
                        pea.Graphics.DrawImage(images.Images[defaultImageIndex], 1.5f * userTabMargin, userTabMargin);
                        if (i != selectedIndex)
                        {
                            pea.Graphics.DrawString(Controls[i].Text, userTabFont, inactiveForeBrush, 2.5f * userTabMargin + images.Images[defaultImageIndex].Width, 1.5f * userTabMargin);
                        }
                        else
                        {
                            pea.Graphics.DrawString(Controls[i].Text, boldTabFont, foreBrush, 2.5f * userTabMargin + images.Images[defaultImageIndex].Width, 1.5f * userTabMargin);
                        }
                    }
                    else
                    {
                        if (i != selectedIndex)
                        {
                            pea.Graphics.DrawString(Controls[i].Text, userTabFont, inactiveForeBrush, 1.5f * userTabMargin, 1.5f * userTabMargin);
                        }
                        else
                        {
                            pea.Graphics.DrawString(Controls[i].Text, boldTabFont, foreBrush, 1.5f * userTabMargin, 1.5f * userTabMargin);
                        }
                    }
                    pea.Graphics.TranslateTransform(s.Width, 0);
                }

                // Draw the scroll buttons, if necessary
                canScrollLeft = canScrollRight = false;
                if (showScrollButton == CTabScrollButtonStyle.Always || (showScrollButton == CTabScrollButtonStyle.Auto && totalTabSpan > calcWidth))
                {
                    if (invert)
                    {
                        pea.Graphics.ResetTransform();
                        pea.Graphics.TranslateTransform(0, clientRectangle.Height);
                    }
                    else
                    {
                        pea.Graphics.Transform = m;
                    }
                    pea.Graphics.FillRectangle(inActiveBrush, calcWidth - 2 * tabsRectangle.Height, 0, 2 * tabsRectangle.Height, tabsRectangle.Height);
                    pea.Graphics.DrawRectangle(borderPen, calcWidth - 2 * tabsRectangle.Height, 0, 2 * tabsRectangle.Height, tabsRectangle.Height);
                    if (((showScrollButton == CTabScrollButtonStyle.Always && totalTabSpan > calcWidth - 2 * Convert.ToInt32(tabsRectangle.Height)) || (showScrollButton == CTabScrollButtonStyle.Auto && totalTabSpan > calcWidth)) && tabLeftDif < totalTabSpan - Convert.ToSingle(tabLengths[tabLengths.Count - 1]))
                    {
                        canScrollLeft = true;
                        pea.Graphics.FillPolygon(borderPen.Brush, leftArrow);
                    }
                    if (tabLeftDif > 0)
                    {
                        canScrollRight = true;
                        pea.Graphics.FillPolygon(borderPen.Brush, rightArrow);
                    }
                    pea.Graphics.DrawPolygon(borderPen, leftArrow);
                    pea.Graphics.DrawPolygon(borderPen, rightArrow);
                }
            }

            // Reset the transform and draw the border.
            pea.Graphics.ResetTransform();
            pea.Graphics.DrawRectangle(borderPen, 0, 0, ClientRectangle.Width - 1, ClientRectangle.Height - 1);
        }
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            CalculateRectangles();
            CalculateLastVisibleTabIndex();
            if (tabLengths.Count > 0 && tabLeftDif >= totalTabSpan - Convert.ToSingle(tabLengths[tabLengths.Count - 1]))
            {
                tabLeftDif = 0;
                ScrollTabs(-Convert.ToInt32(totalTabSpan - Convert.ToSingle(tabLengths[tabLengths.Count - 1])));
            }
            PerformLayout();
            InU();
        }
        protected override Control.ControlCollection CreateControlsInstance()
        {
            return new CTabControl.ControlCollection(this);
        }
        #endregion

        #region "Code related to calculate properties"
        /// <summary>
        /// Calculates the last visible tab shown on the control.
        /// </summary>
        private void CalculateLastVisibleTabIndex()
        {
            lastVisibleTabLeft = 0.0f;
            float t = 0.0f;
            for (int i = 0; i < tabLengths.Count; i++)
            {
                lastVisibleTabIndex = i;
                t += Convert.ToSingle(tabLengths[i]) + 2.0f;
                if (t > calcWidth + tabLeftDif)
                {
                    break;
                }
                lastVisibleTabLeft = t;
            }
        }

        /// <summary>
        /// Calculates and caches the length of each tab given the value
        /// of the <see cref="Control.Text"/> property of each
        /// <see cref="CTabPage"/>.
        /// </summary>
        private void CalculateTabLengths()
        {
            totalTabSpan = 0.0f;
            tabLengths.Clear();
            Graphics g = CreateGraphics();
            float f = 0.0f;
            CTabPage userTabPage;
            for (int i = 0; i < Controls.Count; i++)
            {
                f = g.MeasureString(Controls[i].Text, boldTabFont).Width + 4.0f * Convert.ToSingle(userTabMargin);
                userTabPage = Controls[i] as CTabPage;
                if (userTabPage != null && images != null && userTabPage.ImageIndex > -1 && userTabPage.ImageIndex < images.Images.Count && images.Images[userTabPage.ImageIndex] != null)
                {
                    f += images.Images[userTabPage.ImageIndex].Width + 2.0f * userTabMargin;
                }
                else if (userTabPage != null && images != null && defaultImageIndex > -1 && defaultImageIndex < images.Images.Count && images.Images[defaultImageIndex] != null)
                {
                    f += images.Images[defaultImageIndex].Width + 2.0f * userTabMargin;
                }
                tabLengths.Add(f);
                totalTabSpan += f;
            }
        }

        /// <summary>
        /// Calculates the span of a tab given the value of the <see cref="Font"/>
        /// property.
        /// </summary>
        private void CalculateTabSpan()
        {
            tabSpan = 0;
            if (images != null)
            {
                for (int i = 0; i < images.Images.Count; i++)
                {
                    tabSpan = Math.Max(tabSpan, images.Images[i].Height);
                }
            }
            tabSpan = Math.Max(tabSpan, CreateGraphics().MeasureString(@"ABCDEFGHIJKLMNOPQURSTUVWXYZabcdefghijklmnopqrstuvwxyz", userTabFont).Height + 1.0f);
        }

        /// <summary>
        /// Calculates the rectangles for the tab area, the client area,
        /// the display area, and the transformed display area.
        /// </summary>
        private void CalculateRectangles()
        {
            int spanAndMargin = Convert.ToInt32(tabSpan) + 3 * userTabMargin + 2;
            Size s;

            calcHeight = (DockStyle.Top == userTabDock || DockStyle.Bottom == userTabDock) ? Height : Width;
            calcWidth = (DockStyle.Top == userTabDock || DockStyle.Bottom == userTabDock) ? Width : Height;

            tabsRectangle.X = 0;
            tabsRectangle.Y = 0;
            s = tabsRectangle.Size;
            s.Width = calcWidth;
            s.Height = spanAndMargin;
            tabsRectangle.Size = s;

            leftArrow[0].X = s.Width - s.Height - userTabMargin;
            leftArrow[0].Y = userTabMargin;
            leftArrow[1].X = leftArrow[0].X;
            leftArrow[1].Y = s.Height - userTabMargin;
            leftArrow[2].X = s.Width - 2 * s.Height + userTabMargin;
            leftArrow[2].Y = s.Height / 2;

            rightArrow[0].X = s.Width - s.Height + userTabMargin;
            rightArrow[0].Y = userTabMargin;
            rightArrow[1].X = rightArrow[0].X;
            rightArrow[1].Y = s.Height - userTabMargin;
            rightArrow[2].X = s.Width - userTabMargin;
            rightArrow[2].Y = s.Height / 2;

            clientRectangle.X = 0;
            clientRectangle.Y = spanAndMargin;
            s = clientRectangle.Size;
            s.Width = calcWidth;
            s.Height = calcHeight - spanAndMargin;
            clientRectangle.Size = s;

            displayRectangle.X = userTabMargin + 1;
            displayRectangle.Y = spanAndMargin + userTabMargin + 1;
            s = displayRectangle.Size;
            s.Width = calcWidth - 2 * (userTabMargin + 1);
            s.Height = clientRectangle.Size.Height - 2 * userTabMargin - 2;
            displayRectangle.Size = s;

            switch (userTabDock)
            {
                case DockStyle.Top:
                    transformedDisplayRectangle.Location = displayRectangle.Location;
                    transformedDisplayRectangle.Size = displayRectangle.Size;
                    break;
                case DockStyle.Bottom:
                    transformedDisplayRectangle.X = userTabMargin + 1;
                    transformedDisplayRectangle.Y = userTabMargin + 1;
                    transformedDisplayRectangle.Size = displayRectangle.Size;
                    break;
                case DockStyle.Right:
                    transformedDisplayRectangle.X = userTabMargin + 1;
                    transformedDisplayRectangle.Y = userTabMargin + 1;
                    s.Height = displayRectangle.Size.Width;
                    s.Width = displayRectangle.Size.Height;
                    transformedDisplayRectangle.Size = s;
                    break;
                case DockStyle.Left:
                    transformedDisplayRectangle.X = displayRectangle.Top;
                    transformedDisplayRectangle.Y = calcWidth - displayRectangle.Right;
                    s.Height = displayRectangle.Size.Width;
                    s.Width = displayRectangle.Size.Height;
                    transformedDisplayRectangle.Size = s;
                    break;
            }
        }

        /// <summary>
        /// Invalidates and updates the <see cref="CTablControl"/>.
        /// </summary>
        private void InU()
        {
            Invalidate();
            Update();
        }
        #endregion

        #region "Code related to internal class"
        /// <summary>
        /// Let's the tabs scroll.
        /// </summary>
        private class ScrollerThread
        {
            /// <summary>
            /// Creates a new instance of the
            /// <see cref="CTabControl.ScrollerThread"/> class.
            /// </summary>
            /// <param name="amount">The amount to scroll.</param>
            /// <param name="control">The control to scroll.</param>
            public ScrollerThread(int amount, CTabControl control)
            {
                this.tabControl = control;
                this.amount = new object[] { amount };
                scroller = new ScrollTabsDelegate(tabControl.ScrollTabs);
            }

            /// <summary>
            /// Scrolls the tabs on the <see cref="CTabControl"/>
            /// by the given amount.
            /// </summary>
            public void ScrollIt()
            {
                bool keepScrolling = false;
                lock (tabControl)
                {
                    keepScrolling = tabControl.keepScrolling;
                }
                while (keepScrolling)
                {

                    tabControl.Invoke(scroller, amount);
                    lock (tabControl)
                    {
                        keepScrolling = tabControl.keepScrolling;
                    }
                }
            }

            /// <summary>
            /// The control to scroll.
            /// </summary>
            private CTabControl tabControl;

            /// <summary>
            /// The amount to scroll.
            /// </summary>
            private object[] amount;

            /// <summary>
            /// A delegate to scroll the tabs.
            /// </summary>
            private ScrollTabsDelegate scroller;

            /// <summary>
            /// A delegate to use in scrolling the tabs.
            /// </summary>
            private delegate void ScrollTabsDelegate(int amount);
        }

        /// <summary>
        /// A <see cref="CTabControl"/>-specific
        /// <see cref="Control.ControlCollection"/>.
        /// </summary>
        public new class ControlCollection : Control.ControlCollection
        {
            /// <summary>
            /// Creates a new instance of the
            /// <see cref="CTabControl.ControlCollection"/> class with 
            /// the specified <i>owner</i>.
            /// </summary>
            /// <param name="owner">
            /// The <see cref="CTabControl"/> that owns this collection.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if <i>owner</i> is <b>null</b>.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// Thrown if <i>owner</i> is not a <see cref="CTabControl"/>.
            /// </exception>
            public ControlCollection(Control owner) : base(owner)
            {
                if (owner == null)
                {
                    throw new ArgumentNullException("owner", "Tried to create a CTabControl.ControlCollection with a null owner.");
                }
                this.owner = owner as CTabControl;
                if (this.owner == null)
                {
                    throw new ArgumentException("Tried to create a CTabControl.ControlCollection with a non-CTabControl owner.", "owner");
                }
                monitor = new EventHandler(this.owner.ChildTabTextChanged);
            }

            /// <summary>
            /// Overridden. Adds a <see cref="Control"/> to the
            /// <see cref="CTabControl"/>.
            /// </summary>
            /// <param name="value">
            /// The <see cref="Control"/> to add, which must be a
            /// <see cref="CTabPage"/>.
            /// </param>
            /// <exception cref="ArgumentNullException">
            /// Thrown if <i>value</i> is <b>null</b>.
            /// </exception>
            /// <exception cref="ArgumentException">
            /// Thrown if <i>value</i> is not a <see cref="CTabPage"/>.
            /// </exception>
            public override void Add(Control value)
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value", "Tried to add a null value to the CTabControl.ControlCollection.");
                }
                var p = value as CTabPage;
                if (p == null)
                {
                    throw new ArgumentException("Tried to add a non-CTabPage control to the CTabControl.ControlCollection.", "value");
                }
                p.SendToBack();
                base.Add(p);
                p.TextChanged += monitor;
            }

            /// <summary>
            /// Overridden. Inherited from <see cref="Control.ControlCollection.Remove( Control )"/>.
            /// </summary>
            /// <param name="value"></param>
            public override void Remove(Control value)
            {
                value.TextChanged -= monitor;
                base.Remove(value);
            }

            /// <summary>
            /// Overridden. Inherited from <see cref="Control.ControlCollection.Clear()"/>.
            /// </summary>
            public override void Clear()
            {
                foreach (Control c in this)
                {
                    c.TextChanged -= monitor;
                }
                base.Clear();
            }

            /// <summary>
            /// The owner of this <see cref="CTabControl.ControlCollection"/>.
            /// </summary>
            private CTabControl owner;

            private EventHandler monitor;
        }
        #endregion

    }
    public enum CTabScrollButtonStyle
    {
        /// <summary>
        /// Indicates that the scroll buttons should get drawn
        /// regardless of whether the tabs extend beyond the
        /// visual tab area.
        /// </summary>
        Always,

        /// <summary>
        /// Indicates that the scroll buttons should get drawn
        /// only when the tabs extend beyond the visible span
        /// of the tab rectangle.
        /// </summary>
        Auto,

        /// <summary>
        /// Indicates that the scroll buttons should never get
        /// drawn.
        /// </summary>
        Never
    }
}
