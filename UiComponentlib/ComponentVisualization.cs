using Rootech.UI.Component.Properties;
using System;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Runtime.InteropServices;

namespace Rootech.UI.Component
{
    public class ComponentVisualization
    {
        #region Color constants
        //Constant color values
        public static readonly Color PRIMARY_TEXT_BLACK = Color.FromArgb(222, 0, 0, 0);
        public static readonly Brush PRIMARY_TEXT_BLACK_BRUSH = new SolidBrush(PRIMARY_TEXT_BLACK);
        public static Color SECONDARY_TEXT_BLACK = Color.FromArgb(138, 0, 0, 0);
        public static Brush SECONDARY_TEXT_BLACK_BRUSH = new SolidBrush(SECONDARY_TEXT_BLACK);
        public static readonly Color DISABLED_OR_HINT_TEXT_BLACK = Color.FromArgb(66, 0, 0, 0);
        public static readonly Brush DISABLED_OR_HINT_TEXT_BLACK_BRUSH = new SolidBrush(DISABLED_OR_HINT_TEXT_BLACK);
        public static readonly Color DIVIDERS_BLACK = Color.FromArgb(31, 0, 0, 0);
        public static readonly Brush DIVIDERS_BLACK_BRUSH = new SolidBrush(DIVIDERS_BLACK);

        public static readonly Color PRIMARY_TEXT_WHITE = Color.FromArgb(255, 255, 255, 255);
        public static readonly Brush PRIMARY_TEXT_WHITE_BRUSH = new SolidBrush(PRIMARY_TEXT_WHITE);
        public static Color SECONDARY_TEXT_WHITE = Color.FromArgb(179, 255, 255, 255);
        public static Brush SECONDARY_TEXT_WHITE_BRUSH = new SolidBrush(SECONDARY_TEXT_WHITE);
        public static readonly Color DISABLED_OR_HINT_TEXT_WHITE = Color.FromArgb(77, 255, 255, 255);
        public static readonly Brush DISABLED_OR_HINT_TEXT_WHITE_BRUSH = new SolidBrush(DISABLED_OR_HINT_TEXT_WHITE);
        public static readonly Color DIVIDERS_WHITE = Color.FromArgb(31, 255, 255, 255);
        public static readonly Brush DIVIDERS_WHITE_BRUSH = new SolidBrush(DIVIDERS_WHITE);

        // Checkbox colors
        public static readonly Color CHECKBOX_OFF_LIGHT = Color.FromArgb(138, 0, 0, 0);
        public static readonly Brush CHECKBOX_OFF_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_LIGHT);
        public static readonly Color CHECKBOX_OFF_DISABLED_LIGHT = Color.FromArgb(66, 0, 0, 0);
        public static readonly Brush CHECKBOX_OFF_DISABLED_LIGHT_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_LIGHT);

        public static readonly Color CHECKBOX_OFF_DARK = Color.FromArgb(179, 255, 255, 255);
        public static readonly Brush CHECKBOX_OFF_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DARK);
        public static readonly Color CHECKBOX_OFF_DISABLED_DARK = Color.FromArgb(77, 255, 255, 255);
        public static readonly Brush CHECKBOX_OFF_DISABLED_DARK_BRUSH = new SolidBrush(CHECKBOX_OFF_DISABLED_DARK);

        //Raised button
        public static readonly Color RAISED_BUTTON_BACKGROUND = Color.FromArgb(255, 255, 255, 255);
        public static readonly Brush RAISED_BUTTON_BACKGROUND_BRUSH = new SolidBrush(RAISED_BUTTON_BACKGROUND);
        public static readonly Color RAISED_BUTTON_TEXT_LIGHT = PRIMARY_TEXT_WHITE;
        public static readonly Brush RAISED_BUTTON_TEXT_LIGHT_BRUSH = new SolidBrush(RAISED_BUTTON_TEXT_LIGHT);
        public static readonly Color RAISED_BUTTON_TEXT_DARK = PRIMARY_TEXT_BLACK;
        public static readonly Brush RAISED_BUTTON_TEXT_DARK_BRUSH = new SolidBrush(RAISED_BUTTON_TEXT_DARK);

        //Flat button
        public static readonly Color FLAT_BUTTON_BACKGROUND_HOVER_LIGHT = Color.FromArgb(20.PercentageToColorComponent(), 0x999999.ToColor());
        public static readonly Brush FLAT_BUTTON_BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_HOVER_LIGHT);
        public static readonly Color FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT = Color.FromArgb(40.PercentageToColorComponent(), 0x999999.ToColor());
        public static readonly Brush FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_PRESSED_LIGHT);
        public static readonly Color FLAT_BUTTON_DISABLEDTEXT_LIGHT = Color.FromArgb(26.PercentageToColorComponent(), 0x000000.ToColor());
        public static readonly Brush FLAT_BUTTON_DISABLEDTEXT_LIGHT_BRUSH = new SolidBrush(FLAT_BUTTON_DISABLEDTEXT_LIGHT);

        public static readonly Color FLAT_BUTTON_BACKGROUND_HOVER_DARK = Color.FromArgb(15.PercentageToColorComponent(), 0xCCCCCC.ToColor());
        public static readonly Brush FLAT_BUTTON_BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_HOVER_DARK);
        public static readonly Color FLAT_BUTTON_BACKGROUND_PRESSED_DARK = Color.FromArgb(25.PercentageToColorComponent(), 0xCCCCCC.ToColor());
        public static readonly Brush FLAT_BUTTON_BACKGROUND_PRESSED_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_BACKGROUND_PRESSED_DARK);
        public static readonly Color FLAT_BUTTON_DISABLEDTEXT_DARK = Color.FromArgb(30.PercentageToColorComponent(), 0xFFFFFF.ToColor());
        public static readonly Brush FLAT_BUTTON_DISABLEDTEXT_DARK_BRUSH = new SolidBrush(FLAT_BUTTON_DISABLEDTEXT_DARK);

        //ContextMenuStrip
        public static readonly Color CMS_BACKGROUND_LIGHT_HOVER = Color.FromArgb(255, 238, 238, 238);
        public static readonly Brush CMS_BACKGROUND_HOVER_LIGHT_BRUSH = new SolidBrush(CMS_BACKGROUND_LIGHT_HOVER);

        public static readonly Color CMS_BACKGROUND_DARK_HOVER = Color.FromArgb(38, 204, 204, 204);
        public static readonly Brush CMS_BACKGROUND_HOVER_DARK_BRUSH = new SolidBrush(CMS_BACKGROUND_DARK_HOVER);

        //Application background
        public static readonly Color BACKGROUND_LIGHT = Color.FromArgb(255, 255, 255, 255);
        public static Brush BACKGROUND_LIGHT_BRUSH = new SolidBrush(BACKGROUND_LIGHT);

        public static readonly Color BACKGROUND_DARK = Color.FromArgb(255, 51, 51, 51);
        public static Brush BACKGROUND_DARK_BRUSH = new SolidBrush(BACKGROUND_DARK);

        //Application action bar
        public readonly Color ACTION_BAR_TEXT = Color.FromArgb(255, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_BRUSH = new SolidBrush(Color.FromArgb(255, 255, 255, 255));
        public readonly Color ACTION_BAR_TEXT_SECONDARY = Color.FromArgb(153, 255, 255, 255);
        public readonly Brush ACTION_BAR_TEXT_SECONDARY_BRUSH = new SolidBrush(Color.FromArgb(153, 255, 255, 255));
        #endregion

        private ColorScheme _colorScheme;
        public ColorScheme ColorScheme
        {
            get { return _colorScheme; }
            set
            {
                _colorScheme = value;
            }
        }
       
        #region Fonts
        private readonly PrivateFontCollection privateFontCollection = new PrivateFontCollection();
        public Font ROBOTO_MEDIUM_12;
        public Font ROBOTO_REGULAR_11;
        public Font ROBOTO_MEDIUM_11;
        public Font ROBOTO_MEDIUM_10;
        #endregion

        public ComponentVisualization()
        {
            // load fontfamilys
            ROBOTO_MEDIUM_12 = new Font(GetFontFamilyByResource(Resources.Roboto_Medium), 12f);
            ROBOTO_MEDIUM_10 = new Font(GetFontFamilyByResource(Resources.Roboto_Medium), 10f);
            ROBOTO_REGULAR_11 = new Font(GetFontFamilyByResource(Resources.Roboto_Regular), 11f);
            ROBOTO_MEDIUM_11 = new Font(GetFontFamilyByResource(Resources.Roboto_Medium), 11f);

            ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
        }
        
        private FontFamily GetFontFamilyByResource(byte[] fontResource)
        {
            int dataLength = fontResource.Length;
            IntPtr fontPtr = Marshal.AllocCoTaskMem(dataLength);
            Marshal.Copy(fontResource, 0, fontPtr, dataLength);

            uint cFonts = 0;
            AddFontMemResourceEx(fontPtr, (uint)fontResource.Length, IntPtr.Zero, ref cFonts);
            privateFontCollection.AddMemoryFont(fontPtr, dataLength);

            return privateFontCollection.Families.Last();
        }

        [DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont, IntPtr pvd, [In] ref uint pcFonts);
        
    }
}
