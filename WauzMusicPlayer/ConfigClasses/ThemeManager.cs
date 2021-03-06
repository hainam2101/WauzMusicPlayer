﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;

namespace WauzMusicPlayer
{
    class ThemeManager
    {

        public static Image image;

        public static string imageString;

        public static Color mainBackColorVariant1;

        public static Color mainBackColorVariant2;

        public static Color accentBackColor;

        public static Color highlightBackColor;

        public static Color mainFontColor;

        public static Color highlightFontColor;

        private static List<Themable> themableForms = new List<Themable>();

        private static string[] predefinedThemes = { "penguinpurple", "feelsgoodman", "forthehorde", "stardustcrusaders" };

        public static void SetTheme(MusicPlayerForm musicPlayer, string themeName)
        {
            musicPlayer.penguinPurpleToolStripMenuItem.Checked = false;
            musicPlayer.pureWhiteToolStripMenuItem.Checked = false;
            musicPlayer.deepBlackToolStripMenuItem.Checked = false;
            musicPlayer.feelsGoodManToolStripMenuItem.Checked = false;
            musicPlayer.forTheHordeToolStripMenuItem.Checked = false;
            musicPlayer.stardustCrusadersToolStripMenuItem.Checked = false;

            FileInfo themeFile = new FileInfo(musicPlayer.themePath + "\\" + themeName + ".wzmp");

            if (themeFile.Exists)
            {
                string themeProperties = musicPlayer.ReadConfig(themeFile.FullName);
                SetThemeCustom(themeProperties.Split(';'));
            }
            else if (themeName.Equals("pureWhite"))
            {
                SetThemePureWhite();
                musicPlayer.pureWhiteToolStripMenuItem.Checked = true;
            }
            else if (themeName.Equals("deepBlack"))
            {
                SetThemeDeepBlack();
                musicPlayer.deepBlackToolStripMenuItem.Checked = true;
            }
            else if (themeName.Equals("feelsGoodMan"))
            {
                SetThemeFeelsGoodMan();
                musicPlayer.feelsGoodManToolStripMenuItem.Checked = true;
            }
            else if (themeName.Equals("forTheHorde"))
            {
                SetThemeForTheHorde();
                musicPlayer.forTheHordeToolStripMenuItem.Checked = true;
            }
            else if (themeName.Equals("stardustCrusaders"))
            {
                SetThemeStardustCrusaders();
                musicPlayer.stardustCrusadersToolStripMenuItem.Checked = true;
            }
            else
            {
                SetThemePenguinPurple();
                musicPlayer.penguinPurpleToolStripMenuItem.Checked = true;
            }

            themableForms.RemoveAll(item => item == null);
            foreach (Themable themableForm in themableForms)
                themableForm.ApplyTheme();
        }

        public static void AddThemableForm(Themable themableForm)
        {
            themableForms.Add(themableForm);
        }

        public static bool IsPredefinedTheme(string themeName)
        {
            return predefinedThemes.Contains(themeName.ToLower());
        }

        public static Dictionary<string, Bitmap> GetMascotMap()
        {
            Dictionary<string, Bitmap> mascotMap = new Dictionary<string, Bitmap>
            {
                { "Penguin", Properties.Resources.penguin },
                { "Pepe the Frog", Properties.Resources.pepe },
                { "Garrosh Hellscream", Properties.Resources.garrosh },
                { "Star Platinum", Properties.Resources.star_platinum }
            };

            return mascotMap;
        }

        public static Image PaintImage(Image image, Color color)
        {
            Bitmap bitmap = new Bitmap(image);
            for (int y = 0; (y <= (bitmap.Height - 1)); y++) {
                for (int x = 0; (x <= (bitmap.Width - 1)); x++) {
                    Color pixel = bitmap.GetPixel(x, y);
                    if (pixel.A == 0)
                        continue;
                    pixel = color;
                    bitmap.SetPixel(x, y, pixel);
                }
            }
            return bitmap;
        }

        private static void SetThemeCustom(string[] themeProperties)
        {
            imageString = themeProperties[6];
            if(imageString.StartsWith("Custom:"))
                image = ImageFromBase64String(imageString.Replace("Custom:", ""));
            else
                image = GetMascotMap()[imageString];
            mainBackColorVariant1 = ColorTranslator.FromHtml(themeProperties[0]);
            mainBackColorVariant2 = ColorTranslator.FromHtml(themeProperties[1]);
            accentBackColor = ColorTranslator.FromHtml(themeProperties[2]);
            highlightBackColor = ColorTranslator.FromHtml(themeProperties[3]);
            mainFontColor = ColorTranslator.FromHtml(themeProperties[4]);
            highlightFontColor = ColorTranslator.FromHtml(themeProperties[5]);
        }

        private static void SetThemePenguinPurple()
        {
            image = Properties.Resources.penguin;
            imageString = "Penguin";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#323232");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#4b4b4b");
            accentBackColor = ColorTranslator.FromHtml("#321932");
            highlightBackColor = ColorTranslator.FromHtml("#5A3C1E");
            mainFontColor = ColorTranslator.FromHtml("#FFFFFF");
            highlightFontColor = ColorTranslator.FromHtml("#FFA500");
        }

        private static void SetThemePureWhite()
        {
            image = Properties.Resources.penguin;
            imageString = "Penguin";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#DDDDDD");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#BBBBBB");
            accentBackColor = ColorTranslator.FromHtml("#FFFFFF");
            highlightBackColor = ColorTranslator.FromHtml("#000000");
            mainFontColor = ColorTranslator.FromHtml("#000000");
            highlightFontColor = ColorTranslator.FromHtml("#4488FF");
        }

        private static void SetThemeDeepBlack()
        {
            image = Properties.Resources.penguin;
            imageString = "Penguin";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#000000");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#111111");
            accentBackColor = ColorTranslator.FromHtml("#060606");
            highlightBackColor = ColorTranslator.FromHtml("#110000");
            mainFontColor = ColorTranslator.FromHtml("#666666");
            highlightFontColor = ColorTranslator.FromHtml("#FF3333");
        }

        private static void SetThemeFeelsGoodMan()
        {
            image = Properties.Resources.pepe;
            imageString = "Pepe the Frog";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#F0E0D6");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#FFFFEE");
            accentBackColor = ColorTranslator.FromHtml("#FFFFEE");
            highlightBackColor = ColorTranslator.FromHtml("#FFFFFF");
            mainFontColor = ColorTranslator.FromHtml("#800000");
            highlightFontColor = ColorTranslator.FromHtml("#117743");
        }

        private static void SetThemeForTheHorde()
        {
            image = Properties.Resources.garrosh;
            imageString = "Garrosh Hellscream";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#000000");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#28150b");
            accentBackColor = ColorTranslator.FromHtml("#7d0d0d");
            highlightBackColor = ColorTranslator.FromHtml("#7d0d0d");
            mainFontColor = ColorTranslator.FromHtml("#fffccc");
            highlightFontColor = ColorTranslator.FromHtml("#fffccc");
        }

        private static void SetThemeStardustCrusaders()
        {
            image = Properties.Resources.star_platinum;
            imageString = "Star Platinum";
            mainBackColorVariant1 = ColorTranslator.FromHtml("#001015");
            mainBackColorVariant2 = ColorTranslator.FromHtml("#000050");
            accentBackColor = ColorTranslator.FromHtml("#927ddd");
            highlightBackColor = ColorTranslator.FromHtml("#8800cc");
            mainFontColor = ColorTranslator.FromHtml("#ff66dd");
            highlightFontColor = ColorTranslator.FromHtml("#ffcc00");
        }

        public static string ImageToBase64String(Image image)
        {
            using (MemoryStream memory = new MemoryStream()) {
                image.Save(memory, ImageFormat.Gif);
                return Convert.ToBase64String(memory.ToArray());
            }
        }

        public static Image ImageFromBase64String(string base64)
        {
            return Image.FromStream(new MemoryStream(Convert.FromBase64String(base64)));
        }
    }
}
