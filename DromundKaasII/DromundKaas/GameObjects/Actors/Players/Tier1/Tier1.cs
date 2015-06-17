using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players.Tier1
{
    public abstract class Tier1 : Player
    {
        protected Tier1(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        protected Tier1(Primal candidate)
            : base(candidate.MapPosition)
        {
            this.Score = candidate.Score;
            this.Stats = candidate.Stats;
        }
    }
}
