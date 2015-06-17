using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players.Tier1
{
    class Fighter : Tier1
    {
        public Fighter(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public Fighter(Primal candidate)
            : base(candidate)
        {

        }
    }
}
