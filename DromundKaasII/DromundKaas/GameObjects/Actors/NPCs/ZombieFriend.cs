using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using DromundKaasII.Input;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.NPCs
{
    public class ZombieFriend : Actor
    {
        public ZombieFriend(Vector2 MapPosition)
            : base(MapPosition)
        {

        }

        public override void Act(Engine.GameState G)
        {
            this.DesiredAction = GameInputs.Up;
        }
    }
}
