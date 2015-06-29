using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics.UI
{
    /// <summary>
    /// A Button holding several useful properties to an UI button.
    /// </summary>
    public class Button
    {
        /// <summary>
        /// Initializes a new Button.
        /// </summary>
        /// <param name="Text">The text of the button.</param>
        /// <param name="Click">The Action to do on click.</param>
        /// <param name="Location">The grid position of the button.</param>
        /// <param name="IsActive">Whether the button is currently active.</param>
        public Button(string Text, Action Click, Vector2 Location,bool IsActive)
        {
            this.Text = Text;
            this.Click = Click;
            this.Location = Location;
            this.IsActive = IsActive;
        }

        /// <summary>
        /// The text of the button.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The Action to do on click.
        /// </summary>
        public Action Click { get; set; }

        /// <summary>
        /// The grid position of the button.
        /// </summary>
        public Vector2 Location { get; set; }

        /// <summary>
        /// Whether the button is currently active.
        /// </summary>
        public bool IsActive { get; set; }
    }
}
