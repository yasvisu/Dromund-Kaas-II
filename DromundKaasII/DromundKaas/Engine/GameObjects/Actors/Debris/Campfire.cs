using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Interfaces;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Debris
{
    public class Campfire : Actor, IIlluminator
    {
        public Campfire(Vector2 MapPosition, Statblock Stats)
            : base(MapPosition, null, Stats)
        {
            this.Stats.IlluminationRange = 3;
            this.IlluminationColor = Color.LightYellow;
        }

        public override void Act(GameState G)
        {

        }

        public Color IlluminationColor { get; set; }
        public float IlluminationRange
        {
            get
            {
                return this.Stats.IlluminationRange;
            }
        }

        public bool HasIlluminated { get; set; }
    }
}
