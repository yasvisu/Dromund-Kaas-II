using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.NPCs
{
    public abstract class Npc : Actor
    {
        public Npc(Vector2 MapPosition)
            : base(MapPosition)
        {

        }
    }
}
