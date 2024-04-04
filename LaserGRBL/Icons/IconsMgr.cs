﻿using Base.Drawing;
using LaserGRBL.UserControls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Security.Cryptography;
using System.Windows.Forms;

namespace LaserGRBL.Icons
{

    public class LoadedImageTag
    {
        public bool Colorize { get; set; }
        public string ResourceName { get; set; }
        public Size Size { get; set; }

    }

    public static class IconsMgr
    {
        // buttons list
        private static List<object> mControls = new List<object>();
        // light color set
        private static Dictionary<string, Color> mLightIconColors = new Dictionary<string, Color>
        {
            { string.Empty, Color.FromArgb(50, 50, 50) },
            { "custom-reset", Color.FromArgb(199, 193, 13) },
            { "custom-unlock", Color.FromArgb(230, 138, 7) },
            { "zeroing", Color.FromArgb(14, 136, 229) },
            { "center", Color.FromArgb(19, 169, 142) },
            { "corner", Color.FromArgb(19, 169, 142) },
            { "frame", Color.FromArgb(0, 173, 16) },
            { "focus", Color.FromArgb(197, 60, 221) },
            { "blink", Color.FromArgb(231, 70, 143) },
            { "mdi-home", Color.FromArgb(79, 188, 243) },
            { "mdi-arrow-up-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-top-right-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-right-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-bottom-right-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-down-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-bottom-left-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-left-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-arrow-top-left-bold-outline", Color.FromArgb(0, 122, 217) },
            { "mdi-stop", Color.FromArgb(176, 58, 58) },
            { "mdi-play", Color.FromArgb(0, 123, 16) },
            { "mdi-power-plug", Color.FromArgb(0, 173, 16) },
            { "mdi-power-plug-off", Color.FromArgb(176, 58, 58) },
            { "mdi-close-box", Color.FromArgb(176, 58, 58) },
            { "mdi-checkbox-marked", Color.FromArgb(0, 173, 16) },
            { "mdi-play-circle", Color.FromArgb(0, 173, 16) },
            { "mdi-stop-circle", Color.FromArgb(176, 58, 58) },
            { "mdi-information-slab-box", Color.FromArgb(79, 188, 243) },
        };
        // dark color set
        private static Dictionary<string, Color> mDarkIconColors = new Dictionary<string, Color>
        {
            { string.Empty, Color.FromArgb(250, 250, 250) },
            { "custom-reset", Color.FromArgb(230, 230, 20) },
            { "custom-unlock", Color.FromArgb(246, 163, 41) },
            { "zeroing", Color.FromArgb(79, 188, 243) },
            { "center", Color.FromArgb(85, 231, 192) },
            { "corner", Color.FromArgb(85, 231, 192) },
            { "frame", Color.FromArgb(71, 200, 86) },
            { "focus", Color.FromArgb(193, 114, 235) },
            { "blink", Color.FromArgb(243, 79, 133) },
            { "mdi-home", Color.FromArgb(79, 188, 243) },
            { "mdi-arrow-up-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-top-right-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-right-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-bottom-right-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-down-bold-outline" , Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-bottom-left-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-left-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-arrow-top-left-bold-outline", Color.FromArgb( 0, 122, 217) },
            { "mdi-stop", Color.FromArgb(248, 90, 90) },
            { "mdi-play", Color.FromArgb(71, 200, 86) },
            { "mdi-power-plug", Color.FromArgb(71, 200, 86) },
            { "mdi-power-plug-off", Color.FromArgb(248, 90, 90) },
            { "mdi-close-box", Color.FromArgb(248, 90, 90) },
            { "mdi-checkbox-marked", Color.FromArgb(71, 200, 86) },
            { "mdi-play-circle", Color.FromArgb(71, 200, 86) },
            { "mdi-stop-circle", Color.FromArgb(248, 90, 90) },
            { "mdi-information-slab-box", Color.FromArgb(79, 188, 243) },
        };
        // current icon colors
        private static Dictionary<string, Color> mIconColors = mLightIconColors;

        // reload image
        private static Image LoadImage(object resourceName, Size size, bool colorize = true)
        {
            Bitmap image = null;
            string strResourceName = (string)resourceName;
            if (SvgIcons.SvgIcons.Contains(strResourceName))
            {
                image = SvgIcons.SvgIcons.LoadImage(strResourceName, 256, 256);
            }
            else
            {
                if (SvgIcons.SvgIcons.Contains(strResourceName))
                {
                    image = SvgIcons.SvgIcons.LoadImage(strResourceName, 256, 256);
                }
                else
                {
                    image = SvgIcons.SvgIcons.LoadImage("mdi-square-rounded", 256, 256);
                }
            }
            Image result = image;
            // if resize needed
            if (!size.IsEmpty)
            {
                Bitmap newImage = new Bitmap(size.Width, size.Height);
                using (Graphics g = Graphics.FromImage(newImage))
                {
                    g.SmoothingMode = SmoothingMode.AntiAlias;
                    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    g.DrawImage(image, 0, 0, size.Width, size.Height);
                }
                image.Dispose();
                result = newImage;
            }
            // colorize
            if (colorize) {
                if (mIconColors.TryGetValue(strResourceName, out Color color))
                {
                    result = ImageTransform.SetColor(result, color);
                }
                else
                {
                    result = ImageTransform.SetColor(result, mIconColors[string.Empty]);
                }
            }
            // set resource link
            result.Tag = new LoadedImageTag
            {
                Colorize = colorize,
                ResourceName = strResourceName,
                Size = size
            };
            return result;
        }

        // add control to list
        private static void AddControl(object control)
        {
            mControls.Add(control);
            if (control is Control ctrl)
            {
                ctrl.Disposed += ControlDisposed;
            }
        }

        // prepare button
        public static void PrepareButton(ImageButton button, string resourceName, Size size, string resourceNameAlt = null)
        {
            button.Image = LoadImage(resourceName, size);
            button.SizingMode = ImageButton.SizingModes.StretchImage;
            if (!string.IsNullOrEmpty(resourceNameAlt))
            {
                button.AltImage = LoadImage(resourceNameAlt, size);
            }
            button.SizingMode = ImageButton.SizingModes.StretchImage;
            AddControl(button);
        }
        // prepare button
        public static void PrepareButton(ImageButton button, string resourceName, string resourceNameAlt = null)
        {
            PrepareButton(button, resourceName, Size.Empty, resourceNameAlt);
        }

        // prepare button
        public static void PrepareButton(GrblButton button, string resourceName)
        {
            PrepareButton(button, resourceName, Size.Empty);
        }

        // prepare button
        public static void PrepareButton(GrblButton button, string resourceName, Size size)
        {
            button.Image = LoadImage(resourceName, size);
            AddControl(button);
        }

        // prepare picturebox
        public static void PreparePictureBox(PictureBox pictureBox, string resourceName)
        {
            pictureBox.Image = LoadImage(resourceName, Size.Empty);
            AddControl(pictureBox);
        }

        // prepare menu item
        public static void PrepareMenuItem(ToolStripMenuItem item, string resourceName, bool colorize = true)
        {
            item.Image = LoadImage(resourceName, new Size(16, 16), colorize);
            AddControl(item);
        }

        private static void ControlDisposed(object sender, EventArgs e)
        {
            mControls.RemoveAt(mControls.IndexOf(sender as Control));
        }

        internal static void OnColorChannge()
        {
            mIconColors = ColorScheme.DarkScheme ? mDarkIconColors : mLightIconColors;
            foreach (object control in mControls)
            {
                if (control is Control ctrl)
                {
                    ctrl.Invalidate();
                }
                if (control is ImageButton imageBtn)
                {
                    imageBtn.Image = ReloadLoadImage(imageBtn.Image);
                    imageBtn.AltImage = ReloadLoadImage(imageBtn.AltImage);
                }
                else if (control is GrblButton grblBtn)
                {
                    grblBtn.Image = ReloadLoadImage(grblBtn.Image);
                }
                else if (control is PictureBox pictureBox)
                {
                    pictureBox.Image = ReloadLoadImage(pictureBox.Image);
                }
                else if (control is ToolStripMenuItem item)
                {
                    item.Image = ReloadLoadImage(item.Image);
                }
            }
        }

        private static Image ReloadLoadImage(Image image)
        {
            if (image != null)
            {
                LoadedImageTag tag = image.Tag as LoadedImageTag;
                if (tag != null)
                {
                    image.Dispose();
                    image = LoadImage(tag.ResourceName, tag.Size, tag.Colorize);
                }
            }
            return image;
        }
    }

}
