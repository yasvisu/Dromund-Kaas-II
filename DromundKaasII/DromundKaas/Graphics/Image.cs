using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace DromundKaasII.Graphics
{
    /// <summary>
    /// Image class to encapsulate image necessities, including drawing / updating.
    /// </summary>
    public class Image
    {
        Vector2 origin;
        ContentManager content;
        SpriteFont font;

        /// <summary>
        /// Initializes a new image with default values.
        /// </summary>
        public Image()
        {
            this.Path = this.Text = String.Empty;
            this.FontName = "Fonts/TextFont";
            this.Position = Vector2.Zero;
            this.Scale = Vector2.One;
            this.Alpha = 1.0f;
            this.SourceRect = Rectangle.Empty;
        }

        /// <summary>
        /// The Alpha-channel of the image.
        /// </summary>
        public float Alpha { get; set; }

        /// <summary>
        /// The text of the image.
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// The font name of the image.
        /// </summary>
        public string FontName { get; set; }

        /// <summary>
        /// The path of the image.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// The grid position of the image.
        /// </summary>
        public Vector2 Position { get; set; }

        /// <summary>
        /// The scale of the image.
        /// </summary>
        public Vector2 Scale { get; set; }

        /// <summary>
        /// The source rectangle of the image.
        /// </summary>
        public Rectangle SourceRect { get; set; }

        /// <summary>
        /// The Texture of the image.
        /// </summary>
        public Texture2D Texture { get; set; }

        /// <summary>
        /// Load all content.
        /// </summary>
        public void LoadContent()
        {
            this.content = new ContentManager(ScreenManager.Instance.Content.ServiceProvider, "Content");

            if (this.Path != String.Empty)
            {
                this.Texture = this.content.Load<Texture2D>(this.Path);
            }

            this.font = this.content.Load<SpriteFont>(this.FontName);

            Vector2 dimensions = Vector2.Zero;

            if (this.Texture != null)
            {
                dimensions.X += this.Texture.Width;
            }
            dimensions.X += this.font.MeasureString(this.Text).X;

            if (this.Texture != null)
            {
                dimensions.Y = Math.Max(this.Texture.Height, this.font.MeasureString(this.Text).Y);
            }
            else
            {
                dimensions.Y = this.font.MeasureString(this.Text).Y;
            }

            if (this.SourceRect == Rectangle.Empty)
            {
                this.SourceRect = new Rectangle(0, 0, (int)dimensions.X, (int)dimensions.Y);
            }
        }

        /// <summary>
        /// Unload all content.
        /// </summary>
        public void UnloadContent()
        {
            this.content.Unload();
        }

        /// <summary>
        /// Update this element.
        /// </summary>
        /// <param name="gameTime">The GameTime to update to.</param>
        public void Update(GameTime gameTime)
        { }

        /// <summary>
        /// Draw this element.
        /// </summary>
        /// <param name="spriteBatch">The SpriteBatch to draw to.</param>
        public void Draw(SpriteBatch spriteBatch)
        {
            this.origin = new Vector2(this.SourceRect.Width / 2, this.SourceRect.Height / 2);
            spriteBatch.Draw(this.Texture, this.Position + this.origin, this.SourceRect, Color.White * this.Alpha, 0.0f, this.origin, this.Scale, SpriteEffects.None, 0.0f);
        }
    }
}
