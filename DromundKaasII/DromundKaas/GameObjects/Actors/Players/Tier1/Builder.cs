using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players.Tier1
{
    class Builder : Player
    {
        public Builder(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public Builder(Primal candidate)
            : base(candidate.MapPosition)
        {
            this.Score = candidate.Score;
            this.Stats = candidate.Stats;
        }
    }
}
