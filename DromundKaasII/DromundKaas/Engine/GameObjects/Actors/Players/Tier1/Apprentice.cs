using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.Engine.GameObjects.Actors.Players.Tier1
{
    class Apprentice : Tier1
    {
        public Apprentice(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public Apprentice(Primal candidate)
            : base(candidate)
        {
            candidate.Stats.Dexterity += 1;
            candidate.Stats.Intelligence += 2;
            candidate.Stats.Psychic += 2;
        }
    }
}
