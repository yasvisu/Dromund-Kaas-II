using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects
{
    /// <summary>
    /// A basic illuminator.
    /// </summary>
    public class Illuminator : IIlluminator
    {
        /// <summary>
        /// Initializes a new Illuminator.
        /// </summary>
        /// <param name="IlluminationColor">The illumination color of the illuminator.</param>
        /// <param name="IlluminationRange">The range the illuminator illuminates in.</param>
        /// <param name="MapPosition">The map position of the illuminator.</param>
        public Illuminator(Color IlluminationColor, float IlluminationRange, Vector2 MapPosition)
        {
            this.IlluminationColor = IlluminationColor;
            this.IlluminationRange = IlluminationRange;
            this.MapPosition = MapPosition;
        }

        /// <summary>
        /// Initializes a new Illuminator with another illuminator's values.
        /// </summary>
        /// <param name="I"></param>
        public Illuminator(IIlluminator I)
            : this(I.IlluminationColor, I.IlluminationRange, I.MapPosition)
        { }

        /// <summary>
        /// The illumination color of the illuminator.
        /// </summary>
        public Color IlluminationColor { get; set; }

        /// <summary>
        /// The range the illuminator illuminates in.
        /// </summary>
        public float IlluminationRange { get; set; }

        /// <summary>
        /// Whether the illuminator has illuminated already.
        /// </summary>
        public bool HasIlluminated { get; set; }

        /// <summary>
        /// The map position of the illuminator.
        /// </summary>
        public Vector2 MapPosition { get; set; }
    }
}
