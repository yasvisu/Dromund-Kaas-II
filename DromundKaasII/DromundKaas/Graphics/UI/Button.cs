using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics.UI
{
    public class Button
    {
        public Button(Action Click, Vector2 Location, Texture2D Texture, bool IsActive)
        {
            this.Click = Click;
            this.Location = Location;
            this.Texture = Texture;
            this.IsActive = IsActive;
        }

        public Action Click { get; set; }
        public Vector2 Location { get; set; }
        public Texture2D Texture { get; set; }
        public bool IsActive { get; set; }
        public int ClickTimeout { get; set; }
    }
}
