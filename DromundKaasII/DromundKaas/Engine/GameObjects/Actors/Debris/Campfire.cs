using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Debris
{
    /// <summary>
    /// A basic illuminating campfire.
    /// </summary>
    public class Campfire : Debris, IIlluminator
    {
        /// <summary>
        /// Initializes a new Campfire.
        /// </summary>
        /// <param name="MapPosition">The Campfire's position.</param>
        /// <param name="Stats">The Campfire's stats.</param>
        public Campfire(Vector2 MapPosition, Statblock Stats)
            : base(MapPosition, null, Stats)
        {
            this.Stats.IlluminationRange = 3;
            this.IlluminationColor = Color.LightYellow;
        }

        /// <summary>
        /// Tells campfire to burn.
        /// </summary>
        /// <param name="G">The gamestate to process.</param>
        public override void Act(GameState G)
        {
            this.Stats.Health--;
        }

        /// <summary>
        /// The illumination color of the campfire.
        /// </summary>
        public Color IlluminationColor { get; set; }

        /// <summary>
        /// The illumination range of the campfire.
        /// </summary>
        public float IlluminationRange
        {
            get
            {
                return this.Stats.IlluminationRange;
            }
        }

        /// <summary>
        /// Whether the campfire has illuminated.
        /// </summary>
        public bool HasIlluminated { get; set; }
    }
}
