using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Graphics.UI
{
    public class StatusBar
    {

        public StatusBar(Rectangle Bounds, float FillRatio, Color Color)
        {
            this.Bounds = Bounds;
            this.FillRatio = FillRatio;
            this.Color = Color;
            this.MaxWidth = Bounds.Width;
            this.MaxHeight = Bounds.Height;
        }

        public Rectangle Bounds { get; set; }
        public float FillRatio { get; set; }
        public Color Color { get; set; }

        public int MaxWidth { get; private set; }
        public int MaxHeight { get; private set; }
    }
}
