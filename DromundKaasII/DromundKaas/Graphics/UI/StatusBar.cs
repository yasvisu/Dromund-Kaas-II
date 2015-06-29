using Microsoft.Xna.Framework;

namespace DromundKaasII.Graphics.UI
{
    /// <summary>
    /// A status bar to display some ratio visually.
    /// </summary>
    public class StatusBar
    {
        /// <summary>
        /// Initializes a new StatusBar.
        /// </summary>
        /// <param name="Bounds">The bounds of the StatusBar.</param>
        /// <param name="FillRatio">The fill ratio of the StatusBar.</param>
        /// <param name="Color">The Color of the StatusBar.</param>
        public StatusBar(Rectangle Bounds, float FillRatio, Color Color)
        {
            this.Bounds = Bounds;
            this.FillRatio = FillRatio;
            this.Color = Color;
            this.MaxWidth = Bounds.Width;
            this.MaxHeight = Bounds.Height;
        }

        /// <summary>
        /// The bounds of the StatusBar.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// The fill ratio of the StatusBar.
        /// </summary>
        public float FillRatio { get; set; }

        /// <summary>
        /// The Color of the StatusBar.
        /// </summary>
        public Color Color { get; set; }


        /// <summary>
        /// The maximum width of the StatusBar.
        /// </summary>
        public int MaxWidth { get; private set; }
        /// <summary>
        /// The maximum height of the StatusBar.
        /// </summary>
        public int MaxHeight { get; private set; }
    }
}
