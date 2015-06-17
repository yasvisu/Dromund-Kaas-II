using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace DromundKaasII.GameObjects.Actors.Players
{
    public class Primal : Player
    {

        public Primal(Vector2 MapPosition)
            : base(MapPosition)
        {
            this.Stats = new Statblock();
        }


    }
}
