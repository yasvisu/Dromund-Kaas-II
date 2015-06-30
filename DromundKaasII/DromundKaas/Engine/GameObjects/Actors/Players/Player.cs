using System;
using System.Collections.Generic;

using DromundKaasII.Engine.GameObjects.Skills;
using DromundKaasII.Input;
using DromundKaasII.Interfaces;

using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players
{
    /// <summary>
    /// Catch-all class for all players.
    /// </summary>
    public abstract class Player : Actor, IPlayer
    {
        /// <summary>
        /// Initializes a new Player.
        /// </summary>
        /// <param name="MapPosition">The Player's map position.</param>
        public Player(Vector2 MapPosition)
            : base(MapPosition)
        { }

        /// <summary>
        /// Initializes a new Player.
        /// </summary>
        /// <param name="MapPosition">The Player's map position.</param>
        /// <param name="SkillChain">The skills the Player can pick from.</param>
        public Player(Vector2 MapPosition, Dictionary<string, Skill> SkillChain)
            : base(MapPosition, SkillChain)
        { }

        /// <summary>
        /// Player score. NOT IMPLEMENTED!
        /// </summary>
        public int Score
        {
            get { throw new NotImplementedException(); }
            set { throw new NotImplementedException(); }
        }

        /// <summary>
        /// Whether the player is ready for level-up.
        /// </summary>
        public bool LevelUp { get; set; }

        // Illumination
        /// <summary>
        /// The illumination color of the player.
        /// </summary>
        public Color IlluminationColor { get; set; }

        /// <summary>
        /// The range in which the player will illuminate.
        /// </summary>
        public float IlluminationRange
        {
            get
            {
                return this.Stats.IlluminationRange;
            }
        }

        /// <summary>
        /// Whether the player has illuminated already.
        /// </summary>
        public bool HasIlluminated { get; set; }

        /// <summary>
        /// Sets the player's desired action based on the last input.
        /// </summary>
        /// <param name="G">The gamestate to process.</param>
        public override void Act(GameState G)
        {
            if (this.Stats.Experience > (this.Stats.Level + 1) * 100)
            {
                this.LevelUp = true;
            }
            base.Act(G);
        }
    }
}
