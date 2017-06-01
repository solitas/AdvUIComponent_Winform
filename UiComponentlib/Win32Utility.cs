using System;
using System.Runtime.InteropServices;

namespace Rootech.UI.Component
{
    class Win32Utility
    {

        public struct MARGINS
        {
            private int leftWidth;
            private int rightWidth;
            private int topHeight;
            private int bottomHeight;

            public int LeftWidth { get { return leftWidth; } set { leftWidth = value; } }
            public int RightWidth { get { return rightWidth; } set { rightWidth = value; } }
            public int TopHeight { get { return topHeight; } set { topHeight = value; } }
            public int BottomHeight { get { return bottomHeight; } set { bottomHeight = value; } }
        }
        [Flags()]
        public enum RedrawWindowFlag : uint
        {
            Invalidate      /**/ = 0X1,
            InternalPaint   /**/ = 0X2,
            Erase           /**/ = 0X4,
            Validate        /**/ = 0X8,
            NoInternalPaint /**/ = 0X10,
            NoErase         /**/ = 0X20,
            NoChildren      /**/ = 0X40,
            AllChildren     /**/ = 0X80,
            UpdateNow       /**/ = 0X100,
            EraseNow        /**/ = 0X200,
            Frame           /**/ = 0X400,
            NoFrame         /**/ = 0X800
        }

        public static int WM_NCPAINT                                    /**/ = 0x0085;
        public static int WM_NCLBUTTONDOWN                              /**/ = 0x00A1;
        public static int HT_CAPTION                                    /**/ = 0x0002;
        public static int WM_LBUTTONDBLCLK                              /**/ = 0x0203;
        public static int CS_DROPSHADOW                                 /**/ = 0x00020000;
        public static int WM_MOUSEMOVE                                  /**/ = 0x0200;
        public static int WM_LBUTTONDOWN                                /**/ = 0x0201;
        public static int WM_LBUTTONUP                                  /**/ = 0x0202;
        public static int WM_RBUTTONDOWN                                /**/ = 0x0204;

        public static int WM_SYSCOMMAND                                 /**/ = 0x0112;
        public static int WS_MINIMIZEBOX                                /**/ = 0x20000;
        public static int WS_SYSMENU                                    /**/ = 0x00080000;

        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hWnd, IntPtr hDC);
        [DllImport("User32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, RedrawWindowFlag flags);
        [DllImport("user32.dll")]
        public static extern int TrackPopupMenuEx(IntPtr hmenu, uint fuFlags, int x, int y, IntPtr hwnd, IntPtr lptpm);
        [DllImport("user32.dll")]
        public static extern IntPtr GetSystemMenu(IntPtr hWnd, bool bRevert);
        [DllImport("user32.dll")]
        public static extern IntPtr MonitorFromWindow(IntPtr hwnd, uint dwFlags);
        [DllImport("User32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetMonitorInfo(HandleRef hmonitor, [In, Out] MONITORINFOEX info);
        #region "Code related to Desktop window manager api"
        [DllImport("dwmapi.dll")]
        public static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS pMarInset);
        [DllImport("dwmapi.dll")]
        public static extern int DwmSetWindowAttribute(IntPtr hwnd, int attr, ref int attrValue, int attrSize);
        [DllImport("dwmapi.dll")]
        public static extern int DwmIsCompositionEnabled(ref int pfEnabled);
        #endregion
    }
    [StructLayout(LayoutKind.Sequential, CharSet = CharSet.Auto, Pack = 4)]
    public class MONITORINFOEX
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFOEX));
        public RECT rcMonitor = new RECT();
        public RECT rcWork = new RECT();
        public int dwFlags = 0;
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public char[] szDevice = new char[32];
    }
    public struct RECT
    {
        public int left;
        public int top;
        public int right;
        public int bottom;

        public int Left { get; internal set; }

        public int Width()
        {
            return right - left;
        }

        public int Height()
        {
            return bottom - top;
        }
    }
}
