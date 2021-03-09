using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Windows.Forms;


namespace WinformThemes
{
    public class WinFormThemeHelper
    {
        private string Name { get; set;  }
        private List<ControlTheme> ControlThemes = new List<ControlTheme>();

        public WinFormThemeHelper(string name)
        {
            this.Name = name; 
        }


        private static List<WinFormThemeHelper> Themes = null;

        private static void Initialize()
        {
            if (Themes == null)
            {
                Themes = new List<WinFormThemeHelper>();
                WinFormThemeHelper darkTheme = new WinFormThemeHelper("Dark");
                darkTheme.ControlThemes.Add(new ControlTheme( new TextBox().GetType(), Color.White, Color.Black));
                darkTheme.ControlThemes.Add(new ControlTheme( new Control().GetType(), Color.White, Color.Black));// keep 'control' type last

                WinFormThemeHelper lightTheme = new WinFormThemeHelper("Light");
                darkTheme.ControlThemes.Add(new ControlTheme( new TextBox().GetType(), Color.White, Color.Black));
                darkTheme.ControlThemes.Add(new ControlTheme(new Control().GetType(), Color.White, Color.Black));// keep 'control' type last

                Themes.Add(darkTheme);
                Themes.Add(lightTheme);
            }
        }

        private static WinFormThemeHelper GetTheme(string themeName)
        {
            WinFormThemeHelper retVal = null;
            foreach (WinFormThemeHelper h in Themes)
            {
                if (h.Name == themeName)
                {
                    retVal = h;
                    break;
                }
            }
            return retVal;
        }

        public static void ApplyTheme(Form form, string themeName)
        {
            Initialize();

            ApplyThemeToElement(form, GetTheme( themeName));
        }

        private static void ApplyThemeToElement(Control control, WinFormThemeHelper theme)
        {
            //for (int i = 0; i < theme.ControlThemes.Count; i++)
            //{
            //    if( control as theme.ControlThemes[i] != null)
            //    {
            //    }
            //}
        }



    }

    public class ControlTheme
    {
        public Type ApplicableType { get; private set;  }
        public Color ForegroundColor { get; private set; }
        public Color BackgroundColor { get; private set; }

        public ControlTheme(Type type, Color fgColor, Color bgColor)
        {
            this.ApplicableType = type;
            this.ForegroundColor = fgColor;
            this.BackgroundColor = bgColor;
        }

    }
}
